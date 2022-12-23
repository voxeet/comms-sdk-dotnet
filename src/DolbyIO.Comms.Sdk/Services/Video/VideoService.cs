namespace DolbyIO.Comms.Services
{
    /// <summary>
    /// The VideoService allows managing the local participant's and remote participants' video.
    /// </summary>
    public sealed class VideoService
    {
        private LocalVideoService _local = new LocalVideoService();

        /// <summary>
        /// Gets the local video service.
        /// </summary>
        public LocalVideoService Local { get => _local; }

        private RemoteVideoService _remote = new RemoteVideoService();

        /// <summary>
        /// Gets the remote video service.
        /// </summary>
        public RemoteVideoService Remote { get => _remote; }
    }
}