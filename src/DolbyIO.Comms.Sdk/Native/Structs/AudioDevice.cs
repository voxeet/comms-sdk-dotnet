using System.Runtime.InteropServices;

namespace DolbyIO.Comms
{
    /// <summary>
    /// The AudioDevice class contains a platform-agnostic description of an audio device.
    /// </summary>
    /// @ingroup device_management
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct AudioDevice
    {
        /// <summary>
        /// The unique identifier of the audio device.
        /// </summary>
        /// <returns>The identifier.</returns>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.DeviceUidSize, ArraySubType = UnmanagedType.U1)]
        public readonly byte[] Uid;

        /// <summary>
        /// The name of the audio device.
        /// </summary>
        /// <returns>The name of the device.</returns>
        [MarshalAs(UnmanagedType.LPStr)]
        public readonly string Name;

        /// <summary>
        /// Information whether the device is the input or output device.
        /// </summary>
        /// <returns>Information whether the device is the input or output device.</returns>
        [MarshalAs(UnmanagedType.I4)]
        public readonly DeviceDirection Direction;
    }
}