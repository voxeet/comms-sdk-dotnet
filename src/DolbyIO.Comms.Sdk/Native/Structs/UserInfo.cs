using System.Runtime.InteropServices;

namespace DolbyIO.Comms
{
    /// <summary>
    /// The UserInfo class contains information about the participant who opened
    /// a session.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class UserInfo
    {
        /// <summary>
        /// The unique identifier of the participant who opened the session.
        /// </summary>
        /// <returns>The participant ID.</returns>
        [MarshalAs(UnmanagedType.LPStr)]
        public readonly string Id = "";

        /// <summary>
        /// The name of the participant.
        /// </summary>
        /// <returns>The participant name.</returns>
        [MarshalAs(UnmanagedType.LPStr)]
        public string Name = "";
        
        /// <summary>
        /// The external unique identifier that an application can add
        /// to the participant while opening a session. If a participant uses the
        /// same external ID in a few conferences, the participant ID also remains the same
        /// across all sessions.
        /// </summary>
        /// <returns>The external ID of the participant.</returns>
        [MarshalAs(UnmanagedType.LPStr)]
        public string ExternalId = "";

        /// <summary>
        /// The URL of the participant's avatar.
        /// </summary>
        /// <returns>The URL of the avatar.</returns>
        [MarshalAs(UnmanagedType.LPStr)]
        public string AvatarURL = "";
    }
}