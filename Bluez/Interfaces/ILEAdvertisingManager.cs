using Tmds.DBus.Protocol;
using Bluez.DBus;

namespace Bluez.Interfaces;

public interface ILEAdvertisingManager : IDBusObject
{
    Task RegisterAdvertisementAsync(ObjectPath advertisement, Dictionary<string, VariantValue> options);
    Task UnregisterAdvertisementAsync(ObjectPath service);

    Task<byte> GetActiveInstancesAsync();
    Task<byte> GetSupportedInstancesAsync();
    Task<string[]> GetSupportedIncludesAsync();
    Task<string[]> GetSupportedSecondaryChannelsAsync();
    Task<string[]> GetSupportedFeaturesAsync();
    Task<Dictionary<string, VariantValue>> GetSupportedCapabilitiesAsync();
    Task<LEAdvertisingManager1Properties> GetPropertiesAsync();
    Task<INullableLEAdvertisingManager1Properties> GetNullablePropertiesAsync();

    ValueTask<IDisposable> WatchPropertiesChangedAsync(Action<IChangedLEAdvertisingManager1Properties> handler, bool emitOnCapturedContext = true);
}
