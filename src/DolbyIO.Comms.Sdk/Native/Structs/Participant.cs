using System.Runtime.InteropServices;

namespace DolbyIO.Comms
{
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
        /// <returns>If true, the participant is sending audio.</returns>
        [MarshalAs(UnmanagedType.U1)]
        public readonly bool IsSendingAudio = false;

        /// <summary>
        /// A boolean value that indicates whether a remote participant is audible locally. This property is always
        /// false for the local participant.
        /// </summary>
        /// <returns>If true, the participant is audible.</returns>
        [MarshalAs(UnmanagedType.U1)]
        public readonly bool IsAudibleLocally = false;
    }
}