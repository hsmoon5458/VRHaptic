using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingKnob : MonoBehaviour
{
    private bool thumbTouched, indexTouched;
    public static bool knobEnabled;
    void Update()
    {
        if(thumbTouched && indexTouched)
        {
            knobEnabled = true;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "R1Tip")
        {
            thumbTouched = true;
        }

        if (other.gameObject.name == "R2Tip")
        {
            indexTouched = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "R1Tip")
        {
            thumbTouched = false;
        }

        if (other.gameObject.name == "R2Tip")
        {
            indexTouched = false;
        }
    }
}
