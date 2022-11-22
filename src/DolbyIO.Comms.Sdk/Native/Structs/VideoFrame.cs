using System;
using System.Runtime.InteropServices;

namespace DolbyIO.Comms
{
    public class VideoFrame : SafeHandle 
    {
        public int Width;
        public int Height;

        internal VideoFrame(NativeVideoFrame frame)
            : base(IntPtr.Zero, true)
        {
            Width = frame.Width;
            Height = frame.Height;
            SetHandle(frame.Buffer);
        }

        public override bool IsInvalid { get => handle == IntPtr.Zero; }

        protected override bool ReleaseHandle()
        {
            Native.DeleteVideoFrameBuffer(handle);
            return true;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct NativeVideoFrame
    {
        [MarshalAs(UnmanagedType.I4)]
        public int Width;

        [MarshalAs(UnmanagedType.I4)]
        public int Height;

        public IntPtr Buffer;
    }
}