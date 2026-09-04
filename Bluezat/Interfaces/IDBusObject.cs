using Tmds.DBus.Protocol;

namespace Bluezat.Interfaces;

public interface IDBusObject
{
    DBusConnection Connection { get; }
    string Destination { get; }
    DBusService Service { get; }
    ObjectPath Path { get; }
}
