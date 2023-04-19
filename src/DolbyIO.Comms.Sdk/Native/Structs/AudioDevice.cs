using System;
using System.Runtime.InteropServices;

namespace DolbyIO.Comms
{
    
    
    /// <summary>
    /// The AudioDevice class contains a platform-agnostic description of an audio device.
    /// </summary>
    /// @ingroup device_management
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct AudioDevice : IEquatable<DeviceIdentity>, 
                                IEquatable<AudioDevice>
    {
        /// <summary>
        /// The unique identifier of the audio device.
        /// </summary>
        /// <returns>The identifier.</returns>
        internal readonly DeviceIdentity Identity;

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

        /// <inheritdoc/>
        public bool Equals(AudioDevice obj)
        {
            return Native.AudioDeviceEquals(Identity.Value, obj.Identity.Value);
        }

        /// <inheritdoc/>
        public bool Equals(DeviceIdentity id)
        {
            return Native.AudioDeviceEquals(Identity.Value, id.Value);
        }
    }
}