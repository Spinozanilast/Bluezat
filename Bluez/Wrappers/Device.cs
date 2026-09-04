using Bluez.DBus;
using Bluez.Events;
using Bluez.Interfaces;
using Tmds.DBus.Protocol;

namespace Bluez.Wrappers;

public class Device : IDevice, IDisposable
{
    public DBusConnection Connection => _device.Connection;
    public string Destination => _device.Destination;
    public DBusService Service => _device.Service;
    public ObjectPath Path => _device.Path;

    private IDevice _device;
    private IDisposable _propertyWatcher;
    private event DeviceEventHandlerAsync? _connected;
    private event DeviceEventHandlerAsync? _servicesResolved;

    internal static async Task<Device> CreateAsync(IDevice device)
    {
        var newDevice = new Device
        {
            _device = device,
        };

        newDevice._propertyWatcher = await device.WatchPropertiesChangedAsync(newDevice.OnPropertiesChanges);

        return newDevice;
    }

    ~Device()
    {
        Dispose();
    }

    public event DeviceEventHandlerAsync Connected
    {
        add
        {
            _connected += value;

            FireEventIfPropertyAlreadyTrueAsync(_connected, _device.GetConnectedAsync(),
                nameof(DeviceProperty.Connected));
        }
        remove => _connected -= value;
    }

    public event DeviceEventHandlerAsync Disconnected;

    public event DeviceEventHandlerAsync ServicesResolved
    {
        add
        {
            _servicesResolved += value;
            FireEventIfPropertyAlreadyTrueAsync(_servicesResolved, _device.GetServicesResolvedAsync(),
                nameof(DeviceProperty.ServicesResolved));
        }
        remove { _servicesResolved -= value; }
    }

    public void Dispose()
    {
        _propertyWatcher?.Dispose();
        _propertyWatcher = null;

        GC.SuppressFinalize(this);
    }

    public Task DisconnectAsync()
    {
        return _device.DisconnectAsync();
    }

    public Task ConnectAsync()
    {
        return _device.ConnectAsync();
    }

    public Task ConnectProfileAsync(string uUID)
    {
        return _device.ConnectProfileAsync(uUID);
    }

    public Task DisconnectProfileAsync(string uUID)
    {
        return _device.DisconnectProfileAsync(uUID);
    }

    public Task PairAsync()
    {
        return _device.PairAsync();
    }

    public Task CancelPairingAsync()
    {
        return _device.CancelPairingAsync();
    }

    public Task SetAliasAsync(string value)
    {
        return _device.SetAliasAsync(value);
    }

    public Task SetTrustedAsync(bool value)
    {
        return _device.SetTrustedAsync(value);
    }

    public Task SetBlockedAsync(bool value)
    {
        return _device.SetBlockedAsync(value);
    }

    public Task SetWakeAllowedAsync(bool value)
    {
        return _device.SetWakeAllowedAsync(value);
    }

    public Task<string> GetAddressAsync()
    {
        return _device.GetAddressAsync();
    }

    public Task<string> GetAddressTypeAsync()
    {
        return _device.GetAddressTypeAsync();
    }

    public Task<string> GetNameAsync()
    {
        return _device.GetNameAsync();
    }

    public Task<string> GetAliasAsync()
    {
        return _device.GetAliasAsync();
    }

    public Task<uint> GetClassAsync()
    {
        return _device.GetClassAsync();
    }

    public Task<ushort> GetAppearanceAsync()
    {
        return _device.GetAppearanceAsync();
    }

    public Task<string> GetIconAsync()
    {
        return _device.GetIconAsync();
    }

    public Task<bool> GetPairedAsync()
    {
        return _device.GetPairedAsync();
    }

    public Task<bool> GetBondedAsync()
    {
        return _device.GetBondedAsync();
    }

    public Task<bool> GetTrustedAsync()
    {
        return _device.GetTrustedAsync();
    }

    public Task<bool> GetBlockedAsync()
    {
        return _device.GetBlockedAsync();
    }

    public Task<bool> GetLegacyPairingAsync()
    {
        return _device.GetLegacyPairingAsync();
    }

    public Task<bool> GetCablePairingAsync()
    {
        return _device.GetCablePairingAsync();
    }

    public Task<short> GetRSSIAsync()
    {
        return _device.GetRSSIAsync();
    }

    public Task<bool> GetConnectedAsync()
    {
        return _device.GetConnectedAsync();
    }

    public Task<string[]> GetUUIDsAsync()
    {
        return _device.GetUUIDsAsync();
    }

    public Task<string> GetModaliasAsync()
    {
        return _device.GetModaliasAsync();
    }

    public Task<ObjectPath> GetAdapterAsync()
    {
        return _device.GetAdapterAsync();
    }

    public Task<Dictionary<ushort, VariantValue>> GetManufacturerDataAsync()
    {
        return _device.GetManufacturerDataAsync();
    }

    public Task<Dictionary<string, VariantValue>> GetServiceDataAsync()
    {
        return _device.GetServiceDataAsync();
    }

    public Task<short> GetTxPowerAsync()
    {
        return _device.GetTxPowerAsync();
    }

    public Task<bool> GetServicesResolvedAsync()
    {
        return _device.GetServicesResolvedAsync();
    }

    public Task<byte[]> GetAdvertisingFlagsAsync()
    {
        return _device.GetAdvertisingFlagsAsync();
    }

    public Task<Dictionary<byte, VariantValue>> GetAdvertisingDataAsync()
    {
        return _device.GetAdvertisingDataAsync();
    }

    public Task<bool> GetWakeAllowedAsync()
    {
        return _device.GetWakeAllowedAsync();
    }

    public Task<Dictionary<ObjectPath, Dictionary<string, VariantValue>>> GetSetsAsync()
    {
        return _device.GetSetsAsync();
    }

    public Task<DeviceProperties> GetPropertiesAsync()
    {
        return _device.GetPropertiesAsync();
    }

    public Task<INullableDeviceProperties> GetNullablePropertiesAsync()
    {
        return _device.GetNullablePropertiesAsync();
    }

    public ValueTask<IDisposable> WatchDisconnectedAsync(Action<(string Name, string Message)> handler,
        bool emitOnCapturedContext = true)
    {
        return _device.WatchDisconnectedAsync(handler, emitOnCapturedContext);
    }

    public ValueTask<IDisposable> WatchPropertiesChangedAsync(Action<IChangedDeviceProperties> handler,
        bool emitOnCapturedContext = true)
    {
        throw new NotImplementedException();
    }

    private async void FireEventIfPropertyAlreadyTrueAsync(DeviceEventHandlerAsync handler,
        Task<bool> propertyValueGetterTask, string propName)
    {
        try
        {
            var value = await propertyValueGetterTask;
            if (value)
            {
                handler?.Invoke(this, new BluezEventArgs(isStateChange: false));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error checking if '{propName}' is already true: {ex}");
        }
    }

    private void OnPropertiesChanges(IChangedDeviceProperties changedProperties)
    {
        if (changedProperties.HasConnectedChanged)
        {
            if (changedProperties.Connected ?? false)
            {
                _connected?.Invoke(this, new BluezEventArgs());
            }
            else
            {
                Disconnected?.Invoke(this, new BluezEventArgs());
            }
        }

        if (changedProperties.HasServicesResolvedChanged)
        {
            if (true.Equals(changedProperties.ServicesResolved))
            {
                _servicesResolved?.Invoke(this, new BluezEventArgs());
            }
        }
    }
}