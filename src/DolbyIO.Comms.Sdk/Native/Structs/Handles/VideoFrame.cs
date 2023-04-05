using System;
using System.Runtime.InteropServices;

namespace DolbyIO.Comms
{
    /// <summary>
    /// The VideoFrame object wraps the decoded ARGB8888 video frames.
    /// </summary>
    public class VideoFrame : SafeHandle 
    {
        /// <summary>
        /// The width of the video frame.
        /// </summary>
        public int Width;

        /// <summary>
        /// The height of the video frame.
        /// </summary>
        public int Height;

        internal VideoFrame(int width, int height, IntPtr buffer)
            : base(IntPtr.Zero, true)
        {
            Width = width;
            Height = height;
            SetHandle(buffer);
        }
        
        /// <inheritdoc/>
        public override bool IsInvalid => handle == IntPtr.Zero || handle == new IntPtr(-1);

        /// <inheritdoc/>
        protected override bool ReleaseHandle()
        {
            return Native.DeleteVideoFrameBuffer(handle);
        }

        /// <summary>
        /// Gets a copy of the native video frame as a byte array.
        /// </summary>
        /// <returns>A byte array containing the video frame.</returns>
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