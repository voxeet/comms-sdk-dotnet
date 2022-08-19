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
    ///  <see cref="DolbyIO.Comms.Services.MediaDevice.GetAudioDevices">GetAudioDevices</see> method.
    ///  2. Set the desired input audio device by calling the 
    ///  <see cref="DolbyIO.Comms.Services.MediaDevice.SetPreferredAudioInputDevice(AudioDevice)">SetPreferredAudioInputDevice</see> method.
    ///  3. Set the desired output audio device by calling the
    ///  <see cref="DolbyIO.Comms.Services.MediaDevice.SetPreferredAudioOutputDevice(AudioDevice)">SetPreferredAudioOutputDevice</see> method.
    ///  4. Subscribe to the <see cref="DolbyIO.Comms.Services.MediaDevice.Added">Added</see>, 
    ///  <see cref="DolbyIO.Comms.Services.MediaDevice.Removed">Removed</see>, and 
    ///  <see cref="DolbyIO.Comms.Services.MediaDevice.Changed">Changed</see> events.
    /// </summary>
    public class MediaDevice
    {
        private DeviceAddedEventHandler _added;

        /// <summary>
        /// Emitted when a new device is added to the system.
        /// </summary>
        public DeviceAddedEventHandler Added
        {
            set 
            { 
                Native.SetOnDeviceAddedHandler(value);
                _added = value;
            }
        }

        private DeviceRemovedEventHandler _removed;

        /// <summary>
        /// Emitted when a device is removed from the system.
        /// </summary>
        public DeviceRemovedEventHandler Removed
        {
            set 
            { 
                Native.SetOnDeviceRemovedHandler(value);
                _removed = value;
            }
        }

        private DeviceChangedEventHandler _changed;

        /// <summary>
        /// Emitted when the currently used input or output device has changed.
        /// </summary>
        public DeviceChangedEventHandler Changed
        {
            set 
            { 
                Native.SetOnDeviceChangedHandler(value);
                _changed = value;
            }
        }

        /// <summary>
        ///     Gets a list of all audio devices that are currently available in the system.
        /// </summary>
        /// <returns>
        ///     The result object producing a list containing the audio
        ///     devices asynchronously.
        /// </returns>
        /// <example>
        ///     <code>
        ///         try 
        ///         {
        ///             List&lt;AudioDevice&gt; devices = await sdk.MediaDevice.GetAudioDevices();
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
            }).ConfigureAwait(false);
        }

        /// <summary>
        ///  Gets the audio input device that is currently used by the system.
        /// </summary>
        /// <returns>The currently used input audio device.</returns>
        public async Task<AudioDevice> GetCurrentAudioInputDevice()
        {
            return await Task.Run(() => 
            {
                AudioDevice d;
                Native.CheckException(Native.GetCurrentAudioInputDevice(out d));
                return d;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the audio output device that is currently used by the system.
        /// </summary>
        /// <returns>The currently used output audio device.</returns>
        public async Task<AudioDevice> GetCurrentAudioOuputDevice()
        {
            return await Task.Run(() => 
            {
                AudioDevice d;
                Native.CheckException(Native.GetCurrentAudioOutputDevice(out d));
                return d;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Sets the preferred input audio device.
        /// </summary>
        /// <param name="device">Structure containing information about the preferred input device.</param>
        /// <returns></returns>
        public async Task SetPreferredAudioInputDevice(AudioDevice device)
        {
            await Task.Run(() => 
            {
                Native.CheckException(Native.SetPreferredAudioInputDevice(device));
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Sets the preferred output audio device.
        /// </summary>
        /// <param name="device">Structure containing information about the preferred output device.</param>
        /// <returns></returns>
        public async Task SetPreferredAudioOuputDevice(AudioDevice device)
        {
            await Task.Run(() => 
            {
                Native.CheckException(Native.SetPreferredAudioOutputDevice(device));
            }).ConfigureAwait(false);
        }
    }
}