using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.VisualScripting;
using DolbyIO.Comms;

namespace DolbyIO.Comms.Unity
{
    [UnitTitle("Remote Player Position")]
    [UnitCategory("DolbyIO")]
    public class RemotePlayerPositionUnit : PositionUnit
    {
        [DoNotSerialize]
        public ValueInput Participant;

        protected override void Definition()
        {
            base.Definition();

            InputTrigger = ControlInput(nameof(InputTrigger), SetPosition);
            OutputTrigger = ControlOutput(nameof(OutputTrigger));

            Participant = ValueInput<Participant>(nameof(Participant));
        }

        private ControlOutput SetPosition(Flow flow)
        {
            var position = flow.GetValue<Vector3>(Postition);
            var p = flow.GetValue<Participant>(Participant);

            Debug.Log(p.Id);

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

            return OutputTrigger;
        }
    }
}