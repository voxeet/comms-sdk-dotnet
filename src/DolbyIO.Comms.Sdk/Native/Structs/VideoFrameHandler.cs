using System;
using System.Runtime.InteropServices;

#nullable enable

namespace DolbyIO.Comms
{
    /// <summary>
    /// The video frame handler for local video streams.
    ///
    /// The application can set the video frame handler when starting a local camera stream. The frame handler can be used to
    /// capture the camera frames for local camera preview.
    /// </summary>
    public class VideoFrameHandler : IDisposable
    {
        internal VideoFrameHandlerHandle Handle;
        private VideoSink? _sink;

        public VideoSink? Sink 
        {
            get => _sink;
            set
            {
                _sink = value;
                Native.SetVideoFrameHandlerSink(Handle, _sink!.Handle);
            }
        }

        public VideoFrameHandler()
        {
            Handle = Native.CreateVideoFrameHandler();
        }

        internal VideoFrameHandler(VideoFrameHandlerHandle handle)
        {
            Handle = handle;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Handle != null && !Handle.IsInvalid)
            {
                Handle.Dispose();
            }
        }
    }
}