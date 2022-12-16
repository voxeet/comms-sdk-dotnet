using System.Threading.Tasks;

#nullable enable

namespace DolbyIO.Comms
{
    /// <summary>
    /// The local video service is used to control local participant's video capture
    /// and sending into conference.
    /// </summary>
    public sealed class LocalVideoService
    {
        public async Task StartAsync(VideoDevice? device = null, VideoFrameHandler? handler = null)
        {
            VideoDevice input = device ?? new VideoDevice("", "");
            VideoFrameHandler inputHandler = handler ?? new VideoFrameHandler(new VideoFrameHandlerHandle());
            
            await Task.Run(() => Native.CheckException(Native.StartVideo(input, inputHandler.Handle))).ConfigureAwait(false);
        }

        public async Task StopAsync()
        {
            await Task.Run(() => Native.CheckException(Native.StopVideo())).ConfigureAwait(false);
        }
    }
}