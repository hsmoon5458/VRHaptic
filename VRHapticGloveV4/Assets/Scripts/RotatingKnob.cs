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
    public static int axisSelected = 0; //1 is X, 2 is y, and Z is 3.
    public GameObject knobParent, fingertip; // to rotate the knob
    private GameObject touchedFingerObject;
    private float tempKnobAngle, tempFingerAngle;

    RaycastHit hit;
    private float raycastRange = 50f;
    public LayerMask axisTargetLayer;

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
            this.gameObject.GetComponent<Renderer>().material.color = Color.green;
            knobParent.transform.eulerAngles = new Vector3(0, touchedFingerObject.transform.eulerAngles.z, 90);
        }
        else
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.red;
            knobParent.transform.rotation = fingertip.transform.rotation;
        }

        if (Physics.Raycast(fingertip.transform.position, fingertip.transform.right, out hit, raycastRange, axisTargetLayer))
        {
            if(hit.transform.name == "XaxisRaycastTarget")
            {
                Debug.Log("X Axis Hit!");
                axisSelected = 1;
            }
            else if (hit.transform.name == "YaxisRaycastTarget")
            {
                Debug.Log("Y Axis Hit!");
                axisSelected = 2;
            }
            else if(hit.transform.name == "ZaxisRaycastTarget")
            {
                Debug.Log("Z Axis Hit!");
                axisSelected = 3;
            }
            else
            {
                axisSelected = 0;
            }

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
