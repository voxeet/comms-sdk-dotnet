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
        private AudioDeviceAddedEventHandler _audioAdded;

        /// <summary>
        /// Sets the <see cref="AudioDeviceAddedEventHandler"/> that is raised when a new audio device is added to the system.
        /// </summary>
        /// <value>The <see cref="AudioDeviceAddedEventHandler"/> event handler.</value>
        public AudioDeviceAddedEventHandler AudioAdded
        {
            set 
            { 
                Native.SetOnAudioDeviceAddedHandler(value);
                _audioAdded = value;
            }
        }

        private AudioDeviceRemovedEventHandler _audioRemoved;

        /// <summary>
        /// Sets the <see cref="AudioDeviceRemovedEventHandler"/> that is raised when an audio device is removed from the system.
        /// </summary>
        /// <value>The <see cref="AudioDeviceRemovedEventHandler"/> event handler.</value>
        public AudioDeviceRemovedEventHandler AudioRemoved
        {
            set 
            { 
                Native.SetOnAudioDeviceRemovedHandler(value);
                _audioRemoved = value;
            }
        }

        private AudioDeviceChangedEventHandler _audioChanged;

        /// <summary>
        /// Sets the <see cref="AudioDeviceChangedEventHandler"/> that is raised when the currently used input or output audio device has changed.
        /// </summary>
        /// <value>The <see cref="AudioDeviceChangedEventHandler"/> event handler.</value>
        public AudioDeviceChangedEventHandler AudioChanged
        {
            set 
            { 
                Native.SetOnAudioDeviceChangedHandler(value);
                _audioChanged = value;
            }
        }

        private VideoDeviceAddedEventHandler _videoAdded;

        /// <summary>
        /// Sets the <see cref="VideoDeviceAddedEventHandler"/> that is raised when a new video device is added to the system.
        /// </summary>
        /// <value>The <see cref="AudioDeviceAddedEventHandler"/> event handler.</value>
        public VideoDeviceAddedEventHandler VideoAdded
        {
            set
            {
                Native.SetOnVideoDeviceAddedHandler(value);
                _videoAdded = value;
            }
        }

        private VideoDeviceChangedEventHandler _videoChanged;

        /// <summary>
        /// Sets the <see cref="VideoDeviceChangedEventHandler"/> that is raised when the currently used input or output video device has changed.
        /// </summary>
        /// <value>The <see cref="VideoDeviceChangedEventHandler"/> event handler.</value>
        public VideoDeviceChangedEventHandler VideoChanged
        {
            set
            {
                Native.SetOnVideoDeviceChangedHandler(value);
                _videoChanged = value;
            }
        }

        private VideoDeviceRemovedEventHandler _videoRemoved;

        /// <summary>
        /// Sets the <see cref="VideoDeviceRemovedEventHandler"/> that is raised when a video device is removed from the system.
        /// </summary>
        /// <value>The <see cref="VideoDeviceRemovedEventHandler"/> event handler.</value>
        public VideoDeviceRemovedEventHandler VideoRemoved
        {
            set
            {
                Native.SetOnVideoDeviceRemovedHandler(value);
                _videoRemoved = value;
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

        /// <summary>
        /// Gets a list of all video devices that are currently available in the system.
        /// </summary>
        /// <returns>The <xref href="System.Threading.Tasks.Task`1"/> that represents an asynchronous operation.
        /// The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns a list of <see cref="VideoDevice">video devices</see>
        /// that are currently available in the system.</returns>
        public async Task<List<VideoDevice>> GetVideoDevicesAsync()
        {
            return await Task.Run(() => 
            {
                List<VideoDevice> devices = new List<VideoDevice>();
                VideoDevice[] src = new VideoDevice[0];
                int size = 0;

                Native.CheckException(Native.GetVideoDevices(ref size, out src));
                
                for (int i = 0; i < size; i++)
                {
                    devices.Add(src[i]);
                }

                return devices;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the video device that is currently used by the system.
        /// </summary>
        /// <returns> The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the <see cref="VideoDevice">video device</see>
        /// that is currently used by the system.</returns>
        public async Task<VideoDevice> GetCurrentVideoDeviceAsync()
        {
            return await Task.Run(() => 
            {
                VideoDevice device;
                Native.CheckException(Native.GetCurrentVideoDevice(out device));
                return device;
            }).ConfigureAwait(false);
        }
    }
}