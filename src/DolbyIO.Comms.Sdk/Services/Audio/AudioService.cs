namespace DolbyIO.Comms.Services
{
    /// <summary>
    /// The Audio service allows enabling and disabling the local participant's and remote participants' audio. The service offers two accessors that contain options that impact either sending the local participant's audio to a conference or receiving remote participants' audio.
    /// </summary>
    /// <example>
    /// <code>
    /// try
    /// {
    ///     await _sdk.Audio.Local.Start();
    ///     await _sdk.Audio.Remote.Stop();
    /// }
    /// catch
    /// {
    ///     // Error handling
    /// }
    /// </code>
    /// </example>
    public class AudioService
    {
        private LocalAudioService _local = new LocalAudioService();
        private RemoteAudioService _remote = new RemoteAudioService();

        /// <summary>
        /// Allows accessing methods impacting audio that the local participant's sends to a conference.
        /// </summary>
        /// <value>The service that allows setting options impacting audio sent by the local participant.</value>
        public LocalAudioService Local { get => _local; }

        /// <summary>
        /// Allows accessing methods impacting audio that the local participant's receives from a conference.
        /// </summary>
        /// <value>The service that allows setting options impacting audio that the local participant receives.</value>
        public RemoteAudioService Remote { get => _remote; }
    }
}