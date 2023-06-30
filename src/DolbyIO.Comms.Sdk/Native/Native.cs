using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace DolbyIO.Comms
{
    /**
     * Native Interop for the Dolby.io Communications C++ SDK
     * @nodocument
     */
    #nullable enable
    internal class Native
    {
        internal const string LibName = "DolbyIO.Comms.Native";

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int Init(string accessToken, RefreshTokenCallBack cb);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int RegisterComponentVersion(string name, string version);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int SetLogLevel([MarshalAs(UnmanagedType.I4)] LogLevel level);
    
        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int Open(UserInfo user, [Out] UserInfo res);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        internal static extern int Close();

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int Create(ConferenceOptions options, [Out] Conference infos);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int Join(Conference conference, JoinOptions options, [Out] Conference res);
        
        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int Listen(Conference conference, ListenOptions options, [Out] Conference res);
       
        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int Demo(SpatialAudioStyle audioStyle, [Out] Conference conference);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        internal static extern int GetCurrentConference([Out] Conference conference);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        internal static extern int GetParticipants(ref int size, out IntPtr participants);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int Mute(bool muted);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int RemoteMute(bool muted, string participantId);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int StartAudio();

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int StopAudio();

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int StartRemoteAudio(string participantId);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int StopRemoteAudio(string participantId);
        
        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int SetSpatialEnvironment(float scaleX, float scaleY, float scaleZ,
                                                        float forwardX, float forwardY, float forwardZ,
                                                        float upX, float upY, float upZ,
                                                        float rightX, float rightY, float rightZ);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int SetSpatialDirection(float x, float y, float z);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int SetSpatialPosition(string userId, float x, float y, float z);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int SendMessage(string message);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int DeclineInvitation(string conferenceId);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        internal static extern bool AudioDeviceEquals(IntPtr id1, IntPtr id2);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int GetAudioDevices(ref int size, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] out AudioDevice[] devices);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        internal static extern int SetPreferredAudioInputDevice(AudioDevice device);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        internal static extern int SetPreferredAudioOutputDevice(AudioDevice device);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        internal static extern int GetCurrentAudioInputDevice(out AudioDevice device);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        internal static extern int GetCurrentAudioOutputDevice(out AudioDevice device);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        internal static extern bool DeleteDeviceIdentity(IntPtr handle);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int Leave();

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int Release();

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern string GetLastErrorMsg();

        // Video
        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int GetVideoDevices(ref int size, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] out VideoDevice[] devices);

        [DllImport (Native.LibName, CharSet = CharSet.Ansi)]
        internal static extern int GetCurrentVideoDevice(out VideoDevice device);

        [DllImport (Native.LibName, CharSet = CharSet.Ansi)]
        internal static extern VideoSinkHandle CreateVideoSink(VideoSink.VideoSinkOnFrame f);

        [DllImport (Native.LibName, CharSet = CharSet.Ansi)]
        internal static extern bool DeleteVideoSink(IntPtr handle);

        [DllImport (Native.LibName, CharSet = CharSet.Ansi)]
        internal static extern bool DeleteVideoFrameBuffer(IntPtr handle);

        [DllImport (Native.LibName, CharSet = CharSet.Ansi)]
        internal static extern int SetVideoSink(VideoTrack track, VideoSinkHandle handle);
        
        [DllImport (Native.LibName, CharSet = CharSet.Ansi)]
        internal static extern int SetNullVideoSink(VideoTrack track);

        [DllImport (Native.LibName, CharSet = CharSet.Ansi)]
        internal static extern int StartVideo(VideoDevice device, VideoFrameHandlerHandle handler);

        [DllImport (Native.LibName, CharSet = CharSet.Ansi)]
        internal static extern int StopVideo();

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        internal static extern int GetScreenShareSources(ref int size, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] out ScreenShareSource[] sources);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        internal static extern int StartScreenShare(ScreenShareSource source, VideoFrameHandlerHandle handler);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        internal static extern int StopScreenShare();

        [DllImport (Native.LibName, CharSet = CharSet.Ansi)]
        internal static extern VideoFrameHandlerHandle CreateVideoFrameHandler();

        [DllImport (Native.LibName, CharSet = CharSet.Ansi)]
        internal static extern bool DeleteVideoFrameHandler(IntPtr handle);

        [DllImport (Native.LibName, CharSet = CharSet.Ansi)]
        internal static extern int SetVideoFrameHandlerSink(VideoFrameHandlerHandle handle, VideoSinkHandle sink);

        // Events Handling
        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void AddOnConferenceStatusUpdatedHandler(int hash, ConferenceStatusUpdatedEventHandler handler);                                      

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern int RemoveOnConferenceStatusUpdatedHandler(int hash, ConferenceStatusUpdatedEventHandler handler);                                      

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void AddOnParticipantAddedHandler(int hash, ParticipantAddedEventHandler handler);   
        
        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void RemoveOnParticipantAddedHandler(int hash, ParticipantAddedEventHandler handler);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void AddOnParticipantUpdatedHandler(int hash, ParticipantUpdatedEventHandler handler);
        
        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void RemoveOnParticipantUpdatedHandler(int hash, ParticipantUpdatedEventHandler handler);  

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void AddOnConferenceMessageReceivedHandler(int hash, ConferenceMessageReceivedEventHandler handler);
    
        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void RemoveOnConferenceMessageReceivedHandler(int hash, ConferenceMessageReceivedEventHandler handler);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void AddOnConferenceInvitationReceivedHandler(int hash, ConferenceInvitationReceivedEventHandler handler);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void RemoveOnConferenceInvitationReceivedHandler(int hash, ConferenceInvitationReceivedEventHandler handler);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void AddOnAudioDeviceAddedHandler(int hash, AudioDeviceAddedEventHandler handler);   

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void RemoveOnAudioDeviceAddedHandler(int hash, AudioDeviceAddedEventHandler handler);   

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void AddOnAudioDeviceRemovedHandler(int hash, AudioDeviceRemovedEventHandler handler);   

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void RemoveOnAudioDeviceRemovedHandler(int hash, AudioDeviceRemovedEventHandler handler);   

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void AddOnAudioDeviceChangedHandler(int hash, AudioDeviceChangedEventHandler handler);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void RemoveOnAudioDeviceChangedHandler(int hash, AudioDeviceChangedEventHandler handler);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void AddOnVideoDeviceAddedHandler(int hash, VideoDeviceAddedEventHandler handler);   

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void RemoveOnVideoDeviceAddedHandler(int hash, VideoDeviceAddedEventHandler handler);   

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void AddOnVideoDeviceRemovedHandler(int hash, VideoDeviceRemovedEventHandler handler);   

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void RemoveOnVideoDeviceRemovedHandler(int hash, VideoDeviceRemovedEventHandler handler);   

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void AddOnVideoDeviceChangedHandler(int hash, VideoDeviceChangedEventHandler handler);
        
        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void RemoveOnVideoDeviceChangedHandler(int hash, VideoDeviceChangedEventHandler handler);
        
        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void AddOnActiveSpeakerChangeHandler(int hash, ActiveSpeakerChangeEventHandler handler);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void RemoveOnActiveSpeakerChangeHandler(int hash, ActiveSpeakerChangeEventHandler handler);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void AddOnSignalingChannelExceptionHandler(int hash, SignalingChannelErrorEventHandler handler);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void RemoveOnSignalingChannelExceptionHandler(int hash, SignalingChannelErrorEventHandler handler);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void AddOnInvalidTokenExceptionHandler(int hash, InvalidTokenErrorEventHandler handler);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void RemoveOnInvalidTokenExceptionHandler(int hash, InvalidTokenErrorEventHandler handler);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void AddOnDvcErrorExceptionHandler(int hash, DvcErrorEventHandler handler);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void RemoveOnDvcErrorExceptionHandler(int hash, DvcErrorEventHandler handler);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void AddOnPeerConnectionFailedExceptionHandler(int hash, PeerConnectionErrorEventHandler handler);

        [DllImport (LibName, CharSet = CharSet.Ansi)]
        internal static extern void RemoveOnPeerConnectionFailedExceptionHandler(int hash, PeerConnectionErrorEventHandler handler);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        internal static extern void AddOnConferenceVideoTrackAddedHandler(int hash, VideoTrackAddedEventHandler handler);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        internal static extern void RemoveOnConferenceVideoTrackAddedHandler(int hash, VideoTrackAddedEventHandler handler);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        internal static extern void AddOnConferenceVideoTrackRemovedHandler(int hash, VideoTrackRemovedEventHandler handler);

        [DllImport(LibName, CharSet = CharSet.Ansi)]
        internal static extern void RemoveOnConferenceVideoTrackRemovedHandler(int hash, VideoTrackRemovedEventHandler handler);

        internal static void CheckException(int err)
        {
            if (Result.Success != (Result)err)
            {
                throw new DolbyIOException(Native.GetLastErrorMsg());
            }
        }
    }
}
