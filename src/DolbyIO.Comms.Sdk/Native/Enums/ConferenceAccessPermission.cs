namespace DolbyIO.Comms
{
    /// <summary>
    /// The ConferenceAccessPermissions enum contains the available permissions for application users who are invited to a conference.
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
}