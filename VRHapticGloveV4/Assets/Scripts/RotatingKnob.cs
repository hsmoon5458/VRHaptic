using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class RotatingKnob : MonoBehaviour
{
    private bool thumbTouched, indexTouched;
    public static bool knobEnabled = false;
    public static float knobAngle;
    public GameObject knobParent, fingertip; // to rotate the knob
    private GameObject touchedFingerObject;
    public static GameObject knob; //remove this after the test
    private float tempKnobAngle, tempFingerAngle;

    public float xAngle = 0, yAngle = 0, zAngle = 0;

    private void Start()
    {
        knob = this.gameObject;
    }
    void Update()
    {
        if (thumbTouched && indexTouched && !knobEnabled)
        {
            knobEnabled = true;
            knobAngle = this.transform.eulerAngles.z;
            tempKnobAngle = this.transform.eulerAngles.z;
            tempFingerAngle = touchedFingerObject.transform.eulerAngles.x;

        }

        if (knobEnabled)
        {
            //this nigga has a problem
            this.gameObject.GetComponent<Renderer>().material.color = Color.green;
            knobParent.transform.eulerAngles = new Vector3(xAngle, touchedFingerObject.transform.eulerAngles.z, 90);
        }
        else
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.red;
            knobParent.transform.rotation = fingertip.transform.rotation;
        }

    }
    //rotating knob should be place on participant right hand
    //so it should be rotated by researcher left hand
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "L1TipNetwork")
        {
            thumbTouched = true;
        }

        if (other.gameObject.name == "L2TipNetwork")
        {
            indexTouched = true;
            touchedFingerObject = other.gameObject;
            Debug.Log(touchedFingerObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "L1TipNetwork")
        {
            thumbTouched = false;
            knobEnabled = false;
        }

        if (other.gameObject.name == "L2TipNetwork")
        {
            indexTouched = false;
            touchedFingerObject = null;
            knobEnabled = false;
        }
    }
}
