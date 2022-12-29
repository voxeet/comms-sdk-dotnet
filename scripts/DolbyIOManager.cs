using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using UnityEngine;
using Unity.VisualScripting;
using DolbyIO.Comms;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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

        /// <summary>
        /// For convenience during early development and prototyping, a GetToken (linked) method is provided for you to 
        /// acquire a client access token directly from the application. However, please note Dolby does not recommend 
        /// using this mechanism in the production software for (security best practices)[https://docs.dolby.io/communications-apis/docs/guides-client-authentication] reasons.
        /// </summary>
        /// <param name="key">The customer key.</param>
        /// <param name="secret">The customer secret.</param>
        /// <returns>An asynchronous task containing the token.</returns>
        public static async Task<string> GetToken(string key, string secret)
        {
            string result = "";

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, " https://session.voxeet.com/v1/oauth2/token");
                var auth = $"{Uri.EscapeUriString(key)}:{Uri.EscapeUriString(secret)}";
                
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", $"{Convert.ToBase64String(Encoding.UTF8.GetBytes(auth))}");
                request.Content = new FormUrlEncodedContent(new Dictionary<string, string> {{"grant_type", "client_credentials"}});

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);

                result = jsonString;
                if (!json.TryGetValue("access_token", out result))
                {
                    throw new Exception("Unable to access json token");
                }
            }

            return result;
        }

        public static void QueueOnMainThread(Action a)
        {
            lock(_backlog)
            {
                _backlog.Add(a);
            }
        }

        public static void ClearQueue()
        {
            lock(_backlog)
            {
                _backlog.Clear();
            }
        }

        private void FixedUpdate()
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
                }
            }
            catch (DolbyIOException e)
            {
                Debug.LogError(e);
            }
            finally
            {
                DolbyIOManager.ClearQueue();
                _sdk.Dispose();
            }
        }
    }
}

