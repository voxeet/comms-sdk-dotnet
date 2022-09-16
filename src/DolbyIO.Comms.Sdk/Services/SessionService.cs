using System.Threading.Tasks;

namespace DolbyIO.Comms.Services 
{
    /// <summary>
    /// The session service is responsible for connecting the SDK with the Dolby.io
    /// backend by opening and closing sessions.
    ///
    /// To use the session service, follow these steps:
    /// 1. Open a session using the <see cref="OpenAsync(UserInfo)"/> method.
    /// 2. Join a conference using the <see cref="DolbyIO.Comms.Services.ConferenceService"/>.
    /// 3. Leave the conference and close the session using the <see cref="CloseAsync"/> method.
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
        /// <value><c>true</c> if a session is open; otherwise, <c>false</c>.</value>
        public bool IsOpen { get => _isOpen; }

        /// <summary>
        /// Opens a new session for the specified participant.
        /// </summary>
        /// <param name="user">Information about the participant who opens the session.</param>
        /// <returns>The <xref href="System.Threading.Tasks.Task`1"/> that represents the asynchronous open operation.
        /// The <xref href="System.Threading.Tasks.Task`1.Result"/> property returns the <see cref="UserInfo"/> object
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
        /// <returns>A <xref href="System.Threading.Tasks.Task"/> that represents the asynchronous operation.</returns>
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