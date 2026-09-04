using Tmds.DBus.Protocol;

namespace Bluezat.Interfaces;

public interface IBatteryProviderManager : IDBusObject
{
    Task RegisterBatteryProviderAsync(ObjectPath provider);
    Task UnregisterBatteryProviderAsync(ObjectPath provider);
}
