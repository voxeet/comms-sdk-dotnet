using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace DolbyIO.Comms.Services 
{
    /// <summary>
    /// The Session service is responsible for connecting SDK with the Dolby.io
    /// backend by opening and closing sessions. Opening a session is mandatory
    /// before joining conferences.
    ///
    /// To use the Session service, follow these steps:
    /// 1. Open a session using the <see cref="DolbyIO.Comms.Services.SessionService.OpenAsync(UserInfo)"/> method.
    /// 2. Join a conference. See <see cref="DolbyIO.Comms.Services.ConferenceService"/>
    /// 3. Leave the conference and close the session using the 
    /// <see cref="DolbyIO.Comms.Services.SessionService.CloseAsync"/> method.
    /// </summary>
    /// <example>
    /// <code>
    /// try
    /// {
    ///     UserInfo info;
    ///     info.Name = "Some Name";
    ///     
    ///     info = await _sdk.Session.OpenAsync(info);
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
        /// Gets the user information for the currently opened session.
        /// </summary>
        public UserInfo User { get; private set; }

        /// <summary>
        /// Opens a new session for the specified participant.
        /// </summary>
        /// <param name="user">Information about the participant who tries to open a session.</param>
        /// <returns>The UserInfo about the opened session.</returns>
        public async Task<UserInfo> OpenAsync(UserInfo user)
        {
            return await Task.Run(() => 
            {
                UserInfo res = new UserInfo();
                Native.CheckException(Native.Open(user, res));
                User = res;
                return res;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Closes the current session.
        /// </summary>
        public async Task CloseAsync()
        {
            await Task.Run(() =>
            {
                Native.CheckException(Native.Close());
            }).ConfigureAwait(false);
        }
    }
}