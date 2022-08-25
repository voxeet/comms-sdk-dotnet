using System;
using System.Runtime.InteropServices;

namespace DolbyIO.Comms
{
    // -- Sdk --

    /// <summary>
    /// The signaling channel error event handler.
    /// See <see cref="DolbyIO.Comms.DolbyIOSDK.SignalingChannelError">DolbyIOSDK.SignalingChannelError</see> 
    /// </summary>
    /// <param name="message">The reason of the error.</param>
    public delegate void SignalingChannelErrorEventHandler(string message);

    /// <summary>
    /// The invalid token event handler.
    /// See <see cref="DolbyIO.Comms.DolbyIOSDK.InvalidTokenError">DolbyIOSDK.InvalidTokenError</see> 
    /// </summary>
    /// <param name="reason">The reason of the error.</param>
    /// <param name="description">A more complete description of the error.</param>
    public delegate void InvalidTokenErrorEventHandler(string reason, string description);

    // -- Conference --

    /// <summary>
    /// The conference status updated event handler.
    /// See <see cref="DolbyIO.Comms.Services.Conference.StatusUpdated">Conference.StatusUpdated</see> 
    /// </summary>
    /// <param name="status">The status of the conference.</param>
    /// <param name="conferenceId">The corresponding conference Id.</param>
    public delegate void ConferenceStatusUpdatedEventHandler([MarshalAs(UnmanagedType.I4)]ConferenceStatus status, String conferenceId);

    /// <summary>
    /// The participant added event handler.
    /// See <see cref="DolbyIO.Comms.Services.Conference.ParticipantAdded">Conference.ParticipantAdded</see> 
    /// </summary>
    /// <param name="participant">The participant that was added.</param>
    public delegate void ParticipantAddedEventHandler(Participant participant);
    
    /// <summary>
    /// The participant updated event handler.
    /// See <see cref="DolbyIO.Comms.Services.Conference.ParticipantUpdated">Conference.ParticipantUpdated</see> 
    /// </summary>
    /// <param name="participant">The participant that was updated.</param>
    public delegate void ParticipantUpdatedEventHandler(Participant participant);

    /// <summary>
    /// The active speaker change event handler.
    /// See <see cref="DolbyIO.Comms.Services.Conference.ActiveSpeakerChange">Conference.ActiveSpeakerChange</see> 
    /// </summary>
    /// <param name="conferenceId">The corresponding conference Id.</param>
    /// <param name="count">The numbers of active speakers.</param>
    /// <param name="activeSpeakers">The array of active speakers Id.</param>
    #nullable enable
    public delegate void ActiveSpeakerChangeEventHandler(string conferenceId, int count, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 1)] string[]? activeSpeakers);
    
    /// <summary>
    /// The conference message received event handler.
    /// See <see cref="DolbyIO.Comms.Services.Conference.MessageReceived">Conference.MessageReceived</see>
    /// </summary>
    /// <param name="conferenceId"></param>
    /// <param name="userId"></param>
    /// <param name="info"></param>
    /// <param name="message"></param>
    public delegate void ConferenceMessageReceivedEventHandler(string conferenceId, string userId, ParticipantInfo info, string message);

    /// <summary>
    /// The conference invitation received event handler.
    /// See <see cref="DolbyIO.Comms.Services.Conference.InvitationReceived">Conference.InvitationReceived</see>
    /// </summary>
    /// <param name="conferenceId"></param>
    /// <param name="conferenceAlias"></param>
    /// <param name="info"></param>
    public delegate void ConferenceInvitationReceivedEventHandler(string conferenceId, string conferenceAlias, ParticipantInfo info);

    /// <summary>
    /// The dvc error event handler.
    /// See <see cref="DolbyIO.Comms.Services.Conference.DvcError">Conference.DvcError</see>
    /// </summary>
    /// <param name="reason"></param>
    public delegate void DvcErrorEventHandler(string reason);
    
    /// <summary>
    /// The peer connection error event handler.
    /// See <see cref="DolbyIO.Comms.Services.Conference.PeerConnectionError">Conference.PeerConnectionError</see>
    /// </summary>
    /// <param name="reason"></param>
    public delegate void PeerConnectionErrorEventHandler(string reason);

    // -- Devices --

    /// <summary>
    /// The device added event handler.
    /// See <see cref="DolbyIO.Comms.Services.MediaDevice.Added">MediaDevice.Added</see>
    /// </summary>
    /// <param name="device"></param>
    public delegate void DeviceAddedEventHandler(AudioDevice device);

    /// <summary>
    /// The device removed event handler.
    /// See <see cref="DolbyIO.Comms.Services.MediaDevice.Removed">MediaDevice.Removed</see>
    /// </summary>
    /// <param name="uid"></param>
    public delegate void DeviceRemovedEventHandler(byte[] uid);

    /// <summary>
    /// The device changed event handler.
    /// See <see cref="DolbyIO.Comms.Services.MediaDevice.Changed">MediaDevice.Changed</see>
    /// </summary>
    /// <param name="device"></param>
    /// <param name="noDevice"></param>
    public delegate void DeviceChangedEventHandler(AudioDevice device, bool noDevice);
}