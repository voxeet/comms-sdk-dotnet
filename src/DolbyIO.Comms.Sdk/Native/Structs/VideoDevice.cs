using System.Runtime.InteropServices;

namespace DolbyIO.Comms
{
    /// <summary>
    /// The VideoDevice class contains a platform-agnostic description of a video device.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct VideoDevice
    {
        /// <summary>
        /// The unique identifier of the audio device.
        /// </summary>
        /// <returns>The unique identifier of the device.</returns>
        public readonly string Uid;

        /// <summary>
        /// The name of the video device.
        /// </summary>
        /// <returns>The name of the device.</returns>
        public readonly string Name;

        internal VideoDevice(string uid, string name)
        {
            Uid = uid;
            Name = name;
        }
    }
}