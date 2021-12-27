using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingertipBehavior : MonoBehaviour
{
    [SerializeField]
    public static bool thumbTouchedThumb, thumbTouchedIndex, indexTouchedThumb, indexTouchedIndex, myPinchingStatus, networkPinchingStatus;
    void Update()
    {

    }

    //right hand setup only (participant side only)
    private void OnTriggerEnter(Collider other)
    {
        //for creating object at participant side
        if (this.gameObject.name == "R1Tip")
        {
            if(other.gameObject.name == "L1TipNetwork")
            {
                thumbTouchedThumb = true;
            }
            if(other.gameObject.name == "L2TipNetwork")
            {
                thumbTouchedIndex = true;
            }
        }
        if (this.gameObject.name == "R2Tip")
        {
            if (other.gameObject.name == "L1TipNetwork")
            {
                indexTouchedThumb = true;
            }
            if (other.gameObject.name == "L2TipNetwork")
            {
                indexTouchedIndex = true;
            }
        }

        //for pinching and scaling
        if(this.gameObject.name == "R2Tip")
        {
            if(other.gameObject.name == "R1Tip")
            {
                myPinchingStatus = true;
            }
        }
        //network player pinching
        if (this.gameObject.name == "L2TipNetwork")
        {
            if (other.gameObject.name == "L1TipNetwork")
            {
                networkPinchingStatus = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (this.gameObject.name == "R2Tip")
        {
            if (other.gameObject.name == "R1Tip")
            {
                myPinchingStatus = false;
            }
        }
        //network player pinching
        if (this.gameObject.name == "L2TipNetwork")
        {
            if (other.gameObject.name == "L1TipNetwork")
            {
                networkPinchingStatus = false;
            }
        }
    }
}
