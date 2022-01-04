using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float completionTime, tempTime;
    private int gameStep = 0; //1 is creating, 2 is scaling, 3 rotating, 4 is positioning

    public GameObject leftHand, rightHand, leftHandController, rightHandController, leftHandTracking, rightHandTracking;
    public TextMeshProUGUI xtmp, ytmp, ztmp;

    //test code
    public GameObject myLeftHand;
    private GameObject networkLeftHand, networkRightHand;
    //test code end
    void Start()
    {
        if(LobbyNetworkManager.userType == 1) // reseracher uses left hand only
        {
            leftHand.SetActive(true);
            rightHand.SetActive(false);

            if(LobbyNetworkManager.interactionType == 1) //if is controller setting
            {
                leftHandController.SetActive(true);
                leftHandTracking.SetActive(false);
            }
            else // if it is hand tracking setting
            {
                leftHandController.SetActive(false);
                leftHandTracking.SetActive(true);
            }
        }

        if (LobbyNetworkManager.userType == 2) // participant uses right hand only
        {
            leftHand.SetActive(false);
            rightHand.SetActive(true);

            if (LobbyNetworkManager.interactionType == 1) //if is controller setting
            {
                rightHandController.SetActive(true);
                rightHandTracking.SetActive(false);
            }
            else // if it is hand tracking setting
            {
                rightHandController.SetActive(false);
                rightHandTracking.SetActive(true);
            }
        }
    }
    
    void Update()
    {
        /*
        button_A = OVRInput.Get(OVRInput.Button.One);
        button_B = OVRInput.Get(OVRInput.Button.Two);
        button_X = OVRInput.Get(OVRInput.Button.Three);
        button_Y = OVRInput.Get(OVRInput.Button.Four);
        
        tempTime += Time.deltaTime;
        */
        //test code
        try
        {
            /*
            xtmp.text = RotatingKnob..transform.eulerAngles.x.ToString("F0");
            ytmp.text = RotatingKnob.testKnobParent.transform.eulerAngles.y.ToString("F0");
            ztmp.text = RotatingKnob.testKnobParent.transform.eulerAngles.z.ToString("F0");
            */
        }
        catch
        {

        }
            

        if (Input.GetKeyDown("e"))
        {
            myLeftHand.SetActive(true);
            networkLeftHand = PhotonView.Find(1001).gameObject.transform.GetChild(0).gameObject;
            networkLeftHand.SetActive(true);
            networkRightHand = PhotonView.Find(1001).gameObject.transform.GetChild(1).gameObject;
            networkRightHand.SetActive(true);
        }
        //test code end
    }

    public void LevelCompleted()
    {
        completionTime = tempTime;
        string minutes = Mathf.Floor(completionTime / 60).ToString("00");
        string seconds = (completionTime % 60).ToString("00");
    }
    
}
