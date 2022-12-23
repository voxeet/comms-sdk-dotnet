using System;
using System.Runtime.InteropServices;

namespace DolbyIO.Comms
{
    internal sealed class VideoSinkHandle : SafeHandle
    {
        public VideoSinkHandle()
            : base(IntPtr.Zero, true)
        {}

        public override bool IsInvalid => handle == IntPtr.Zero || handle == new IntPtr(-1);

        public IntPtr GetIntPtr()
        {
            return handle;
        }

        protected override bool ReleaseHandle()
        {
            return Native.DeleteVideoSink(handle);
        }
    }
}