using System.Threading.Tasks;

namespace DolbyIO.Comms
{
    /// <summary>
    /// The remote video service.
    /// </summary>
    public sealed class RemoteVideoService
    {
        public async Task SetVideoSinkAsync(VideoSink sink)
        {
            await Task.Run(() => Native.CheckException(Native.SetVideoSink(sink.Handle.GetIntPtr()))).ConfigureAwait(false);
        }
    }
}