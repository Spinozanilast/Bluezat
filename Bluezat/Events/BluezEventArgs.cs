namespace Bluezat.Events;

public class BluezEventArgs(bool isStateChange = true)
{
    public bool IsStateChange { get; } = isStateChange;
}