using System;

namespace DolbyIO.Comms
{
    /// <summary>
    /// The possible screen share source types.
    /// </summary>
    public enum ScreenShareType
    {
        /// <summary>
        /// Entire monitor or display screen.
        /// </summary>
        Screen = 0,

        /// <summary>
        /// Single applicaton window.
        /// </summary>
        Window = 1
    }
}