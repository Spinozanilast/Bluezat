using Tmds.DBus.Protocol;
using Bluez.DBus;

namespace Bluez.Interfaces;

public interface IGattService : IDBusObject
{
    Task<ushort> GetHandleAsync();
    Task<string> GetUUIDAsync();
    Task<ObjectPath> GetDeviceAsync();
    Task<bool> GetPrimaryAsync();
    Task<ObjectPath[]> GetIncludesAsync();
    Task<GattServiceProperties> GetPropertiesAsync();
    Task<INullableGattServiceProperties> GetNullablePropertiesAsync();

    ValueTask<IDisposable> WatchPropertiesChangedAsync(Action<IChangedGattServiceProperties> handler, bool emitOnCapturedContext = true);
}
