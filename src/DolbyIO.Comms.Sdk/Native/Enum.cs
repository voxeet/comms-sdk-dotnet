namespace DolbyIO.Comms
{
    internal enum Result 
    {
        Error = -1,
        Success = 0
    }

    /// <summary>
    /// The logging levels to set. The logging levels allow classifying the
    /// entries in the log files in terms of urgency to help to control the amount of
    /// logged information.
    /// </summary>
    public enum LogLevel {
        /// <summary>
        /// Turns off logging.
        /// </summary>
        Off = 0,    
        /// <summary>
        /// The error level logging generates logs when an error occurs and
        /// the SDK cannot properly function.
        /// </summary>
        Error,   
        /// <summary>
        /// The warning level logging generates logs when the SDK detects an
        /// unexpected problem but is still able to work as usual.
        /// </summary>
        Warning,
        /// <summary>
        /// The info level logging generates an informative number of logs.
        /// </summary>
        Info,
        /// <summary>
        /// The debug level logging generates a high number of logs to provide
        /// diagnostic information in a detailed manner.
        /// </summary>
        Debug,
        /// <summary>
        /// The verbose level logging generates the highest number of logs,
        /// including even the HTTP requests.
        /// </summary>
        Verbose,
    }

    /// <summary>
    /// Conference Access Permissions provided to a particpant who is invited
    /// to a conference.
    /// </summary>
    public enum ConferenceAccessPermissions {
        /// <summary> 
        /// Allows a participant to invite participants to a conference. 
        /// </summary>
        Invite = 0,
        /// <summary>
        /// Allows a participant to join a conference.
        /// </summary>
        Join,
        /// <summary>
        /// Allows a participant to send an audio stream during a conference.
        /// </summary>
        SendAudio,
        /// <summary>
        /// Allows a participant to send a video stream during a conference.
        /// </summary>
        SendVideo,   
        /// <summary>
        /// Allows a participant to share their screen during a conference.
        /// </summary>
        ShareScreen, 
        /// <summary>
        /// Allows a participant to share a video during a conference.
        /// </summary>
        ShareVideo,
        /// <summary>
        ///  Allows a participant to share a file during a conference.
        /// </summary>
        ShareFile,  
        /// <summary>
        /// Allows a participant to send a message to other participants during a conference. 
        /// Message size is limited to 16KB.
        /// </summary>
        SendMessage, 
        /// <summary>
        /// Allows a participant to record a conference.
        /// </summary>
        Record,  
        /// <summary>
        /// Allows a participant to stream a conference.
        /// </summary>    
        Stream,
        /// <summary>
        ///  Allows a participant to kick other participants from a conference.
        /// </summary>
        Kick,       
        /// <summary>
        /// Allows a participant to update other participants permissions.
        /// </summary>
        UpdatePermissions, 
    }

    /// <summary>
    /// Possible values representing the current status of a conference.
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
        /// The conference is destroyed on the server.
        /// </summary>
        Destroyed,
        /// <summary>
        /// A conference error occurred.
        /// </summary>
        Error
    }

    /// <summary>
    /// The possible statuses of conference participants.
    /// </summary>
    public enum ParticipantStatus 
    {
        /// <summary>
        /// A participant is invited to a conference and is waiting for an invitation.
        /// </summary>
        Reserved = 0,
        /// <summary>
        /// A participant received a conference invitation and is connecting to the conference.
        /// </summary>
        Connecting,
        /// <summary>
        /// A participant successfully connected to the conference.
        /// </summary>
        OnAir,
        /// <summary>
        /// An invited participant declined the conference invitation.
        /// </summary>
        Decline,
        /// <summary>
        /// A participant does not send any audio, video, or screen-share stream to the conference.
        /// </summary>
        Inactive,
        /// <summary>
        /// A participant left the conference.
        /// </summary>
        Left,
        /// <summary>
        /// A participant experiences a peer connection problem.
        /// </summary>
        Warning,
        /// <summary>
        /// A participant cannot connect to the conference due to a peer connection failure.
        /// </summary>
        Error
    }

    /// <summary>
    /// The type of participant.
    /// </summary>
    public enum ParticipantType 
    {
        /// <summary>
        /// A participant who can send and receive an audio and video stream during a conference.
        /// </summary>
        User = 0,
        /// <summary>
        /// A participant who can receive audio and video streams, but cannot send any stream to a conference.
        /// </summary>
        Listener,
        /// @cond DO_NOT_DOCUMENT
        Speaker,
        PSTN,
        Mixer,
        DVCS,
        None,
        Robot,
        RobotSpeaker,
        RobotListener,
        RobotPSTN,
        RobotMixer,
        RobotNone
        /// @endcond DO_NOT_DOCUMENT
    }

    /// <summary>
    /// The possible spatial audio styles of the conference. Setting
    /// SpatialAudioStyle is possible only if the DolbyVoice flag is set to true.
    /// You can either use the individual or shared option.
    /// </summary>
    public enum SpatialAudioStyle
    {
        None = 0,
        Individual,
        Shared
    }

    public enum DeviceDirection
    {
        None = 0,
        Input,
        Output,
        Both
    }
}