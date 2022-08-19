namespace DolbyIO.Comms
{
    internal enum Result 
    {
        Error = -1,
        Success = 0
    }

    /// <summary>
    /// The LogLevel enum gathers logging levels to set. The logging levels allow classifying the
    /// entries in the log files in terms of urgency to help to control the amount of
    /// logged information.
    /// </summary>
    public enum LogLevel {
        /// <summary>
        /// Disables logging.
        /// </summary>
        Off = 0,    
        /// <summary>
        /// Generates logs only when an error occurs that does not allow
        /// the SDK to function properly.
        /// </summary>
        Error,   
        /// <summary>
        /// Generates logs when the SDK detects an
        /// unexpected problem but is still able to work as usual.
        /// </summary>
        Warning,
        /// <summary>
        /// Generates an informative number of logs.
        /// </summary>
        Info,
        /// <summary>
        /// Generates a high number of logs to provide
        /// diagnostic information in a detailed manner.
        /// </summary>
        Debug,
        /// <summary>
        /// Generates the highest number of logs,
        /// including HTTP requests.
        /// </summary>
        Verbose,
    }

    /// <summary>
    /// The Conference Access Permissions enum contains the available permissions for application users who are invited to a conference.
    /// </summary>
    public enum ConferenceAccessPermissions {
        /// <summary> 
        /// Allows a participant to invite other participants to a conference. 
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
        /// Allows a participant to share a video file during a conference.
        /// </summary>
        ShareVideo,
        /// <summary>
        ///  Allows a participant to share a file during a conference.
        /// </summary>
        ShareFile,  
        /// <summary>
        /// Allows a participant to send a message to other participants during a conference.
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
        /// Allows a participant to update permissions of other participants.
        /// </summary>
        UpdatePermissions, 
    }

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
        /// The conference is destroyed on the server.
        /// </summary>
        Destroyed,
        /// <summary>
        /// A conference error occurred.
        /// </summary>
        Error
    }

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
    /// The SpatialAudioStyle enum gathers the possible spatial audio styles of the conference. Setting
    /// SpatialAudioStyle is possible only if the DolbyVoice flag is set to true.
    /// </summary>
    public enum SpatialAudioStyle
    {
        /// <summary>
        /// Disables spatial audio in a conference.
        /// </summary>
        None = 0,
        /// <summary>
        /// Sets the spatial location that is based on the spatial scene, local participant's position, and remote participants' positions. This allows a client to control the position using the local, self-contained logic. However, the client has to communicate a large set of requests constantly to the server, which increases network traffic, log subsystem pressure, and complexity of the client-side application. This option is selected by default. We recommend this mode for A/V congruence scenarios in video conferencing and similar applications.
        /// </summary>
        Individual,
        /// <summary>
        /// Sets the spatial location that is based on the spatial scene and the local participant's position, while the relative positions among participants are calculated by the Dolby.io server. This way, the spatial scene is shared by all participants, so that each client can set a position and participate in the shared scene. This approach simplifies communication between the client and the server and decreases network traffic. We recommend this mode for virtual space scenarios, such as 2D or 3D games, trade shows, virtual museums, water cooler scenarios, etc.
        ///
        /// **Note**: The shared style currently does not support recording conferences.
        /// </summary>
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