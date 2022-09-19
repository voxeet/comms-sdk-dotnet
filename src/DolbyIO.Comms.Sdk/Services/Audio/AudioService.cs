namespace DolbyIO.Comms.Services
{
    /// <summary>
    /// The audio service offers two properties for the services that allow accessing audio methods for the <see cref="Local"/> and <see cref="Remote"/> participants.
    /// </summary>
    /// <example>
    /// <code>
    /// try
    /// {
    ///     await _sdk.Audio.Local.StartAsync();
    ///     await _sdk.Audio.Remote.StopAsync(participantId);
    /// }
    /// catch
    /// {
    ///     // Error handling
    /// }
    /// </code>
    /// </example>
    public sealed class AudioService
    {
        private LocalAudioService _local = new LocalAudioService();

        /// <summary>
        /// Gets the local audio service.
        /// </summary>
        /// <value>The service that allows accessing audio methods for the local participant.</value>
        public LocalAudioService Local { get => _local; }

        private RemoteAudioService _remote = new RemoteAudioService();

        /// <summary>
        /// Gets the remote audio service.
        /// </summary>
        /// <value>The service that allows accessing audio methods for remote participants.</value>
        public RemoteAudioService Remote { get => _remote; }
    }
}
