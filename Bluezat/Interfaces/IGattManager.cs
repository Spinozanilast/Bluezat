using Tmds.DBus.Protocol;

namespace Bluezat.Interfaces;

public interface IGattManager : IDBusObject
{
    Task RegisterApplicationAsync(ObjectPath application, Dictionary<string, VariantValue> options);
    Task UnregisterApplicationAsync(ObjectPath application);
}
