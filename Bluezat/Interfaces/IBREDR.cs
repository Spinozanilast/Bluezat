namespace Bluezat.Interfaces;

public interface IBREDR : IDBusObject
{
    ValueTask<IDisposable> WatchDisconnectedAsync(Action<(string Name, string Message)> handler, bool emitOnCapturedContext = true);
}
