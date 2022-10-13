using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.VisualScripting;
using DolbyIO.Comms;
using DolbyIO.Comms.Unity;

namespace DolbyIO.Comms.Unity
{
    [UnitTitle("Initialize")]
    [UnitCategory("DolbyIO")]
    public class DolbyIOUnit : Unit
    {
        private DolbyIOSDK _sdk = DolbyIOManager.Sdk;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput outputTrigger;

        [DoNotSerialize]
        public ValueInput AccessToken;

        [DoNotSerialize]
        public ValueInput ParticipantName;

        protected override void Definition()
        {
            inputTrigger = ControlInputCoroutine("inputTrigger", InitAndOpen);
            outputTrigger = ControlOutput("outputTrigger");

            AccessToken = ValueInput<string>("AccessToken", "My Access Token");
            ParticipantName = ValueInput<string>(nameof(ParticipantName), "Name");
        }

        private IEnumerator InitAndOpen(Flow flow)
        {
            _sdk.InitAsync(flow.GetValue<string>(AccessToken), () =>
            {
                return flow.GetValue<string>(AccessToken);
            }).Wait();

            _sdk.Conference.ParticipantAdded = new ParticipantAddedEventHandler(Participant =>
            {
                DolbyIOManager.QueueOnMainThread(() => EventBus.Trigger(EventNames.ParticipantAddedEvent, Participant));
            });

            _sdk.Conference.ParticipantUpdated = new ParticipantUpdatedEventHandler(Participant =>
            {
                DolbyIOManager.QueueOnMainThread(() => EventBus.Trigger(EventNames.ParticipantUpdatedEvent, Participant));
            });

            #nullable enable
            _sdk.Conference.ActiveSpeakerChange = new ActiveSpeakerChangeEventHandler
            (
                (string conferenceId, int count, string[]? ids) =>
                {
                    List<string> res = new List<string>();

                    if (count > 0)
                    {
                        res.AddRange(ids!);
                    }

                    DolbyIOManager.QueueOnMainThread(() => EventBus.Trigger(EventNames.ActiveSpeakerChangeEvent, res));
                }
            );

            var userInfo = new UserInfo();
            userInfo.Name = flow.GetValue<string>(ParticipantName);

            _sdk.Session.OpenAsync(userInfo).Wait();

            yield return outputTrigger;
        }
    }
}