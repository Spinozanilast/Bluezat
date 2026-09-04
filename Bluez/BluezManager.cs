using Tmds.DBus.Protocol;
using Bluez.DBus;
using Bluez.Interfaces;
using Wrappers = Bluez.Wrappers;

namespace Bluez;

public static class BluezManager
{
    public static string DbusService = "org.bluez";

    public static async Task<Wrappers.Adapter> GetAdapterAsync(DBusConnection connection, string adapterName)
    {
        var adapterObjectPath = $"/org/bluez/{adapterName}";
        var service = new DBusService(connection, DbusService);
        var adapter = service.CreateAdapter(new ObjectPath(adapterObjectPath));
        var adapterWrapper = await Wrappers.Adapter.CreateAsync(adapter);

        try
        {
            await adapterWrapper.GetNameAsync();
        }
        catch (Exception)
        {
            throw new Exception($"Bluetooth adapter {adapterName} not found.");
        }

        return adapterWrapper;
    }

    public static async Task<Wrappers.Adapter> GetDefaultAdapterAsync(DBusConnection connection)
    {
        return await GetAdapterAsync(connection, "hci0");
    }

    public static async Task<IReadOnlyList<Wrappers.Adapter>> GetAdaptersAsync(DBusConnection connection)
    {
        var managedObjects = await GetManagedObjectsAsync(connection);
        var adapterInterfaceName = Adapter.DBusInterfaceName;

        var adapters = managedObjects
            .Where(obj => obj.Value.ContainsKey(adapterInterfaceName))
            .Select(obj =>
            {
                var service = new DBusService(connection, DbusService);
                return service.CreateAdapter(obj.Key);
            })
            .ToList();

        return await Task.WhenAll(adapters.Select(Wrappers.Adapter.CreateAsync));
    }

    public static IDevice GetDeviceAsync(DBusConnection connection, ObjectPath devicePath)
    {
        var service = new DBusService(connection, DbusService);
        return service.CreateDevice(devicePath);
    }

    public static async Task<IReadOnlyList<Wrappers.Device>> GetDevicesAsync(DBusConnection connection)
    {
        var managedObjects = await GetManagedObjectsAsync(connection);
        const string deviceInterfaceName = Device.DBusInterfaceName;

        var devices = managedObjects
            .Where(obj => obj.Value.ContainsKey(deviceInterfaceName))
            .Select(async obj =>
            {
                var service = new DBusService(connection, DbusService);

                var deviceWrapper = await Wrappers.Device.CreateAsync(service.CreateDevice(obj.Key));
                return deviceWrapper;
            });

        return await Task.WhenAll(devices);
    }

    public static async Task<IReadOnlyList<IDevice>> GetDevicesAsync(DBusConnection connection, IAdapter adapter)
    {
        var allDevices = await GetDevicesAsync(connection);
        var adapterPath = adapter.Path.ToString();

        return allDevices
            .Where(d => d.Path.ToString().StartsWith($"{adapterPath}/"))
            .ToList();
    }

    public static async Task<IGattService> GetGattServiceAsync(DBusConnection connection, ObjectPath servicePath)
    {
        var service = new DBusService(connection, DbusService);
        return service.CreateGattService(servicePath);
    }

    public static async Task<IReadOnlyList<IGattService>> GetGattServicesAsync(DBusConnection connection,
        IDevice device)
    {
        var managedObjects = await GetManagedObjectsAsync(connection);
        var gattServiceInterfaceName = GattService.DBusInterfaceName;
        var devicePath = device.Path.ToString();

        var services = managedObjects
            .Where(obj =>
                obj.Key.ToString().StartsWith($"{devicePath}/") && obj.Value.ContainsKey(gattServiceInterfaceName))
            .Select(obj =>
            {
                var service = new DBusService(connection, DbusService);
                return service.CreateGattService(obj.Key);
            })
            .ToList();

        return services;
    }

    public static async Task<IGattCharacteristic1> GetGattCharacteristicAsync(DBusConnection connection,
        ObjectPath characteristicPath)
    {
        var service = new DBusService(connection, DbusService);
        return service.CreateGattCharacteristic(characteristicPath);
    }

    public static async Task<IReadOnlyList<IGattCharacteristic1>> GetGattCharacteristicsAsync(DBusConnection connection,
        IGattService service)
    {
        var managedObjects = await GetManagedObjectsAsync(connection);
        var characteristicInterfaceName = GattCharacteristic1.DBusInterfaceName;
        var servicePath = service.Path.ToString();

        var characteristics = managedObjects
            .Where(obj =>
                obj.Key.ToString().StartsWith($"{servicePath}/") && obj.Value.ContainsKey(characteristicInterfaceName))
            .Select(obj =>
            {
                var svc = new DBusService(connection, DbusService);
                return svc.CreateGattCharacteristic(obj.Key);
            })
            .ToList();

        return characteristics;
    }

    public static string NormalizeUUID(string uuid)
    {
        if (uuid.Length == 4)
        {
            return $"0000{uuid}-0000-1000-8000-00805f9b34fb".ToLowerInvariant();
        }
        else if (uuid.Length == 8)
        {
            return $"{uuid}-0000-1000-8000-00805f9b34fb".ToLowerInvariant();
        }
        else if (uuid.Length == 36)
        {
            return uuid.ToLowerInvariant();
        }
        else
        {
            throw new ArgumentException($"'{uuid}' isn't a valid 16, 32 or 128 bit UUID.");
        }
    }

    internal static async Task<Dictionary<ObjectPath, Dictionary<string, Dictionary<string, VariantValue>>>>
        GetManagedObjectsAsync(DBusConnection connection)
    {
        var service = new DBusService(connection, DbusService);
        var objectManager = service.CreateObjectManager(new ObjectPath("/"));
        return await objectManager.GetManagedObjectsAsync();
    }

    internal static bool IsMatch(string interfaceName, ObjectPath objectPath,
        Dictionary<string, Dictionary<string, VariantValue>> interfaces, IDBusObject rootObject)
    {
        return IsMatch(interfaceName, objectPath, interfaces.Keys, rootObject);
    }

    internal static bool IsMatch(string interfaceName, ObjectPath objectPath, ICollection<string> interfaces,
        IDBusObject rootObject)
    {
        if (rootObject != null && !objectPath.ToString().StartsWith($"{rootObject.Path}/"))
        {
            return false;
        }

        return interfaces.Contains(interfaceName);
    }
}