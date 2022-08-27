namespace Rst.Handlers.Interfaces;

public interface IAuthenticationStorage
{
    public Task Pass(HttpRequestMessage httpRequestMessage);
    public Task Refresh(HttpRequestMessage httpRequestMessage);
}