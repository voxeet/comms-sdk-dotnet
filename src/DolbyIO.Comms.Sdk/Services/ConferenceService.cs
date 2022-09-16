using System.Collections;
using System.Collections.Generic;
using System;
using System.Numerics;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace DolbyIO.Comms.Services 
{   
    /// <summary>
    /// The Conference service allows joining and leaving conferences as well as
    /// subscribing to conference events. To use the Conference Service, follow these steps:
    /// 1. Open a session using <see cref="DolbyIO.Comms.Services.SessionService.OpenAsync(UserInfo)">session.open</see>.
    /// 2. Subscribe to events exposed through the service, for example 
    /// <see cref="DolbyIO.Comms.Services.ConferenceService.StatusUpdated">statusUpdated</see> and 
    /// <see cref="DolbyIO.Comms.Services.ConferenceService.ParticipantUpdated">participantUpdated</see>.
    /// 3. Create a conference using the <see cref="DolbyIO.Comms.Services.ConferenceService.CreateAsync(ConferenceOptions)">create</see> method.
    /// 4. Join the created conference using the <see cref="DolbyIO.Comms.Services.ConferenceService.JoinAsync(Conference, JoinOptions)">join</see> method
    /// or use the <see cref="DolbyIO.Comms.Services.ConferenceService.ListenAsync(Conference, ListenOptions)">listen</see> method to join the conference as a listener.
    /// 5. Leave the conference using the <see cref="DolbyIO.Comms.Services.ConferenceService.LeaveAsync">leave</see> method.
    /// </summary>
    public class ConferenceService
    {   
        private volatile bool _isInConference = false;

        private ConferenceStatusUpdatedEventHandler _statusUpdated;

        /// <summary>
        /// Raised when a conference status has changed.
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
        /// <value>The StatusUpdated event handler.</value>
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
        /// Raised when a new participant has been added to a conference.
        /// <example>
        /// <code>
        /// _sdk.Conference.ParticipantAdded = delegate (Participant participant) 
        /// {
        /// 
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <value>The ParticipantAdded event handler.</value>
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
        /// Raised when a conference participant has changed a status.
        /// <example>
        /// <code>
        /// _sdk.Conference.ParticipantUpdated = delegate (Participant participant) 
        /// {
        /// 
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <value>The ParticipantUpdated event handler.</value>
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
        /// Raised when an active speaker has changed.
        /// <example>
        /// <code>
        /// _sdk.Conference.ActiveSpeakerChange = delegate (string conferenceId, int count, string[] activeSpeakers) 
        /// {
        /// 
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <value>The ActiveSpeakerChange event handler.</value>
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
        /// Raised when a participant receives a message.
        /// </summary>
        /// <example>
        /// <code>
        /// _sdk.Conference.MessageReceived = delegate (string conferenceId, string userId, ParticipantInfo info, string message) 
        /// {
        /// 
        /// }
        /// </code>
        /// </example>
        /// <value>The MessageReceived event handler.</value>
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
        /// Raised when a participant receives a conference invitation.
        /// </summary>
        /// <example>
        /// <code>
        /// _sdk.Conference.InvitationReceived = delegate (string conferenceId, string conferenceAlias, ParticipantInfo info) 
        /// {
        /// 
        /// }
        /// </code>
        /// </example>
        /// <value>The InvitationReceived event handler.</value>
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
        /// Raised when an error related to the Dolby Voice Codec (DVC) occurs.
        /// </summary>
        /// <example>
        /// <code>
        /// _sdk.Conference.DvcError = delegate (string reason) 
        /// {
        /// 
        /// }
        /// </code>
        /// </example>
        /// <value>The DvcError event handler.</value>
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
        /// Raised when a peer connection problem occurs.
        /// </summary>
        /// <example>
        /// <code>
        /// _sdk.Conference.PeerConnectionError = delegate (string reason, string description) 
        /// {
        /// 
        /// }
        /// </code>
        /// </example>
        /// <value>The PeerConnectionError event handler.</value>
        public PeerConnectionErrorEventHandler PeerConnectionError
        {
            set 
            { 
                Native.SetOnPeerConnectionFailedExceptionHandler(value);
                _peerConnectionError = value;
            }
        }

        /// <summary>
        /// Gets whether the SDK is connected to a conference.
        /// </summary>
        public bool IsInConference { get => _isInConference; }

        /// <summary>
        /// Gets informations about the current conference.
        /// </summary>
        /// <returns>The Conference  object.</returns>
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
        /// Gets the list of participants who are present at the current conference.
        /// </summary>
        /// <returns>The result object producing the List<Participant> asynchronously.</returns>
        public async Task<List<Participant>> GetParticipantsAsync()
        {
            return await Task.Run(() =>
            {
                List<Participant> participants = new List<Participant>();
                IntPtr src;
                int size = 0;

                Native.CheckException(Native.GetParticipants(ref size, out src));

                IntPtr[] tmp = new IntPtr[size];
                Marshal.Copy(src, tmp, 0, size);

                for (int i = 0; i < size; i++)
                {
                    var participant = Marshal.PtrToStructure<Participant>(tmp[i]);
                    participants.Add(participant);
                }

                return participants;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a conference and returns information about the conference
        /// upon completion.
        /// </summary>
        /// <param name="options">The conference options.</param>
        /// <returns>The result object producing the Conference asynchronously.</returns>
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
        /// Joins an existing conference as an active user who can receive
        /// media from the conference and inject media into the conference.
        /// </summary>
        /// <param name="conference">The conference object that need to contain the conference ID.</param>
        /// <param name="options">The join options for the SDK user.</param>
        /// <returns>The result object producing the Conference asynchronously.</returns>
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
        /// Joins an existing conference as a listener who can receive audio and
        /// video streams, but cannot send any stream to the conference.
        /// </summary>
        /// <param name="conference">The conference object that need to contain conference ID.</param>
        /// <param name="options">The listen options for the SDK user.</param>
        /// <returns>The result object producing the Conference asynchronously.</returns>
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
        /// Creates a demo conference and joins the conference upon completion.
        /// </summary>
        /// <param name="spatialAudio">A boolean that indicates whether spatial audio should be enabled for the joining
        /// participant. By default, the parameter is set to true.</param>
        /// <returns>The result object producing the Conference asynchronously.</returns>
        public async Task<Conference> DemoAsync(bool spatialAudio = true)
        {
            return await Task.Run(() => 
            {
                Conference infos = new Conference();
                Native.CheckException(Native.Demo(spatialAudio, infos));
                _isInConference = true;
                return infos;
            }).ConfigureAwait(false);
        }

        /// <summary>
        ///Configures a spatial environment of an application, so the audio
        /// renderer understands which directions the application considers forward,
        /// up, and right and which units it uses for distance.
        /// This method is available only for participants who joined a conference using the join method with enabled spatial audio. To set a spatial environment for listeners, use the [Set Spatial Listeners Audio](https://docs.dolby.io/communications-apis/reference/set-spatial-listeners-audio) REST API.
        /// If not called, the SDK uses the default spatial environment, which consists of the following values:
        /// - forward = (0, 0, 1), where +Z axis is in front
        /// - up = (0, 1, 0), where +Y axis is above
        /// - right = (1, 0, 0), where +X axis is to the right
        /// - scale = (1, 1, 1), where one unit on any axis is 1 meter
        ///
        /// For more information about spatial audio, see the [Spatial Audio](https://docs.dolby.io/communications-apis/docs/guides-spatial-audio) guide.
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
        /// Sets the direction the local participant is facing in space. This method is available only for participants who joined the conference using the join method with enabled spatial audio. To set a spatial direction for listeners, use the [Set Spatial Listeners Audio](https://docs.dolby.io/communications-apis/reference/set-spatial-listeners-audio) REST API.
        ///
        /// If the local participant hears audio from the position (0,0,0) facing down the Z-axis and locates a remote participant in the position (1,0,1), the local participant hears the remote participant from their front-right. If the local participant chooses to change the direction they are facing and rotate +90 degrees about the Y-axis, then instead of hearing the speaker from the front-right position, they hear the speaker from the front-left position.
        ///
        /// For more information about spatial audio, see the [Spatial Audio](https://docs.dolby.io/communications-apis/docs/guides-spatial-audio) guide.
        /// </summary>
        /// <param name="direction">The direction the local participant is facing in space.</param>
        public async Task SetSpatialDirectionAsync(Vector3 direction)
        {
            await Task.Run(() => Native.CheckException(Native.SetSpatialDirection(direction.X, direction.Y, direction.Z))).ConfigureAwait(false);
        }

        /// <summary>
        /// Sets a participant's position in space to enable the spatial audio experience during a Dolby Voice conference. This method is available only for participants who joined the conference using the join method with enabled spatial audio. To set a spatial position for listeners, use the [Set Spatial Listeners Audio](https://docs.dolby.io/communications-apis/reference/set-spatial-listeners-audio) REST API.
        ///
        /// Depending on the specified participant in the participant parameter, the setSpatialPosition method impacts the location from which audio is heard or from which audio is rendered:
        /// - When the specified participant is the local participant, setSpatialPosition sets a location from which the local participant listens to a conference. If the local participant does not have an established location, the participant hears audio from the default location (0, 0, 0).
        /// - When the specified participant is a remote participant, setSpatialPosition ensures the remote participant's audio is rendered from the specified location in space. Setting the remote participants’ positions is required in conferences that use the individual spatial audio style. In these conferences, if a remote participant does not have an established location, the participant does not have a default position and will remain muted until a position is specified. The shared spatial audio style does not support setting the remote participants' positions. In conferences that use the shared style, the spatial scene is shared by all participants, so that each client can set a position and participate in the shared scene.
        ///
        /// For example, if a local participant Eric, who uses the individual spatial audio style and does not have a set direction, calls setSpatialPosition(VoxeetSDK.session.participant, {x:3,y:0,z:0}), Eric hears audio from the position (3,0,0). If Eric also calls setSpatialPosition(Sophia, {x:7,y:1,z:2}), he hears Sophia from the position (7,1,2). In this case, Eric hears Sophia 4 meters to the right, 1 meter above, and 2 meters in front.
        ///
        /// For more information about spatial audio, see the [Spatial Audio](https://docs.dolby.io/communications-apis/docs/guides-spatial-audio) guide.
        /// </summary>
        /// <param name="participantId">The selected participant. Using the local participant sets the location from which the participant will hear a conference. Using a remote participant sets the position from which the participant's audio will be rendered.</param>
        /// <param name="position">The participant's audio location.</param>
        public async Task SetSpatialPositionAsync(string participantId, Vector3 position)
        {
            await Task.Run(() => Native.CheckException(Native.SetSpatialPosition(participantId, position.X, position.Y, position.Z))).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends a message to the current conference. The message must be in a form
        /// of string, such as raw or JSON. The message size is
        /// limited to 16KB.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The returned asynchronous operation.</returns>
        public async Task SendMessageAsync(string message)
        {
            await Task.Run(() => Native.CheckException(Native.SendMessage(message))).ConfigureAwait(false);
        }

        /// <summary>
        /// Declines a conference invitation.
        /// </summary>
        /// <param name="conferenceId">The conference ID.</param>
        /// <returns>The returned asynchronous operation.</returns>
        public async Task DeclineInvitationAsync(string conferenceId) {
            await Task.Run(() => Native.CheckException(Native.DeclineInvitation(conferenceId))).ConfigureAwait(false);
        }

        /// <summary>
        /// Leaves a conference.
        /// </summary>
        /// <returns>The returned asynchronous operation.</returns>
        public async Task LeaveAsync()
        {
            await Task.Run(() => {
                Native.CheckException(Native.Leave());
                _isInConference = false;
            }).ConfigureAwait(false);
        }
    }
}
