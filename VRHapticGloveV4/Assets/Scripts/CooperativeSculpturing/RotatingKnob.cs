using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class RotatingKnob : MonoBehaviour
{
    private bool thumbTouched = false, indexTouched = false;
    public static bool knobEnabled;
    public static bool rotatedFlagX, rotatedFlagY, rotatedFlagZ; //when the knob is spined more than 90 degree
    public static int rotatingAxisSelected = 0; //1 is X, 2 is y, and Z is 3.
    public GameObject knobParent, fingertip; // to rotate the knob
    private GameObject touchedFingerObject;
    [SerializeField]
    private float tempKnobAngle, tempFingerAngleX, tempFingerAngleY, tempFingerAngleZ, rotatedAngleX, rotatedAngleY, rotatedAngleZ;
    //test code
    public static float tx, ty, tz;
    //test code end
    RaycastHit hit;
    private float raycastRange = 50f;
    public LayerMask axisTargetLayer;
    void Start()
    {
        thumbTouched = false;
        indexTouched = false;
    }
    void Update()
    {
        if (thumbTouched && indexTouched && !knobEnabled)
        {
            knobEnabled = true;
            tempKnobAngle = knobParent.transform.eulerAngles.z; //to get the initial rotation of the knob
            tempFingerAngleX = touchedFingerObject.transform.eulerAngles.x;
            tempFingerAngleY = touchedFingerObject.transform.eulerAngles.y;
            tempFingerAngleZ = touchedFingerObject.transform.eulerAngles.z;
        }

        if (knobEnabled)
        {
            //test code
            tx = touchedFingerObject.transform.eulerAngles.x;
            ty = touchedFingerObject.transform.eulerAngles.y;
            tz = touchedFingerObject.transform.eulerAngles.z;
            //test code end

            rotatedAngleX = Mathf.Abs(tempFingerAngleX - touchedFingerObject.transform.eulerAngles.x); //cacluated the difference between initial rotation and moved rotation
            rotatedAngleY = Mathf.Abs(tempFingerAngleY - touchedFingerObject.transform.eulerAngles.y);
            rotatedAngleZ = Mathf.Abs(tempFingerAngleZ - touchedFingerObject.transform.eulerAngles.z);

            if (rotatingAxisSelected == 1)
            {
                knobParent.transform.eulerAngles = new Vector3((tempKnobAngle + rotatedAngleX), 180, 0);
                this.gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
            else if(rotatingAxisSelected == 2)
            {
                knobParent.transform.eulerAngles = new Vector3(0, (tempKnobAngle + rotatedAngleY), 90);
                this.gameObject.GetComponent<Renderer>().material.color = Color.green;
            }
            else if(rotatingAxisSelected == 3)
            {
                knobParent.transform.eulerAngles = new Vector3(tempKnobAngle + rotatedAngleZ, 90, 180);
                this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
            }
            
            if(rotatedAngleX > 35)
            {
                rotatedFlagX = true; //this is used and falsed in NetworkObjectMangager script
                rotatedAngleX = 0;
                tempKnobAngle = 0;
                tempFingerAngleX = 0;
                knobEnabled = false;
            }
            if (rotatedAngleY > 35)
            {
                rotatedFlagY = true; //this is used and falsed in NetworkObjectMangager script
                rotatedAngleY = 0;
                tempKnobAngle = 0;
                tempFingerAngleY = 0;
                knobEnabled = false;
            }
            if (rotatedAngleZ > 35)
            {
                rotatedFlagZ = true; //this is used and falsed in NetworkObjectMangager script
                rotatedAngleZ = 0;
                tempKnobAngle = 0;
                tempFingerAngleZ = 0;
                knobEnabled = false;
            }

        }
        else
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.white;
            knobParent.transform.rotation = fingertip.transform.rotation;
        }

        if (Physics.Raycast(fingertip.transform.position, fingertip.transform.right, out hit, raycastRange, axisTargetLayer))
        {
            if(hit.transform.name == "XaxisRaycastTarget") rotatingAxisSelected = 1;
            
            else if (hit.transform.name == "YaxisRaycastTarget") rotatingAxisSelected = 2;
            
            else if(hit.transform.name == "ZaxisRaycastTarget") rotatingAxisSelected = 3;
            
            else rotatingAxisSelected = 0;

        }

    }
    //rotating knob should be place on participant right hand
    //so it should be rotated by researcher left hand
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "L1TipNetwork") thumbTouched = true;
        
        if (other.gameObject.name == "L2TipNetwork")
        {
            indexTouched = true;

            if (LobbyNetworkManager.interactionType == 1) touchedFingerObject = GameObject.FindWithTag("networkLeftControllerAnchor");
            else touchedFingerObject = GameObject.FindWithTag("networkLeftHandAnchor");
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
