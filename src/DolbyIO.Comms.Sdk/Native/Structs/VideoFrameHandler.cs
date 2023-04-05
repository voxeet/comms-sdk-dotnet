using System;
using System.Runtime.InteropServices;

#nullable enable

namespace DolbyIO.Comms
{
    /// <summary>
    /// The video frame handler for local video streams.
    ///
    /// The application can set the video frame handler when starting a local camera stream. Use the frame handler to
    /// capture camera frames for local camera preview.
    /// </summary>
    public class VideoFrameHandler : IDisposable
    {
        internal VideoFrameHandlerHandle Handle;
        private VideoSink? _sink;

        /// <summary>
        /// The VideoSink used to handle video frames.
        /// </summary>
        public VideoSink? Sink 
        {
            get => _sink;
            set
            {
                _sink = value;
                Native.SetVideoFrameHandlerSink(Handle, _sink!.Handle);
            }
        }

        /// <summary>
        /// Create a new VideoFrameHandler.
        /// </summary>
        public VideoFrameHandler()
        {
            Handle = Native.CreateVideoFrameHandler();
        }
    
        internal VideoFrameHandler(VideoFrameHandlerHandle handle)
        {
            Handle = handle;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc/>
        protected virtual void Dispose(bool disposing)
        {
            if (Handle != null && !Handle.IsInvalid)
            {
                Handle.Dispose();
            }
        }
    }
}