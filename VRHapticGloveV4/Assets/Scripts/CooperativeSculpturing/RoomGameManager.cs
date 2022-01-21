using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;


public class RoomGameManager : MonoBehaviour
{
    [SerializeField]
    private float completionTime, tempTime, step1Timer, step2Timer, step3Timer, step4Timer;
    public static int gameStep = 0; //1 is creating, 2 is scaling, 3 rotating, 4 is positioning
    public int sampleNum, objectNum; // ex) Sample1, s3

    public Transform participantTf, researcherTf;
    public GameObject HankOVRCameraRig;
    [SerializeField]
    private GameObject controllerKnob, handTrackingKnob;
    public GameObject leftHand, rightHand, leftHandTracking, rightHandTracking, leftControllerHand, rightControllerHand;
    private GameObject workspaceCubeGuide, workspaceCylinderGuide, workspaceSphereGuide;
    private GameObject currentWorkingSampleObject, currentWorkingGuideObject, currentWorkingNetoworkObject;//catch the sample object, and changed the guide object corresponds to game step and sample object transform.
    public GameObject handToHandLightString;
    public bool confirmationFlag;
    public static bool resetRotatingFlag;
    private bool positionFixedFlag;

    public GameObject[] completedObjects; //completed networked objects
    public GameObject[] instantiatedObjects; //instantiated network objects
    public delegate void NetworkSettingDelegate(); //to recall the Network Setting for the script in "Network Player" prefab.
    public static NetworkSettingDelegate NetworkPlayerSettingDelegate;
    
    //sound
    public AudioSource roomAudioSource, bgmAudioSource;
    public AudioClip positionSound, levelCompleteSound, confirmSound, rejectSound;

    //RPC
    private PhotonView PV;

    //test code
    private GameObject networkLeftHand;
    public GameObject testLeftAnchor;
    public GameObject testLeftControllerAnchor;
    public GameObject testLeftHandTracking;
    private GameObject testGameObj;
    //test code end
    public void RefreshNetworkPlayerSetting() // reset network setup for changing interaction type
    {
        NetworkPlayerSettingDelegate();
    }

    void Start()
    {
        PV = GetComponent<PhotonView>();
        PlayerSetting();

        //identify the knob in both controller and hand tracking in NetworkPlayer
        StartCoroutine(IdentifyingKnob());
    }

    void Update()
    {
        //timer for performance measurement
        tempTime = Time.deltaTime;

        //test code
        #region Test Code

        if (Input.GetKeyDown("t"))
        {
            BackgroundCubeColor.colorChangeTime = 0.1f;
            BackgroundCubeColor.numberOfCubeColorChanged = 30;
        }

        if (Input.GetKeyDown("c")) //enable controller for developing
        {
            networkLeftHand = GameObject.Find("Participant/LeftControllerAnchor/CustomHandLeft");
            networkLeftHand.SetActive(true);
        }
        if (Input.GetKeyDown("h")) //enable handtracking for developing
        {
            testLeftAnchor.SetActive(true);
            testLeftHandTracking.SetActive(true);
            testLeftControllerAnchor.SetActive(false);
            //networkLeftHand = GameObject.Find("Participant/LeftHand");
            networkLeftHand = PhotonView.Find(1001).gameObject.transform.GetChild(0).gameObject;
            networkLeftHand.SetActive(true);
        }
        if (Input.GetKeyDown("f"))
        {
            StartCoroutine(LevelCompleteEffect());

        }
        if (Input.GetKeyDown("1")) //enable controller for developing
        {
            testGameObj = GameObject.FindWithTag("InstantiatedObject");
            testGameObj.transform.localScale = new Vector3(0.5f, 0.1f, 0.5f);
        }

        #endregion
        //test code end
        #region Confirmation
        if (ConfirmationButtonBehavior.leftConfirmation && ConfirmationButtonBehavior.rightConfirmation) // if both buttons are clicked, make confirmation flag ture for GameStepCheck
        {
            confirmationFlag = true;

            ConfirmationButtonBehavior.leftConfirmation = false;
            ConfirmationButtonBehavior.rightConfirmation = false;
        }

        //confirm the current step whether it's done
        if (confirmationFlag || Input.GetKeyDown(KeyCode.Space))
        {
            currentWorkingNetoworkObject = GameObject.FindWithTag("InstantiatedObject");
            GameStepCheck(gameStep, currentWorkingSampleObject, currentWorkingNetoworkObject);
            confirmationFlag = false;
        }
        #endregion
        //Step 4: Snip movement to the position
        if (gameStep == 4)
        {
            float distance = Vector3.Distance(currentWorkingGuideObject.transform.position, currentWorkingNetoworkObject.transform.position);
            if (distance < 0.1f)
            {
                float step = 0.3f * Time.deltaTime;
                currentWorkingNetoworkObject.transform.position = Vector3.MoveTowards(currentWorkingNetoworkObject.transform.position, currentWorkingSampleObject.transform.position, step);
                PV.RPC("DisableLightString", RpcTarget.AllBuffered);

                //if it arrives to the target, locked in.
                if (distance < 0.01f)
                {
                    currentWorkingNetoworkObject.transform.position = currentWorkingSampleObject.transform.position;

                    if (positionFixedFlag)//this flag is ture in game step 3 and disabled here, this will be played once
                    { 
                        PV.RPC("PositionSoundPlay", RpcTarget.AllBuffered);
                        positionFixedFlag = false;
                    }
                }
            }
        }
    }
    #region Levels
    public void StartLevel1()
    {
        sampleNum = 1;
        LevelStart(sampleNum);
    }
    public void StartLevel2()
    {
        sampleNum = 2;
        LevelStart(sampleNum);
    }
    public void StartLevel3()
    {
        sampleNum = 3;
        LevelStart(sampleNum);
    }
    public void StartLevel4()
    {
        sampleNum = 4;
        LevelStart(sampleNum);
    }
    public void StartLevel5()
    {
        sampleNum = 5;
        LevelStart(sampleNum);
    }
    public void StartLevel6()
    {
        sampleNum = 6;
        LevelStart(sampleNum);
    }
    public void StartLevel7()
    {
        sampleNum = 7;
        LevelStart(sampleNum);
    }
    public void StartLevel8()
    {
        sampleNum = 8;
        LevelStart(sampleNum);
    }
    #endregion
    public void ClickedController()
    {
        LobbyNetworkManager.interactionType = 1;
    }
    public void ClickedHandTracking()
    {
        LobbyNetworkManager.interactionType = 2;
    }

