using System.Threading.Tasks;

namespace DolbyIO.Comms.Services
{
    /// <summary>
    /// The Remote Audio Service provides remote audio functionalities. You can start, stop and mute 
    /// remote audio source. 
    /// 
    /// - See <see cref="DolbyIO.Comms.Services.RemoteAudioService.Start(string)"/> 
    /// - See <see cref="DolbyIO.Comms.Services.RemoteAudioService.Stop(string)"/> 
    /// - See <see cref="DolbyIO.Comms.Services.RemoteAudioService.Mute(bool, string)"/> 
    /// 
    /// **Those methods are only available for non-Dolby Voice conferences.**
    /// 
    /// </summary>
    /// <example>
    /// <code>
    /// try
    /// {
    ///     await _sdk.Audio.Remote.Start(participantId);
    ///     await _sdk.Autio.Remote.Mute(true, participantId);
    ///     await _sdk.Audio.Remote.Stop(participantId);
    /// }
    /// catch
    /// {
    ///     // Error handling
    /// }
    /// </code>
    /// </example>
    public class RemoteAudio
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
        /// <remarks>
        /// Attention: This method is only available for non-Dolby Voice conferences.
        /// </remarks>
        /// <param name="participantId">The ID of the participant whose audio track you would
        /// like to hear.</param>
        /// <returns></returns>
        public async Task Start(string participantId)
        {
            await Task.Run(() => Native.CheckException(Native.StartRemoteAudio(participantId)));
        }

        /// <summary>
        /// Allows the local participant to not receive an audio track from
        /// a selected remote participant. This method only impacts the local
        /// participant; the rest of conference participants
        /// will still hear the participant's audio. This method does not allow
        /// the audio track of the selected remote participant to be
        /// mixed into the Dolby Voice audio stream that the SDK receives.
        /// </summary>
        /// <remarks>
        /// Attention: This method is only available for non-Dolby Voice conferences.
        /// </remarks>
        /// <param name="participantId">The ID of the participant whose audio track you would
        /// like to stop.</param>
        /// <returns></returns>
        public async Task Stop(string participantId)
        {
            await Task.Run(() => Native.CheckException(Native.StopRemoteAudio(participantId)));
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
        /// <returns></returns>
        public async Task Mute(bool muted, string participantId)
        {
            await Task.Run(() => Native.CheckException(Native.RemoteMute(muted, participantId)));
        }
    }
}