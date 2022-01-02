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

    public GameObject leftHand, rightHand;
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
        }

        if (LobbyNetworkManager.userType == 2) // participant uses right hand only
        {
            leftHand.SetActive(false);
            rightHand.SetActive(true);
        }
    }
    
    void Update()
    {
        tempTime += Time.deltaTime;

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
