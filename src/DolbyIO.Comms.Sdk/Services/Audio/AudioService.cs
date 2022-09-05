namespace DolbyIO.Comms.Services
{
    /// <summary>
    /// AudioService provides Local and Remote audio functionalities. It allows you to start, stop and mute local or
    /// remote audio sources.
    /// 
    /// - See <see cref="DolbyIO.Comms.Services.LocalAudioService"/>
    /// - See <see cref="DolbyIO.Comms.Services.RemoteAudioService"/>
    /// 
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
        /// The Local audio service accessor.
        /// </summary>
        public LocalAudioService Local { get => _local; }

        /// <summary>
        /// The Remote audio service accessor.
        /// </summary>
        public RemoteAudioService Remote { get => _remote; }
    }
}