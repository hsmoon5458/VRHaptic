using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkObjectsManager : MonoBehaviour
{
    //creating 3D objects
    public Transform objectSpawnTransform;
    private GameObject networkCube, networkSphere, networkCylinder;
    private GameObject[] networkObjects;
    private float timeCountCube, timeCountSphere, timeCountCylinder, timeReset;
    private bool cubeFlag, sphereFlag, cylinderFlag;
    private bool cubeGenerate, sphereGenerate, cylinderGenerate;
    private float timeToGenerate = 1.2f;

    //scaling 3D objects
    private GameObject leftFingertip, rightFingertip; // to calculate the distance between fingers for scaling
    [SerializeField]
    public static bool xAxisScalingEnabledFlag, yAxisScalingEnabledFlag, zAxisScalingEnabledFlag;
    [SerializeField]
    private float scalingDistance, tempObjDis1, tempObjDis2, tempObjDis3, tempScalingDis;

    //positioning object
    public GameObject lightString;
    private float lightStringDistanceThreshold = 0.3f;

    private void Start()
    {
        StartCoroutine(IdentifyFingertip());
    }

    // Update is called once per frame
    void Update()
    {
        //reset the time for all instantiations so that it does not make objects mistakely      
        #region Condition Check for Instantiation
        //hand shape for cube
        if ((FingertipBehavior.thumbTouchedIndex && FingertipBehavior.indexTouchedThumb))
        {
            timeCountCube += Time.deltaTime; // count time
            if (timeCountCube > timeToGenerate) //if it lasts for two seconds
            {
                cubeGenerate = true;
                timeCountCube = 0; //reset the time for another iteration
                FingertipBehavior.thumbTouchedIndex = false; //falsify the touch
                FingertipBehavior.indexTouchedThumb = false;
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
        if ((FingertipBehavior.thumbTouchedThumb && !FingertipBehavior.indexTouchedIndex))
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
            networkCube.name = "NetworkCube";
            cubeGenerate = false;
        }
        if (Input.GetKeyDown("2") || sphereGenerate)
        {
            networkSphere = PhotonNetwork.Instantiate("NetworkSphere", objectSpawnTransform.position, objectSpawnTransform.rotation);
            networkSphere.name = "NetworkSphere";
            sphereGenerate = false;
        }
        if (Input.GetKeyDown("3") || cylinderGenerate)
        {
            networkCylinder = PhotonNetwork.Instantiate("NetworkCylinder", objectSpawnTransform.position, objectSpawnTransform.rotation);
            networkCylinder.name = "NetworkCylinder";
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

        #region Sacling the 3D Objects
        #region Scaling X axis
        //this X axis is for 1) Cube X axis, 2) Cylinder radius, and 3) Sphere Radius.
        if (XAxisPinching.XScaling && !xAxisScalingEnabledFlag) //if bool is true, this Xsxaling should be excuted to get the initial data of distance between fingers and the object local scale for scaling.
        {
            //identify object
            GameObject tempObject = GameObject.Find("NetworkCube");//this should be changed ----------------
            tempObjDis1 = tempObject.transform.localScale.x;
            tempScalingDis = Vector3.Distance(leftFingertip.transform.position, rightFingertip.transform.position);

            XAxisPinching.XScaling = false;
            xAxisScalingEnabledFlag = true; //this will be set false in FingertipBehavior script (when pinching is disabled)
        }
        if (xAxisScalingEnabledFlag)
        {
            yAxisScalingEnabledFlag = false;
            zAxisScalingEnabledFlag = false;
            //calculate the distance
            scalingDistance = Vector3.Distance(leftFingertip.transform.position, rightFingertip.transform.position);

            //find the difference value and added to original distance
            GameObject tempObject = GameObject.Find("NetworkCube");//this should be changed ----------------
            tempObject.transform.localScale = new Vector3(scalingDistance - tempScalingDis + tempObjDis1, tempObject.transform.localScale.y, tempObject.transform.localScale.z);
        }
        else
        {
            tempObjDis1 = 0;
            //tempScalingDis = 0;
        }
        #endregion
        
        #region Scaling Y axis
        //this is for 1) Cube Y axis and 2) Cylinder Height
        if (YAxisPinching.YScaling && !yAxisScalingEnabledFlag) //if bool is true, this Xsxaling should be excuted to get the initial data of distance between fingers and the object local scale for scaling.
        {
            //identify object
            GameObject tempObject = GameObject.Find("NetworkCube");//this should be changed ----------------
            tempObjDis2 = tempObject.transform.localScale.y;
            tempScalingDis = Vector3.Distance(leftFingertip.transform.position, rightFingertip.transform.position);

            YAxisPinching.YScaling = false;
            yAxisScalingEnabledFlag = true; //this will be set false in FingertipBehavior script (when pinching is disabled)
        }
        if (yAxisScalingEnabledFlag)
        {
            xAxisScalingEnabledFlag = false;
            zAxisScalingEnabledFlag = false;
            //calculate the distance
            scalingDistance = Vector3.Distance(leftFingertip.transform.position, rightFingertip.transform.position);

            //find the difference value and added to original distance
            GameObject tempObject = GameObject.Find("NetworkCube");//this should be changed ----------------
            tempObject.transform.localScale = new Vector3(tempObject.transform.localScale.x, scalingDistance - tempScalingDis + tempObjDis2, tempObject.transform.localScale.z);
        }
        else
        {
            tempObjDis2 = 0;
        }
        #endregion

        #region Scaling Z axis
        if (ZAxisPinching.ZScaling && !zAxisScalingEnabledFlag)
        {
            //identify object
            GameObject tempObject = GameObject.Find("NetworkCube");//this should be changed ----------------
            tempObjDis3 = tempObject.transform.localScale.z;
            tempScalingDis = Vector3.Distance(leftFingertip.transform.position, rightFingertip.transform.position);

            ZAxisPinching.ZScaling = false;
            zAxisScalingEnabledFlag = true;
        }
        if (zAxisScalingEnabledFlag)
        {
            xAxisScalingEnabledFlag = false;
            yAxisScalingEnabledFlag = false;
            //calculate the distance
            scalingDistance = Vector3.Distance(leftFingertip.transform.position, rightFingertip.transform.position);

            //find the difference value and added to original distance
            GameObject tempObject = GameObject.Find("NetworkCube");//this should be changed ----------------
            tempObject.transform.localScale = new Vector3(tempObject.transform.localScale.x, tempObject.transform.localScale.y, scalingDistance - tempScalingDis + tempObjDis3);
        }
        else
        {
            tempObjDis3 = 0;
        }
        #endregion

        #endregion

        #region Rotating the 3D Objects

        #endregion

        #region Positioning the 3D Objects
        if (Vector3.Distance(leftFingertip.transform.position, rightFingertip.transform.position) < lightStringDistanceThreshold)
        {
            lightString.SetActive(true);
        }
        else
        {
            lightString.SetActive(false);
        }
        #endregion
    }

    IEnumerator IdentifyFingertip()
    {
        yield return new WaitForSeconds(3.5f);
        leftFingertip = GameObject.FindWithTag("networkLeftFinger");
        rightFingertip = GameObject.FindWithTag("myRightFinger");
    }
}
