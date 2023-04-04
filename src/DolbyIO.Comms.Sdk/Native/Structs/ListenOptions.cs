using System.Runtime.InteropServices;

namespace DolbyIO.Comms
{
    /// <summary>
    /// The ListenOptions class gathers options for joining a conference as listener
    /// who can only receive media.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class ListenOptions 
    {
        /// <summary>
        /// The options for connecting to the conference.
        /// </summary>
        /// <returns>The connection options.</returns>
        public ConnectionOptions Connection = new ConnectionOptions();

        /// <summary>
        /// The listening mode.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public ListenMode Mode = ListenMode.Regular;
    }
}