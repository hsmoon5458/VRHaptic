using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingertipBehavior : MonoBehaviour
{
    public static bool thumbTouchedThumb, thumbTouchedIndex, indexTouchedThumb, indexTouchedIndex;
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        #region ForLeftHandSetup

        if (this.gameObject.name == "R1Tip")
        {
            if(other.gameObject.name == "L1Tip")
            {
                thumbTouchedThumb = true;
            }
            if(other.gameObject.name == "L2Tip")
            {
                thumbTouchedIndex = true;
            }
        }
        if (this.gameObject.name == "R2Tip")
        {
            if (other.gameObject.name == "L1Tip")
            {
                indexTouchedThumb = true;
            }
            if (other.gameObject.name == "L2Tip")
            {
                indexTouchedIndex = true;
            }
        }
        #endregion


    }
}
