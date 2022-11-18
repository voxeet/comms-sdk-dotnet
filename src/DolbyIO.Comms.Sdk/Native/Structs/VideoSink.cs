using System;
using System.Runtime.InteropServices;

namespace DolbyIO.Comms
{
    internal sealed class VideoSinkHandle : SafeHandle
    {
        internal VideoSinkHandle(IntPtr handle)
            : base(IntPtr.Zero, true)
        {
            SetHandle(handle);
        }

        public override bool IsInvalid { get => handle == IntPtr.Zero; }

        public IntPtr GetIntPtr() 
        {
            return handle;
        }

        protected override bool ReleaseHandle()
        {
            Native.DeleteVideoSink(handle);
            return true;
        }
    }

    /// <summary>
    /// The interface for receiving the raw video frames.
    /// </summary>
    public abstract class VideoSink : IDisposable
    {
        internal delegate void VideoSinkOnFrame(string streamId, string trackId, VideoFrame frame);

        private VideoSinkHandle _handle;

        internal VideoSinkHandle Handle { get => _handle; }

        public VideoSink()
        {
            _handle = new VideoSinkHandle(Native.CreateVideoSink(OnFrame));
        }

        /// <summary>
        /// The callback that is invoked when a video frame is decoded and ready
        /// to be processed.
        /// </summary>
        /// <param name="streamId">The ID of the media stream to which the video track belongs. In the event of a local camera
        /// camera stream, this string may be empty.</param>
        /// <param name="trackId">The ID of the video track to which the frame belongs. In the event of a local camera
        /// camera stream, this string may be empty.</param>
        /// <param name="frame">The video frame.</param>
        public abstract void OnFrame(string streamId, string trackId, VideoFrame frame);

        public void Dispose()
        {
            _handle.Dispose();
        }
    }
}