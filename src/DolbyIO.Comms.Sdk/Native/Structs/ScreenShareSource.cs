using System;
using System.Runtime.InteropServices;

namespace DolbyIO.Comms
{
    /// <summary>
    /// The description of a source for screen sharing.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct ScreenShareSource
    {
        /// <summary>
        /// Title of the source.
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string Title;

        /// <summary>
        /// Id of the source.
        /// </summary>
        public long Id;

        /// <summary>
        /// Type of the screen share.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public ScreenShareType Type;
    }
}