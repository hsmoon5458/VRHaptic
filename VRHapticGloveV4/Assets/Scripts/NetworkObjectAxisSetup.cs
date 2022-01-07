using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkObjectAxisSetup : MonoBehaviour
{
    public GameObject xAxis, yAxis, zAxis;
    
    void Update()
    {
        xAxis.transform.localScale = new Vector3(0.1f, 1, 0.1f);
        yAxis.transform.localScale = new Vector3(0.1f, 1, 0.1f);
        zAxis.transform.localScale = new Vector3(0.1f, 1, 0.1f);
    }
}
