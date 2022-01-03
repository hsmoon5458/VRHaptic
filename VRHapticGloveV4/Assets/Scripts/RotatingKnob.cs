using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class RotatingKnob : MonoBehaviour
{
    private bool thumbTouched, indexTouched;
    public static bool knobEnabled = false;
    public static bool rotatedFlag; //when the knob is spined more than 90 degree
    public static int rotatingAxisSelected = 0; //1 is X, 2 is y, and Z is 3.
    public GameObject knobParent, fingertip; // to rotate the knob
    private GameObject touchedFingerObject;
    [SerializeField]
    private float tempKnobAngle, tempFingerAngle, rotatedAngle;

    public float x, y; //remove this after the test;
    RaycastHit hit;
    private float raycastRange = 50f;
    public LayerMask axisTargetLayer;

    void Update()
    {
        if (thumbTouched && indexTouched && !knobEnabled)
        {
            knobEnabled = true;
            tempKnobAngle = knobParent.transform.eulerAngles.z; //to get the initial rotation of the knob
            tempFingerAngle = touchedFingerObject.transform.eulerAngles.z;
        }

        if (knobEnabled)
        {
            rotatedAngle = tempFingerAngle - touchedFingerObject.transform.eulerAngles.z; //cacluated the difference between initial rotation and moved rotation
            
            if(rotatingAxisSelected == 1)
            {
                knobParent.transform.eulerAngles = new Vector3(-(tempKnobAngle + rotatedAngle), 180, 0);
                this.gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
            else if(rotatingAxisSelected == 2)
            {
                knobParent.transform.eulerAngles = new Vector3(0, -(tempKnobAngle + rotatedAngle), 90);
                this.gameObject.GetComponent<Renderer>().material.color = Color.green;
            }
            else if(rotatingAxisSelected == 3)
            {
                knobParent.transform.eulerAngles = new Vector3(tempKnobAngle + rotatedAngle, 90, 180);
                this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
            }
            
            if(rotatedAngle > 95)
            {
                rotatedFlag = true; //this is used and falsed in NetworkObjectMangager script
                rotatedAngle = 0;
                tempKnobAngle = 0;
                tempFingerAngle = 0;
            }

        }
        else
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.white;
            knobParent.transform.rotation = fingertip.transform.rotation;
        }

        if (Physics.Raycast(fingertip.transform.position, fingertip.transform.right, out hit, raycastRange, axisTargetLayer))
        {
            if(hit.transform.name == "XaxisRaycastTarget")
            {
                rotatingAxisSelected = 1;
            }
            else if (hit.transform.name == "YaxisRaycastTarget")
            {
                rotatingAxisSelected = 2;
            }
            else if(hit.transform.name == "ZaxisRaycastTarget")
            {
                rotatingAxisSelected = 3;
            }
            else
            {
                rotatingAxisSelected = 0;
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
