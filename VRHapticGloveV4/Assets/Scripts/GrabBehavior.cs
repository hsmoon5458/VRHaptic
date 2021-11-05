using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBehavior : MonoBehaviour
{
    public static bool leftGrabbed;
    private bool L1Tip, L2Tip, networkObject;
    void Update()
    {
        //if grab hand collider has a contact with 1)thumb, 2)index, 3)network object
        if (L1Tip && L2Tip && networkObject)
        {
            leftGrabbed = true;
            Debug.Log("grabbed!");
        }
        else
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
