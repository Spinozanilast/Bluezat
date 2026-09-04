using Tmds.DBus.Protocol;
using Bluez.DBus;

namespace Bluez.Interfaces;

public interface INetworkServer : IDBusObject
{
    Task RegisterAsync(string uuid, string bridge);
    Task UnregisterAsync(string uuid);
}
