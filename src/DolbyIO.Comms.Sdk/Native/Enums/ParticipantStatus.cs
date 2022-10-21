namespace DolbyIO.Comms
{
    /// <summary>
    /// The ParticipantStatus enum gathers the possible statuses of a conference participant.
    /// </summary>
    public enum ParticipantStatus 
    {
        /// <summary>
        /// The participant has been invited to a conference and is waiting for an invitation.
        /// </summary>
        Reserved = 0,
        /// <summary>
        /// The participant has received a conference invitation and is connecting to the conference.
        /// </summary>
        Connecting,
        /// <summary>
        /// The participant has successfully connected to the conference.
        /// </summary>
        OnAir,
        /// <summary>
        /// The invited participant has declined the conference invitation.
        /// </summary>
        Decline,
        /// <summary>
        /// The participant does not send any audio, video, or screen-share stream to the conference.
        /// </summary>
        Inactive,
        /// <summary>
        /// The participant has left the conference.
        /// </summary>
        Left,
        /// <summary>
        /// The participant is experiencing a peer connection problem.
        /// </summary>
        Warning,
        /// <summary>
        /// The participant cannot connect to the conference due to a peer connection failure.
        /// </summary>
        Error
    }
}