using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.VisualScripting;
using DolbyIO.Comms;

namespace DolbyIO.Comms.Unity
{
    [UnitTitle("Player Position")]
    [UnitCategory("DolbyIO")]
    public class PlayerPositionUnit : PositionUnit
    {
        [DoNotSerialize]
        public ValueInput Direction;

        protected override void Definition()
        {
            base.Definition();

            InputTrigger = ControlInput("inputTrigger", SetPosition);
            OutputTrigger = ControlOutput("outputTrigger");
            Direction = ValueInput<Vector3>(nameof(Direction), new Vector3(0, 1.0f, 0));
        }

        private ControlOutput SetPosition(Flow flow)
        {
            var direction = flow.GetValue<Vector3>(Direction);
            var position = flow.GetValue<Vector3>(Postition);
    
            _sdk.Conference.SetSpatialPositionAsync
            (
                _sdk.Session.User.Id,
                new System.Numerics.Vector3(position.x, position.y, position.z)
            )
            .ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    throw t.Exception;
                }

                return _sdk.Conference.SetSpatialDirectionAsync
                (
                    new System.Numerics.Vector3(direction.x, direction.y, direction.z)
                );
            })
            .ContinueWith(t =>
            {
                Debug.Log(t.Exception);
            }, TaskContinuationOptions.OnlyOnFaulted);

            return OutputTrigger;
        }
    }
}