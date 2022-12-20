using System.Runtime.InteropServices;

namespace DolbyIO.Comms
{
    /// <summary>
    /// The ParticipantInfo class gathers information about a conference participant.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class ParticipantInfo
    {
        /// <summary>
        /// The external unique identifier that an application can add
        /// to the participant while opening a session. If a participant uses the
        /// same external ID in a few conferences, the participant ID also remains the same
        /// across all sessions.
        /// </summary>
        /// <returns>The external ID of the participant.</returns>
        [MarshalAs(UnmanagedType.LPStr)]
        public readonly string ExternalId = "";

        /// <summary>
        /// The participant name.
        /// </summary>
        /// <returns>The participant name.</returns>
        [MarshalAs(UnmanagedType.LPStr)]
        public readonly string Name = "";
        
        /// <summary>
        /// The URL of the participant's avatar.`
        /// </summary>
        /// <returns>The URL of the avatar.</returns>
        [MarshalAs(UnmanagedType.LPStr)]
        public readonly string AvatarURL = "";
    }
}