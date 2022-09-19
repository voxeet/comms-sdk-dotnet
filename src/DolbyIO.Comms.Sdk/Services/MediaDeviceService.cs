using System.Collections.Generic;
using System.Threading.Tasks;

namespace DolbyIO.Comms.Services
{
    /// <summary>
    /// The media device service provides access to set the input and output audio devices
    /// as well as getting notifications about the added and removed devices.
    ///
    /// To use the media device service, follow these steps:
    /// 1. Get all current audio devices using the <see cref="GetAudioDevicesAsync"/> method.
    /// 2. Set the desired input audio device by calling the <see cref="SetPreferredAudioInputDeviceAsync(AudioDevice)"/> method.
    /// 3. Set the desired output audio device by calling the <see cref="SetPreferredAudioOutputDeviceAsync(AudioDevice)"/> method.
    /// 4. Subscribe to the <see cref="Added"/>, <see cref="Removed"/>, and <see cref="Changed"/> events.
    /// </summary>
    public sealed class MediaDeviceService
    {
        private DeviceAddedEventHandler _added;

        /// <summary>
        /// Sets the <see cref="DeviceAddedEventHandler"/> that is raised when a new audio or video device is added to the system.
        /// </summary>
        /// <value>The <see cref="DeviceAddedEventHandler"/> event handler.</value>
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
        /// Sets the <see cref="DeviceRemovedEventHandler"/> that is raised when an audio or video device is removed from the system.
        /// </summary>
        /// <value>The <see cref="DeviceRemovedEventHandler"/> event handler.</value>
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
        /// Sets the <see cref="DeviceChangedEventHandler"/> that is raised when the currently used input or output device has changed.
        /// </summary>
        /// <value>The <see cref="DeviceChangedEventHandler"/> event handler.</value>
        public DeviceChangedEventHandler Changed
        {
            set 
            { 
                Native.SetOnDeviceChangedHandler(value);
                _changed = value;
            }
        }

        /// <summary>
        /// Gets a list of all audio devices that are currently available in the system.
        /// </summary>
        /// <returns>The <xref href="System.Threading.Tasks.Task`1"/> that represents the asynchronous operation.
        /// The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns a list of <see cref="AudioDevice">audio devices</see>
        /// that are currently available in the system.</returns>
        public async Task<List<AudioDevice>> GetAudioDevicesAsync()
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
        /// Gets the audio input device that is currently used by the system.
        /// </summary>
        /// <returns>The <xref href="System.Threading.Tasks.Task`1"/> that represents the asynchronous operation.
        /// The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the <see cref="AudioDevice">audio device</see>
        /// that is currently used by the system.</returns>
        public async Task<AudioDevice> GetCurrentAudioInputDeviceAsync()
        {
            return await Task.Run(() => 
            {
                AudioDevice device;
                Native.CheckException(Native.GetCurrentAudioInputDevice(out device));
                return device;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the audio output device that is currently used by the system.
        /// </summary>
        /// <returns>The currently used output audio device.</returns>
        public async Task<AudioDevice> GetCurrentAudioOutputDeviceAsync()
        {
            return await Task.Run(() => 
            {
                AudioDevice device;
                Native.CheckException(Native.GetCurrentAudioOutputDevice(out device));
                return device;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Sets the preferred input audio device.
        /// </summary>
        /// <param name="device">The <see cref="AudioDevice"/> object to set as preferred input device.</param>
        /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        public async Task SetPreferredAudioInputDeviceAsync(AudioDevice device)
        {
            await Task.Run(() => Native.CheckException(Native.SetPreferredAudioInputDevice(device))).ConfigureAwait(false);
        }

        /// <summary>
        /// Sets the preferred output audio device.
        /// </summary>
        /// <param name="device">The <see cref="AudioDevice"/> object to set as preferred output device.</param>
        /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
        public async Task SetPreferredAudioOutputDeviceAsync(AudioDevice device)
        {
            await Task.Run(() => Native.CheckException(Native.SetPreferredAudioOutputDevice(device))).ConfigureAwait(false);
        }
    }
}