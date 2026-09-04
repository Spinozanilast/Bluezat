namespace Bluezat.Interfaces;

public interface INetworkServer : IDBusObject
{
    Task RegisterAsync(string uuid, string bridge);
    Task UnregisterAsync(string uuid);
}
