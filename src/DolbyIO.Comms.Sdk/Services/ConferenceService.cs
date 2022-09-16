using System.Numerics;
using System.Threading.Tasks;

namespace DolbyIO.Comms.Services 
{   
    /// <summary>
    /// The conference service allows joining and leaving conferences as well as
    /// subscribing to conference events.
    /// </summary>
    public sealed class ConferenceService
    {
        private ConferenceStatusUpdatedEventHandler _statusUpdated;

        /// <summary>
        /// Sets the <see cref="ConferenceStatusUpdatedEventHandler"/> that is raised when a conference status has changed.
        /// See <see cref="DolbyIO.Comms.ConferenceStatus">ConferenceStatus</see>
        /// <example>
        /// <code>
        /// _sdk.Conference.StatusUpdated = delegate (ConferenceStatus status, string conferenceId) 
        /// {
        /// 
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <value>The <see cref="ConferenceStatusUpdatedEventHandler"/> event handler.</value>
        public ConferenceStatusUpdatedEventHandler StatusUpdated
        {
            set 
            { 
                Native.SetOnConferenceStatusUpdatedHandler(value); 
                _statusUpdated = value;
            }
        }

        private ParticipantAddedEventHandler _participantAdded;

        /// <summary>
        /// Sets the <see cref="ParticipantAddedEventHandler"/> that is raised when a new participant has been added to a conference.
        /// <example>
        /// <code>
        /// _sdk.Conference.ParticipantAdded = (Participant participant) => 
        /// {
        /// 
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <value>The <see cref="ParticipantAddedEventHandler"/> event handler.</value>
        public ParticipantAddedEventHandler ParticipantAdded
        {
            set 
            {
                Native.SetOnParticipantAddedHandler(value); 
                _participantAdded = value;
            }
        }

        private ParticipantUpdatedEventHandler _participantUpdated;

        /// <summary>
        /// Sets the <see cref="ParticipantUpdatedEventHandler"/> that is raised when a conference participant has changed a status.
        /// <example>
        /// <code>
        /// _sdk.Conference.ParticipantUpdated = (Participant participant) =>
        /// {
        /// 
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <value>The <see cref="ParticipantUpdatedEventHandler"/> event handler.</value>
        public ParticipantUpdatedEventHandler ParticipantUpdated
        {
            set 
            {
                Native.SetOnParticipantUpdatedHandler(value); 
                _participantUpdated = value;
            }
        }

        private ActiveSpeakerChangeEventHandler _activeSpeakerChange;

        /// <summary>
        /// Sets the <see cref="ActiveSpeakerChangeEventHandler"/> that is raised when an active speaker has changed.
        /// <example>
        /// <code>
        /// _sdk.Conference.ActiveSpeakerChange = (string conferenceId, int count, string[] activeSpeakers) => 
        /// {
        /// 
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <value>The <see cref="ActiveSpeakerChangeEventHandler"/> event handler.</value>
        public ActiveSpeakerChangeEventHandler ActiveSpeakerChange
        {
            set 
            {
                Native.SetOnActiveSpeakerChangeHandler(value); 
                _activeSpeakerChange = value;
            }
        }

        private ConferenceMessageReceivedEventHandler _messageReceived;

        /// <summary>
        /// Sets the <see cref="ConferenceMessageReceivedEventHandler"/> that is raised when a participant receives a message.
        /// </summary>
        /// <example>
        /// <code>
        /// _sdk.Conference.MessageReceived = (string conferenceId, string userId, ParticipantInfo info, string message) =>
        /// {
        /// 
        /// }
        /// </code>
        /// </example>
        /// <value>The <see cref="ConferenceMessageReceivedEventHandler"/> event handler.</value>
        public ConferenceMessageReceivedEventHandler MessageReceived
        {
            set 
            { 
                Native.SetOnConferenceMessageReceivedHandler(value);
                _messageReceived = value;
            }
        }

        private ConferenceInvitationReceivedEventHandler _invitationReceived;

