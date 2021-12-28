using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XAxisPinching : MonoBehaviour
{
    public static bool XScaling = false;
    private bool rightHandPinched, leftHandPinched;
    void Update()
    {
        if(rightHandPinched && leftHandPinched && !NetworkObjectsManager.xAxisScalingEnabledFlag)
        {
            XScaling = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "R2Tip")
        {
            if (FingertipBehavior.myPinchingStatus)
            {
                rightHandPinched = true;
            }
            else
            {
                rightHandPinched = false;
            }
        }

        if (other.gameObject.name == "L2TipNetwork")
        {
            if (FingertipBehavior.networkPinchingStatus)
            {
                leftHandPinched = true;
            }
            else
            {
                leftHandPinched = false;
            }
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "R2Tip")
        {
            if (FingertipBehavior.myPinchingStatus)
            {
                rightHandPinched = true;
            }
            else
            {
                rightHandPinched = false;
            }
        }

        if (other.gameObject.name == "L2TipNetwork")
        {
            if (FingertipBehavior.networkPinchingStatus)
            {
                leftHandPinched = true;
            }
            else
            {
                leftHandPinched = false;
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "R2Tip")
        {
            if (FingertipBehavior.myPinchingStatus)
            {
                rightHandPinched = false;
            }
        }

        if (other.gameObject.name == "L2TipNetwork")
        {
            if (FingertipBehavior.networkPinchingStatus)
            {
                leftHandPinched = false;
            }
        }
    }
}
