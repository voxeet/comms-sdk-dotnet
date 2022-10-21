namespace DolbyIO.Comms
{
    /// <summary>
    /// The DeviceDirection enum gathers the possible types of devices.
    /// </summary>
    public enum DeviceDirection
    {
        /// <summary>
        /// A device that does not capture or play audio.
        /// </summary>
        None = 0,
        /// <summary>
        /// A device that captures audio, for example, a microphone.
        /// </summary>
        Input,
        /// <summary>
        /// A device that plays audio, for example, a speaker.
        /// </summary>
        Output,
        /// <summary>
        /// A device that captures and plays audio, for example, a headset.
        /// </summary>
        Both
    }
}