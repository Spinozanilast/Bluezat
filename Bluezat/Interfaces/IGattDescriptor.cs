using Tmds.DBus.Protocol;
using Bluezat.DBus;

namespace Bluezat.Interfaces;

public interface IGattDescriptor : IDBusObject
{
    Task<byte[]> ReadValueAsync(Dictionary<string, VariantValue> options);
    Task WriteValueAsync(byte[] value, Dictionary<string, VariantValue> options);

    Task<ushort> GetHandleAsync();
    Task<string> GetUUIDAsync();
    Task<ObjectPath> GetCharacteristicAsync();
    Task<byte[]> GetValueAsync();
    Task<GattDescriptorProperties> GetPropertiesAsync();
    Task<INullableGattDescriptorProperties> GetNullablePropertiesAsync();

    ValueTask<IDisposable> WatchPropertiesChangedAsync(Action<IChangedGattDescriptorProperties> handler, bool emitOnCapturedContext = true);
}
