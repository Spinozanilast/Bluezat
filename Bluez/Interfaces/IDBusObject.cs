using Tmds.DBus.Protocol;

namespace Bluez.Interfaces;

public interface IDBusObject
{
    DBusConnection Connection { get; }
    string Destination { get; }
    DBusService Service { get; }
    ObjectPath Path { get; }
}
