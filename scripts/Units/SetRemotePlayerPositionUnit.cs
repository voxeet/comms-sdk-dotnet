using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.VisualScripting;
using DolbyIO.Comms;

namespace DolbyIO.Comms.Unity
{
    [UnitTitle("Set Remote Player Position")]
    [UnitCategory("DolbyIO")]
    public class SetRemotePlayerPositionUnit : Unit, IDolbyUnit
    {
        protected DolbyIOSDK _sdk = DolbyIOManager.Sdk;
        
        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput InputTrigger;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput OutputTrigger;

        [DoNotSerialize]
        public ValueInput Postition;

        [DoNotSerialize]
        public ValueInput Participant;

        protected override void Definition()
        {
            InputTrigger = ControlInput(nameof(InputTrigger), SetPosition);
            OutputTrigger = ControlOutput(nameof(OutputTrigger));
            
            Postition = ValueInput<Vector3>(nameof(Postition), new Vector3(0, 1.0f, 0));
            Participant = ValueInput<Participant>(nameof(Participant));
        }

        private ControlOutput SetPosition(Flow flow)
        {
            var position = flow.GetValue<Vector3>(Postition);
            var p = flow.GetValue<Participant>(Participant);

            if (_sdk.IsInitialized && _sdk.Session.IsOpen && _sdk.Conference.IsInConference)
            {
                if (p.Id != _sdk.Session.User.Id)
                {
                    _sdk.Conference.SetSpatialPositionAsync
                    (
                        p.Id,
                        new System.Numerics.Vector3(position.x, position.y, position.z)
                    )
                    .ContinueWith(t =>
                    {
                        Debug.Log(t.Exception);
                    }, TaskContinuationOptions.OnlyOnFaulted);
                }
            }

            return OutputTrigger;
        }
    }
}