using System.Threading.Tasks;

namespace DolbyIO.Comms.Services 
{
    /// <summary>
    /// The session service is responsible for connecting the SDK with the Dolby.io
    /// backend by opening and closing sessions.
    /// </summary>
    /// <example>
    /// <code>
    /// try
    /// {
    ///     UserInfo user = new UserInfo();
    ///     user.Name = "Some Name";
    ///     
    ///     user = await _sdk.Session.OpenAsync(user);
    /// }
    /// catch (DolbyIOException e)
    /// {
    ///     // Error handling
    /// }
    /// </code>
    /// </example>
    public sealed class SessionService
    {
        /// <summary>
        /// Gets the local participant object that belongs to the current session.
        /// </summary>
        /// <returns>The UserInfo class that contains information about the participant who opened the session.</returns>
        public UserInfo User { get; private set; }

        private volatile bool _isOpen = false;

        /// <summary>
        /// Gets if a session is currently open.
        /// </summary>
        public bool IsOpen { get => _isOpen; }

        /// <summary>
        /// Opens a new session for the specified participant.
        /// </summary>
        /// <param name="user">Information about the participant who opens the session.</param>
        /// <returns>The <see cref="Task{UserInfo}"/> that represents the asynchronous open operation.
        /// The <see cref="Task{UserInfo}.Result"/> property returns the <see cref="UserInfo"/> object
        /// representing the participant who opened the session.</returns>
        public async Task<UserInfo> OpenAsync(UserInfo user)
        {
            return await Task.Run(() => 
            {
                UserInfo res = new UserInfo();
                Native.CheckException(Native.Open(user, res));
                User = res;
                _isOpen = true;
                return res;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Closes the current session.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        public async Task CloseAsync()
        {
            await Task.Run(() =>
            {
                Native.CheckException(Native.Close());
                User = null;
                _isOpen = false;
            }).ConfigureAwait(false);
        }
    }
}