namespace Bluezat.Interfaces;

public interface ILE : IDBusObject
{
    ValueTask<IDisposable> WatchDisconnectedAsync(Action<(string Name, string Message)> handler, bool emitOnCapturedContext = true);
}
