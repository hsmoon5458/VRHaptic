using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCubeGenerate : MonoBehaviour
{
    public static bool testTouched;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "L2Tip")
        {
            testTouched = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "L2Tip")
        {
            testTouched = true;
        }
    }
}
