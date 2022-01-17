using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class RoomGameManager : MonoBehaviour
{
    [SerializeField]
    private float completionTime, tempTime, step1Timer, step2Timer, step3Timer, step4Timer;
    [SerializeField]
    public static int gameStep = 0; //1 is creating, 2 is scaling, 3 rotating, 4 is positioning
    public int sampleNum, objectNum; // ex) Sample1, s3

    public GameObject leftHand, rightHand, leftHandTracking, rightHandTracking, leftControllerHand, rightControllerHand;
    public TextMeshProUGUI xtmp, ytmp, ztmp;
    public GameObject workspaceCubeGuide, workspaceCylinderGuide, workspaceSphereGuide;
    private GameObject currentWorkingSampleObject, currentWorkingGuideObject, currentWorkingNetoworkObject;//catch the sample object, and changed the guide object corresponds to game step and sample object transform.
    public bool confirmationFlag;
    public static bool stopRotatingFlag;

    public GameObject[] completedObjects; //completed networked objects
    public GameObject[] instantiatedObjects; //instantiated network objects
    public delegate void NetworkSettingDelegate(); //to recall the Network Setting for the script in "Network Player" prefab.
    public static NetworkSettingDelegate NetworkPlayerSettingDelegate;
    //test code
    public GameObject testFinger;
    private GameObject networkLeftHand;
    public GameObject testLeftAnchor;
    public GameObject testLeftControllerAnchor;
    public GameObject testLeftHandTracking;
    //test code end
    public void RefreshNetworkPlayerSetting()
    {
        NetworkPlayerSettingDelegate();
    }

    void Start()
    {
        PlayerSetting();
    }
    
    void Update()
    {
        //need to enable and diable knob at netwokrplayer correspond to game step here.

        //timer for performance measurement
        tempTime = Time.deltaTime;

        //test code
        try
        {   
            xtmp.text = RotatingKnob.tx.ToString("F0");
            ytmp.text = RotatingKnob.ty.ToString("F0");
            ztmp.text = RotatingKnob.tz.ToString("F0");
            
        }
        catch{}
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

        //test code end

        //confirm the current step whether it's done
        if (confirmationFlag || Input.GetKeyDown(KeyCode.Space))
        {
            currentWorkingNetoworkObject = GameObject.FindWithTag("InstantiatedObject");
            GameStepCheck(gameStep, currentWorkingSampleObject, currentWorkingNetoworkObject);
            confirmationFlag = false;
        }

        //Step 4: Snip movement to the position
        if(gameStep == 4)
        {
            if (Vector3.Distance(currentWorkingGuideObject.transform.position, currentWorkingNetoworkObject.transform.position) < 0.1f)
            {
                float step = 0.3f * Time.deltaTime;
                currentWorkingNetoworkObject.transform.position = Vector3.MoveTowards(currentWorkingNetoworkObject.transform.position, currentWorkingSampleObject.transform.position, step);
                //if it arrives to the target, locked in.
                if(Vector3.Distance(currentWorkingSampleObject.transform.position, currentWorkingNetoworkObject.transform.position) < 0.001f)
                {
                    currentWorkingNetoworkObject.transform.position = currentWorkingSampleObject.transform.position;
                }
            }
        }
    }

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
                workspaceCubeGuide.transform.position = new Vector3(0, 1, 0);
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
                workspaceCylinderGuide.transform.position = new Vector3(0, 1, 0);
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
                workspaceSphereGuide.transform.position = new Vector3(0, 1, 0);

                currentWorkingGuideObject = workspaceSphereGuide;
            }
            
        }

        //scaling object
        else if (gameStep == 2){currentWorkingGuideObject.transform.localScale = currentWorkingSampleObject.transform.localScale;}

        //rotating object
        else if (gameStep == 3)
        {
            StartCoroutine(RotateGuideObject());
            //currentWorkingGuideObject.transform.eulerAngles = currentWorkingSampleObject.transform.eulerAngles;
        }
        
        //move the guide object position to right place based on the sample figure
        else if (gameStep == 4){currentWorkingGuideObject.transform.position = currentWorkingSampleObject.transform.position; }
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
            }
            else // if incorrect object was instantiated, destroy all network object
            {
                foreach (GameObject instantiatedObject in instantiatedObjects)
                {
                    PhotonNetwork.Destroy(instantiatedObject);
                }
            }
        }
        else if(currentGameStep == 2)//scaled object check
        {
            if(networkObject.transform.localScale == workspaceObjectGuide.transform.localScale)
            {
                gameStep++;
                step2Timer = tempTime - step1Timer;
                WorkspaceInitialize(sampleNum, objectNum, gameStep);
            }
            else 
            {
                //maybe some message popup that somethings wrong.
            }
        }
        else if (currentGameStep == 3)//rotated object check
        {
            if (networkObject.transform.eulerAngles == workspaceObjectGuide.transform.eulerAngles)
            {
                gameStep++;
                step3Timer = tempTime - step2Timer;
                WorkspaceInitialize(sampleNum, objectNum, gameStep);
            }
            else //reset the euler angles
            {
                networkObject.transform.eulerAngles = new Vector3(0, 0, 0);
                stopRotatingFlag = true;
            }
        }
        else if(currentGameStep == 4)// positioned object check
        {
            if(Vector3.Distance(currentWorkingGuideObject.transform.localPosition, networkObject.transform.localPosition) < 0.001f)
            {
                currentWorkingNetoworkObject.tag = "completedObject"; //change the tag so that network object is not overlapped from other step and level
                step4Timer = tempTime - step3Timer;
                completionTime = tempTime; //save total time for each level

                if (objectNum == 4) // if all objects are done
                {
                    Debug.Log("Level Done");
                    objectNum = 0; //set to 0 for another level
                    gameStep = 0;

                    //do some effect and remove all the objects created.
                    StartCoroutine(LevelCompleteEffect());
                }
                else
                {
                    Debug.Log("Next object start");
                    objectNum++; //otherwise, increase the number to move onto next object to finish the current level
                    gameStep = 1; //start from create object
                    WorkspaceInitialize(sampleNum, objectNum, gameStep);
                }
            }

            else
            {
                Debug.Log("Not close enough");
            }
            
        }
    }
    public void PlayerSetting()
    {
        if (LobbyNetworkManager.userType == 1) // reseracher uses left hand only
        {
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
            leftHand.SetActive(false);
            rightHand.SetActive(true);

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
        yield return new WaitForSeconds(5f);
        completedObjects = GameObject.FindGameObjectsWithTag("completedObject");
        foreach (GameObject completedObject in completedObjects)
        {
            PhotonNetwork.Destroy(completedObject);
        }
        BackgroundCubeColor.colorChangeTime = 2f;
        BackgroundCubeColor.numberOfCubeColorChanged = 6;
    }
}
