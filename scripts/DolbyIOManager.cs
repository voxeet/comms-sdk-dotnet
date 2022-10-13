using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.VisualScripting;
using DolbyIO.Comms;

namespace DolbyIO.Comms.Unity
{
    [AddComponentMenu("DolbyIO/DolbyIO Manager")]
    [Inspectable]
    public class DolbyIOManager : MonoBehaviour
    {
        private static DolbyIOSDK _sdk = new DolbyIOSDK();
        private static List<Action> _backlog = new List<Action>();

        [Inspectable]
        public static DolbyIOSDK Sdk { get => _sdk; }
        
        [Tooltip("Indicates if the DolbyIOManager should automatically leave the current conference on application quit.")]
        public bool AutoLeaveConference = true;
        
        [Tooltip("Indicates if the DolbyIOManager should automatically close the session on application quit.")]
        public bool AutoCloseSession = true;

        public static void QueueOnMainThread(Action a)
        {
            lock(_backlog)
            {
                _backlog.Add(a);
            }
        }

        private void Update()
        {
            lock(_backlog)
            {
                foreach(var action in _backlog)
                {
                    action();
                }
                _backlog.Clear();
            }
        }

        async Task OnApplicationQuit()
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

