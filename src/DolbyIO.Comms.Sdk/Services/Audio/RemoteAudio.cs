using System.Threading.Tasks;

namespace DolbyIO.Comms.Services
{
    /// <summary>
    /// The RemoteAudio service allows the local participant to <see cref="DolbyIO.Comms.Services.RemoteAudio.Mute(bool, string)">mute</see> selected remote participants and <see cref="DolbyIO.Comms.Services.RemoteAudio.Stop(string)"> stop</see> and <see cref="DolbyIO.Comms.Services.RemoteAudio.Start(string)">start</see> receiving audio from remote participants in non-Dolby Voice conferences.
    /// </summary>
    /// <example>
    /// <code>
    /// try
    /// {
    ///     await _sdk.Audio.Remote.Start(participantId);
    ///     await _sdk.Audio.Remote.Mute(true, participantId);
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
        /// Allows the local participant, who used the
        /// <see cref="DolbyIO.Comms.Services.RemoteAudio.Stop(string)"> stop</see> method
        /// on a selected remote participant, to start receiving the remote participant's
        /// audio track.
        /// This method allows an audio track from the
        /// desired remote participant to be mixed into the Dolby Voice audio stream
        /// received by the SDK. If the participant does not have their audio enabled,
        /// this method does not enable their audio track.
        /// </summary>
        /// <remarks>
        /// Attention: This method is only available in non-Dolby Voice conferences.
        /// </remarks>
        /// <param name="participantId">The ID of the remote participant whose audio track should be sent to the local participant.</param>
        /// <returns>A task that represents the returned asynchronous operation.</returns>
        public async Task Start(string participantId)
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
        /// <remarks>
        /// Attention: This method is only available in non-Dolby Voice conferences.
        /// </remarks>
        /// <param name="participantId">The ID of the remote participant whose audio track should not be sent to the local participant.</param>
        /// <returns>A task that represents the returned asynchronous operation.</returns>
        public async Task Stop(string participantId)
        {
            await Task.Run(() => Native.CheckException(Native.StopRemoteAudio(participantId))).ConfigureAwait(false);
        }

        /// <summary>
        /// Stops playing the specified remote participants' audio to the local participant.
        /// The mute method does not notify the server to stop audio stream transmission.
        /// To stop sending an audio stream to the server, use the
        /// <see cref="DolbyIO.Comms.Services.LocalAudio.Stop">stopAudio</see> method.
        /// </summary>
        /// <remarks>
        /// Attention: This method is only available in non-Dolby Voice conferences.
        /// </remarks>
        /// <param name="muted">A boolean value that indicates the required mute state. True
        /// mutes the remote participant, false un-mutes the remote participant.</param>
        /// <param name="participantId">The ID of the remote participant whose audio should not be played.</param>
        /// <returns>A task that represents the returned asynchronous operation.</returns>
        public async Task Mute(bool muted, string participantId)
        {
            await Task.Run(() => Native.CheckException(Native.RemoteMute(muted, participantId))).ConfigureAwait(false);
        }
    }
}