namespace DolbyIO.Comms.Services
{
    /// <summary>
    /// AudioService provides Local and Remote audio functionalities.
    /// </summary>
    public class AudioService
    {
        private LocalAudioService _local = new LocalAudioService();
        private RemoteAudioService _remote = new RemoteAudioService();

        public LocalAudioService Local { get => _local; }
        public RemoteAudioService Remote { get => _remote; }
    }
}