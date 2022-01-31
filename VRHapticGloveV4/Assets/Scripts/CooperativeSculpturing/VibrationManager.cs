using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Ports;

public class VibrationManager : MonoBehaviour
{
    public const float FADE_TIME = 0.5f;
    public const float MAX_PWM = 1f;
    private bool cont = true;
    private bool noShutDown = true;
    //private int prev_received = -1;
    private double sqrt_const = (MAX_PWM / Math.Sqrt(FADE_TIME * 2));
    private float current_amp;
    private float temp_time;

    SerialPort data_stream = new SerialPort("COM4", 115200);

    public static VibrationManager singletone;
    void Start()
    {
        if (!data_stream.IsOpen)
        {
            data_stream.Open(); //Initiate the Serial stream
        }
        
        if (singletone && singletone != this)
        {
            Destroy(this);
        }
        
        else
            singletone = this;
    }
    void Update()
    {
        temp_time += Time.deltaTime;
        if(Input.GetKeyDown("v")) data_stream.WriteLine("L3");
        if (Input.GetKeyDown("b")) data_stream.WriteLine("L9");
    }
    public void CloseIOPort()
    {
        data_stream.Close();
    }
    public void FingerTipVibration(int interaction)
    {
        data_stream.WriteLine("L" + interaction.ToString());        
    }

    public void TriggerVibration(int interaction, OVRInput.Controller controller)
    {
        cont = false;
        switch (interaction)
        {
            case 0:
                //transition to off state
                StartCoroutine(transitionToVibrationPower(0, controller));
                break;
            case 1:
                //transition to 25% state
                StartCoroutine(transitionToVibrationPower(1, controller));
                break;
            case 2:
                //transition to 50% state
                StartCoroutine(transitionToVibrationPower(2, controller));
                break;
            case 3:
                //transition to 75% state
                StartCoroutine(transitionToVibrationPower(3, controller));
                break;
            case 4:
                //transition to 100% state
                StartCoroutine(transitionToVibrationPower(4, controller));
                break;
            case 5:
                //three bursts
                StartCoroutine(threeQuickBursts(controller));
                break;
            case 6:
                //sin wave
                StartCoroutine(sinWave(controller));
                break;
            case 7:
                StartCoroutine(sqrtFade(controller));
                //sqrt off
                break;
            case 8:
                StartCoroutine(zipzap(controller));
                //zipzap
                break;
            case 9:
                //immediate cut
                ControllerVibration(0, controller);
                break;
        }
    }
    public void ControllerVibration(float amp, OVRInput.Controller controller)
    {
        OVRInput.SetControllerVibration(1, amp, controller);
    }

    IEnumerator transitionToVibrationPower(float goal, OVRInput.Controller controller)
    {
        //keep a running time of when this started
        float start_time = temp_time;
        float final_v = goal * (MAX_PWM / 4);
        float prev_v = current_amp;
        while (temp_time - start_time < FADE_TIME && noShutDown)
        {
            float time_c = (float)temp_time - start_time;
            float target = (time_c / FADE_TIME);
            target = target * (final_v - prev_v);
            current_amp = prev_v + (target);
            ControllerVibration(current_amp, controller);
            yield return null;
        }
    }

    IEnumerator sqrtFade(OVRInput.Controller controller)
    {
        //sqrt fade from high pwm to zero
        float prev_millis = temp_time;
        while ((temp_time - prev_millis) <= (FADE_TIME * 2f) && noShutDown)
        {
            double sqrt_curr = Math.Sqrt((FADE_TIME * 2f) - (temp_time - prev_millis));
            
            float target = (float)(sqrt_const * sqrt_curr);
            Debug.Log(target);
            current_amp = target;
            ControllerVibration(current_amp, controller);
            yield return null;
        }
        current_amp = 0;
        ControllerVibration(current_amp, controller);
    }

    IEnumerator sinWave(OVRInput.Controller controller)
    {
        cont = true;
        while (cont)
        {
            current_amp = (float)(Math.Sin(temp_time / 0.15f));
            ControllerVibration(current_amp, controller);
            yield return null;
        }
    }

    IEnumerator threeQuickBursts(OVRInput.Controller controller)
    {
        float prev_millis = temp_time;
        float diff = temp_time - prev_millis;
        float loop_amp = 1f;
        while (diff < 0.75f && noShutDown)
        {
            diff = temp_time - prev_millis;
            if (diff < 0.15f) //first burst
            {
                loop_amp = 1f;
            }
            else if (diff < 0.3f) 
            {
                loop_amp = 0f;
            }else if (diff < 0.45f) //second burst
            {
                loop_amp = 1f;
            }
            else if (diff < 0.6f)
            {
                loop_amp = 0f;
            }
            else //less than 0.75f //third burst
            {
                loop_amp = 1f;
            }
            if (current_amp != loop_amp)
            {
                current_amp = loop_amp;
                ControllerVibration(current_amp, controller);
            }
            yield return null;
        }
        current_amp = 0; //reset the vibration before quit
        ControllerVibration(current_amp, controller);
    }

    IEnumerator zipzap(OVRInput.Controller controller)
    {
        System.Random rand = new System.Random();
        cont = true;
        float prev_millis = temp_time;
        float rand_cycletime = rand.Next(15, 40)/100f;
        while (cont)
        {
            if ((temp_time - prev_millis) >= rand_cycletime)
            {
                rand_cycletime = rand.Next(15, 40)/100f;
                prev_millis = temp_time;
                current_amp = rand.Next(1, 8) / 8f;
                ControllerVibration(current_amp, controller);
            }
            yield return null;
        }
    }
}