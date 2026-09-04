using Tmds.DBus.Protocol;
using Bluez.DBus;

namespace Bluez.Interfaces;

public interface IAdapter : IDBusObject
{
    Task StartDiscoveryAsync();
    Task SetDiscoveryFilterAsync(Dictionary<string, VariantValue> properties);
    Task StopDiscoveryAsync();
    Task RemoveDeviceAsync(ObjectPath device);
    Task<string[]> GetDiscoveryFiltersAsync();

    Task SetAliasAsync(string value);
    Task SetConnectableAsync(bool value);
    Task SetPoweredAsync(bool value);
    Task SetDiscoverableAsync(bool value);
    Task SetDiscoverableTimeoutAsync(uint value);
    Task SetPairableAsync(bool value);
    Task SetPairableTimeoutAsync(uint value);

    Task<string> GetAddressAsync();
    Task<string> GetAddressTypeAsync();
    Task<string> GetNameAsync();
    Task<string> GetAliasAsync();
    Task<uint> GetClassAsync();
    Task<bool> GetConnectableAsync();
    Task<bool> GetPoweredAsync();
    Task<string> GetPowerStateAsync();
    Task<bool> GetDiscoverableAsync();
    Task<uint> GetDiscoverableTimeoutAsync();
    Task<bool> GetPairableAsync();
    Task<uint> GetPairableTimeoutAsync();
    Task<bool> GetDiscoveringAsync();
    Task<string[]> GetUUIDsAsync();
    Task<string> GetModaliasAsync();
    Task<string[]> GetRolesAsync();
    Task<string[]> GetExperimentalFeaturesAsync();
    Task<ushort> GetManufacturerAsync();
    Task<byte> GetVersionAsync();
    Task<AdapterProperties> GetPropertiesAsync();
    Task<INullableAdapterProperties> GetNullablePropertiesAsync();

    ValueTask<IDisposable> WatchPropertiesChangedAsync(Action<IChangedAdapterProperties> handler, bool emitOnCapturedContext = true);
}
