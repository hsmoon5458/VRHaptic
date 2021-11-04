using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkObjectsManager : MonoBehaviour
{
    private GameObject networkCube, networkSphere, networkCylinder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1") || TestCubeGenerate.testTouched)
        {
            networkCube = PhotonNetwork.Instantiate("NetworkCube", new Vector3(1,1,1), Quaternion.identity);
            TestCubeGenerate.testTouched = false;
        }
        if (Input.GetKeyDown("2"))
        {
            networkSphere = PhotonNetwork.Instantiate("NetworkSphere", new Vector3(1, 1, 1), Quaternion.identity);
        }
        if (Input.GetKeyDown("3"))
        {
            networkCylinder = PhotonNetwork.Instantiate("NetworkCylinder", new Vector3(1, 1, 1), Quaternion.identity);
        }

    }
}
