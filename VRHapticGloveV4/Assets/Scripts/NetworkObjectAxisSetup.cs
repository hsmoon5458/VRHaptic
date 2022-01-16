using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkObjectAxisSetup : MonoBehaviour
{
    public GameObject xAxis, yAxis, zAxis;
    public float FixeScale = 1;
    public GameObject parent;

    void Update()
    {
        xAxis.transform.localScale = new Vector3(FixeScale / parent.transform.localScale.x * 0.1f, FixeScale / parent.transform.localScale.y, FixeScale / parent.transform.localScale.z * 0.1f);
        yAxis.transform.localScale = new Vector3(FixeScale / parent.transform.localScale.x * 0.1f, FixeScale / parent.transform.localScale.y, FixeScale / parent.transform.localScale.z * 0.1f);
        zAxis.transform.localScale = new Vector3(FixeScale / parent.transform.localScale.x * 0.1f, FixeScale / parent.transform.localScale.y, FixeScale / parent.transform.localScale.z * 0.1f);
    }
}