    public void LevelStart(int sampleNum)
    {
        //set the timer setting
        tempTime = 0;

        //set the level setting
        objectNum = 1;
        gameStep = 1;
        WorkspaceInitialize(sampleNum, objectNum, gameStep); //start the level with first object, and first game step
    }

    public void LevelCompleted()
    {
        completionTime = tempTime;
        string minutes = Mathf.Floor(completionTime / 60).ToString("00");
        string seconds = (completionTime % 60).ToString("00");
    }

    public void WorkspaceInitialize(int sampleNum, int objectNum, int gameStep)
    {
        //indentify the object
        string tempPath = "Sample" + sampleNum.ToString() + "/s" + objectNum.ToString();
        currentWorkingSampleObject = GameObject.Find(tempPath);

        //creating object
        if (gameStep == 1)
        {
            if (currentWorkingSampleObject.tag == "sampleCube") //check the tag to identify the shape of the sample object
            {
                workspaceCubeGuide.SetActive(true); //activate the object, and deactiavte the other.
                workspaceCubeGuide.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f); //reset transform
                workspaceCubeGuide.transform.eulerAngles = new Vector3(0, 0, 0);
                workspaceCubeGuide.transform.position = new Vector3(0, 1, 0.5f); //should be same as object spawn position
                workspaceCylinderGuide.SetActive(false);
                workspaceSphereGuide.SetActive(false);

                currentWorkingGuideObject = workspaceCubeGuide; //set guide object for game step check
            }
            else if (currentWorkingSampleObject.tag == "sampleCylinder")
            {
                workspaceCubeGuide.SetActive(false);
                workspaceCylinderGuide.SetActive(true);
                workspaceCylinderGuide.transform.localScale = new Vector3(0.2f, 0.1f, 0.2f);
                workspaceCylinderGuide.transform.eulerAngles = new Vector3(0, 0, 0);
                workspaceCylinderGuide.transform.position = new Vector3(0, 1, 0.5f);
                workspaceSphereGuide.SetActive(false);

                currentWorkingGuideObject = workspaceCylinderGuide;
            }
            else // sphere
            {
                workspaceCubeGuide.SetActive(false);
                workspaceCylinderGuide.SetActive(false);
                workspaceSphereGuide.SetActive(true);
                workspaceSphereGuide.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                workspaceSphereGuide.transform.eulerAngles = new Vector3(0, 0, 0);
                workspaceSphereGuide.transform.position = new Vector3(0, 1, 0.5f);

                currentWorkingGuideObject = workspaceSphereGuide;
            }

        }

        //scaling object
        else if (gameStep == 2) currentWorkingGuideObject.transform.localScale = currentWorkingSampleObject.transform.localScale;

        //rotating object
        else if (gameStep == 3) StartCoroutine(RotateGuideObject());

