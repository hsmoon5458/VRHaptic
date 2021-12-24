using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkObjectsManager : MonoBehaviour
{
    private GameObject networkCube, networkSphere, networkCylinder;
    private GameObject[] networkObjects;

    private float timeCountCube, timeCountSphere, timeCountCylinder, timeReset;
    private bool cubeFlag, sphereFlag, cylinderFlag;
    private bool cubeGenerate, sphereGenerate, cylinderGenerate;

    [Range(0.5f, 2.5f)]
    private float timeToGenerate = 1.2f;

    public Transform objectSpawnTransform;
  
    // Update is called once per frame
    void Update()
    {
        //reset the time for all instantiations so that it does not make objects mistakely
        
        #region Condition Check for Instantiation
        //hand shape for cube
        if ((FingertipBehavior.thumbTouchedIndex && FingertipBehavior.thumbTouchedIndex))
        {
            timeCountCube += Time.deltaTime; // count time
            if (timeCountCube > timeToGenerate) //if it lasts for two seconds
            {
                cubeGenerate = true;
                timeCountCube = 0; //reset the time for another iteration
                FingertipBehavior.thumbTouchedIndex = false; //falsify the touch
                FingertipBehavior.thumbTouchedIndex = false;
            }
        }
        else
        {
            timeCountCube = 0;
        }

        //hand shape for sphere
        if ((FingertipBehavior.thumbTouchedThumb && FingertipBehavior.indexTouchedIndex))
        {
            timeCountSphere += Time.deltaTime; // count time
            if (timeCountSphere > timeToGenerate) //if it lasts for certain period
            {
                sphereGenerate = true;
                timeCountSphere = 0; //reset the time for another iteration
                FingertipBehavior.thumbTouchedThumb = false; //falsify the touch
                FingertipBehavior.indexTouchedIndex = false;
            }
        }
        else
        {
            timeCountSphere = 0;
        }

        //hand shape for cylinder
        if ((!FingertipBehavior.thumbTouchedThumb && FingertipBehavior.indexTouchedIndex))
        {
            timeCountCylinder += Time.deltaTime; // count time
            if (timeCountCylinder > timeToGenerate) //if it lasts for certain period
            {
                cylinderGenerate = true;
                timeCountCylinder = 0; //reset the time for another iteration
                FingertipBehavior.thumbTouchedThumb = false; //falsify the touch
                FingertipBehavior.indexTouchedIndex = false;
            }
        }
        else
        {
            timeCountCylinder = 0;
        }

        #endregion

        #region Instantiate 3D Objects
        if (Input.GetKeyDown("1") || cubeGenerate)
        {
            networkCube = PhotonNetwork.Instantiate("NetworkCube", objectSpawnTransform.position, objectSpawnTransform.rotation);
            cubeGenerate = false;
        }
        if (Input.GetKeyDown("2") || sphereGenerate)
        {
            networkSphere = PhotonNetwork.Instantiate("NetworkSphere", objectSpawnTransform.position, objectSpawnTransform.rotation);
            sphereGenerate = false;
        }
        if (Input.GetKeyDown("3") || cylinderGenerate)
        {
            networkCylinder = PhotonNetwork.Instantiate("NetworkCylinder", objectSpawnTransform.position, objectSpawnTransform.rotation);
            cylinderGenerate = false;
        }

        //delete all network object
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
