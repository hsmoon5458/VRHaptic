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
        if (Input.GetKeyDown("1"))
        {
            networkCube = PhotonNetwork.Instantiate("NetworkCube", new Vector3(1,1,1), Quaternion.identity);
        }
        if (Input.GetKeyDown("2"))
        {
            networkSphere = PhotonNetwork.Instantiate("NetworkSphere", new Vector3(1, 1, 1), Quaternion.identity);
        }
        if (Input.GetKeyDown("3"))
        {
            networkCylinder = PhotonNetwork.Instantiate("NetworkCylinder", new Vector3(1, 1, 1), Quaternion.identity);
        }


        if (Input.GetKey("a"))
        {
            networkCube.transform.Translate(Vector3.left * Time.deltaTime);
        }

        if (Input.GetKey("d"))
        {
            networkCube.transform.Translate(Vector3.right * Time.deltaTime);
        }
    }
}
