using Tmds.DBus.Protocol;
using Bluez.DBus;

namespace Bluez.Interfaces;

public interface INetwork : IDBusObject
{
    Task<string> ConnectAsync(string uuid);
    Task DisconnectAsync();

    Task<bool> GetConnectedAsync();
    Task<string> GetInterfaceAsync();
    Task<string> GetUUIDAsync();
    Task<NetworkProperties> GetPropertiesAsync();
    Task<INullableNetworkProperties> GetNullablePropertiesAsync();

    ValueTask<IDisposable> WatchPropertiesChangedAsync(Action<IChangedNetworkProperties> handler, bool emitOnCapturedContext = true);
}
