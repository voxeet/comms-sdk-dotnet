namespace DolbyIO.Comms
{
    /// <summary>
    /// The ConferenceStatus enum gathers possible statuses of a conference.
    /// </summary>
    public enum ConferenceStatus
    {
        /// <summary>
        /// The SDK is creating a new conference.
        /// </summary>
        Creating = 0,
        /// <summary>
        /// The conference is created.
        /// </summary>
        Created,
        /// <summary>
        /// The local participant is joining a conference.
        /// </summary>
        Joining,
        /// <summary>
        /// The local participant successfully joined the conference.
        /// </summary>
        Joined,
        /// <summary>
        /// The local participant is leaving the conference.
        /// </summary>
        Leaving,
        /// <summary>
        /// The local participant left the conference.
        /// </summary>
        Left,
        /// <summary>
        /// The conference is destroyed on the Dolby.io server.
        /// </summary>
        Destroyed,
        /// <summary>
        /// A conference error occurred.
        /// </summary>
        Error
    }
}