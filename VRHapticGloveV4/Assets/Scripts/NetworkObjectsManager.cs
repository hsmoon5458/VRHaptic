using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkObjectsManager : MonoBehaviour
{
    private GameObject networkCube, networkSphere, networkCylinder;
    private GameObject[] networkObjects;

    private float timeCountCube, timeCountSphere;
    private bool cubeGenerate, sphereGenerate, sphereCylinder;

    [Range(0.5f, 2.5f)]
    public float timeToGenerate = 1.2f;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #region Condition Check for Instantiation
        //hand shape for cube
        if ((FingertipBehavior.thumbTouchedIndex && FingertipBehavior.thumbTouchedIndex))
        {
            timeCountCube += Time.deltaTime; // count time
            Debug.Log(timeCountCube);
            if (timeCountCube > timeToGenerate) //if it lasts for two seconds
            {
                cubeGenerate = true;
                timeCountCube = 0; //reset the time for another iteration
                FingertipBehavior.thumbTouchedIndex = false; //falsify the touch
                FingertipBehavior.thumbTouchedIndex = false;
            }
        }

        //hand shape for sphere
        if ((FingertipBehavior.thumbTouchedThumb && FingertipBehavior.indexTouchedIndex))
        {
            timeCountSphere += Time.deltaTime; // count time
            Debug.Log(timeCountSphere);
            if (timeCountSphere > timeToGenerate) //if it lasts for certain period
            {
                sphereGenerate = true;
                timeCountSphere = 0; //reset the time for another iteration
                FingertipBehavior.thumbTouchedThumb = false; //falsify the touch
                FingertipBehavior.indexTouchedIndex = false;
            }
        }
        #endregion

        #region Instantiate 3D Objects
        if (Input.GetKeyDown("1") || cubeGenerate)
        {
            networkCube = PhotonNetwork.Instantiate("NetworkCube", new Vector3(Random.Range(-10f,10f) * 0.1f,1,1), Quaternion.identity);
            cubeGenerate = false;
        }
        if (Input.GetKeyDown("2") || sphereGenerate)
        {
            networkSphere = PhotonNetwork.Instantiate("NetworkSphere", new Vector3(Random.Range(-10f, 10f) * 0.1f, 1, 1), Quaternion.identity);
            sphereGenerate = false;
        }
        if (Input.GetKeyDown("3"))
        {
            networkCylinder = PhotonNetwork.Instantiate("NetworkCylinder", new Vector3(Random.Range(-10f, 10f) * 0.1f, 1, 1), Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            networkObjects = GameObject.FindGameObjectsWithTag("InstantiatedObject");
            foreach (GameObject obj in networkObjects)
            {
                PhotonNetwork.Destroy(obj);
            }

        }
        #endregion
    }
}
