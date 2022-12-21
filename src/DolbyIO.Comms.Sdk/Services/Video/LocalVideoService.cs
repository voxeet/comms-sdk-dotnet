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
        /// <summary>
        /// /**
        /// Starts local video capture.
        ///
        /// This method may be called at any time, regardless of the conference state. If this method is invoked when there's
        /// no active conference, it will still select the camera device and set the video frame handler. If the video frame
        /// handler has a video sink, camera will start delivering frames to the sink.
        ///
        /// This method can also be used to switch cameras at any point. If you have passed in a VideoFrameHandler to the
        /// previous start call and would like to continue using this handler, you must pass the same handler into the
        /// subsequent call used to switch cameras. This will have the effect of just switching cameras, keeping the rest of
        /// the pipeline in tact.
        ///
        /// The ownership of the frame handler remains with the application. The application must not delete the handler, its
        /// sink, until it invokes the StopAsync() method and the StopAsync() method execution is finished.
        ///
        /// If the application uses a null VideoDevice, then a first video device found in the system will be used.
        ///
        /// If this method returns an error, the provided frame handler can be safely deleted by the application.
        ///
        /// If the application starts the video while not in the conference, and later joins the conference, the conference's
        /// local video state is determined by the MediaConstraints passed to the conference::join() method. It is possible to
        /// start local camera preview, but join the conference without video; in order to enable video later in the
        /// conference, the start() method should be used again. It is not possible to disable sending video into the
        /// conference but keep the local camera preview once the conference started video.   
        /// </summary>
        /// <param name="device">The video device to capture from or null.</param>
        /// <param name="handler">The VideoFrameHandler to receive local video frames.</param>
        /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        public async Task StartAsync(VideoDevice? device = null, VideoFrameHandler? handler = null)
        {
            VideoDevice input = device ?? new VideoDevice("", "");
            VideoFrameHandler inputHandler = handler ?? new VideoFrameHandler(new VideoFrameHandlerHandle());
            
            await Task.Run(() => Native.CheckException(Native.StartVideo(input, inputHandler.Handle))).ConfigureAwait(false);
        }

        /// <summary>
        /// Stops local video capture.
        /// </summary>
        /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        public async Task StopAsync()
        {
            await Task.Run(() => Native.CheckException(Native.StopVideo())).ConfigureAwait(false);
        }
    }
}