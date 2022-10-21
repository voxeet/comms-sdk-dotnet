using System.Runtime.InteropServices;

namespace DolbyIO.Comms
{
    /// <summary>
    /// The ConferenceParams class gathers conference parameters.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class ConferenceParams
    {
        /// <summary>
        /// A boolean value that indicates whether the SDK should create a Dolby Voice
        /// conference where each participant receives one audio stream.
        /// </summary>
        /// <returns>If true, the SDK will create a Dolby Voice conference.</returns>
        [MarshalAs(UnmanagedType.U1)]
        public bool DolbyVoice = true;

        /// <summary>
        /// A boolean that indicates whether the conference
        /// should include additional statistics.
        /// </summary>
        /// <returns>If true, the conference will include statistics.</returns>
        [MarshalAs(UnmanagedType.U1)]
        public bool Stats = false;

        /// <summary>
        /// An enum that defines how the spatial location is communicated
        /// between the SDK and the Dolby.io server.
        /// </summary>
        /// <returns>The selected spatial audio style.</returns>
        [MarshalAs(UnmanagedType.I4)]
        public SpatialAudioStyle SpatialAudioStyle = 0;
    }
}