        //move the guide object position to right place based on the sample figure
        else if (gameStep == 4) currentWorkingGuideObject.transform.position = currentWorkingSampleObject.transform.position;
    }

    public void GameStepCheck(int currentGameStep, GameObject workspaceObjectGuide, GameObject networkObject)
    {
        if (currentGameStep == 1)//created object check
        {
            instantiatedObjects = GameObject.FindGameObjectsWithTag("InstantiatedObject");
            //check it has same mesh filter name (cube, cylinder, sphere)
            if (workspaceObjectGuide.GetComponent<MeshFilter>().mesh.name == networkObject.GetComponent<MeshFilter>().mesh.name && (instantiatedObjects.Length == 1))
            {
                gameStep++; // go to next step
                step1Timer = tempTime; //save the time
                WorkspaceInitialize(sampleNum, objectNum, gameStep);
                PV.RPC("ConfirmedSoundPlay", RpcTarget.AllBuffered);
            }
            else // if incorrect object was instantiated, destroy all network object
            {
                PV.RPC("RejectSoundPlay", RpcTarget.AllBuffered);
                foreach (GameObject instantiatedObject in instantiatedObjects)
                {
                    PhotonNetwork.Destroy(instantiatedObject);
                }
            }
        }
        else if (currentGameStep == 2)//scaled object check
        {
            if (networkObject.transform.localScale == workspaceObjectGuide.transform.localScale)
            {
                gameStep++;
                EnableKnob(true);//enable knob for next step
                resetRotatingFlag = true; //reset rotating status to avoid some error
                step2Timer = tempTime - step1Timer;
                WorkspaceInitialize(sampleNum, objectNum, gameStep);
                PV.RPC("ConfirmedSoundPlay", RpcTarget.AllBuffered);
            }
            else
            {
                //maybe some message popup that somethings wrong.
                PV.RPC("RejectSoundPlay", RpcTarget.AllBuffered);
            }
        }
        else if (currentGameStep == 3)//rotated object check
        {
            if (networkObject.transform.eulerAngles == workspaceObjectGuide.transform.eulerAngles)
            {
                positionFixedFlag = true; //to position set
                gameStep++;
                EnableKnob(false);//disable knob for next step
                step3Timer = tempTime - step2Timer;
                WorkspaceInitialize(sampleNum, objectNum, gameStep);
                PV.RPC("ConfirmedSoundPlay", RpcTarget.AllBuffered);
            }
            else //reset the euler angles
            {
                networkObject.transform.eulerAngles = new Vector3(0, 0, 0);
                resetRotatingFlag = true;
                PV.RPC("RejectSoundPlay", RpcTarget.AllBuffered);
            }
        }
        else if (currentGameStep == 4)// positioned object check
        {
            if (Vector3.Distance(currentWorkingGuideObject.transform.localPosition, networkObject.transform.localPosition) < 0.001f)
            {
                currentWorkingNetoworkObject.tag = "completedObject"; //change the tag so that network object is not overlapped from other step and level
                step4Timer = tempTime - step3Timer;
                completionTime = tempTime; //save total time for each level

                if (objectNum == 4) // if all objects are done
                {
                    Debug.Log("Level Done");
                    objectNum = 0; //set to 0 for another level
                    gameStep = 0;
                    currentWorkingGuideObject.SetActive(false); //disable guide object when the level is completed
                    //do some effect and remove all the objects created.
                    PV.RPC("DisableLightString", RpcTarget.AllBuffered);
                    StartCoroutine(LevelCompleteEffect());
                }
                else
                {
                    Debug.Log("Next object start");
                    objectNum++; //otherwise, increase the number to move onto next object to finish the current level
                    gameStep = 1; //start from create object
                    WorkspaceInitialize(sampleNum, objectNum, gameStep);
                    PV.RPC("ConfirmedSoundPlay", RpcTarget.AllBuffered);
                }
            }

            else
            {
                PV.RPC("RejectSoundPlay", RpcTarget.AllBuffered);
            }

        }
    }
    public void PlayerSetting()
    {        
        if (LobbyNetworkManager.userType == 1) // reseracher uses left hand only
        {
            HankOVRCameraRig.transform.position = researcherTf.position; //move the position to researcher position

            leftHand.SetActive(true);
            rightHand.SetActive(false);

            if (LobbyNetworkManager.interactionType == 1) //if is controller setting
            {
                leftHandTracking.SetActive(false);
                leftControllerHand.SetActive(true);
            }
            else // if it is hand tracking setting
            {
                leftHandTracking.SetActive(true);
                leftControllerHand.SetActive(false);
            }
        }

        if (LobbyNetworkManager.userType == 2) // participant uses right hand only
        {
            HankOVRCameraRig.transform.position = participantTf.position; //move the position to participant position

            leftHand.SetActive(false);
            rightHand.SetActive(true);

            if(workspaceCubeGuide == null) // so that when player change the interaction type, guide object is not instantiated again
            {
                StartCoroutine(InstantiateGuide()); //instantiate after few seconds so that network objects are instantiated after the clinet joins the room.
            }
                   
            if (LobbyNetworkManager.interactionType == 1) //if is controller setting
            {
                rightHandTracking.SetActive(false);
                rightControllerHand.SetActive(true);
            }
            else // if it is hand tracking setting
            {
                rightHandTracking.SetActive(true);
                rightControllerHand.SetActive(false);
            }
        }
    }
    public void EnableKnob(bool x)
    {
        if (LobbyNetworkManager.interactionType == 1) controllerKnob.SetActive(x);
        else if (LobbyNetworkManager.interactionType == 2) handTrackingKnob.SetActive(x);
    }
    IEnumerator IdentifyingKnob()
    {
        yield return new WaitForSeconds(1.5f);
        if (LobbyNetworkManager.userType == 2) //only for the participant
        {
            controllerKnob = GameObject.FindGameObjectWithTag("controllerKnob");
            handTrackingKnob = GameObject.FindGameObjectWithTag("handTrackingKnob");
        }

        EnableKnob(false); //disable the at the beginning
    }
    IEnumerator InstantiateGuide()//instantatiate after few seconds after client joins the room
    {
        yield return new WaitForSeconds(3f);
        workspaceCubeGuide = PhotonNetwork.Instantiate("GuideCube", new Vector3(0, 1, 0.5f), Quaternion.identity);
        workspaceCubeGuide.SetActive(false); //will be true when the level start
        workspaceCylinderGuide = PhotonNetwork.Instantiate("GuideCylinder", new Vector3(0, 1, 0.5f), Quaternion.identity);
        workspaceCylinderGuide.SetActive(false);
        workspaceSphereGuide = PhotonNetwork.Instantiate("GuideSphere", new Vector3(0, 1, 0.5f), Quaternion.identity);
        workspaceSphereGuide.SetActive(false);
    }
    IEnumerator RotateGuideObject()
    {
        while (currentWorkingGuideObject.transform.eulerAngles != currentWorkingSampleObject.transform.eulerAngles)
        {
            currentWorkingGuideObject.transform.rotation = Quaternion.Lerp(currentWorkingGuideObject.transform.rotation, currentWorkingSampleObject.transform.rotation, Time.deltaTime * 2.5f);
            yield return null;
        }
    }
    IEnumerator LevelCompleteEffect()
    {
        BackgroundCubeColor.colorChangeTime = 0.1f;
        BackgroundCubeColor.numberOfCubeColorChanged = 30;
        roomAudioSource.PlayOneShot(levelCompleteSound);
        bgmAudioSource.Pause(); //stop bgm while the levelCompleteSound is playing
        yield return new WaitForSeconds(8f);
        bgmAudioSource.Play(); //start the bgm again.
        completedObjects = GameObject.FindGameObjectsWithTag("completedObject");
        foreach (GameObject completedObject in completedObjects)
        {
            PhotonNetwork.Destroy(completedObject);
        }
        BackgroundCubeColor.colorChangeTime = 2f;
        BackgroundCubeColor.numberOfCubeColorChanged = 6;
    }
    public void RoomGAmeManagerSoundPlay(AudioClip clip)
    {
        roomAudioSource.PlayOneShot(clip);
    }

    [PunRPC]
    public void ConfirmedSoundPlay()
    {
        RoomGAmeManagerSoundPlay(confirmSound);
    }
    [PunRPC]
    public void RejectSoundPlay()
    {
        RoomGAmeManagerSoundPlay(rejectSound);
    }
    [PunRPC]
    public void PositionSoundPlay()
    {
        RoomGAmeManagerSoundPlay(positionSound);
    }
    [PunRPC]
    public void RPCLevelCompleteEffectPlay()
    {
        StartCoroutine("RPCLevelCompleteEffect");
    }
    [PunRPC]
    IEnumerator RPCLevelCompleteEffect()
    {
        BackgroundCubeColor.colorChangeTime = 0.1f;
        BackgroundCubeColor.numberOfCubeColorChanged = 30;
        roomAudioSource.PlayOneShot(levelCompleteSound);
        bgmAudioSource.Pause(); //stop bgm while the levelCompleteSound is playing
        yield return new WaitForSeconds(8f);
        bgmAudioSource.Play(); //start the bgm again.
        completedObjects = GameObject.FindGameObjectsWithTag("completedObject");
        foreach (GameObject completedObject in completedObjects)
        {
            PhotonNetwork.Destroy(completedObject);
        }
        BackgroundCubeColor.colorChangeTime = 2f;
        BackgroundCubeColor.numberOfCubeColorChanged = 6;
    }
    [PunRPC]
    public void DisableLightString()
    {
        handToHandLightString.SetActive(false);
    }
    
}