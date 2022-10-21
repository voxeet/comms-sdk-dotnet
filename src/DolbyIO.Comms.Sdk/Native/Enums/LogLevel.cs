namespace DolbyIO.Comms
{
    /// <summary>
    /// The LogLevel enum gathers logging levels to set. The logging levels allow classifying the
    /// entries in the log files in terms of urgency to help to control the amount of
    /// logged information.
    /// </summary>
    public enum LogLevel {
        /// <summary>
        /// Disables logging.
        /// </summary>
        Off = 0,    
        /// <summary>
        /// Generates logs only when an error occurs that does not allow
        /// the SDK to function properly.
        /// </summary>
        Error,   
        /// <summary>
        /// Generates logs when the SDK detects an
        /// unexpected problem but is still able to work as usual.
        /// </summary>
        Warning,
        /// <summary>
        /// Generates an informative number of logs.
        /// </summary>
        Info,
        /// <summary>
        /// Generates a high number of logs to provide
        /// diagnostic information in a detailed manner.
        /// </summary>
        Debug,
        /// <summary>
        /// Generates the highest number of logs,
        /// including HTTP requests.
        /// </summary>
        Verbose,
    }
}