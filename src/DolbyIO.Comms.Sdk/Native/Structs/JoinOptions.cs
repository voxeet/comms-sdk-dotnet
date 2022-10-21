using System.Runtime.InteropServices;

namespace DolbyIO.Comms
{
    /// <summary>
    /// The JoinOptions class gathers options for joining a conference as a user
    /// who can send and receive media.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class JoinOptions
    {
        /// <summary>
        /// The options for connecting to the conference.
        /// </summary>
        /// <returns>The connection options.</returns>
        public ConnectionOptions Connection = new ConnectionOptions();
        
        /// <summary>
        /// The media constraints for the user.
        /// </summary>
        /// <returns>The media constraints.</returns>
        public MediaConstraints Constraints = new MediaConstraints();
    }
}