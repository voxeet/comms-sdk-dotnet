using System;
using System.Runtime.InteropServices;

#nullable enable

namespace DolbyIO.Comms
{
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