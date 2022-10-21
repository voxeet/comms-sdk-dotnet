using System.Runtime.InteropServices;

namespace DolbyIO.Comms
{
    /// <summary>
    /// The ConnectionOptions class contains options that define how the application expects to join a
    /// conference in terms of media preference.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class ConnectionOptions
    {
        /// <summary>
        /// The conference access token that is required to join a protected conference if
        /// the conference is created using the [create](ref:conference#operation-create-conference)
        /// REST API. While calling the join or listen method, the application has to externally
        /// fetch the token and provide it to the SDK.
        /// </summary>
        /// <returns>The conference access token.</returns>
        [MarshalAs(UnmanagedType.LPStr)]
        public string ConferenceAccessToken = "";

        /// <summary>
        /// A boolean that enables spatial audio for the joining participant. This boolean must be set to
        /// true if spatial audio style is enabled. For more information, refer to our sample
        /// application code.
        /// </summary>
        /// <returns>If true, spatial audio is enabled.</returns>
        [MarshalAs(UnmanagedType.U1)]
        public bool SpatialAudio = false;
    }
}