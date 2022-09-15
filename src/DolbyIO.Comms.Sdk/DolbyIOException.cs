using System;

namespace DolbyIO.Comms 
{
    /// <summary>
    /// Represents errors that occur in the underlying C++ SDK during application execution.
    /// </summary>
    public sealed class DolbyIOException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DolbyIO.Comms.DolbyIOException">DolbyIOException</see>.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        internal DolbyIOException(string message)
            : base(message)
        {
        }
    }
}
