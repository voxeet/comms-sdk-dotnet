using System;
using System.Runtime.InteropServices;

namespace DolbyIO.Comms
{
    /// <summary>
    /// The VideoSink class is an interface for receiving raw video frames.
    /// </summary>
    public abstract class VideoSink : IDisposable
    {
        internal delegate void VideoSinkOnFrame(int width, int height, IntPtr buffer);

        internal VideoSinkHandle _handle;

        internal VideoSinkHandle Handle { get => _handle; }

        internal VideoSinkOnFrame _delegate;

        /// <summary>
        /// Create a new VideoSink.
        /// </summary>
        public VideoSink()
        {
            _delegate = OnNativeFrame;
            _handle = Native.CreateVideoSink(_delegate);
        }

        internal void OnNativeFrame(int width, int height, IntPtr buffer)
        {
            VideoFrame frame = new VideoFrame(width, height, buffer);
            OnFrame(frame);
        }

        /// <summary>
        /// The callback that is invoked when a video frame is decoded and ready
        /// to be processed.
        /// </summary>
        /// <param name="frame">The video frame.</param>
        public abstract void OnFrame(VideoFrame frame);

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc/>
        protected virtual void Dispose(bool disposing)
        {
            if (_handle != null && !_handle.IsInvalid)
            {
                _handle.Dispose();
            }
        }
    }
}