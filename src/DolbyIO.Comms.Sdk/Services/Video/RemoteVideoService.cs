using System.Threading.Tasks;

#nullable enable

namespace DolbyIO.Comms
{
    /// <summary>
    /// The remote video service.
    /// </summary>
    public sealed class RemoteVideoService
    {
        /// <summary>
        /// Sets the video sink to be used by all conferences.
        ///
        /// The video sink passed to this method will be used for passing the decoded video frames to the application. The
        /// ownership of the sink remains with the application, and the SDK will not delete it. The application should set null
        /// sink and ensure that the SetVideoSinkAsync() call returned before deleting the previously set sink object.
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