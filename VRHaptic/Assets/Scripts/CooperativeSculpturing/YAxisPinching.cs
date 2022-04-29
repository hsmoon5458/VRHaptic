using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YAxisPinching : MonoBehaviour
{
    public static bool YScaling = false;
    private bool rightHandPinched, leftHandPinched;
    private bool vibrationFlag;
    private GameObject gameManager;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }
    void Update()
    {
        //check vibrationflag continously;
        vibrationFlag = gameManager.GetComponent<RoomGameManager>().vibrationFlag;

        if (rightHandPinched && leftHandPinched && !NetworkObjectsManager.yAxisScalingEnabledFlag)
        {
            YScaling = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "R2Tip")
        {
            if (FingertipBehavior.myPinchingStatus)
            {
                rightHandPinched = true;
                if (vibrationFlag)
                {
                    VibrationManager.singletone.TriggerVibration(6, OVRInput.Controller.RTouch);
                    VibrationManager.singletone.FingerTipVibration(6);
                }
            }
            else
            {
                rightHandPinched = false;
                if (vibrationFlag)
                {
                    VibrationManager.singletone.TriggerVibration(9, OVRInput.Controller.RTouch);
                    VibrationManager.singletone.FingerTipVibration(9);
                }
            }
        }

        if (other.gameObject.name == "L2TipNetwork")
        {
            if (FingertipBehavior.networkPinchingStatus)
            {
                leftHandPinched = true;
            }
            else
            {
                leftHandPinched = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "R2Tip")
        {
            if (FingertipBehavior.myPinchingStatus)
            {
                rightHandPinched = true;
            }
            else
            {
                rightHandPinched = false;
            }
        }

        if (other.gameObject.name == "L2TipNetwork")
        {
            if (FingertipBehavior.networkPinchingStatus)
            {
                leftHandPinched = true;
            }
            else
            {
                leftHandPinched = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "R2Tip")
        {
            if (FingertipBehavior.myPinchingStatus)
            {
                rightHandPinched = false;
            }
        }

        if (other.gameObject.name == "L2TipNetwork")
        {
            if (FingertipBehavior.networkPinchingStatus)
            {
                leftHandPinched = false;
            }
        }
    }
}
