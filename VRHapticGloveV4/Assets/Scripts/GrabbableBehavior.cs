using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GrabbableBehavior : MonoBehaviour
{
    private GameObject leftHandGrabCollider;
    void Start()
    {
        leftHandGrabCollider = GameObject.Find(PhotonNetwork.AuthValues.UserId + "/LeftHand");
        //leftHandGrabCollider = GameObject.Find("HankOVRCameraRig/TrackingSpace/LeftHandAnchor/OVRCustomHandPrefab_L/OculusHand_L/b_l_wrist/LeftGrabCollider");
    }

    // Update is called once per frame
    void Update()
    {
        if (GrabBehavior.leftGrabbed)
        {
            if(this.gameObject == GrabBehavior.grabbedObject)
            {
                transform.SetParent(leftHandGrabCollider.transform);
            }
            
        }
        else
        {
            transform.parent = null;
        }
    }
}
