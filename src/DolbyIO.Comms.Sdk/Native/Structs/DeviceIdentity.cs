using System;
using System.Runtime.InteropServices;
using System.Runtime.ConstrainedExecution;

namespace DolbyIO.Comms
{
    /// <summary>
    /// An Internal representation of an <see cref="AudioDevice"/> identity.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class DeviceIdentity
    {
        internal readonly IntPtr Value;
    }
}