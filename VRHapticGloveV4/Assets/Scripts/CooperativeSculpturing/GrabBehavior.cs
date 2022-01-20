using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBehavior : MonoBehaviour
{
    public static bool leftGrabbed;
    public static GameObject grabbedObject;
    private bool L1Tip, L2Tip, networkObject;
    void Update()
    {
        //if grab hand collider has a contact with 1)thumb, 2)index, 3)network object
        if (L1Tip && L2Tip && networkObject)
        {
            leftGrabbed = true;
            Debug.Log("grabbed!");
        }
        //just two tips are detached conditions since, if the grabbed object is moving fast with a hand,
        //grabbed object is detached from the grab collider, and make it false, so just tips are conditions
        else if (!L1Tip && !L2Tip)
        {
            leftGrabbed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //for letf hand only for now
        if(other.gameObject.name == "L1Tip")
        {
            L1Tip = true;
        }
        if (other.gameObject.name == "L2Tip")
        {
            L2Tip = true;
        }
        if (other.gameObject.name.Contains("Network"))
        {
            networkObject = true;
            grabbedObject = other.gameObject;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        //for letf hand only for now
        if (other.gameObject.name == "L1Tip")
        {
            L1Tip = false;
        }
        if (other.gameObject.name == "L2Tip")
        {
            L2Tip = false;
        }
        if (other.gameObject.name.Contains("Network"))
        {
            networkObject = false;
        }
    }
}
