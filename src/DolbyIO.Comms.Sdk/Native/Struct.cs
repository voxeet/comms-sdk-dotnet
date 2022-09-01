using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace DolbyIO.Comms
{  
    internal class Constants {
        public const int MaxPermissions = 12;
        public const int DeviceUidSize = 24;
    }

    /// <summary>
    /// The class that represents the participant who tries to open
    /// a session.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class UserInfo
    {
        /// <summary>
        /// The unique ID for the participant opening the session, this field 
        /// is provided by the backend when session is opened.
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public readonly string Id = "";

        /// <summary>
        /// The name of the participant.
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string Name = "";
        
        /// <summary>
        /// The external unique identifier that the customer's application can add 
        /// to the participant while opening a session. If a participant uses the 
        /// same external ID in conferences, the participant's ID also remains the same
        /// across all sessions.
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string ExternalId = "";

        /// <summary>
        /// The URL of the participant's avatar.
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string AvatarURL = "";
    }

    /// <summary>
    /// Informations about a conference participant.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class ParticipantInfo 
    {
        /// <summary>
        /// The external unique identifier that the customer's application can add 
        /// to the participant while opening a session. If a participant uses the 
        /// same external ID in conferences, the participant's ID also remains the same
        /// across all sessions.
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public readonly string ExternalId = "";

        /// <summary>
        /// The participant's name.
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public readonly string Name = "";
        
        /// <summary>
        /// The URL of the participant's avatar.
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public readonly string AvatarURL = "";
    }

    /// <summary>
    /// Contains the current status of a conference participant and
    /// information whether the participant's audio is enabled.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class Participant
    {
        /// <summary>
        /// Additional information about the participant.
        /// </summary>
        public ParticipantInfo Info = new ParticipantInfo();

        /// <summary>
        /// The unique identifier of the participant.
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public readonly string Id = "";

        /// <summary>
        /// The type of the participant.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public readonly ParticipantType Type;

        /// <summary>
        /// The current status of the participant.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public readonly ParticipantStatus Status;

        /// <summary>
        /// A boolean that informs whether the participant is sending audio into conference.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public readonly bool IsSendingAudio = false;

        /// <summary>
        /// A boolean that indicates whether a remote participant is audible locally. This will always 
        /// be false for the local participant.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public readonly bool IsAudibleLocally = false;
    }

    /// <summary>
    /// The conference parameters.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class ConferenceParams
    {
        /// <summary>
        /// A boolean that indicates whether the SDK should create a Dolby Voice 
        /// conference where each participant receives one audio stream.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool DolbyVoice = true;

        /// <summary>
        /// A boolean that indicates whether the conference
        /// should include additional statistics.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool Stats = false;

        /// <summary>
        /// An enum that defines how the spatial location is communicated
        /// between the SDK and the Dolby.io server.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public SpatialAudioStyle SpatialAudioStyle = 0;
    }

    /// <summary>
    /// The conference options structure that provides additional
    /// information about a conference.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class ConferenceOptions
    {
        /// <summary>
        /// The conference parameters.
        /// </summary>
        public ConferenceParams Params = new ConferenceParams();

        [MarshalAs(UnmanagedType.LPStr)]
        public string Alias;
    }

    /// <summary>
    /// Contains the conference information. This structure provides
    /// conference details that are required to join a specific conference. The SDK
    /// returns ConferenceInfos to describe the created or joined conference.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class ConferenceInfos
    {
        /// <summary>
        /// The unique conference identifier.
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string Id;

        /// <summary>
        /// The conference alias. The alias is optional in the case of using the conference ID.
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string Alias;

        /// <summary>
        /// Indicates whether the conference represented by the object has been just created.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public readonly bool IsNew;

        /// <summary>
        /// The current status of the conference.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public readonly ConferenceStatus Status;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.MaxPermissions)]
        private ConferenceAccessPermissions[] _permissions = new ConferenceAccessPermissions[Constants.MaxPermissions];

        private int _permissionsCount = 0;

        /// <summary>
        /// The spatial audio style used in the joined conference.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public readonly SpatialAudioStyle SpatialAudioStyle = 0;

        /// <summary>
        /// Permissions that allow a conference participant to perform limited 
        /// actions during a protected conference.
        /// </summary>
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
    /// The options that define how the application expects to join a
    /// conference in terms of media preference.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class ConnectionOptions
    {
        /// <summary>
        /// The conference access token that is required to join a protected conference if
        /// the conference is created using the [create](ref:conference#operation-create-conference)
        /// REST API. The application needs to externally fetch and provide the token to
        /// the SDK when it calls the join or listen method.
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string ConferenceAccessToken = "";

        /// <summary>
        /// Enables spatial audio for the joining participant. This boolean should be set to
        /// true if the spatial_style is not disabled. For more information, refer to our sample
        /// application code.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool SpatialAudio = false;
    }

    /// <summary>
    /// The local media constraints for application joining a conference.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class MediaConstraints
    {
        /// <summary>
        /// A boolean that indicates whether the application should
        /// capture local audio and send it to a conference.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool Audio = false;

        /// <summary>
        /// Enables and disables audio processing on the server for the 
        /// injected audio.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool AudioProcessing = true;

        /// <summary>
        /// Allows the user to join a conference as a sender only. This
        /// is strictly intended for Server Side SDK applications that
        /// want to inject media without recording. This flag is
        /// ignored by the Client SDK applications.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        internal bool SendOnly = false;
    }

    /// <summary>
    /// The options for joining a conference as an active user.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class JoinOptions
    {
        /// <summary>
        /// The options for connecting to the conference.
        /// </summary>
        public ConnectionOptions Connection = new ConnectionOptions();
        
        /// <summary>
        /// The media constraints for the active user.
        /// </summary>
        public MediaConstraints Constraints = new MediaConstraints();
    }
    
    /// <summary>
    /// The options for listening to a conference as listener.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class ListenOptions 
    {
        /// <summary>
        /// The options for connecting to the conference.
        /// </summary>
        public ConnectionOptions Connection = new ConnectionOptions();
    }

    /// <summary>
    /// The platform agnostic description of an audio device.
    /// </summary>
    /// @ingroup device_management
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct AudioDevice
    {
        /// <summary>
        /// The unique identifier of the audio device.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.DeviceUidSize, ArraySubType = UnmanagedType.U1)]
        public readonly byte[] Uid;

        /// <summary>
        /// The name of the audio device.
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public readonly string Name;

        [MarshalAs(UnmanagedType.I4)]
        public readonly DeviceDirection Direction;
    }
}