using System.Threading.Tasks;

namespace DolbyIO.Comms.Services
{
    /// <summary>
    /// The Remote Audio Service provides remote audio functionalities. You can start, stop and mute 
    /// remote audio source. 
    /// 
    /// - See <see cref="DolbyIO.Comms.Services.RemoteAudioService.StartAsync(string)"/> 
    /// - See <see cref="DolbyIO.Comms.Services.RemoteAudioService.StopAsync(string)"/> 
    /// - See <see cref="DolbyIO.Comms.Services.RemoteAudioService.MuteAsync(bool, string)"/> 
    /// 
    /// </summary>
    /// <example>
    /// <code>
    /// try
    /// {
    ///     await _sdk.Audio.Remote.StartAsync(participantId);
    ///     await _sdk.Audio.Remote.MuteAsync(true, participantId);
    ///     await _sdk.Audio.Remote.StopAsync(participantId);
    /// }
    /// catch
    /// {
    ///     // Error handling
    /// }
    /// </code>
    /// </example>
    public sealed class RemoteAudioService
    {
        /// <summary>
        /// Allows the local participant, who used the StopRemoteAudio method
        /// on a selected remote participant, to start receiving the remote participant's
        /// audio track.
        /// This method allows an audio track from the
        /// desired remote participant to be mixed into the Dolby Voice audio stream
        /// received by the SDK. If the participant does not have their audio enabled,
        /// this method does not enable their audio track.
        /// </summary>
        /// <param name="participantId">The ID of the participant whose audio track you would
        /// like to hear.</param>
        public async Task StartAsync(string participantId)
        {
            await Task.Run(() => Native.CheckException(Native.StartRemoteAudio(participantId))).ConfigureAwait(false);
        }

        /// <summary>
        /// Allows the local participant to not receive an audio track from
        /// a selected remote participant. This method only impacts the local
        /// participant; the rest of conference participants
        /// will still hear the participant's audio. This method does not allow
        /// the audio track of the selected remote participant to be
        /// mixed into the Dolby Voice audio stream that the SDK receives.
        /// </summary>
        /// <param name="participantId">The ID of the participant whose audio track you would
        /// like to stop.</param>
        public async Task StopAsync(string participantId)
        {
            await Task.Run(() => Native.CheckException(Native.StopRemoteAudio(participantId))).ConfigureAwait(false);
        }

        /// <summary>
        /// Mutes and un-mutes a specified remote participant.
        /// </summary>
        /// <remarks>
        /// Attention: This method is only available for non-Dolby Voice conferences.
        /// </remarks>
        /// <param name="muted">A boolean value that indicates the required mute state. True
        /// mutes the remote participant, false un-mutes the remote participant.</param>
        /// <param name="participantId">The ID of the remote participant to be muted.</param>
        public async Task MuteAsync(bool muted, string participantId)
        {
            await Task.Run(() => Native.CheckException(Native.RemoteMute(muted, participantId))).ConfigureAwait(false);
        }
    }
}