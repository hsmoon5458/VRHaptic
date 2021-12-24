using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingertipBehavior : MonoBehaviour
{
    public static bool thumbTouchedThumb, thumbTouchedIndex, indexTouchedThumb, indexTouchedIndex, pinchingStatus;
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        #region ForLeftHandSetup
        //for creating object
        if (this.gameObject.name == "L1Tip")
        {
            if(other.gameObject.name == "R1Tip")
            {
                thumbTouchedThumb = true;
            }
            if(other.gameObject.name == "R2Tip")
            {
                thumbTouchedIndex = true;
            }
        }
        if (this.gameObject.name == "L2Tip")
        {
            if (other.gameObject.name == "R1Tip")
            {
                indexTouchedThumb = true;
            }
            if (other.gameObject.name == "R2Tip")
            {
                indexTouchedIndex = true;
            }
        }

        //for pinching and scaling
        if(this.gameObject.name == "L2Tip")
        {
            if(other.gameObject.name == "L1Tip")
            {
                pinchingStatus = true;
            }
        }
        if (this.gameObject.name == "R2Tip")
        {
            if(other.gameObject.name == "R1Tip")
            {
                pinchingStatus = true;
            }
        }
        #endregion

    }
}
