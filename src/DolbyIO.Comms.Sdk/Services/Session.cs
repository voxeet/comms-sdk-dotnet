using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace DolbyIO.Comms.Services 
{
    /// <summary>
    /// The Session service is responsible for connecting the SDK with the Dolby.io
    /// backend by opening and closing sessions. Opening a session is mandatory
    /// before joining conferences.
    ///
    /// To use the Session Service, follow these steps:
    /// 1. Open a session using the <see cref="DolbyIO.Comms.Services.Session.Open(UserInfo)">open</see> method.
    /// 2. Join a conference using the <see cref="DolbyIO.Comms.Services.Conference">Conference service</see>.
    /// 3. Leave the conference and close the session using the 
    /// <see cref="DolbyIO.Comms.Services.Session.Close">close</see> method.
    /// </summary>
    /// <example>
    /// <code>
    /// try
    /// {
    ///     UserInfo info;
    ///     info.Name = "Some Name";
    ///     
    ///     info = await _sdk.Session.Open(info);
    /// }
    /// catch (DolbyIOException e)
    /// {
    ///     // Error handling
    /// }
    /// </code>
    /// </example>
    public class Session
    {
        private UserInfo _user;
        private volatile bool _isOpen = false;

        /// <summary>
        /// Opens a new session for the specified participant.
        /// </summary>
        /// <param name="user">Information about the participant who opens the session.</param>
        /// <returns>The task object representing the asynchronous operation that returns UserInfo.</returns>
        public async Task<UserInfo> Open(UserInfo user)
        {
            return await Task.Run(() => 
            {
                UserInfo res = new UserInfo();
                Native.CheckException(Native.Open(user, res));
                _user = res;
                _isOpen = true;
                return res;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Closes the current session.
        /// </summary>
        /// <returns>The returned asynchronous operation.</returns>
        public async Task Close()
        {
            await Task.Run(() =>
            {
                Native.CheckException(Native.Close());
                _isOpen = false;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the local participant object that belongs to the current session.
        /// </summary>
        /// <returns>The UserInfo class that contains information about the participant who opened the session.</returns>
        public UserInfo User { get => _user; }

        /// <summary>
        /// Indicates if the Session is open.
        /// </summary>
        public bool IsOpen { get => _isOpen; }
    }
}