using Tmds.DBus.Protocol;
using Bluezat.DBus;

namespace Bluezat.Interfaces;

public interface IDevice : IDBusObject
{
    Task DisconnectAsync();
    Task ConnectAsync();
    Task ConnectProfileAsync(string uUID);
    Task DisconnectProfileAsync(string uUID);
    Task PairAsync();
    Task CancelPairingAsync();

    Task SetAliasAsync(string value);
    Task SetTrustedAsync(bool value);
    Task SetBlockedAsync(bool value);
    Task SetWakeAllowedAsync(bool value);

    Task<string> GetAddressAsync();
    Task<string> GetAddressTypeAsync();
    Task<string> GetNameAsync();
    Task<string> GetAliasAsync();
    Task<uint> GetClassAsync();
    Task<ushort> GetAppearanceAsync();
    Task<string> GetIconAsync();
    Task<bool> GetPairedAsync();
    Task<bool> GetBondedAsync();
    Task<bool> GetTrustedAsync();
    Task<bool> GetBlockedAsync();
    Task<bool> GetLegacyPairingAsync();
    Task<bool> GetCablePairingAsync();
    Task<short> GetRSSIAsync();
    Task<bool> GetConnectedAsync();
    Task<string[]> GetUUIDsAsync();
    Task<string> GetModaliasAsync();
    Task<ObjectPath> GetAdapterAsync();
    Task<Dictionary<ushort, VariantValue>> GetManufacturerDataAsync();
    Task<Dictionary<string, VariantValue>> GetServiceDataAsync();
    Task<short> GetTxPowerAsync();
    Task<bool> GetServicesResolvedAsync();
    Task<byte[]> GetAdvertisingFlagsAsync();
    Task<Dictionary<byte, VariantValue>> GetAdvertisingDataAsync();
    Task<bool> GetWakeAllowedAsync();
    Task<Dictionary<ObjectPath, Dictionary<string, VariantValue>>> GetSetsAsync();
    Task<DeviceProperties> GetPropertiesAsync();
    Task<INullableDeviceProperties> GetNullablePropertiesAsync();

    ValueTask<IDisposable> WatchDisconnectedAsync(Action<(string Name, string Message)> handler, bool emitOnCapturedContext = true);
    ValueTask<IDisposable> WatchPropertiesChangedAsync(Action<IChangedDeviceProperties> handler, bool emitOnCapturedContext = true);
}
