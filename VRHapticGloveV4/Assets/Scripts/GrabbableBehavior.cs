using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableBehavior : MonoBehaviour
{
    private GameObject leftHandGrabCollider;
    void Start()
    {
        leftHandGrabCollider = GameObject.Find("HankOVRCameraRig/TrackingSpace/LeftHandAnchor/OVRCustomHandPrefab_L/OculusHand_L/b_l_wrist/LeftGrabCollider");
        Debug.Log(leftHandGrabCollider);
    }

    // Update is called once per frame
    void Update()
    {
        if (GrabBehavior.leftGrabbed)
        {
            transform.SetParent(leftHandGrabCollider.transform);
        }
        else
        {
            transform.parent = null;
        }
    }
}
