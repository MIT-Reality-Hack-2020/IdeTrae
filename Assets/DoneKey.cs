using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRKeys
{
    public class DoneKey : Key
    {
        public override void HandleTriggerEnter(Collider other)
        {
            keyboard.Submit();
        }
    }
}
