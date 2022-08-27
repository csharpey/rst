using System.Net;
using System.Net.Http.Headers;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Rst.Handlers.Interfaces;

namespace Rst.Handlers;

public class JwtAuthenticationStorage : IAuthenticationStorage
{
    private readonly List<CancellationTokenSource> _tokenSources = new();
    private readonly ClientCredentialsTokenRequest _tokenRequest;
    private readonly IDistributedCache _cache;
    private readonly HttpClient _client;
    private readonly ILogger<JwtAuthenticationStorage> _logger;

    private const string TokenKey = "5709395A-4800-4502-BBC9-A7B2EF651887";

    public JwtAuthenticationStorage(
        IOptions<ClientCredentialsTokenRequest> options,
        ILogger<JwtAuthenticationStorage> logger,
        IDistributedCache cache,
        HttpClient client)
    {
        _logger = logger;
        _cache = cache;
        _tokenRequest = options.Value;
        _client = client;
    }

    public async Task Pass(HttpRequestMessage httpRequestMessage)
    {
        httpRequestMessage.Headers.Authorization = await Get();
    }

    private async Task<AuthenticationHeaderValue> Get()
    {
        var token = await _cache.GetStringAsync(TokenKey);
        return new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token ?? string.Empty);
    }

    public async Task Refresh(HttpRequestMessage httpRequestMessage)
    {
        using var source = new CancellationTokenSource();

        _tokenSources.Add(source);

        try
        {
            await Refresh(source.Token);
        }
        catch (OperationCanceledException)
        {
            /* ignore */
        }
        finally
        {
            _tokenSources.Clear();
        }

        await Pass(httpRequestMessage);
    }

    private async Task Refresh(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        using (_logger.BeginScope("Address {Address}", _tokenRequest.Address))
        using (_logger.BeginScope("ClientId {ClientId}", _tokenRequest.ClientId))
        {
            var response = await _client.RequestClientCredentialsTokenAsync(_tokenRequest, token);

            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                await _cache.SetStringAsync(TokenKey, response.AccessToken, token);
                _logger.LogDebug("Successfully refresh token");
            }
            else
            {
                _logger.LogError("StatusCode {StatusCode}", response.HttpStatusCode);
            }

            foreach (var source in _tokenSources)
            {
                source.Cancel();
            }
        }
    }
}