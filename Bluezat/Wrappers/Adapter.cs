using Bluezat.DBus;
using Bluezat.Events;
using Bluezat.Interfaces;
using Tmds.DBus.Protocol;

namespace Bluezat.Wrappers;

public class Adapter : IAdapter, IDisposable
{
    public DBusConnection Connection => _adapter.Connection;
    public string Destination => _adapter.Destination;
    public DBusService Service => _adapter.Service;
    public ObjectPath Path => _adapter.Path;

    private IAdapter _adapter = null!;
    private IDisposable? _interfacesWatcher;
    private IDisposable? _propertyWatcher;
    private DeviceChangeEventHandlerAsync? _deviceFound;
    private AdapterEventHandlerAsync? _poweredOn;

    ~Adapter()
    {
        Dispose();
    }

    internal static async Task<Adapter> CreateAsync(IAdapter adapter)
    {
        var newAdapter = new Adapter
        {
            _adapter = adapter,
        };

        var objectManager = new ObjectManager(adapter.Connection, adapter.Destination, adapter.Path);
        newAdapter._interfacesWatcher = await objectManager.WatchInterfacesAddedAsync(newAdapter.OnDeviceFound);
        newAdapter._propertyWatcher = await adapter.WatchPropertiesChangedAsync(newAdapter.OnPropertiesChanges);

        return newAdapter;
    }

    private async ValueTask OnDeviceFound(
        (ObjectPath ObjectPath, Dictionary<string, Dictionary<string, VariantValue>> Interfaces) args)
    {
        if (BluezManager.IsMatch(DBus.Device.DBusInterfaceName, args.ObjectPath, args.Interfaces, this))
        {
            var device = BluezManager.GetDeviceAsync(Connection, args.ObjectPath);
            var deviceWrapper = await Device.CreateAsync(device);
            _deviceFound?.Invoke(this, new FoundDeviceBluezEventArgs(deviceWrapper));
        }
    }

    public void Dispose()
    {
        _interfacesWatcher?.Dispose();
        _interfacesWatcher = null;

        GC.SuppressFinalize(this);
    }

    public event DeviceChangeEventHandlerAsync DeviceFound
    {
        add
        {
            _deviceFound += value;
            _ = FireEventForExistingDevicesAsync();
        }
        remove => _deviceFound -= value;
    }

    public event AdapterEventHandlerAsync PoweredOn
    {
        add
        {
            _poweredOn += value;
            FireEventIfPropertyAlreadyTrueAsync(
                handler: _poweredOn,
                propertyValueGetterTask: _adapter.GetPoweredAsync(),
                propName: nameof(AdapterProperty.Powered));
        }
        remove => _poweredOn -= value;
    }

    public event AdapterEventHandlerAsync? PoweredOff;

    public Task StartDiscoveryAsync()
    {
        return _adapter.StartDiscoveryAsync();
    }

    public Task SetDiscoveryFilterAsync(Dictionary<string, VariantValue> properties)
    {
        return _adapter.SetDiscoveryFilterAsync(properties);
    }

    public Task StopDiscoveryAsync()
    {
        return _adapter.StopDiscoveryAsync();
    }

    public Task RemoveDeviceAsync(ObjectPath device)
    {
        return _adapter.RemoveDeviceAsync(device);
    }

    public Task<string[]> GetDiscoveryFiltersAsync()
    {
        return _adapter.GetDiscoveryFiltersAsync();
    }

    public Task SetAliasAsync(string value)
    {
        return _adapter.SetAliasAsync(value);
    }

    public Task SetConnectableAsync(bool value)
    {
        return _adapter.SetConnectableAsync(value);
    }

    public Task SetPoweredAsync(bool value)
    {
        return _adapter.SetPoweredAsync(value);
    }

    public Task SetDiscoverableAsync(bool value)
    {
        return _adapter.SetDiscoverableAsync(value);
    }

    public Task SetDiscoverableTimeoutAsync(uint value)
    {
        return _adapter.SetDiscoverableTimeoutAsync(value);
    }

    public Task SetPairableAsync(bool value)
    {
        return _adapter.SetPairableAsync(value);
    }

    public Task SetPairableTimeoutAsync(uint value)
    {
        return _adapter.SetPairableTimeoutAsync(value);
    }

    public Task<string> GetAddressAsync()
    {
        return _adapter.GetAddressAsync();
    }

    public Task<string> GetAddressTypeAsync()
    {
        return _adapter.GetAddressTypeAsync();
    }

    public Task<string> GetNameAsync()
    {
        return _adapter.GetNameAsync();
    }

    public Task<string> GetAliasAsync()
    {
        return _adapter.GetAliasAsync();
    }

    public Task<uint> GetClassAsync()
    {
        return _adapter.GetClassAsync();
    }

    public Task<bool> GetConnectableAsync()
    {
        return _adapter.GetConnectableAsync();
    }

    public Task<bool> GetPoweredAsync()
    {
        return _adapter.GetPoweredAsync();
    }

    public Task<string> GetPowerStateAsync()
    {
        return _adapter.GetPowerStateAsync();
    }

    public Task<bool> GetDiscoverableAsync()
    {
        return _adapter.GetDiscoverableAsync();
    }

    public Task<uint> GetDiscoverableTimeoutAsync()
    {
        return _adapter.GetDiscoverableTimeoutAsync();
    }

    public Task<bool> GetPairableAsync()
    {
        return _adapter.GetPairableAsync();
    }

    public Task<uint> GetPairableTimeoutAsync()
    {
        return _adapter.GetPairableTimeoutAsync();
    }

    public Task<bool> GetDiscoveringAsync()
    {
        return _adapter.GetDiscoveringAsync();
    }

    public Task<string[]> GetUUIDsAsync()
    {
        return _adapter.GetUUIDsAsync();
    }

    public Task<string> GetModaliasAsync()
    {
        return _adapter.GetModaliasAsync();
    }

    public Task<string[]> GetRolesAsync()
    {
        return _adapter.GetRolesAsync();
    }

    public Task<string[]> GetExperimentalFeaturesAsync()
    {
        return _adapter.GetExperimentalFeaturesAsync();
    }

    public Task<ushort> GetManufacturerAsync()
    {
        return _adapter.GetManufacturerAsync();
    }

    public Task<byte> GetVersionAsync()
    {
        return _adapter.GetVersionAsync();
    }

    public Task<AdapterProperties> GetPropertiesAsync()
    {
        return _adapter.GetPropertiesAsync();
    }

    public Task<INullableAdapterProperties> GetNullablePropertiesAsync()
    {
        return _adapter.GetNullablePropertiesAsync();
    }

    public ValueTask<IDisposable> WatchPropertiesChangedAsync(Action<IChangedAdapterProperties> handler,
        bool emitOnCapturedContext = true)
    {
        return _adapter.WatchPropertiesChangedAsync(handler, emitOnCapturedContext);
    }

    private async Task FireEventForExistingDevicesAsync()
    {
        var devices = await this.GetDevicesAsync();

        foreach (var device in devices)
        {
            _deviceFound?.Invoke(this, new FoundDeviceBluezEventArgs(device, isStateChange: false));
        }
    }

    private async void FireEventIfPropertyAlreadyTrueAsync(AdapterEventHandlerAsync handler,
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

    private void OnPropertiesChanges(IChangedAdapterProperties changedProperties)
    {
        if (!changedProperties.HasPoweredChanged) return;

        if (true.Equals(changedProperties.Powered))
        {
            _poweredOn?.Invoke(this, new BluezEventArgs());
        }
        else
        {
            PoweredOff?.Invoke(this, new BluezEventArgs());
        }
    }
}