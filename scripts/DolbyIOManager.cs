using UnityEngine;
using DolbyIO.Comms;

namespace DolbyIO.Comms.Unity
{
    [AddComponentMenu("DolbyIO/DolbyIO Manager")]
    public class DolbyIOManager : MonoBehaviour
    {
        private static DolbyIOSDK _sdk = new DolbyIOSDK();

        public static DolbyIOSDK Sdk { get => _sdk; }

        void OnApplicationQuit()
        {
            try
            {
                _sdk.Dispose();
            }
            catch (DolbyIOException e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}

