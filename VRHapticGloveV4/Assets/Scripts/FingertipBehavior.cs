using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingertipBehavior : MonoBehaviour
{
    public static bool F1F1, F1F2, F2F1, F2F2; //thumb touch thumb, thumb touch index, index touch thumb, index touch index
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (this.gameObject.name == "L1Tip")
        {
            if(other.gameObject.name == "R1Tip")
            {
                F1F1 = true;
                Debug.Log("F1F1");
            }
            if(other.gameObject.name == "R2Tip")
            {
                F1F2 = true;
                Debug.Log("F1F2");
            }
        }
        if (this.gameObject.name == "L2Tip")
        {
            if (other.gameObject.name == "R1Tip")
            {
                F2F1 = true;
                Debug.Log("F2F1");
            }
            if (other.gameObject.name == "R2Tip")
            {
                F2F2 = true;
                Debug.Log("F2F2");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (this.gameObject.name == "L1Tip")
        {
            if (other.gameObject.name == "R1Tip")
            {
                F1F1 = false;
                Debug.Log("F1F1");
            }
            if (other.gameObject.name == "R2Tip")
            {
                F1F2 = false;
                Debug.Log("F1F2");
            }
        }
        if (this.gameObject.name == "L2Tip")
        {
            if (other.gameObject.name == "R1Tip")
            {
                F2F1 = false;
                Debug.Log("F2F1");
            }
            if (other.gameObject.name == "R2Tip")
            {
                F2F2 = false;
                Debug.Log("F2F2");
            }
        }
    }
}
