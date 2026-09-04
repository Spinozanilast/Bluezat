using Bluez.Wrappers;

namespace Bluez.Events;

public delegate Task DeviceChangeEventHandlerAsync(Adapter sender, FoundDeviceBluezEventArgs eventArgs);

public delegate Task AdapterEventHandlerAsync(Adapter sender, BluezEventArgs eventArgs);

public delegate Task DeviceEventHandlerAsync(Device sender, BluezEventArgs eventArgs);