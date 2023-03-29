using System;
using System.Runtime.InteropServices;

namespace DolbyIO.Comms {
    /// <summary>
    /// The video track description structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct VideoTrack
    {
        /// <summary>
        /// The id the participant to whom belongs the track.
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public readonly string ParticipantId;

        /// <summary>
        /// The ID of the stream to which belongs the track.
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public readonly string StreamId;

        /// <summary>
        /// The ID of the video track.
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public readonly string TrackId;

        /// <summary>
        /// The ID of the track in the SDP.
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public readonly string SdpTrackId;

        /// <summary>
        /// A boolean indicating whether the track is a screenshare.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public readonly bool IsScreenshare;

        /// <summary>
        /// A boolean indicating whether the track is from a remote participant.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public readonly bool IsRemote;
    }
}