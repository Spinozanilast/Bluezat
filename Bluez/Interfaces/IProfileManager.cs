using Tmds.DBus.Protocol;
using Bluez.DBus;

namespace Bluez.Interfaces;

public interface IProfileManager : IDBusObject
{
    Task RegisterProfileAsync(ObjectPath profile, string uUID, Dictionary<string, VariantValue> options);
    Task UnregisterProfileAsync(ObjectPath profile);
}
