using Bluez.Interfaces;
using Bluez.Wrappers;

namespace Bluez;

public static class Extensions
{
    public static async Task<IReadOnlyList<Device>> GetDevicesAsync(this IAdapter adapter)
    {
        return await BluezManager.GetDevicesAsync(adapter.Connection);
    }
}