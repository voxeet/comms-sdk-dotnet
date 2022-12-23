using System.Threading.Tasks;

#nullable enable

namespace DolbyIO.Comms
{
    /// <summary>
    /// The RemoteVideoService allows the local participant to locally start and stop remote participants` video streams transmission.
    /// </summary>
    public sealed class RemoteVideoService
    {
        /// <summary>
        /// Sets a video sink to allow passing decoded video frames to an application. The set sink is used in all conferences. 
        /// An application is responsible for the sink and the SDK does not delete it. The application should set a null
        /// sink and ensure that the SetVideoSinkAsync() call returns before deleting the previously set sink object.
        /// </summary>
        /// <param name="sink">The VideoSink used to receive video frames.</param>
        /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        public async Task SetVideoSinkAsync(VideoSink? sink)
        {
            VideoSinkHandle handle = sink != null ? sink.Handle : new VideoSinkHandle();
            await Task.Run(() => Native.CheckException(Native.SetVideoSink(handle))).ConfigureAwait(false);
        }
    }
}