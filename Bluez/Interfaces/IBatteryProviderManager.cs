using Tmds.DBus.Protocol;
using Bluez.DBus;

namespace Bluez.Interfaces;

public interface IBatteryProviderManager : IDBusObject
{
    Task RegisterBatteryProviderAsync(ObjectPath provider);
    Task UnregisterBatteryProviderAsync(ObjectPath provider);
}
