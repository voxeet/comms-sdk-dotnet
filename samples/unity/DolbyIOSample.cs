using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using UnityEngine;
using DolbyIO.Comms;

public class DolbyIOSample : MonoBehaviour
{
    DolbyIOSDK sdk = DolbyIOSDK.Instance;

    void Awake () 
    {   

    }

    // Start is called before the first frame update
    async Task Start()
    {
        sdk.Conference.StatusUpdated = OnConferenceStatusUpdated;
        
        try
        {
            await sdk.InitAsync("voxeetio");
            await sdk.Session.OpenAsync("voxeetio");
            await sdk.Conference.JoinAsync("unity_toto", "test_unity");
        }
        catch (DolbyIOException e)
        {
            print(e);
        }
    }

    void OnConferenceStatusUpdated(int status, string conferenceId) 
    {
        print("OnConferenceStatusUpdated: " + status);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
