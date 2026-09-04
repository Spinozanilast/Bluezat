using Tmds.DBus.Protocol;
using Bluezat.DBus;

namespace Bluezat.Interfaces;

public interface IMedia : IDBusObject
{
    Task RegisterEndpointAsync(ObjectPath endpoint, Dictionary<string, VariantValue> properties);
    Task UnregisterEndpointAsync(ObjectPath endpoint);
    Task RegisterPlayerAsync(ObjectPath player, Dictionary<string, VariantValue> properties);
    Task UnregisterPlayerAsync(ObjectPath player);
    Task RegisterApplicationAsync(ObjectPath application, Dictionary<string, VariantValue> options);
    Task UnregisterApplicationAsync(ObjectPath application);

    Task<string[]> GetSupportedUUIDsAsync();
    Task<string[]> GetSupportedFeaturesAsync();
    Task<MediaProperties> GetPropertiesAsync();
    Task<INullableMediaProperties> GetNullablePropertiesAsync();

    ValueTask<IDisposable> WatchPropertiesChangedAsync(Action<IChangedMediaProperties> handler, bool emitOnCapturedContext = true);
}
