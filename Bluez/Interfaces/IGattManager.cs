using Tmds.DBus.Protocol;
using Bluez.DBus;

namespace Bluez.Interfaces;

public interface IGattManager : IDBusObject
{
    Task RegisterApplicationAsync(ObjectPath application, Dictionary<string, VariantValue> options);
    Task UnregisterApplicationAsync(ObjectPath application);
}
