using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.VisualScripting;
using DolbyIO.Comms;

namespace DolbyIO.Comms.Unity
{
    [UnitTitle("Demo Conference")]
    [UnitCategory("DolbyIO")]
    public class DemoUnit : Unit
    {
        private DolbyIOSDK _sdk = DolbyIOManager.Sdk;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput outputTrigger;

        [DoNotSerialize]
        public ValueInput SpatialAudio;

        protected override void Definition()
        {
            inputTrigger = ControlInputCoroutine("inputTrigger", Demo);
            outputTrigger = ControlOutput("outputTrigger");

            SpatialAudio = ValueInput<bool>(nameof(SpatialAudio), true);
        }

        private IEnumerator Demo(Flow flow)
        {
            _sdk.Conference.DemoAsync(flow.GetValue<bool>(SpatialAudio)).Wait();

            yield return outputTrigger;
        }
    }
}