        /// <summary>
        /// Sets the <see cref="ConferenceInvitationReceivedEventHandler"/> that is raised when a participant receives a conference invitation.
        /// </summary>
        /// <example>
        /// <code>
        /// _sdk.Conference.InvitationReceived = (string conferenceId, string conferenceAlias, ParticipantInfo info) =>
        /// {
        /// 
        /// }
        /// </code>
        /// </example>
        /// <value>The <see cref="ConferenceInvitationReceivedEventHandler"/> event handler.</value>
        public ConferenceInvitationReceivedEventHandler InvitationReceived
        {
            set 
            { 
                Native.SetOnConferenceInvitationReceivedHandler(value);
                _invitationReceived = value;
            }
        }

        private DvcErrorEventHandler _dvcError;

        /// <summary>
        /// Sets the <see cref="DvcErrorEventHandler"/> that is raised when an error related to the Dolby Voice Codec (DVC) occurs.
        /// </summary>
        /// <example>
        /// <code>
        /// _sdk.Conference.DvcError = (string reason) =>
        /// {
        /// 
        /// }
        /// </code>
        /// </example>
        /// <value>The <see cref="DvcErrorEventHandler"/> event handler.</value>
        public DvcErrorEventHandler DvcError
        {
            set 
            { 
                Native.SetOnDvcErrorExceptionHandler(value);
                _dvcError = value;
            }
        }

        private PeerConnectionErrorEventHandler _peerConnectionError;

        /// <summary>
        /// Sets the <see cref="PeerConnectionErrorEventHandler"/> that is raised when a peer connection problem occurs.
        /// </summary>
        /// <example>
        /// <code>
        /// _sdk.Conference.PeerConnectionError = (string reason, string description) =>
        /// {
        /// 
        /// }
        /// </code>
        /// </example>
        /// <value>The <see cref="PeerConnectionErrorEventHandler"/> event handler.</value>
        public PeerConnectionErrorEventHandler PeerConnectionError
        {
            set 
            { 
                Native.SetOnPeerConnectionFailedExceptionHandler(value);
                _peerConnectionError = value;
            }
        }

        private volatile bool _isInConference = false;

        /// <summary>
        /// Gets whether the SDK is connected to a conference.
        /// </summary>
        public bool IsInConference { get => _isInConference; }

