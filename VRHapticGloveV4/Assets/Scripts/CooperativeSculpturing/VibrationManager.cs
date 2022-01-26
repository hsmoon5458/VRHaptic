using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    private float tempTime;
    public static VibrationManager singletone;
    void Start()
    {
        if (singletone && singletone != this)
        {
            Destroy(this);
        }

        else
            singletone = this;
    }
    private void Update()
    {
        tempTime += Time.deltaTime;
    }

    public void TriggerVibration(int iteration, int frequency, int strength, OVRInput.Controller controller)
    {
        OVRHapticsClip clip = new OVRHapticsClip();

        for (int i = 0; i < iteration; i++)
        {
            clip.WriteSample(i % frequency == 0 ? (byte)strength : (byte)0);
        }

        if (controller == OVRInput.Controller.LTouch)
        {
            OVRHaptics.LeftChannel.Preempt(clip);
        }

        else if (controller == OVRInput.Controller.RTouch)
        {
            OVRHaptics.RightChannel.Preempt(clip);
        }
    }
    public void ControllerVibration(float amp)
    {
        OVRInput.SetControllerVibration(1, amp, OVRInput.Controller.RTouch);
    }
}