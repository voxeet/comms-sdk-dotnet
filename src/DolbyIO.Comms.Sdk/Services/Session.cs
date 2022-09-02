using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace DolbyIO.Comms.Services 
{
    /// <summary>
    /// The Session Service is responsible for connecting SDK with the Dolby.io
    /// backend by opening and closing sessions. Opening a session is mandatory
    /// before joining conferences.
    ///
    /// To use the Session Service, follow these steps:
    /// 1. Open a session using the <see cref="DolbyIO.Comms.Services.Session.Open(UserInfo)"/> method.
    /// 2. Join a conference. See <see cref="DolbyIO.Comms.Services.Conference"/>
    /// 3. Leave the conference and close the session using the 
    /// <see cref="DolbyIO.Comms.Services.Session.Close"/> method.
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

        /// <summary>
        /// Opens a new session for the specified participant.
        /// </summary>
        /// <param name="user">Information about the participant who tries to open a session.</param>
        /// <returns>The UserInfo about the opened session.</returns>
        public async Task<UserInfo> Open(UserInfo user)
        {
            return await Task.Run(() => 
            {
                UserInfo res = new UserInfo();
                Native.CheckException(Native.Open(user, res));
                _user = res;
                return res;
            });
        }

        /// <summary>
        /// Closes the current session.
        /// </summary>
        /// <returns></returns>
        public async Task Close()
        {
            await Task.Run(() =>
            {
                Native.CheckException(Native.Close());
            });
        }

        /// <summary>
        /// Gets the user informations for the currently opened session.
        /// </summary>
        public UserInfo User
        {
            get
            {
                return _user;
            }
        }
    }
}