using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Rst.Handlers.Interfaces;

namespace Rst.Handlers;

/// <inheritdoc />
public class JwtAuthenticationStorage : IAuthenticationStorage
{
    private enum TokenRequestState : long
    {
        Uninitialized,
        Ready,
        Loading
    }

    private static readonly AutoResetEvent Event = new(false);
    private static long _state = (long)TokenRequestState.Uninitialized;
    private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(5);

    private readonly ClientCredentialsTokenRequest _tokenRequest;
    private readonly IHttpContextAccessor _accessor;
    private readonly IDistributedCache _cache;
    private readonly HttpClient _client;
    private readonly ILogger<JwtAuthenticationStorage> _logger;

    private const string TokenKey = "5709395A-4800-4502-BBC9-A7B2EF651887";

    /// <summary>
    /// </summary>
    /// <param name="options"></param>
    /// <param name="logger"></param>
    /// <param name="cache"></param>
    /// <param name="client"></param>
    /// <param name="accessor"></param>
    public JwtAuthenticationStorage(
        IOptions<ClientCredentialsTokenRequest> options,
        ILogger<JwtAuthenticationStorage> logger,
        IDistributedCache cache,
        HttpClient client, IHttpContextAccessor accessor)
    {
        _logger = logger;
        _cache = cache;
        _tokenRequest = options.Value;
        _client = client;
        _accessor = accessor;
    }

    /// <inheritdoc />
    public async Task PassAsync(HttpRequestMessage httpRequestMessage, CancellationToken token)
    {
        httpRequestMessage.Headers.Authorization = await TokenAsync(token);
    }

    /// <inheritdoc />
    public async Task<AuthenticationHeaderValue> TokenAsync(CancellationToken token)
    {
        if (Interlocked.Read(ref _state) == (long)TokenRequestState.Loading)
        {
            await Task.Yield();
            Event.WaitOne(Timeout);
        }

        var accessToken = await _cache.GetStringAsync(TokenKey, token: token);
        Event.Reset();
        return new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, accessToken ?? string.Empty);
    }

    /// <inheritdoc />
    public async Task RefreshAsync(HttpRequestMessage httpRequestMessage, CancellationToken token)
    {
        long state;
        if ((state = Interlocked.Read(ref _state)) == (long)TokenRequestState.Loading)
        {
            await PassAsync(httpRequestMessage, token);
            return;
        }

        void SetState(TokenRequestState newState)
        {
            while ((state = Interlocked.CompareExchange(ref _state, (long)newState, state)) != (long)newState)
            {
            }
        }

        SetState(TokenRequestState.Loading);

        try
        {
            await Refresh(token);
        }
        catch (OperationCanceledException e)
        {
            _logger.LogDebug(e, "Refreshing token task has been canceled");
            throw;
        }
        finally
        {
            SetState(TokenRequestState.Ready);
            Event.Set();
        }

        await PassAsync(httpRequestMessage, token);
    }

    private async Task Refresh(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        using (_logger.BeginScope("Address {Address}", _tokenRequest.Address))
        using (_logger.BeginScope("ClientId {ClientId}", _tokenRequest.ClientId))
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            _logger.LogDebug("Refreshing token in progress");
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

            stopwatch.Stop();

            RequestTokenEventSource.Log.TokenRequested(
                _accessor.HttpContext?.TraceIdentifier ?? string.Empty,
                stopwatch.ElapsedMilliseconds);
        }
    }
}