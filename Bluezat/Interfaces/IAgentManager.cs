using Tmds.DBus.Protocol;

namespace Bluezat.Interfaces;

public interface IAgentManager : IDBusObject
{
    Task RegisterAgentAsync(ObjectPath agent, string capability);
    Task UnregisterAgentAsync(ObjectPath agent);
    Task RequestDefaultAgentAsync(ObjectPath agent);
}
