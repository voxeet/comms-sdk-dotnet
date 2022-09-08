using System.Collections;
using System.Collections.Generic;
using System;
using System.Numerics;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace DolbyIO.Comms.Services 
{   
    /// <summary>
    /// The Conference Service allows joining and leaving conferences as well as
    /// subscribing to conference events. To use the Conference Service, follow these steps:
    /// 1. Open a session using <see cref="DolbyIO.Comms.Services.Session.Open(UserInfo)">Session.Open(UserInfo)</see>.
    /// 2. Subscribe to the events exposed through the service. (Ex. 
    /// <see cref="DolbyIO.Comms.Services.Conference.StatusUpdated"/>,
    /// <see cref="DolbyIO.Comms.Services.Conference.ParticipantUpdated"/>
    /// )
    /// 3. Create a conference via the <see cref="DolbyIO.Comms.Services.Conference.Create(ConferenceOptions)"/> method.
    /// 4. Join the created conference via the <see cref="DolbyIO.Comms.Services.Conference.Join(ConferenceInfos, JoinOptions)"/>
    /// or <see cref="DolbyIO.Comms.Services.Conference.Listen(ConferenceInfos, ListenOptions)"/>method.
    /// 5. Leave the conference using the <see cref="DolbyIO.Comms.Services.Conference.Leave"/> method.
    /// </summary>
    public class Conference
    {   
        private volatile Boolean _isInConference = false;

        private ConferenceStatusUpdatedEventHandler _statusUpdated;

        /// <summary>
        /// The conference status updated event. Raised when the conference status changes.
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
        /// The participant added event. Raised when a participant is added to the conference.
        /// <example>
        /// <code>
        /// _sdk.Conference.ParticipantAdded = delegate (Participant participant) 
        /// {
        /// 
        /// }
        /// </code>
        /// </example>
        /// </summary>
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
        /// The participant updated event. Raised when a participant is updated.
        /// <example>
        /// <code>
        /// _sdk.Conference.ParticipantUpdated = delegate (Participant participant) 
        /// {
        /// 
        /// }
        /// </code>
        /// </example>
        /// </summary>
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
        /// The active speaker change event. Raised when the active speakers are changing.
        /// <example>
        /// <code>
        /// _sdk.Conference.ActiveSpeakerChange = delegate (string conferenceId, int count, string[] activeSpeakers) 
        /// {
        /// 
        /// }
        /// </code>
        /// </example>
        /// </summary>
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
        /// The message received event. Raised when a message is received while in conference.
        /// </summary>
        /// <example>
        /// <code>
        /// _sdk.Conference.MessageReceived = delegate (string conferenceId, string userId, ParticipantInfo info, string message) 
        /// {
        /// 
        /// }
        /// </code>
        /// </example>
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
        /// The invitation received event. Raised when an invitation to a conference is received.
        /// </summary>
        /// <example>
        /// <code>
        /// _sdk.Conference.InvitationReceived = delegate (string conferenceId, string conferenceAlias, ParticipantInfo info) 
        /// {
        /// 
        /// }
        /// </code>
        /// </example>
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
        /// The dvc error event. Raised when an error occurs in the dvc library.
        /// </summary>
        /// <example>
        /// <code>
        /// _sdk.Conference.DvcError = delegate (string reason) 
        /// {
        /// 
        /// }
        /// </code>
        /// </example>
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
        /// The peer connection error event. Raised when an error occurs in the peer connection.
        /// </summary>
        /// <example>
        /// <code>
        /// _sdk.Conference.PeerConnectionError = delegate (string reason, string description) 
        /// {
        /// 
        /// }
        /// </code>
        /// </example>
        public PeerConnectionErrorEventHandler PeerConnectionError
        {
            set 
            { 
                Native.SetOnPeerConnectionFailedExceptionHandler(value);
                _peerConnectionError = value;
            }
        }

        /// <summary>
        /// Indicates that the sdk is in a conference.
        /// </summary>
        public Boolean IsInConference
        {
            get { return _isInConference; }
        }

        /// <summary>
        ///  Gets the full information about the currently active conference.
        /// </summary>
        /// <returns>The ConferenceInfos describing the conference.</returns>
        public async Task<ConferenceInfos> Current()
        {
            return await Task.Run(() =>
            {
                ConferenceInfos infos = new ConferenceInfos();
                Native.CheckException(Native.GetCurrentConference(infos));
                return infos;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a conference and returns information about the conference
        /// upon completion.
        /// </summary>
        /// <param name="options">The conference options.</param>
        /// <returns>The result object producing the ConferenceInfos asynchronously.</returns>
        public async Task<ConferenceInfos> Create(ConferenceOptions options)
        {
            return await Task.Run(() => 
            {
                ConferenceInfos infos = new ConferenceInfos();
                Native.CheckException(Native.Create(options, infos));
                return infos;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Joins an existing conference as an active user who can both receive
        /// media from the conference and inject media into the conference.
        /// </summary>
        /// <param name="infos">The conference options that need to contain conferenceId.</param>
        /// <param name="options">The join options for the SDK user.</param>
        /// <returns>The result object producing the ConferenceInfos asynchronously.</returns>
        public async Task<ConferenceInfos> Join(ConferenceInfos infos, JoinOptions options)
        {
            return await Task.Run(() => 
            {
                ConferenceInfos res = new ConferenceInfos();
                Native.CheckException(Native.Join(infos, options, res));
                _isInConference = true;
                return res;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Joins an existing conference as a listener who can receive audio and
        /// video streams, but cannot send any stream to the conference.
        /// </summary>
        /// <param name="infos">The conference options that need to contain conferenceId.</param>
        /// <param name="options">The listen options for the SDK user.</param>
        /// <returns></returns>
        public async Task<ConferenceInfos> Listen(ConferenceInfos infos, ListenOptions options)
        {
            return await Task.Run(() =>
            {
                ConferenceInfos res = new ConferenceInfos();
                Native.CheckException(Native.Listen(infos, options, res));
                _isInConference = true;
                return res;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a demo conference and joins to it upon completion.
        /// </summary>
        /// <param name="spatialAudio">A boolean flag enabling spatial audio for the joining
        /// participant. The default value is true.</param>
        /// <returns>Resulting conference informations</returns>
        public async Task<ConferenceInfos> Demo(bool spatialAudio = true)
        {
            return await Task.Run(() => 
            {
                ConferenceInfos infos = new ConferenceInfos();
                Native.CheckException(Native.Demo(spatialAudio, infos));
                _isInConference = true;
                return infos;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Updates the spatial audio configuration to enable experiencing
        /// spatial audio during a conference. This method contains information
        /// about a spatial environment of an application, so the audio renderer
        /// understands which directions the application considers forward, up, and
        /// right and which units it uses for distance. This method is available only
        /// for participants who joined a conference using the join method with the
        /// SpatialAudio parameter enabled.
        /// </summary>
        /// <param name="scale">A scale that defines how to convert units from the
        /// coordinate system of an application (pixels or centimeters) into meters
        /// used by the spatial audio coordinate system.</param>
        /// <param name="forward">A vector describing the direction the application
        /// considers as forward. The value can be either +1, 0, or -1 and must be
        /// orthogonal to up and right.</param>
        /// <param name="up">A vector describing the direction the application considers as
        /// up. The value can be either +1, 0, or -1 and must be orthogonal to
        /// forward and right.</param>
        /// <param name="right">A vector describing the direction the application considers
        /// as right. The value can be either +1, 0, or -1 and must be orthogonal to
        /// forward and up.</param>
        /// <returns></returns>
        public async Task SetSpatialEnvironment(Vector3 scale, Vector3 forward, Vector3 up, Vector3 right)
        {
            await Task.Run(() => Native.CheckException(Native.SetSpatialEnvironment(
                scale.X, scale.Y, scale.Z,
                forward.X, forward.Y, forward.Z,
                up.X, up.Y, up.Z,
                right.X, right.Y, right.Z
            ))).ConfigureAwait(false);
        }

        /// <summary>
        /// Updates the spatial audio configuration to enable experiencing
        /// spatial audio during a conference. This method contains information
        /// about the direction the local participant is facing in space.
        /// This method is available only for participants who joined a conference
        /// using the join method with the SpatialAudio parameter enabled.
        /// </summary>
        /// <param name="direction">The direction faced by the local participant.</param>
        /// <returns></returns>
        public async Task SetSpatialDirection(Vector3 direction)
        {
            await Task.Run(() => Native.CheckException(Native.SetSpatialDirection(direction.X, direction.Y, direction.Z))).ConfigureAwait(false);
        }

        /// <summary>
        /// Updates the spatial audio configuration to enable experiencing
        /// spatial audio during a conference. This method contains information
        /// about the participant's location.
        /// This method is available only for participants who joined a conference
        /// using the join method with the SpatialAudio parameter enabled.
        /// Depending on the selected SpatialAudioStyle, the method requires either
        /// providing only the position of the local participant or the positions of
        /// all participants. When using the individual SpatialAudioStyle, remote
        /// participants' audio is disabled until the participants are assigned
        /// to specific locations and each time new participants join the conference,
        /// the positions need to be updated.
        /// </summary>
        /// <param name="userId">The participant whose position is updated.</param>
        /// <param name="participantId">The location of given participant.</param>
        /// <returns></returns>
        public async Task SetSpatialPosition(String participantId, Vector3 position)
        {
            await Task.Run(() => Native.CheckException(Native.SetSpatialPosition(participantId, position.X, position.Y, position.Z))).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends a message to the current conference. The message size is
        /// limited to 16KB and can be any kind of string, from raw to Json.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task SendMessage(string message)
        {
            await Task.Run(() => Native.CheckException(Native.SendMessage(message))).ConfigureAwait(false);
        }

        /// <summary>
        /// Declines a conference invitation.
        /// </summary>
        /// <param name="conferenceId">The conference ID.</param>
        /// <returns></returns>
        public async Task DeclineInvitation(string conferenceId) {
            await Task.Run(() => Native.CheckException(Native.DeclineInvitation(conferenceId))).ConfigureAwait(false);
        }

        /// <summary>
        /// Leaves a conference.
        /// </summary>
        /// <returns></returns>
        public async Task Leave()
        {
            await Task.Run(() => {
                Native.CheckException(Native.Leave());
                _isInConference = false;
            }).ConfigureAwait(false);
        }
    }
}
