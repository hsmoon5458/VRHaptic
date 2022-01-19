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
    private float rotatingSpeed = 1.4f;
    //scaling 3D objects
    private GameObject leftFingertip, rightFingertip; // to calculate the distance between fingers for scaling
    public static bool xAxisScalingEnabledFlag, yAxisScalingEnabledFlag, zAxisScalingEnabledFlag;
    private float scalingDistance, tempObjDis1, tempObjDis2, tempObjDis3, tempScalingDis;
    public GameObject scalingAxisObject; //x, y, z axis object
    //rotating object
    private bool rotateCompletedFlag;
    //positioning object
    public GameObject handToHandLightString, leftHandToObjectLightString, rightHandToObjectLightString;
    private float lightStringDistanceThreshold = 0.5f, positioningThreshold = 0.1f, objectMovementSpeed = 0.1f;
    private Vector3 tempRightFingerPosition, tempTargetPosition;
    private bool positioiningFlag;

    //sound
    public AudioSource objectAudioSource;
    public AudioClip instantiateSound, scaleSound, rotateSound;

    private void Start()
    {
        InvokeRepeating("IdentifyFingertip", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //keep finding the fingertip objects, if they are both identified, cancel invoking.
        if (leftFingertip != null && rightFingertip != null) CancelInvoke("IdentifyFingertip");
        else InvokeRepeating("IdentifyFingertip", 1.0f, 1.0f);

        //reset the time for all instantiations so that it does not make objects mistakely      
        #region Condition Check for Instantiation
        if(RoomGameManager.gameStep == 1)
        {
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
        }
        #endregion

        //Step 1
        #region Instantiate 3D Objects
        if(RoomGameManager.gameStep == 1)
        {
            //disable light string from game step 4
            handToHandLightString.SetActive(false);
            leftHandToObjectLightString.SetActive(false);
            rightHandToObjectLightString.SetActive(false);

            if (cubeGenerate)
            {
                networkCube = PhotonNetwork.Instantiate("NetworkCube", objectSpawnTransform.position, objectSpawnTransform.rotation);
                networkCube.name = "NetworkCube";
                objectAudioSource.PlayOneShot(instantiateSound);
                cubeGenerate = false;
            }
            if (sphereGenerate)
            {
                networkSphere = PhotonNetwork.Instantiate("NetworkSphere", objectSpawnTransform.position, objectSpawnTransform.rotation);
                networkSphere.name = "NetworkSphere";
                objectAudioSource.PlayOneShot(instantiateSound);
                sphereGenerate = false;
            }
            if (cylinderGenerate)
            {
                networkCylinder = PhotonNetwork.Instantiate("NetworkCylinder", objectSpawnTransform.position, objectSpawnTransform.rotation);
                networkCylinder.name = "NetworkCylinder";
                objectAudioSource.PlayOneShot(instantiateSound);
                cylinderGenerate = false;
            }
        }
        /*
        //delete all network object
        if (Input.GetKeyDown(KeyCode.Space))
        {
            networkObjects = GameObject.FindGameObjectsWithTag("InstantiatedObject");
            foreach (GameObject obj in networkObjects)
            {
                PhotonNetwork.Destroy(obj);
            }

        }
        */
        #endregion

        //Step 2
        #region Sacling the 3D Objects
        if(RoomGameManager.gameStep == 2)
        {
            scalingAxisObject.SetActive(true); //activate the scaling axis object.
            //this X axis is for 1) Cube X axis, 2) Cylinder radius, and 3) Sphere Radius.
            if (XAxisPinching.XScaling && !xAxisScalingEnabledFlag) //if bool is true, this Xsxaling should be excuted to get the initial data of distance between fingers and the object local scale for scaling.
            {
                //identify object
                GameObject tempObject = GameObject.FindWithTag("InstantiatedObject");
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
                GameObject tempObject = GameObject.FindWithTag("InstantiatedObject");
                Vector3 tempTf = tempObject.transform.localScale; //for ticking sound, get the scale to check the difference
                //scale the object with snipping
                tempObject.transform.localScale = new Vector3(Mathf.RoundToInt((scalingDistance - tempScalingDis + tempObjDis1) * 10) * 0.1f, tempObject.transform.localScale.y, tempObject.transform.localScale.z);
                
                //if there's a difference, make a sound.
                if(tempTf != tempObject.transform.localScale)
                {
                    objectAudioSource.PlayOneShot(scaleSound);
                }
            }
            else{tempObjDis1 = 0;}

            //this is for 1) Cube Y axis and 2) Cylinder Height
            if (YAxisPinching.YScaling && !yAxisScalingEnabledFlag) //if bool is true, this Xsxaling should be excuted to get the initial data of distance between fingers and the object local scale for scaling.
            {
                //identify object
                GameObject tempObject = GameObject.FindWithTag("InstantiatedObject");
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
                GameObject tempObject = GameObject.FindWithTag("InstantiatedObject");
                Vector3 tempTf = tempObject.transform.localScale; //for ticking sound, get the scale to check the difference
                //scale the object with snipping
                tempObject.transform.localScale = new Vector3(tempObject.transform.localScale.x, Mathf.RoundToInt((scalingDistance - tempScalingDis + tempObjDis2) * 10) * 0.1f, tempObject.transform.localScale.z);

                //if there's a difference, make a sound.
                if (tempTf != tempObject.transform.localScale)
                {
                    objectAudioSource.PlayOneShot(scaleSound);
                }
            }
            else{tempObjDis2 = 0;}


            if (ZAxisPinching.ZScaling && !zAxisScalingEnabledFlag)
            {
                //identify object
                GameObject tempObject = GameObject.FindWithTag("InstantiatedObject");
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
                GameObject tempObject = GameObject.FindWithTag("InstantiatedObject");
                Vector3 tempTf = tempObject.transform.localScale; //for ticking sound, get the scale to check the difference
                //scale the object with snipping
                tempObject.transform.localScale = new Vector3(tempObject.transform.localScale.x, tempObject.transform.localScale.y, Mathf.RoundToInt((scalingDistance - tempScalingDis + tempObjDis3) * 10) * 0.1f);

                //if there's a difference, make a sound.
                if (tempTf != tempObject.transform.localScale)
                {
                    objectAudioSource.PlayOneShot(scaleSound);
                }
            }
            else{tempObjDis3 = 0;}
        }
        #endregion

        //Step 3
        #region Rotating the 3D Objects
        if(RoomGameManager.gameStep == 3)
        {
            scalingAxisObject.SetActive(false); //after scaling is done, deactivate the scaling axis object

            if (!rotateCompletedFlag)
            {
                if (RotatingKnob.rotatedFlagX && RotatingKnob.rotatingAxisSelected == 1)
                {
                    rotateCompletedFlag = true;
                    GameObject tempObject = GameObject.FindWithTag("InstantiatedObject");
                    objectAudioSource.PlayOneShot(rotateSound);
                    StartCoroutine(RotatingObject(tempObject, 1));
                    RotatingKnob.rotatedFlagX = false;
                }
                if (RotatingKnob.rotatedFlagY && RotatingKnob.rotatingAxisSelected == 2)
                {
                    rotateCompletedFlag = true;
                    GameObject tempObject = GameObject.FindWithTag("InstantiatedObject");
                    objectAudioSource.PlayOneShot(rotateSound);
                    StartCoroutine(RotatingObject(tempObject, 2));
                    RotatingKnob.rotatedFlagY = false;
                }
                if (RotatingKnob.rotatedFlagZ && RotatingKnob.rotatingAxisSelected == 3)
                {
                    rotateCompletedFlag = true;
                    GameObject tempObject = GameObject.FindWithTag("InstantiatedObject");
                    objectAudioSource.PlayOneShot(rotateSound);
                    StartCoroutine(RotatingObject(tempObject, 3));
                    RotatingKnob.rotatedFlagZ = false;
                }
            }

            if(RoomGameManager.resetRotatingFlag == true) //from Room Game manager, if the rotation is not correct, stop the rotation
            {
                GameObject tempObject = GameObject.FindWithTag("InstantiatedObject");
                StopCoroutine(RotatingObject(tempObject, 1));
                StopCoroutine(RotatingObject(tempObject, 2));
                StopCoroutine(RotatingObject(tempObject, 3));
                RotatingKnob.rotatedFlagX = false;
                RotatingKnob.rotatedFlagY = false;
                RotatingKnob.rotatedFlagZ = false;
                rotateCompletedFlag = false;
            }
        }
        #endregion

        //Step 4
        #region Positioning the 3D Objects
        if(RoomGameManager.gameStep == 4) // positioning step
        {
            if (Vector3.Distance(leftFingertip.transform.position, rightFingertip.transform.position) < lightStringDistanceThreshold && !positioiningFlag)
            {
                GameObject tempObject = GameObject.FindWithTag("InstantiatedObject");
                tempRightFingerPosition = rightFingertip.transform.position;
                positioiningFlag = true;
                StartCoroutine(PositioningGetFingerPosition()); //delay for 0.4 second to get the position in the middle of range, so that temp position is not set to the edge of the threshold
            }

            if (positioiningFlag)
            {
                handToHandLightString.SetActive(true);//this is where the light string audio source located
                leftHandToObjectLightString.SetActive(true);
                rightHandToObjectLightString.SetActive(true);

                VibrationManager.singletone.TriggerVibration(40, 2, 55, OVRInput.Controller.RTouch);


                if (rightFingertip.transform.position.x - tempRightFingerPosition.x > positioningThreshold)
                {
                    GameObject tempObject = GameObject.FindWithTag("InstantiatedObject");
                    tempObject.transform.position += transform.right * Time.deltaTime * objectMovementSpeed;
                }
                if (rightFingertip.transform.position.y - tempRightFingerPosition.y > positioningThreshold)
                {
                    GameObject tempObject = GameObject.FindWithTag("InstantiatedObject");
                    tempObject.transform.position += transform.up * Time.deltaTime * objectMovementSpeed;
                }
                if (rightFingertip.transform.position.z - tempRightFingerPosition.z > positioningThreshold)
                {
                    GameObject tempObject = GameObject.FindWithTag("InstantiatedObject");
                    tempObject.transform.position += transform.forward * Time.deltaTime * objectMovementSpeed;
                }
                if (rightFingertip.transform.position.x - tempRightFingerPosition.x < -positioningThreshold)
                {
                    GameObject tempObject = GameObject.FindWithTag("InstantiatedObject");
                    tempObject.transform.position += -transform.right * Time.deltaTime * objectMovementSpeed;
                }
                if (rightFingertip.transform.position.y - tempRightFingerPosition.y < -positioningThreshold)
                {
                    GameObject tempObject = GameObject.FindWithTag("InstantiatedObject");
                    tempObject.transform.position += -transform.up * Time.deltaTime * objectMovementSpeed;
                }
                if (rightFingertip.transform.position.z - tempRightFingerPosition.z < -positioningThreshold)
                {
                    GameObject tempObject = GameObject.FindWithTag("InstantiatedObject");
                    tempObject.transform.position += -transform.forward * Time.deltaTime * objectMovementSpeed;
                }

                //while the flag is enabled, distnace gets further, turn off the ligth string
                if (Vector3.Distance(leftFingertip.transform.position, rightFingertip.transform.position) > lightStringDistanceThreshold)
                {
                    handToHandLightString.SetActive(false);
                    leftHandToObjectLightString.SetActive(false);
                    rightHandToObjectLightString.SetActive(false);
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

    IEnumerator RotatingObject(GameObject obj, int axis)
    {
        if (axis == 1)
        {
            //to rotate the object smoothly, using lerp to rotate the object
            while (Mathf.RoundToInt(obj.transform.eulerAngles.x) != 45)
            {
                obj.transform.rotation = Quaternion.Lerp(obj.transform.rotation, Quaternion.Euler(45, 0, 0), rotatingSpeed * Time.deltaTime);
                yield return null;
            }
            //at last, round up to integer so that it lands onto integer angle
            obj.transform.eulerAngles = new Vector3(Mathf.RoundToInt(obj.transform.eulerAngles.x), Mathf.RoundToInt(obj.transform.eulerAngles.y), Mathf.RoundToInt(obj.transform.eulerAngles.z));
        }
        else if (axis == 2)
        {
            while (Mathf.RoundToInt(obj.transform.eulerAngles.y) != 45)
            {
                obj.transform.rotation = Quaternion.Lerp(obj.transform.rotation, Quaternion.Euler(0, 45, 0), rotatingSpeed * Time.deltaTime);
                yield return null;
            }
            obj.transform.eulerAngles = new Vector3(Mathf.RoundToInt(obj.transform.eulerAngles.x), Mathf.RoundToInt(obj.transform.eulerAngles.y), Mathf.RoundToInt(obj.transform.eulerAngles.z));
        }
        else if (axis == 3)
        {
            while (Mathf.RoundToInt(obj.transform.eulerAngles.z) != 45)
            {
                obj.transform.rotation = Quaternion.Lerp(obj.transform.rotation, Quaternion.Euler(0, 0, 45), rotatingSpeed * Time.deltaTime);
                yield return null;
            }
            obj.transform.eulerAngles = new Vector3(Mathf.RoundToInt(obj.transform.eulerAngles.x), Mathf.RoundToInt(obj.transform.eulerAngles.y), Mathf.RoundToInt(obj.transform.eulerAngles.z));
        }

        yield return new WaitForSeconds(2f);
    }

    IEnumerator PositioningGetFingerPosition()
    {
        yield return new WaitForSeconds(0.4f);
        tempRightFingerPosition = rightFingertip.transform.position;
        positioiningFlag = true;
    }
}
