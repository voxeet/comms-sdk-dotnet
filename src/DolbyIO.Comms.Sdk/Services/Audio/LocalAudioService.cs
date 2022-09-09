using System.Threading.Tasks;

namespace DolbyIO.Comms.Services
{
    /// <summary>
    /// The Local Audio Service provides local audio functionalities. you can start, stop and mute 
    /// the local audio source.
    /// 
    /// - See <see cref="DolbyIO.Comms.Services.LocalAudioService.StartAsync"/>
    /// - See <see cref="DolbyIO.Comms.Services.LocalAudioService.StopAsync"/>
    /// - See <see cref="DolbyIO.Comms.Services.LocalAudioService.MuteAsync(bool)"/>
    /// 
    /// </summary>
    /// <example>
    /// <code>
    /// try
    /// {
    ///     await _sdk.Audio.Local.StartAsync();
    ///     await _sdk.Audio.Local.MuteAsync(true);
    ///     await _sdk.Audio.Local.StopAsync();
    /// }
    /// catch
    /// {
    ///     // Error handling
    /// }
    /// </code>
    /// </example>
    public sealed class LocalAudioService
    {
        /// <summary>
        /// Creates a WebRTC AudioTrack and attaches the AudioTrack to
        /// an active Peer Connection. This method also connects the Audio Sink of the
        /// media_source_interface with the WebRTC Audio Source, creating the audio
        /// delivery pipeline.
        /// </summary>
        public async Task StartAsync()
        {
            await Task.Run(() => Native.CheckException(Native.StartAudio())).ConfigureAwait(false);
        }

        /// <summary>
        /// Destructs a WebRTC AudioTrack and detaches it from an active
        /// Peer Connection. This method also disconnects the Audio Sink of the
        /// media_source_interface from the WebRTC Audio Source, which deconstructs
        /// the audio delivery pipeline.
        /// </summary>
        public async Task StopAsync()
        {
            await Task.Run(() => Native.CheckException(Native.StopAudio())).ConfigureAwait(false);
        }

        /// <summary>
        /// Mutes and un-mutes the local participant's microphone.
        /// </summary>
        /// <param name="muted">A boolean value that indicates the required mute state. True
        /// mutes the microphone, false un-mutes the microphone.</param>
        public async Task MuteAsync(bool muted)
        {
            await Task.Run(() => Native.CheckException(Native.Mute(muted))).ConfigureAwait(false);
        }
    }
}