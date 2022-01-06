using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ControllerHandTrackingConvert : MonoBehaviour
{
    public UnityEvent onHandsActive;
    public UnityEvent onControllersActive;


    // Update is called once per frame
    void Update()
    {
    if (OVRInput.IsControllerConnected(OVRInput.Controller.Hands))
        {
            onHandsActive.Invoke();
        }
        else
        {
            onControllersActive.Invoke();
        }
    }
}
