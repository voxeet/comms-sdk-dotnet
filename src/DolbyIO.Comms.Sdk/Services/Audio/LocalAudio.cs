using System.Threading.Tasks;

namespace DolbyIO.Comms.Services
{
    /// <summary>
    /// The Local Audio Service provides local audio functionalities. you can start, stop and mute 
    /// the local audio source.
    /// 
    /// - See <see cref="DolbyIO.Comms.Services.LocalAudioService.Start"/>
    /// - See <see cref="DolbyIO.Comms.Services.LocalAudioService.Stop"/>
    /// - See <see cref="DolbyIO.Comms.Services.LocalAudioService.Mute(bool)"/>
    /// 
    /// </summary>
    /// <example>
    /// <code>
    /// try
    /// {
    ///     await _sdk.Audio.Local.Start();
    ///     await _sdk.Autio.Local.Mute(true);
    ///     await _sdk.Audio.Local.Stop();
    /// }
    /// catch
    /// {
    ///     // Error handling
    /// }
    /// </code>
    /// </example>
    public class LocalAudio
    {
        /// <summary>
        /// Creates a WebRTC AudioTrack and attaches the AudioTrack to
        /// an active Peer Connection. This method also connects the Audio Sink of the
        /// media_source_interface with the WebRTC Audio Source, creating the audio
        /// delivery pipeline.
        /// </summary>
        /// <returns></returns>
        public async Task Start()
        {
            await Task.Run(() => Native.CheckException(Native.StartAudio()));
        }

        /// <summary>
        /// Destructs a WebRTC AudioTrack and detaches it from an active
        /// Peer Connection. This method also disconnects the Audio Sink of the
        /// media_source_interface from the WebRTC Audio Source, which deconstructs
        /// the audio delivery pipeline.
        /// </summary>
        /// <returns></returns>
        public async Task Stop()
        {
            await Task.Run(() => Native.CheckException(Native.StopAudio()));
        }

        /// <summary>
        /// Mutes and un-mutes the local participant's microphone.
        /// </summary>
        /// <param name="muted">A boolean value that indicates the required mute state. True
        /// mutes the microphone, false un-mutes the microphone.</param>
        /// <returns></returns>
        public async Task Mute(bool muted)
        {
            await Task.Run(() => Native.CheckException(Native.Mute(muted)));
        }
    }
}