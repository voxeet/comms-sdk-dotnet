using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace DolbyIO.Comms
{
    internal class Constants
    {
        public const int MaxPermissions = 12;
        public const int DeviceUidSize = 24;
    }

    /// <summary>
    /// The UserInfo class contains information about the participant who opened
    /// a session.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class UserInfo
    {
        /// <summary>
        /// The unique identifier of the participant who opened the session.
        /// </summary>
        /// <returns>The participant ID.</returns>
        [MarshalAs(UnmanagedType.LPStr)]
        public readonly string Id = "";

        /// <summary>
        /// The name of the participant.
        /// </summary>
        /// <returns>The participant name.</returns>
        [MarshalAs(UnmanagedType.LPStr)]
        public string Name = "";
        
        /// <summary>
        /// The external unique identifier that an application can add
        /// to the participant while opening a session. If a participant uses the
        /// same external ID in a few conferences, the participant ID also remains the same
        /// across all sessions.
        /// </summary>
        /// <returns>The external ID of the participant.</returns>
        [MarshalAs(UnmanagedType.LPStr)]
        public string ExternalId = "";

        /// <summary>
        /// The URL of the participant's avatar.
        /// </summary>
        /// <returns>The URL of the avatar.</returns>
        [MarshalAs(UnmanagedType.LPStr)]
        public string AvatarURL = "";
    }

    /// <summary>
    /// The ParticipantInfo class gathers information about a conference participant.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class ParticipantInfo
    {
        /// <summary>
        /// The external unique identifier that an application can add
        /// to the participant while opening a session. If a participant uses the
        /// same external ID in a few conferences, the participant ID also remains the same
        /// across all sessions.
        /// </summary>
        /// <returns>The external ID of the participant.</returns>
        [MarshalAs(UnmanagedType.LPStr)]
        public readonly string ExternalId = "";

        /// <summary>
        /// The participant name.
        /// </summary>
        /// <returns>The participant name.</returns>
        [MarshalAs(UnmanagedType.LPStr)]
        public readonly string Name = "";
        
        /// <summary>
        /// The URL of the participant's avatar.
        /// </summary>
        /// <returns>The URL of the avatar.</returns>
        [MarshalAs(UnmanagedType.LPStr)]
        public readonly string AvatarURL = "";
    }

    /// <summary>
    /// The Participant class contains information about a conference participant.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class Participant
    {
        /// <summary>
        /// Additional information about the participant.
        /// </summary>
        /// <returns>The ParticipantInfo class that contains additional information about the participant.</returns>
        public ParticipantInfo Info = new ParticipantInfo();

        /// <summary>
        /// The unique identifier of the participant.
        /// </summary>
        /// <returns>The participant ID.</returns>
        [MarshalAs(UnmanagedType.LPStr)]
        public readonly string Id = "";

        /// <summary>
        /// The type of the participant.
        /// </summary>
        /// <returns>The participant type.</returns>
        [MarshalAs(UnmanagedType.I4)]
        public readonly ParticipantType Type;

        /// <summary>
        /// The current status of the participant.
        /// </summary>
        /// <returns>The status of the participant.</returns>
        [MarshalAs(UnmanagedType.I4)]
        public readonly ParticipantStatus Status;

        /// <summary>
        /// A boolean value that indicates whether the participant is sending audio to a conference.
        /// </summary>
        /// <returns>If true, the participant is sending audio; otherwise, it is not.</returns>
        [MarshalAs(UnmanagedType.U1)]
        public readonly bool IsSendingAudio = false;

        /// <summary>
        /// A boolean value that indicates whether a remote participant is audible locally. This property is always
        /// false for the local participant.
        /// </summary>
        /// <returns>If true, the participant is audible; otherwise, it is not.</returns>
        [MarshalAs(UnmanagedType.U1)]
        public readonly bool IsAudibleLocally = false;
    }

    /// <summary>
    /// The ConferenceParams class gathers conference parameters.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class ConferenceParams
    {
        /// <summary>
        /// A boolean value that indicates whether the SDK should create a Dolby Voice
        /// conference where each participant receives one audio stream.
        /// </summary>
        /// <returns>If true, the SDK will create a Dolby Voice conference; otherwise, it will not.</returns>
        [MarshalAs(UnmanagedType.U1)]
        public bool DolbyVoice = true;

        /// <summary>
        /// A boolean that indicates whether the conference
        /// should include additional statistics.
        /// </summary>
        /// <returns>If true, the conference will include statistics; otherwise, it will not.</returns>
        [MarshalAs(UnmanagedType.U1)]
        public bool Stats = false;

        /// <summary>
        /// An enum that defines how the spatial location is communicated
        /// between the SDK and the Dolby.io server.
        /// </summary>
        /// <returns>The selected spatial audio style.</returns>
        [MarshalAs(UnmanagedType.I4)]
        public SpatialAudioStyle SpatialAudioStyle = 0;
    }

    /// <summary>
    /// The ConferenceOptions class provides additional
    /// information about a conference.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class ConferenceOptions
    {
        /// <summary>
        /// The conference parameters.
        /// </summary>
        /// <returns>The conference parameters.</returns>
        public ConferenceParams Params = new ConferenceParams();

        /// <summary>
        /// The conference alias.
        /// </summary>
        /// <returns>The alias.</returns>
        [MarshalAs(UnmanagedType.LPStr)]
        public string Alias;
    }

    /// <summary>
    /// The Conference class contains information about a conference. This structure provides
    /// conference details that are required to join a specific conference. The SDK
    /// returns Conference to describe the created or joined conference.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class Conference
    {
        /// <summary>
        /// The unique conference identifier.
        /// </summary>
        /// <returns>The conference identifier.</returns>
        [MarshalAs(UnmanagedType.LPStr)]
        public string Id;

        /// <summary>
        /// The conference alias. The alias must be a logical and unique string that consists
        /// of up to 250 characters, such as letters, digits, and symbols other than #.
        /// The alias is case insensitive, which means that using "foobar" and "FOObar"
        /// aliases refers to the same conference. The alias is optional in the case
        /// of using the conference ID.
        /// </summary>
        /// <returns>The conference alias.</returns>
        [MarshalAs(UnmanagedType.LPStr)]
        public string Alias;

        /// <summary>
        /// A boolean that indicates whether the conference represented by the object has been just created.
        /// </summary>
        /// <returns>If true, the conference is new; otherwise, it is not.</returns>
        [MarshalAs(UnmanagedType.U1)]
        public readonly bool IsNew;

        /// <summary>
        /// The current status of the conference.
        /// </summary>
        /// <returns>The conference status.</returns>
        [MarshalAs(UnmanagedType.I4)]
        public readonly ConferenceStatus Status;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.MaxPermissions)]
        private ConferenceAccessPermissions[] _permissions = new ConferenceAccessPermissions[Constants.MaxPermissions];

        private int _permissionsCount = 0;

        /// <summary>
        /// The spatial audio style used in the joined conference.
        /// </summary>
        /// <returns>The spatial audio style.</returns>
        [MarshalAs(UnmanagedType.I4)]
        public readonly SpatialAudioStyle SpatialAudioStyle = 0;

        /// <summary>
        /// Permissions that allow a conference participant to perform limited
        /// actions during a protected conference.
        /// </summary>
        /// <returns>The conference permissions.</returns>
        public List<ConferenceAccessPermissions> Permissions
        {
            get
            {
                return _permissions.Take(_permissionsCount).ToList();
            }

            set
            {
                if (value.Count > Constants.MaxPermissions) {
                    throw new DolbyIOException("Too many permissions");
                }
                Array.Copy(value.ToArray(), _permissions, value.Count);
                _permissionsCount = value.Count;
            }
        }
    }

    /// <summary>
    /// The ConnectionOptions class contains options that define how the application expects to join a
    /// conference in terms of media preference.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class ConnectionOptions
    {
        /// <summary>
        /// The conference access token that is required to join a protected conference if
        /// the conference is created using the [create](ref:conference#operation-create-conference)
        /// REST API. While calling the join or listen method, the application has to externally
        /// fetch the token and provide it to the SDK.
        /// </summary>
        /// <returns>The conference access token.</returns>
        [MarshalAs(UnmanagedType.LPStr)]
        public string ConferenceAccessToken = "";

        /// <summary>
        /// A boolean that enables spatial audio for the joining participant. This boolean must be set to
        /// true if spatial audio style is enabled. For more information, refer to our sample
        /// application code.
        /// </summary>
        /// <returns>If true, spatial audio is enabled; otherwise, it is not.</returns>
        [MarshalAs(UnmanagedType.U1)]
        public bool SpatialAudio = false;
    }

    /// <summary>
    /// The MediaConstraints class contains the local media constraints for an application joining a conference.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class MediaConstraints
    {
        /// <summary>
        /// A boolean that indicates whether the application should
        /// capture the local audio and send it to the conference.
        /// </summary>
        /// <returns>If true, the SDK will capture audio and sent it to the conference; otherwise, will is not.</returns>
        [MarshalAs(UnmanagedType.U1)]
        public bool Audio = false;

        /// <summary>
        /// A boolean that enables and disables audio processing on the server side for the 
        /// injected audio.
        /// </summary>
        /// <returns>If true, audio will be processed; otherwise, will will not.</returns>
        [MarshalAs(UnmanagedType.U1)]
        public bool AudioProcessing = true;

        /// <summary>
        /// A boolean that allows a participant to join a conference as a sender. This
        /// is strictly intended for Server Side SDK applications that
        /// want to inject media without recording. This flag is
        /// ignored by the Client SDK applications.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        internal bool SendOnly = false;
    }

    /// <summary>
    /// The JoinOptions class gathers options for joining a conference as a user
    /// who can send and receive media.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class JoinOptions
    {
        /// <summary>
        /// The options for connecting to the conference.
        /// </summary>
        /// <returns>The connection options.</returns>
        public ConnectionOptions Connection = new ConnectionOptions();
        
        /// <summary>
        /// The media constraints for the user.
        /// </summary>
        /// <returns>The media constraints.</returns>
        public MediaConstraints Constraints = new MediaConstraints();
    }
    
    /// <summary>
    /// The ListenOptions class gathers options for joining a conference as listener
    /// who can only receive media.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class ListenOptions 
    {
        /// <summary>
        /// The options for connecting to the conference.
        /// </summary>
        /// <returns>The connection options.</returns>
        public ConnectionOptions Connection = new ConnectionOptions();
    }

    /// <summary>
    /// The AudioDevice class contains a platform-agnostic description of an audio device.
    /// </summary>
    /// @ingroup device_management
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct AudioDevice
    {
        /// <summary>
        /// The unique identifier of the audio device.
        /// </summary>
        /// <returns>The identifier.</returns>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.DeviceUidSize, ArraySubType = UnmanagedType.U1)]
        public readonly byte[] Uid;

        /// <summary>
        /// The name of the audio device.
        /// </summary>
        /// <returns>The name of the device.</returns>
        [MarshalAs(UnmanagedType.LPStr)]
        public readonly string Name;

        /// <summary>
        /// Information whether the device is the input or output device.
        /// </summary>
        /// <returns>Information whether the device is the input or output device.</returns>
        [MarshalAs(UnmanagedType.I4)]
        public readonly DeviceDirection Direction;
    }
}