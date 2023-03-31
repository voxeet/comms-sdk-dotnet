using System.Runtime.InteropServices;

namespace DolbyIO.Comms
{
    /// <summary>
    /// The ConferenceOptions class provides additional
    /// information about a conference.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class ConferenceOptions
    {
        /// <summary>
        /// The conference parameters.
        /// </summary>
        public ConferenceParams Params = new ConferenceParams();

        /// <summary>
        /// The conference alias.
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string Alias;
    }
}