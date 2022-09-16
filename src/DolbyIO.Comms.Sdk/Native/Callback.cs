using System.Runtime.InteropServices;

namespace DolbyIO.Comms
{
    /// <summary>
    /// The refresh token callback that is required by <see cref="DolbyIO.Comms.DolbyIOSDK.InitAsync(string, RefreshTokenCallBack)">Session.Init</see>.
    /// </summary>
    /// <returns>Returns a string containing the refreshed access token.</returns>
    [return: MarshalAs(UnmanagedType.LPStr)]
    public delegate string RefreshTokenCallBack();
}