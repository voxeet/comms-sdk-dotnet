using System.Threading.Tasks;

namespace DolbyIO.Comms.Services
{
    /// <summary>
    /// The local audio service allows enabling and disabling the local participant's audio.
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
        /// Enables the local participant's audio and sends the audio to a conference.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        public async Task StartAsync()
        {
            await Task.Run(() => Native.CheckException(Native.StartAudio())).ConfigureAwait(false);
        }

        /// <summary>
        /// Disables the local participant's audio and stops sending the audio to a conference.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        public async Task StopAsync()
        {
            await Task.Run(() => Native.CheckException(Native.StopAudio())).ConfigureAwait(false);
        }

        /// <summary>
        /// Stops sending the local participant's audio to the conference. The mute method does not notify the server to stop audio stream transmission.
        /// </summary>
        /// <param name="muted">A boolean value that indicates the required mute state. True
        /// mutes the microphone, false un-mutes the microphone.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        /// <remarks>
        /// To stop sending audio to the conference, use the <see cref="StopAsync">stopAudio</see> method.
        /// </remarks>
        public async Task MuteAsync(bool muted)
        {
            await Task.Run(() => Native.CheckException(Native.Mute(muted))).ConfigureAwait(false);
        }
    }
}