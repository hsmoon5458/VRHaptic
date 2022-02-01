using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FingertipBehavior : MonoBehaviour
{
    public static bool thumbTouchedThumb, thumbTouchedIndex, indexTouchedThumb, indexTouchedIndex, myPinchingStatus, networkPinchingStatus;
    private bool vibrationFlag;
    private GameObject gameManager;
    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1) gameManager = GameObject.Find("GameManager");
    }
    void Update()
    {
        //check vibrationflag continously, it shuold be working in room1 only
        if (SceneManager.GetActiveScene().buildIndex == 1) vibrationFlag = gameManager.GetComponent<RoomGameManager>().vibrationFlag;        

        if (RoomGameManager.gameStep == 0) //reset before go into step 1.
        {
            thumbTouchedThumb = false;
            thumbTouchedIndex = false;
            indexTouchedThumb = false;
            indexTouchedIndex = false;
        }
        //to disable pinching and scaling
        if (!myPinchingStatus || !networkPinchingStatus)
        {
            NetworkObjectsManager.xAxisScalingEnabledFlag = false;
            NetworkObjectsManager.yAxisScalingEnabledFlag = false;
            NetworkObjectsManager.zAxisScalingEnabledFlag = false;
        }
    }

    //right hand setup only (participant side only)
    private void OnTriggerEnter(Collider other)
    {
        //for creating object at participant side
        if (this.gameObject.name == "R1Tip")
        {
            if (other.gameObject.name == "L1TipNetwork")
            {
                thumbTouchedThumb = true;
                if (vibrationFlag)
                {
                    VibrationManager.singletone.TriggerVibration(3, OVRInput.Controller.RTouch);
                    VibrationManager.singletone.FingerTipVibration(3);
                }
                
            }
            if (other.gameObject.name == "L2TipNetwork")
            {
                thumbTouchedIndex = true;
                if (vibrationFlag)
                {
                    VibrationManager.singletone.TriggerVibration(3, OVRInput.Controller.RTouch);
                    VibrationManager.singletone.FingerTipVibration(3);
                }
                
            }
        }
        if (this.gameObject.name == "R2Tip")
        {
            if (other.gameObject.name == "L1TipNetwork")
            {
                indexTouchedThumb = true;
                if (vibrationFlag)
                {
                    VibrationManager.singletone.TriggerVibration(3, OVRInput.Controller.RTouch);
                    VibrationManager.singletone.FingerTipVibration(3);
                }
                
            }
            if (other.gameObject.name == "L2TipNetwork")
            {
                indexTouchedIndex = true;
                if (vibrationFlag)
                {
                    VibrationManager.singletone.TriggerVibration(3, OVRInput.Controller.RTouch);
                    VibrationManager.singletone.FingerTipVibration(3);
                }
                
            }
        }

        //for pinching and scaling
        if (this.gameObject.name == "R2Tip")
        {
            if (other.gameObject.name == "R1Tip")
            {
                myPinchingStatus = true;
                
            }
        }

        //network player pinching
        if (this.gameObject.name == "L2TipNetwork")
        {
            if (other.gameObject.name == "L1TipNetwork")
            {
                networkPinchingStatus = true;
            }
        }
        #region LeftControllerVibrate
        /*
        //Left hand side (just for the left hand controller vibration)
        if (this.gameObject.name == "L1Tip")
        {
            if (other.gameObject.name == "R1Tip")
            {
                if (vibrationFlag)
                {
                    VibrationManager.singletone.TriggerVibration(3, OVRInput.Controller.LTouch);
                }
            }
            if (other.gameObject.name == "R2Tip")
            {
                if (vibrationFlag)
                {
                    VibrationManager.singletone.TriggerVibration(3, OVRInput.Controller.LTouch);
                }
            }
        }
        if (this.gameObject.name == "L2Tip")
        {
            if (other.gameObject.name == "R1Tip") 
            {
                if (vibrationFlag)
                {
                    VibrationManager.singletone.TriggerVibration(3, OVRInput.Controller.LTouch);
                }
            }
            if (other.gameObject.name == "R2Tip") 
            {
                if (vibrationFlag)
                {
                    VibrationManager.singletone.TriggerVibration(3, OVRInput.Controller.LTouch);
                }
            }
        }

        if (this.gameObject.name == "L2Tip")
        {
            if (other.gameObject.name == "L1Tip") 
            {
                if (vibrationFlag)
                {
                    VibrationManager.singletone.TriggerVibration(3, OVRInput.Controller.LTouch);
                }
            }
        }
        */
        #endregion
    }

    private void OnTriggerExit(Collider other)
    {
        if (this.gameObject.name == "R2Tip")
        {
            if (other.gameObject.name == "R1Tip")
            {
                myPinchingStatus = false;
            }
        }
        //network player pinching
        if (this.gameObject.name == "L2TipNetwork")
        {
            if (other.gameObject.name == "L1TipNetwork")
            {
                networkPinchingStatus = false;
            }
        }

        //vibration off
        if (this.gameObject.name == "R1Tip")
        {
            if (other.gameObject.name == "L1TipNetwork")
            {
                if (vibrationFlag)
                {
                    VibrationManager.singletone.TriggerVibration(9, OVRInput.Controller.RTouch);
                    VibrationManager.singletone.FingerTipVibration(9);
                }
            }
            if (other.gameObject.name == "L2TipNetwork")
            {
                if (vibrationFlag)
                {
                    VibrationManager.singletone.TriggerVibration(9, OVRInput.Controller.RTouch);
                    VibrationManager.singletone.FingerTipVibration(9);
                }
            }
        }
        if (this.gameObject.name == "R2Tip")
        {
            if (other.gameObject.name == "L1TipNetwork")
            {
                if (vibrationFlag)
                {
                    VibrationManager.singletone.TriggerVibration(9, OVRInput.Controller.RTouch);
                    VibrationManager.singletone.FingerTipVibration(9);
                }
            }

            if (other.gameObject.name == "L2TipNetwork")
            {
                if (vibrationFlag)
                {
                    VibrationManager.singletone.TriggerVibration(9, OVRInput.Controller.RTouch);
                    VibrationManager.singletone.FingerTipVibration(9);
                }
            }
        }


        #region LeftHandVibrate
        /*
        //Left hand side (just for the left hand controller vibration)
        if (this.gameObject.name == "L1Tip")
        {
            if (vibrationFlag)
            {
                if (other.gameObject.name == "R1Tip") VibrationManager.singletone.TriggerVibration(9, OVRInput.Controller.LTouch);
                if (other.gameObject.name == "R2Tip") VibrationManager.singletone.TriggerVibration(9, OVRInput.Controller.LTouch);
            }
        }
        if (this.gameObject.name == "L2Tip")
        {
            if (vibrationFlag)
            {
                if (other.gameObject.name == "R1Tip") VibrationManager.singletone.TriggerVibration(9, OVRInput.Controller.LTouch);
                if (other.gameObject.name == "R2Tip") VibrationManager.singletone.TriggerVibration(9, OVRInput.Controller.LTouch);
            }
        }

        if (this.gameObject.name == "L2Tip")
        {
            if (vibrationFlag)
            {
                if (other.gameObject.name == "L1Tip") VibrationManager.singletone.TriggerVibration(9, OVRInput.Controller.LTouch);
            }
        }
        */
        #endregion
    }
}




