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
    /// 
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

        public abstract void OnFrame(string streamId, string trackId, VideoFrame frame);

        public void Dispose()
        {
            _handle.Dispose();
        }
    }
}