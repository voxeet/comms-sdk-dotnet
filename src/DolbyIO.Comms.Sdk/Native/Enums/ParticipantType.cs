namespace DolbyIO.Comms
{
    /// <summary>
    /// The ParticipantType enum gathers the possible participant types.
    /// </summary>
    public enum ParticipantType 
    {
        /// <summary>
        /// The participant who can send and receive an audio and video stream during a conference.
        /// </summary>
        User = 0,
        /// <summary>
        /// The participant who can receive audio and video streams, but cannot send any stream to a conference.
        /// </summary>
        Listener,
        /// <summary>
        /// A deprecated type.
        /// </summary>
        Speaker,
        /// <summary>
        /// A participant who connected to the conference using Public Switched Telephone Network (PSTN).
        /// </summary>
        PSTN,
        /// <summary>
        /// A special participant who joins a conference to record it.
        /// </summary>
        Mixer,
        /// <summary>
        /// A deprecated type.
        /// </summary>
        DVCS,
        /// <summary>
        /// A participant who does not have an assigned type.
        /// </summary>
        None,
        /// <summary>
        /// A USER who is present during a replay of a recorded conference.
        /// </summary>
        Robot,
        /// <summary>
        /// A deprecated type.
        /// </summary>
        RobotSpeaker,
        /// <summary>
        /// A LISTENER who is present during a replay of a recorded conference.
        /// </summary>
        RobotListener,
        /// <summary>
        /// A PSTN participant who is present during a replay of a recorded conference.
        /// </summary>
        RobotPSTN,
        /// <summary>
        /// A MIXER who is present during a replay of a recorded conference.
        /// </summary>
        RobotMixer,
        /// <summary>
        /// A participant who does not have an assigned type during a replay of a recorded conference.
        /// </summary>
        RobotNone
    }
}