        /// <summary>
        /// Gets the object that represents the currently active conference.
        /// </summary>
        /// <returns>The <see cref="Task{Conference}"/> that represents the asynchronous operation.
        /// The <see cref="Task{Conference}.Result"/> property returns the currently active <see cref="Conference" />.</returns>
        public async Task<Conference> CurrentAsync()
        {
            return await Task.Run(() =>
            {
                Conference conference = new Conference();
                Native.CheckException(Native.GetCurrentConference(conference));
                return conference;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a conference and returns information about the conference upon completion.
        /// </summary>
        /// <param name="options">The conference options.</param>
        /// <returns>The <see cref="Task{Conference}"/> that represents the asynchronous operation.
        /// The <see cref="Task{Conference}.Result"/> property returns the newly created <see cref="Conference" />.</returns>
        public async Task<Conference> CreateAsync(ConferenceOptions options)
        {
            return await Task.Run(() => 
            {
                Conference conference = new Conference();
                Native.CheckException(Native.Create(options, conference));
                return conference;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Joins an existing conference as a user.
        /// </summary>
        /// <param name="conference">The conference object that represents the conference to join.</param>
        /// <param name="options">The join options for the current participant.</param>
        /// <returns>The <see cref="Task{Conference}"/> that represents the asynchronous operation.
        /// The <see cref="Task{Conference}.Result"/> property returns the joined <see cref="Conference" /> object.</returns>
        public async Task<Conference> JoinAsync(Conference conference, JoinOptions options)
        {
            return await Task.Run(() => 
            {
                Conference res = new Conference();
                Native.CheckException(Native.Join(conference, options, res));
                _isInConference = true;
                return res;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Joins an existing conference as a listener.
        /// </summary>
        /// <param name="conference">The conference object that represents the conference to listen to.</param>
        /// <param name="options">The join options for the current participant.</param>
        /// <returns>The <see cref="Task{Conference}"/> that represents the asynchronous operation.
        /// The <see cref="Task{Conference}.Result"/> property returns the joined <see cref="Conference" /> object.</returns>
        public async Task<Conference> ListenAsync(Conference conference, ListenOptions options)
        {
            return await Task.Run(() =>
            {
                Conference res = new Conference();
                Native.CheckException(Native.Listen(conference, options, res));
                _isInConference = true;
                return res;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a demo conference and joins it upon completion.
        /// </summary>
        /// <param name="spatialAudio">A boolean that indicates whether spatial audio should be enabled for the joining
        /// participant. By default, the parameter is set to true.</param>
        /// <returns>The <see cref="Task{Conference}"/> that represents the asynchronous operation.
        /// The <see cref="Task{Conference}.Result"/> property returns the joined <see cref="Conference" /> object.</returns>
        public async Task<Conference> DemoAsync(bool spatialAudio = true)
        {
            return await Task.Run(() => 
            {
                Conference conference = new Conference();
                Native.CheckException(Native.Demo(spatialAudio, conference));
                _isInConference = true;
                return conference;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Configures a spatial environment of an application, so the audio
        /// renderer understands which directions the application considers forward,
        /// up, and right and which units it uses for distance.
        /// This method is available only for participants who joined a conference using the join method with enabled spatial audio. To set a spatial environment for listeners, use the <see href="https://docs.dolby.io/communications-apis/reference/set-spatial-listeners-audio">Set Spatial Listeners Audio</see> REST API.
        /// If not called, the SDK uses the default spatial environment, which consists of the following values:
        /// - forward = (0, 0, 1), where +Z axis is in front
        /// - up = (0, 1, 0), where +Y axis is above
        /// - right = (1, 0, 0), where +X axis is to the right
        /// - scale = (1, 1, 1), where one unit on any axis is 1 meter
        ///
        /// For more information about spatial audio, see the <see href="https://docs.dolby.io/communications-apis/docs/guides-spatial-audio">Spatial Audio</see> guide.
        /// </summary>
        /// <param name="scale">A scale that defines how to convert units from the coordinate system of an application (pixels or centimeters) into meters used by the spatial audio coordinate system. For example, if SpatialScale is set to (100,100,100), it indicates that 100 of the applications units (cm) map to 1 meter for the audio coordinates. In such a case, if the listener's location is (0,0,0)cm and a remote participant's location is (200,200,200)cm, the listener has an impression of hearing the remote participant from the (2,2,2)m location. </param>
        /// <param name="forward">A vector describing the direction the application
        /// considers as forward. The value can be either +1, 0, or -1 and must be
        /// orthogonal to up and right.</param>
        /// <param name="up">A vector describing the direction the application considers as
        /// up. The value can be either +1, 0, or -1 and must be orthogonal to
        /// forward and right.</param>
        /// <param name="right">A vector describing the direction the application considers
        /// as right. The value can be either +1, 0, or -1 and must be orthogonal to
        /// forward and up.</param>
        public async Task SetSpatialEnvironmentAsync(Vector3 scale, Vector3 forward, Vector3 up, Vector3 right)
        {
            await Task.Run(() => Native.CheckException(Native.SetSpatialEnvironment(
                scale.X, scale.Y, scale.Z,
                forward.X, forward.Y, forward.Z,
                up.X, up.Y, up.Z,
                right.X, right.Y, right.Z
            ))).ConfigureAwait(false);
        }

        /// <summary>
        /// Sets the direction the local participant is facing in space. This method is available only for participants who joined the conference using the join method with enabled spatial audio. To set a spatial direction for listeners, use the <see href="https://docs.dolby.io/communications-apis/reference/set-spatial-listeners-audio">Set Spatial Listeners Audio</see> REST API.
        ///
        /// If the local participant hears audio from the position (0,0,0) facing down the Z-axis and locates a remote participant in the position (1,0,1), the local participant hears the remote participant from their front-right. If the local participant chooses to change the direction they are facing and rotate +90 degrees about the Y-axis, then instead of hearing the speaker from the front-right position, they hear the speaker from the front-left position.
        ///
        /// For more information about spatial audio, see the <see href="https://docs.dolby.io/communications-apis/docs/guides-spatial-audio">Spatial Audio</see> guide.
        /// </summary>
        /// <param name="direction">The direction the local participant is facing in space.</param>
        public async Task SetSpatialDirectionAsync(Vector3 direction)
        {
            await Task.Run(() => Native.CheckException(Native.SetSpatialDirection(direction.X, direction.Y, direction.Z))).ConfigureAwait(false);
        }

        /// <summary>
        /// Sets a participant's position in space to enable the spatial audio experience during a Dolby Voice conference. This method is available only for participants who joined the conference using the join method with enabled spatial audio. To set a spatial position for listeners, use the <see href="https://docs.dolby.io/communications-apis/reference/set-spatial-listeners-audio">Set Spatial Listeners Audio</see> REST API.
        ///
        /// Depending on the specified participant in the participant parameter, the setSpatialPosition method impacts the location from which audio is heard or from which audio is rendered:
        /// - When the specified participant is the local participant, setSpatialPosition sets a location from which the local participant listens to a conference. If the local participant does not have an established location, the participant hears audio from the default location (0, 0, 0).
        /// - When the specified participant is a remote participant, setSpatialPosition ensures the remote participant's audio is rendered from the specified location in space. Setting the remote participants’ positions is required in conferences that use the individual spatial audio style. In these conferences, if a remote participant does not have an established location, the participant does not have a default position and will remain muted until a position is specified. The shared spatial audio style does not support setting the remote participants' positions. In conferences that use the shared style, the spatial scene is shared by all participants, so that each client can set a position and participate in the shared scene.
        ///
        /// For example, if a local participant Eric, who uses the individual spatial audio style and does not have a set direction, calls setSpatialPosition(VoxeetSDK.session.participant, {x:3,y:0,z:0}), Eric hears audio from the position (3,0,0). If Eric also calls setSpatialPosition(Sophia, {x:7,y:1,z:2}), he hears Sophia from the position (7,1,2). In this case, Eric hears Sophia 4 meters to the right, 1 meter above, and 2 meters in front.
        ///
        /// For more information about spatial audio, see the <see href="https://docs.dolby.io/communications-apis/docs/guides-spatial-audio">Spatial Audio</see> guide.
        /// </summary>
        /// <param name="participantId">The selected participant. Using the local participant sets the location from which the participant will hear a conference. Using a remote participant sets the position from which the participant's audio will be rendered.</param>
        /// <param name="position">The participant's audio location.</param>
        public async Task SetSpatialPositionAsync(string participantId, Vector3 position)
        {
            await Task.Run(() => Native.CheckException(Native.SetSpatialPosition(participantId, position.X, position.Y, position.Z))).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends a message to the current conference.
        /// </summary>
        /// <remarks>
        /// Attention: The message size is limited to 16KB.
        /// </remarks>
        /// <param name="message">The message to send to the conference.</param>
        public async Task SendMessageAsync(string message)
        {
            await Task.Run(() => Native.CheckException(Native.SendMessage(message))).ConfigureAwait(false);
        }

        /// <summary>
        /// Declines a conference invitation.
        /// </summary>
        /// <param name="conferenceId">The conference identifier.</param>
        public async Task DeclineInvitationAsync(string conferenceId) {
            await Task.Run(() => Native.CheckException(Native.DeclineInvitation(conferenceId))).ConfigureAwait(false);
        }

        /// <summary>
        /// Leaves a conference.
        /// </summary>
        public async Task LeaveAsync()
        {
            await Task.Run(() => {
                Native.CheckException(Native.Leave());
                _isInConference = false;
            }).ConfigureAwait(false);
        }
    }
}
