using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DolbyIO.Comms.Services
{
    /// <summary>
    /// The Device Management Service provides an interface for setting
    /// the input and output audio devices as well as getting notifications about
    /// the added and removed devices.
    ///
    /// To use the Device Management Service, follow these steps:
    ///  1. Get all current audio devices using the 
    ///  <see cref="DolbyIO.Comms.Services.MediaDevice.GetAudioDevices"/> method.
    ///  2. Set the desired input audio device by calling the 
    ///  <see cref="DolbyIO.Comms.Services.MediaDevice.SetPreferredAudioInputDevice(AudioDevice)"/> method.
    ///  3. Set the desired output audio device by calling the
    ///  <see cref="DolbyIO.Comms.Services.MediaDevice.SetPreferredAudioOutputDevice(AudioDevice)"/> method.
    ///  4. Subscribe to <see cref="DolbyIO.Comms.Services.MediaDevice.Added"/>, 
    ///  <see cref="DolbyIO.Comms.Services.MediaDevice.Removed"/>, and 
    ///  <see cref="DolbyIO.Comms.Services.MediaDevice.Changed"/> events.
    /// </summary>
    public class MediaDevice
    {
        /// <summary>
        /// The media device Added event. Raised when a new device is added to the system.
        /// </summary>
        public DeviceAddedEventHandler Added
        {
            set { Native.SetOnDeviceAddedHandler(value); }
        }

        /// <summary>
        /// The media device Removed event. Raised when a device is removed from the system.
        /// </summary>
        public DeviceRemovedEventHandler Removed
        {
            set { Native.SetOnDeviceRemovedHandler(value); }
        }

        /// <summary>
        /// The media device Changed event. Raised when the currently used input or output device has changed.
        /// </summary>
        public DeviceChangedEventHandler Changed
        {
            set { Native.SetOnDeviceChangedHandler(value); }
        }

        /// <summary>
        ///     Gets a list of the currently available audio devices in the system.
        /// </summary>
        /// <returns>
        ///     The result object producing a list containing the audio
        ///     devices asynchronously.
        /// </returns>
        /// <example>
        ///     <code>
        ///         try 
        ///         {
        ///             List&lt;AudioDevice&gt; devices = await _sdk.MediaDevice.GetAudioDevices();
        ///         }
        ///         catch (DolbyIOException e)
        ///         {
        ///             // Error Handling
        ///         }
        ///     </code>
        /// </example>
        public async Task<List<AudioDevice>> GetAudioDevices()
        {
            return await Task.Run(() => 
            {
                List<AudioDevice> devices = new List<AudioDevice>();
                AudioDevice[] src = new AudioDevice[0];
                int size = 0;

                Native.CheckException(Native.GetAudioDevices(ref size, out src));
                
                for (int i = 0; i < size; i++)
                {
                    devices.Add(src[i]);
                }

                return devices;
            });
        }

        /// <summary>
        ///  Gets the audio input device that is currently used by the system.
        /// </summary>
        /// <returns>Currently used input audio device.</returns>
        public async Task<AudioDevice> GetCurrentAudioInputDevice()
        {
            return await Task.Run(() => 
            {
                AudioDevice d;
                Native.CheckException(Native.GetCurrentAudioInputDevice(out d));
                return d;
            });
        }

        /// <summary>
        /// Gets the audio output device currently used by the system.
        /// </summary>
        /// <returns>Currently used output audio device.</returns>
        public async Task<AudioDevice> GetCurrentAudioOuputDevice()
        {
            return await Task.Run(() => 
            {
                AudioDevice d;
                Native.CheckException(Native.GetCurrentAudioOutputDevice(out d));
                return d;
            });
        }

        /// <summary>
        /// Sets the preferred input audio device.
        /// </summary>
        /// <param name="device">Structure containing information about the desired input device.</param>
        /// <returns></returns>
        public async Task SetPreferredAudioInputDevice(AudioDevice device)
        {
            await Task.Run(() => 
            {
                Native.CheckException(Native.SetPreferredAudioInputDevice(device));
            });
        }

        /// <summary>
        /// Sets the preferred output audio device.
        /// </summary>
        /// <param name="device">Structure containing information about the desired output device.</param>
        /// <returns></returns>
        public async Task SetPreferredAudioOutputDevice(AudioDevice device)
        {
            await Task.Run(() => 
            {
                Native.CheckException(Native.SetPreferredAudioOutputDevice(device));
            });
        }
    }
}