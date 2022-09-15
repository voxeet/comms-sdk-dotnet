using UnityEngine;
using DolbyIO.Comms;

namespace DolbyIO.Comms.Unity
{
    [AddComponentMenu("DolbyIO/DolbyIO Manager")]
    public class DolbyIOManager : MonoBehaviour
    {
        private static DolbyIOSDK _sdk = new DolbyIOSDK();

        public static DolbyIOSDK Sdk { get => _sdk; }
     
        [Tooltip("Indicates if the DolbyIOManager should automatically leave the current conference on application quit.")]
        public bool AutoLeaveConference = true;
        
        [Tooltip("Indicates if the DolbyIOManager should automatically close the session on application quit.")]
        public bool AutoCloseSession = true;

        async void OnApplicationQuit()
        {
            try
            {
                if (_sdk.IsInitialized)
                {
                    if (AutoLeaveConference && _sdk.Conference.IsInConference)
                    {
                        await _sdk.Conference.LeaveAsync();
                    }

                    if (AutoCloseSession && _sdk.Session.IsOpen)
                    {
                        await _sdk.Session.CloseAsync();
                    }

                    _sdk.Dispose();
                }
            }
            catch (DolbyIOException e)
            {
                Debug.LogError(e);
            }
        }
    }
}

