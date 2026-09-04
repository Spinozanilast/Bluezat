using Tmds.DBus.Protocol;
using System.Runtime.InteropServices;
using Bluezat.DBus;

namespace Bluezat.Interfaces;

public interface IGattCharacteristic1 : IDBusObject
{
    Task<byte[]> ReadValueAsync(Dictionary<string, VariantValue> options);
    Task WriteValueAsync(byte[] value, Dictionary<string, VariantValue> options);
    Task<(SafeHandle Fd, ushort Mtu)> AcquireWriteAsync(Dictionary<string, VariantValue> options);
    Task<(SafeHandle Fd, ushort Mtu)> AcquireNotifyAsync(Dictionary<string, VariantValue> options);
    Task StartNotifyAsync();
    Task StopNotifyAsync();

    Task<ushort> GetHandleAsync();
    Task<string> GetUUIDAsync();
    Task<ObjectPath> GetServiceAsync();
    Task<byte[]> GetValueAsync();
    Task<bool> GetNotifyingAsync();
    Task<string[]> GetFlagsAsync();
    Task<bool> GetWriteAcquiredAsync();
    Task<bool> GetNotifyAcquiredAsync();
    Task<ushort> GetMTUAsync();
    Task<GattCharacteristic1Properties> GetPropertiesAsync();
    Task<INullableGattCharacteristic1Properties> GetNullablePropertiesAsync();

    ValueTask<IDisposable> WatchPropertiesChangedAsync(Action<IChangedGattCharacteristic1Properties> handler, bool emitOnCapturedContext = true);
}
