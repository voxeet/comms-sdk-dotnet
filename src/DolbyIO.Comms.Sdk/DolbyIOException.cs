using System;

namespace DolbyIO.Comms
{
    public sealed class DolbyIOException : Exception
    {
        public DolbyIOException(string msg)
            : base(msg)
        {
        }
    }
}