using System;
using System.Runtime.InteropServices;

namespace DolbyIO.Comms
{
    /// <summary>
    /// The VideoSink class is an interface for receiving raw video frames.
    /// </summary>
    public abstract class VideoSink : IDisposable
    {
        internal delegate void VideoSinkOnFrame(string streamId, string trackId, int width, int height, IntPtr buffer);

        internal VideoSinkHandle _handle;

        internal VideoSinkHandle Handle { get => _handle; }

        internal VideoSinkOnFrame _delegate;

        public VideoSink()
        {
            _delegate = OnNativeFrame;
            _handle = Native.CreateVideoSink(_delegate);
        }

        internal void OnNativeFrame(string streamId, string trackId, int width, int height, IntPtr buffer)
        {
            VideoFrame frame = new VideoFrame(width, height, buffer);
            OnFrame(streamId, trackId, frame);
        }

        /// <summary>
        /// The callback that is invoked when a video frame is decoded and ready
        /// to be processed.
        /// </summary>
        /// <param name="streamId">The ID of the media stream to which the video track belongs. In the event of a local camera
        /// camera stream, this string may be empty.</param>
        /// <param name="trackId">The ID of the video track to which the video frame belongs. In the event of a local camera
        /// camera stream, this string may be empty.</param>
        /// <param name="frame">The video frame.</param>
        public abstract void OnFrame(string streamId, string trackId, VideoFrame frame);

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_handle != null && !_handle.IsInvalid)
            {
                _handle.Dispose();
            }
        }
    }
}