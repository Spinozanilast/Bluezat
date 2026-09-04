using Tmds.DBus.Protocol;

namespace Bluezat.Interfaces;

public interface IProfileManager : IDBusObject
{
    Task RegisterProfileAsync(ObjectPath profile, string uUID, Dictionary<string, VariantValue> options);
    Task UnregisterProfileAsync(ObjectPath profile);
}
