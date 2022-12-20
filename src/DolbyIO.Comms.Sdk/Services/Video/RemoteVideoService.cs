using System.Threading.Tasks;

#nullable enable

namespace DolbyIO.Comms
{
    /// <summary>
    /// The remote video service.
    /// </summary>
    public sealed class RemoteVideoService
    {
        public async Task SetVideoSinkAsync(VideoSink? sink)
        {
            VideoSinkHandle handle = sink != null ? sink.Handle : new VideoSinkHandle();
            await Task.Run(() => Native.CheckException(Native.SetVideoSink(handle))).ConfigureAwait(false);
        }
    }
}