using Tmds.DBus.Protocol;
using Bluez.DBus;

namespace Bluez.Interfaces;

public interface IAgentManager : IDBusObject
{
    Task RegisterAgentAsync(ObjectPath agent, string capability);
    Task UnregisterAgentAsync(ObjectPath agent);
    Task RequestDefaultAgentAsync(ObjectPath agent);
}
