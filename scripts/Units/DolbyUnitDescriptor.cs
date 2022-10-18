using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

#if UNITY_EDITOR

namespace DolbyIO.Comms.Unity
{
    public class DolbyUnitDescriptor<T> : UnitDescriptor<T> where T : Unit
    {
        public DolbyUnitDescriptor(T node) : base(node) { }
        
        protected override void DefinedPort(IUnitPort port, UnitPortDescription description)
        {
            base.DefinedPort(port, description);

            string value = "";
            if (UnitHelpers.Tips.TryGetValue(port.key, out value))
            {
                description.summary = value;
            }
        }
    }
}

#endif