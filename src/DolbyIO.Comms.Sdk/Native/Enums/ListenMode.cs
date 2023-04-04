namespace DolbyIO.Comms 
{
    /// <summary>
    /// The listening mode for listeners.
    /// </summary>
    public enum ListenMode
    {
        /// <summary>
        /// Receive multiple streams.
        /// </summary>
        Regular = 0,

        /// <summary>
        /// Receive a realtime mixed stream.
        /// </summary>
        RtsMixed = 1
    }
}