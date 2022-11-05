using System.Net.Http.Headers;

namespace Rst.Handlers.Interfaces;

public interface IAuthenticationStorage
{
    /// <summary>
    /// Request distributed service token
    /// </summary>
    /// <param name="token"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task{TResult}"/> Authentication Bearer token of current service</returns>
    Task<AuthenticationHeaderValue> TokenAsync(CancellationToken token);

    /// <summary>
    /// Modify http request with authentication header
    /// </summary>
    /// <param name="httpRequestMessage"></param>
    /// <param name="token"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task PassAsync(HttpRequestMessage httpRequestMessage, CancellationToken token);

    /// <summary>
    /// Refresh authentication token and call <see cref="PassAsync"/>
    /// </summary>
    /// <param name="httpRequestMessage"></param>
    /// <param name="token"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task RefreshAsync(HttpRequestMessage httpRequestMessage, CancellationToken token);
}