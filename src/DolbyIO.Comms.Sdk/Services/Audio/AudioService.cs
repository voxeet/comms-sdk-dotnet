namespace DolbyIO.Comms.Services
{
    /// <summary>
    /// The audio service allows access to local and remote audio services.
    /// </summary>
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