/*
   //this is for vibration only.
   private void OnTriggerStay(Collider other)
   {
       //Right hand side
       if (this.gameObject.name == "R1Tip")
       {
           if (other.gameObject.name == "L1TipNetwork") VibrationManager.singletone.TriggerVibration(vib_itr, vib_freq, vib_amp, OVRInput.Controller.RTouch);
           if (other.gameObject.name == "L2TipNetwork") VibrationManager.singletone.TriggerVibration(vib_itr, vib_freq, vib_amp, OVRInput.Controller.RTouch);  
       }
       if (this.gameObject.name == "R2Tip")
       {
           if (other.gameObject.name == "L1TipNetwork") VibrationManager.singletone.TriggerVibration(vib_itr, vib_freq, vib_amp, OVRInput.Controller.RTouch);
           if (other.gameObject.name == "L2TipNetwork") VibrationManager.singletone.TriggerVibration(vib_itr, vib_freq, vib_amp, OVRInput.Controller.RTouch);
       }
       if (this.gameObject.name == "R2Tip")
       {
           if (other.gameObject.name == "R1Tip") VibrationManager.singletone.TriggerVibration(vib_itr, vib_freq, vib_amp, OVRInput.Controller.RTouch);
       }

       //Left hand side
       if (this.gameObject.name == "L1Tip")
       {
           if (other.gameObject.name == "R1Tip") VibrationManager.singletone.TriggerVibration(vib_itr, vib_freq, vib_amp, OVRInput.Controller.LTouch);
           if (other.gameObject.name == "R2Tip") VibrationManager.singletone.TriggerVibration(vib_itr, vib_freq, vib_amp, OVRInput.Controller.LTouch);
       }
       if (this.gameObject.name == "L2Tip")
       {
           if (other.gameObject.name == "R1Tip") VibrationManager.singletone.TriggerVibration(vib_itr, vib_freq, vib_amp, OVRInput.Controller.LTouch);
           if (other.gameObject.name == "R2Tip") VibrationManager.singletone.TriggerVibration(vib_itr, vib_freq, vib_amp, OVRInput.Controller.LTouch);
       }

       if (this.gameObject.name == "L2Tip")
       {
           if (other.gameObject.name == "L1Tip") VibrationManager.singletone.TriggerVibration(vib_itr, vib_freq, vib_amp, OVRInput.Controller.LTouch);
       }
   }
   */
