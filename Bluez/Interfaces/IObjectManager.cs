using Tmds.DBus.Protocol;
using Bluez.DBus;

namespace Bluez.Interfaces;

public interface IObjectManager : IDBusObject
{
    Task<Dictionary<ObjectPath, Dictionary<string, Dictionary<string, VariantValue>>>> GetManagedObjectsAsync();

    ValueTask<IDisposable> WatchInterfacesAddedAsync(Action<(ObjectPath Object, Dictionary<string, Dictionary<string, VariantValue>> Interfaces)> handler, bool emitOnCapturedContext = true);
    ValueTask<IDisposable> WatchInterfacesRemovedAsync(Action<(ObjectPath Object, string[] Interfaces)> handler, bool emitOnCapturedContext = true);
}
