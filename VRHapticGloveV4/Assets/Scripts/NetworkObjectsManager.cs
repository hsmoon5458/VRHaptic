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
    private float rotatingSpeed = 5f;
    //scaling 3D objects
    private GameObject leftFingertip, rightFingertip; // to calculate the distance between fingers for scaling
    public static bool xAxisScalingEnabledFlag, yAxisScalingEnabledFlag, zAxisScalingEnabledFlag;
    private float scalingDistance, tempObjDis1, tempObjDis2, tempObjDis3, tempScalingDis;
    
    //positioning object
    public GameObject handToHandLightString;//, leftHandToObjectLightString, rightHandToObjectLightString;
    private float lightStringDistanceThreshold = 0.5f, positioningThreshold = 0.1f, objectMovementSpeed = 0.1f;
    private Vector3 tempRightFingerPosition, tempTargetPosition;
    private bool positioiningFlag;

    private void Start()
    {
        InvokeRepeating("IdentifyFingertip", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //keep finding the fingertip objects
        if (leftFingertip != null && rightFingertip != null) // if they are both identified, cancel invoking.
        {
            CancelInvoke("IdentifyFingertip");
        }

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

        //Step 1 Done
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

        //Step 2 Done
        #region Sacling the 3D Objects
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
            tempObject.transform.localScale = new Vector3(Mathf.RoundToInt((scalingDistance - tempScalingDis + tempObjDis1)*10) * 0.1f, tempObject.transform.localScale.y, tempObject.transform.localScale.z);
        }
        else
        {
            tempObjDis1 = 0;
            //tempScalingDis = 0;
        }

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
            tempObject.transform.localScale = new Vector3(tempObject.transform.localScale.x, Mathf.RoundToInt((scalingDistance - tempScalingDis + tempObjDis2)*10) * 0.1f, tempObject.transform.localScale.z);
        }
        else
        {
            tempObjDis2 = 0;
        }


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
            tempObject.transform.localScale = new Vector3(tempObject.transform.localScale.x, tempObject.transform.localScale.y, Mathf.RoundToInt((scalingDistance - tempScalingDis + tempObjDis3)*10)*0.1f);
        }
        else
        {
            tempObjDis3 = 0;
        }
        #endregion

        //Step 3 
        #region Rotating the 3D Objects
        
        if(RotatingKnob.rotatedFlag && RotatingKnob.rotatingAxisSelected == 1)
        {
            GameObject tempObject = GameObject.Find("NetworkCube");//this should be changed ----------------
            StartCoroutine(RotatingObject(tempObject, Mathf.RoundToInt(tempObject.transform.eulerAngles.x), 1));
            RotatingKnob.rotatedFlag = false;
        }
        if (RotatingKnob.rotatedFlag && RotatingKnob.rotatingAxisSelected == 2)
        {
            GameObject tempObject = GameObject.Find("NetworkCube");//this should be changed ----------------
            StartCoroutine(RotatingObject(tempObject, Mathf.RoundToInt(tempObject.transform.eulerAngles.y), 2));
            RotatingKnob.rotatedFlag = false;
        }
        if (RotatingKnob.rotatedFlag && RotatingKnob.rotatingAxisSelected == 3)
        {
            GameObject tempObject = GameObject.Find("NetworkCube");//this should be changed ----------------
            StartCoroutine(RotatingObject(tempObject, Mathf.RoundToInt(tempObject.transform.eulerAngles.z), 3));
            RotatingKnob.rotatedFlag = false;
        }

        #endregion

        //Step 4 Done
        #region Positioning the 3D Objects
        if(GameManager.gameStep == 1515) // positioning step
        {
            if (Vector3.Distance(leftFingertip.transform.position, rightFingertip.transform.position) < lightStringDistanceThreshold && !positioiningFlag)
            {
                GameObject tempObject = GameObject.Find("NetworkCube");//this should be changed ----------------
                                                                       //tempRightFingerPosition = rightFingertip.transform.position;
                                                                       //positioiningFlag = true;
                StartCoroutine(PositioningGetFingerPosition()); //delay for 0.4 second to get the position in the middle of range, so that temp position is not set to the edge of the threshold
            }

            if (positioiningFlag)
            {
                handToHandLightString.SetActive(true);

                if (rightFingertip.transform.position.x - tempRightFingerPosition.x > positioningThreshold)
                {
                    GameObject tempObject = GameObject.Find("NetworkCube");//this should be changed ----------------
                    tempObject.transform.position += transform.right * Time.deltaTime * objectMovementSpeed;
                }
                if (rightFingertip.transform.position.y - tempRightFingerPosition.y > positioningThreshold)
                {
                    GameObject tempObject = GameObject.Find("NetworkCube");//this should be changed ----------------
                    tempObject.transform.position += transform.up * Time.deltaTime * objectMovementSpeed;
                }
                if (rightFingertip.transform.position.z - tempRightFingerPosition.z > positioningThreshold)
                {
                    GameObject tempObject = GameObject.Find("NetworkCube");//this should be changed ----------------
                    tempObject.transform.position += transform.forward * Time.deltaTime * objectMovementSpeed;
                }
                if (rightFingertip.transform.position.x - tempRightFingerPosition.x < -positioningThreshold)
                {
                    GameObject tempObject = GameObject.Find("NetworkCube");//this should be changed ----------------
                    tempObject.transform.position += -transform.right * Time.deltaTime * objectMovementSpeed;
                }
                if (rightFingertip.transform.position.y - tempRightFingerPosition.y < -positioningThreshold)
                {
                    GameObject tempObject = GameObject.Find("NetworkCube");//this should be changed ----------------
                    tempObject.transform.position += -transform.up * Time.deltaTime * objectMovementSpeed;
                }
                if (rightFingertip.transform.position.z - tempRightFingerPosition.z < -positioningThreshold)
                {
                    GameObject tempObject = GameObject.Find("NetworkCube");//this should be changed ----------------
                    tempObject.transform.position += -transform.forward * Time.deltaTime * objectMovementSpeed;
                }

                //while the flag is enabled, distnace gets further, turn off the ligth string
                if (Vector3.Distance(leftFingertip.transform.position, rightFingertip.transform.position) > lightStringDistanceThreshold)
                {
                    handToHandLightString.SetActive(false);
                    positioiningFlag = false;
                }
            }
        }       
        #endregion

    }

    private void IdentifyFingertip()
    {
        if(LobbyNetworkManager.userType == 1) // researcher
        {
            leftFingertip = GameObject.FindWithTag("myLeftIndexFinger");
            rightFingertip = GameObject.FindWithTag("networkRightIndexFinger");
        }
        else if(LobbyNetworkManager.userType == 2) // participant
        {
            leftFingertip = GameObject.FindWithTag("networkLeftIndexFinger");
            rightFingertip = GameObject.FindWithTag("myRightIndexFinger");
        }
    }

    IEnumerator RotatingObject(GameObject obj, int startAngle, int axis)
    {
        if (axis == 1)
        {
            //to rotate the object smoothly, using lerp to rotate the object
            while (Mathf.RoundToInt(obj.transform.eulerAngles.x) != startAngle + 45)
            {
                Debug.Log("Spinning");
                Debug.Log(Mathf.RoundToInt(obj.transform.eulerAngles.x));
                Debug.Log(startAngle + 45);
                obj.transform.rotation = Quaternion.Lerp(obj.transform.rotation, Quaternion.Euler(startAngle + 45, obj.transform.eulerAngles.y, obj.transform.eulerAngles.z), rotatingSpeed * Time.deltaTime);
                yield return null;
            }
            //at last, round up to integer so that it lands onto integer angle
            obj.transform.eulerAngles = new Vector3(Mathf.RoundToInt(obj.transform.eulerAngles.x), Mathf.RoundToInt(obj.transform.eulerAngles.y), Mathf.RoundToInt(obj.transform.eulerAngles.z));
        }
        else if (axis == 2)
        {
            while (Mathf.RoundToInt(obj.transform.eulerAngles.y) != startAngle + 45)
            {
                obj.transform.rotation = Quaternion.Lerp(obj.transform.rotation, Quaternion.Euler(obj.transform.eulerAngles.x, startAngle + 45, obj.transform.eulerAngles.z), rotatingSpeed * Time.deltaTime);
                yield return null;
            }
            obj.transform.eulerAngles = new Vector3(Mathf.RoundToInt(obj.transform.eulerAngles.x), Mathf.RoundToInt(obj.transform.eulerAngles.y), Mathf.RoundToInt(obj.transform.eulerAngles.z));
        }
        else if (axis == 3)
        {
            while (Mathf.RoundToInt(obj.transform.eulerAngles.z) != startAngle + 45)
            {
                obj.transform.rotation = Quaternion.Lerp(obj.transform.rotation, Quaternion.Euler(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y, startAngle + 45), rotatingSpeed * Time.deltaTime);
                yield return null;
            }
            obj.transform.eulerAngles = new Vector3(Mathf.RoundToInt(obj.transform.eulerAngles.x), Mathf.RoundToInt(obj.transform.eulerAngles.y), Mathf.RoundToInt(obj.transform.eulerAngles.z));
        }

        
    }

    IEnumerator PositioningGetFingerPosition()
    {
        yield return new WaitForSeconds(0.4f);
        tempRightFingerPosition = rightFingertip.transform.position;
        positioiningFlag = true;
    }
}
