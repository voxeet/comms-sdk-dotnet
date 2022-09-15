using System.Threading.Tasks;

namespace DolbyIO.Comms.Services
{
    /// <summary>
    /// The LocalAudio service allows <see cref="DolbyIO.Comms.Services.LocalAudioService.StartAsync">enabling</see>, <see cref="DolbyIO.Comms.Services.LocalAudioService.StopAsync">disabling</see>, and <see cref="DolbyIO.Comms.Services.LocalAudioService.MuteAsync(bool)">muting</see> the local participant's audio.
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
    public class LocalAudioService
    {
        /// <summary>
        /// Enables the local participant's audio and sends the audio to a conference.
        /// The method creates a WebRTC AudioTrack and attaches the AudioTrack to
        /// an active Peer Connection. It also connects the Audio Sink of the
        /// media_source_interface with the WebRTC Audio Source, creating the audio
        /// delivery pipeline.
        /// </summary>
        public async Task StartAsync()
        {
            await Task.Run(() => Native.CheckException(Native.StartAudio())).ConfigureAwait(false);
        }

        /// <summary>
        /// Disables the local participant's audio and stops sending the audio to a conference.
        /// The method destructs a WebRTC AudioTrack and detaches it from an active
        /// Peer Connection. It also disconnects the Audio Sink of the
        /// media_source_interface from the WebRTC Audio Source, which deconstructs
        /// the audio delivery pipeline.
        /// </summary>
        public async Task StopAsync()
        {
            await Task.Run(() => Native.CheckException(Native.StopAudio())).ConfigureAwait(false);
        }

        /// <summary>
        /// Stops playing the local participant's audio to the conference. The mute method does not notify the server to stop audio stream transmission. To stop sending an audio stream to the server, use the <see cref="DolbyIO.Comms.Services.LocalAudioService.Stop">stopAudio</see> method.
        /// </summary>
        /// <param name="muted">A boolean value that indicates the required mute state. True
        /// mutes the microphone, false un-mutes the microphone.</param>
        public async Task MuteAsync(bool muted)
        {
            await Task.Run(() => Native.CheckException(Native.Mute(muted))).ConfigureAwait(false);
        }
    }
}