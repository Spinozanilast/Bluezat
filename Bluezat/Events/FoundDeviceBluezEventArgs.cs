using Bluezat.Wrappers;

namespace Bluezat.Events;

public class FoundDeviceBluezEventArgs(Device device, bool isStateChange = true) : BluezEventArgs(isStateChange)
{
    public Device Device { get; } = device;
}