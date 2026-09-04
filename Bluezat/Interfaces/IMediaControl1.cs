using Tmds.DBus.Protocol;
using Bluezat.DBus;

namespace Bluezat.Interfaces;

public interface IMediaControl1 : IDBusObject
{
    Task PlayAsync();
    Task PauseAsync();
    Task StopAsync();
    Task NextAsync();
    Task PreviousAsync();
    Task VolumeUpAsync();
    Task VolumeDownAsync();
    Task FastForwardAsync();
    Task RewindAsync();

    Task<bool> GetConnectedAsync();
    Task<ObjectPath> GetPlayerAsync();
    Task<MediaControl1Properties> GetPropertiesAsync();
    Task<INullableMediaControl1Properties> GetNullablePropertiesAsync();

    ValueTask<IDisposable> WatchPropertiesChangedAsync(Action<IChangedMediaControl1Properties> handler, bool emitOnCapturedContext = true);
}
