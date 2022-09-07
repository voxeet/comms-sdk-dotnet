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
    public class Audio
    {
        private LocalAudio _local = new LocalAudio();
        private RemoteAudio _remote = new RemoteAudio();

        /// <summary>
        /// The Local audio service accessor.
        /// </summary>
        public LocalAudio Local { get => _local; }

        /// <summary>
        /// The Remote audio service accessor.
        /// </summary>
        public RemoteAudio Remote { get => _remote; }
    }
}