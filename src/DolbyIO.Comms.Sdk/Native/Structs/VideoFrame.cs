using System;
using System.Runtime.InteropServices;

namespace DolbyIO.Comms
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct VideoFrame
    {
        public int Width;

        public int Height;

        internal IntPtr _buffer;
    }
}