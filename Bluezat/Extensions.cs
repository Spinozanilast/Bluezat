using Bluezat.Interfaces;
using Bluezat.Wrappers;

namespace Bluezat;

public static class Extensions
{
    public static async Task<IReadOnlyList<Device>> GetDevicesAsync(this IAdapter adapter)
    {
        return await BluezManager.GetDevicesAsync(adapter.Connection);
    }
}