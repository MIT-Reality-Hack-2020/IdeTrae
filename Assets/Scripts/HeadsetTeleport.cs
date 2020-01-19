using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HeadsetTeleport : MonoBehaviour
{
    enum HeadsetType
    {
        idea,
        exit
    }

    private HeadsetType headsetType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "head") {
            if (headsetType == HeadsetType.idea)
            {
                // TODO teleporat to idea room
            }
            else if (headsetType == HeadsetType.exit)
            {
                // TODO teleport to lobby
            }
            
        }
    }
}
