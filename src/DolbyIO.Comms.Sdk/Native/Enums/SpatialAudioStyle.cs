namespace DolbyIO.Comms
{
    /// <summary>
    /// The SpatialAudioStyle enum gathers the possible spatial audio styles of the conference. Setting
    /// SpatialAudioStyle is possible only if the DolbyVoice flag is set to true.
    /// </summary>
    public enum SpatialAudioStyle
    {
        /// <summary>
        /// Disables spatial audio in a conference.
        /// </summary>
        None = 0,
        /// <summary>
        /// Sets the spatial location that is based on the spatial scene, local participant's position, and remote participants' positions. This allows a client to control the position using the local, self-contained logic. However, the client has to communicate a large set of requests constantly to the server, which increases network traffic, log subsystem pressure, and complexity of the client-side application. This option is selected by default. We recommend this mode for A/V congruence scenarios in video conferencing and similar applications.
        /// </summary>
        Individual,
        /// <summary>
        /// Sets the spatial location that is based on the spatial scene and the local participant's position, while the relative positions among participants are calculated by the Dolby.io server. This way, the spatial scene is shared by all participants, so that each client can set a position and participate in the shared scene. This approach simplifies communication between the client and the server and decreases network traffic. We recommend this mode for virtual space scenarios, such as 2D or 3D games, trade shows, virtual museums, water cooler scenarios, etc.
        ///
        /// **Note**: The shared style currently does not support recording conferences.
        /// </summary>
        Shared
    }
}