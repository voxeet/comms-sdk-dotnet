using System;
using System.Runtime.InteropServices;

namespace DolbyIO.Comms
{
    public class VideoFrame : SafeHandle 
    {
        public int Width;
        public int Height;

        internal VideoFrame(int width, int height, IntPtr buffer)
            : base(IntPtr.Zero, true)
        {
            Width = width;
            Height = height;
            SetHandle(buffer);
        }

        public override bool IsInvalid => handle == IntPtr.Zero || handle == new IntPtr(-1);

        protected override bool ReleaseHandle()
        {
            return Native.DeleteVideoFrameBuffer(handle);
        }

        public byte[] GetBuffer()
        {
            byte[] buffer = new byte[Width * Height * 4];
            Marshal.Copy(handle, buffer, 0, buffer.Length);

            return buffer;
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