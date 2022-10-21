using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.VisualScripting;
using DolbyIO.Comms;

namespace DolbyIO.Comms.Unity
{
    [UnitTitle("Get Audio Devices")]
    [UnitCategory("DolbyIO")]
    public class AudioDeviceUnit : Unit
    {
        protected DolbyIOSDK _sdk = DolbyIOManager.Sdk;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput InputTrigger;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput OutputTrigger;

        [DoNotSerialize]
        public ValueInput Direction;

        [DoNotSerialize]
        public ValueOutput AudioDevices;
        
        protected override void Definition()
        {
            InputTrigger = ControlInput(nameof(InputTrigger), GetDevices);
            OutputTrigger = ControlOutput(nameof(OutputTrigger));

            Direction = ValueInput<DolbyIO.Comms.DeviceDirection>(nameof(Direction), DolbyIO.Comms.DeviceDirection.Input);
            AudioDevices = ValueOutput<List<DolbyIO.Comms.AudioDevice>>(nameof(AudioDevices));
        }

        private ControlOutput GetDevices(Flow flow)
        {
            var direction = flow.GetValue<DolbyIO.Comms.DeviceDirection>(Direction);
            var devices = _sdk.MediaDevice.GetAudioDevicesAsync().Result;

            flow.SetValue(AudioDevices, devices.FindAll(d => d.Direction == direction));

            return OutputTrigger;
        }
    }
}