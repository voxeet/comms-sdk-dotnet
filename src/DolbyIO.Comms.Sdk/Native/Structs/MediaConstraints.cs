using System.Runtime.InteropServices;

namespace DolbyIO.Comms
{
    /// <summary>
    /// The MediaConstraints class contains the local media constraints for an application joining a conference.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class MediaConstraints
    {
        /// <summary>
        /// A boolean that indicates whether the application should
        /// capture the local audio and send it to the conference.
        /// </summary>
        /// <returns>If true, the SDK will capture audio and send it to the conference.</returns>
        [MarshalAs(UnmanagedType.U1)]
        public bool Audio = false;

        /// <summary>
        /// A boolean that allows a participant to join a conference as a sender. This
        /// is strictly intended for Server Side SDK applications that
        /// want to inject media without recording. This flag is
        /// ignored by the Client SDK applications.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        internal bool SendOnly = false;
    }
}