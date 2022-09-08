using System;
using System.Runtime.InteropServices;

namespace DolbyIO.Comms
{
    // -- Sdk --

    /// <summary>
    /// The <see cref="DolbyIO.Comms.DolbyIOSDK.SignalingChannelError">DolbyIOSDK.SignalingChannelError</see> event handler.
    /// </summary>
    /// <param name="message">The reason for the error.</param>
    public delegate void SignalingChannelErrorEventHandler(string message);

    /// <summary>
    /// The <see cref="DolbyIO.Comms.DolbyIOSDK.InvalidTokenError">DolbyIOSDK.InvalidTokenError</see> handler.
    /// </summary>
    /// <param name="reason">The reason for the error.</param>
    /// <param name="description">An additional description of the error.</param>
    public delegate void InvalidTokenErrorEventHandler(string reason, string description);

    // -- Conference --

    /// <summary>
    /// The <see cref="DolbyIO.Comms.Services.Conference.StatusUpdated">Conference.StatusUpdated</see> event handler.
    /// </summary>
    /// <param name="status">The status of the conference.</param>
    /// <param name="conferenceId">The corresponding conference ID.</param>
    public delegate void ConferenceStatusUpdatedEventHandler([MarshalAs(UnmanagedType.I4)]ConferenceStatus status, String conferenceId);

    /// <summary>
    /// The <see cref="DolbyIO.Comms.Services.Conference.ParticipantAdded">Conference.ParticipantAdded</see> event handler. 
    /// </summary>
    /// <param name="participant">The added participant.</param>
    public delegate void ParticipantAddedEventHandler(Participant participant);
    
    /// <summary>
    /// The <see cref="DolbyIO.Comms.Services.Conference.ParticipantUpdated">Conference.ParticipantUpdated</see> event handler.
    /// </summary>
    /// <param name="participant">The participant who changed status.</param>
    public delegate void ParticipantUpdatedEventHandler(Participant participant);

    /// <summary>
    /// The <see cref="DolbyIO.Comms.Services.Conference.ActiveSpeakerChange">Conference.ActiveSpeakerChange</see> event handler. 
    /// </summary>
    /// <param name="conferenceId">The corresponding conference ID.</param>
    /// <param name="count">The number of active speakers.</param>
    /// <param name="activeSpeakers">The array of IDs of the active speakers.</param>
    public delegate void ActiveSpeakerChangeEventHandler(string conferenceId, int count, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 1)] string[]? activeSpeakers);
    
    /// <summary>
    /// The <see cref="DolbyIO.Comms.Services.Conference.MessageReceived">Conference.MessageReceived</see> event handler.
    /// </summary>
    /// <param name="conferenceId">The conference ID.</param>
    /// <param name="userId">The ID of the participant who sent the message.</param>
    /// <param name="info">Additional information about the participant who sent the message.</param>
    /// <param name="message">The received message.</param>
    public delegate void ConferenceMessageReceivedEventHandler(string conferenceId, string userId, ParticipantInfo info, string message);

    /// <summary>
    /// The <see cref="DolbyIO.Comms.Services.Conference.InvitationReceived">Conference.InvitationReceived</see> event handler.
    /// </summary>
    /// <param name="conferenceId">The conference ID.</param>
    /// <param name="conferenceAlias">The conference alias.</param>
    /// <param name="info">Additional information about the participant who sent the invitation.</param>
    public delegate void ConferenceInvitationReceivedEventHandler(string conferenceId, string conferenceAlias, ParticipantInfo info);

    /// <summary>
    /// The <see cref="DolbyIO.Comms.Services.Conference.DvcError">Conference.DvcError</see> event handler.
    /// </summary>
    /// <param name="reason">The reason for the error.</param>
    public delegate void DvcErrorEventHandler(string reason);
    
    /// <summary>
    /// The <see cref="DolbyIO.Comms.Services.Conference.PeerConnectionError">Conference.PeerConnectionError</see> event handler.
    /// </summary>
    /// <param name="reason">The reason for the error.</param>
    public delegate void PeerConnectionErrorEventHandler(string reason);

    // -- Devices --

    /// <summary>
    /// The <see cref="DolbyIO.Comms.Services.MediaDevice.Added">MediaDevice.Added</see> event handler.
    /// </summary>
    /// <param name="device">The added device.</param>
    public delegate void DeviceAddedEventHandler(AudioDevice device);

    /// <summary>
    /// The <see cref="DolbyIO.Comms.Services.MediaDevice.Removed">MediaDevice.Removed</see> event handler.
    /// </summary>
    /// <param name="uid">A unique device identifier of the removed device.</param>
    public delegate void DeviceRemovedEventHandler([MarshalAs(UnmanagedType.LPArray, SizeConst = Constants.DeviceUidSize, ArraySubType = UnmanagedType.U1)] byte[] uid);

    /// <summary>
    /// The <see cref="DolbyIO.Comms.Services.MediaDevice.Changed">MediaDevice.Changed</see> event handler.
    /// </summary>
    /// <param name="device">The new device.</param>
    /// <param name="noDevice">A boolean indicating whether...</param>
    public delegate void DeviceChangedEventHandler(AudioDevice device, bool noDevice);
}