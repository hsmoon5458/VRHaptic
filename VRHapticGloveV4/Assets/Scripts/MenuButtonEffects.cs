using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonEffects : MonoBehaviour
{
    public ParticleSystem S1L1_effect, S1L2_effect, S2L1_effect, S2L2_effect, S3L1_effect, S3L2_effect, tutorial_effect, stop_effect;
    void Update()
    {
        if (LaunchPadFingerTrigger.song1_lv1)
        {
            S1L1_effect.Play();
        }

        if (LaunchPadFingerTrigger.song1_lv2)
        {
            S1L2_effect.Play();
        }

        if (LaunchPadFingerTrigger.song2_lv1)
        {
            S2L1_effect.Play();
        }

        if (LaunchPadFingerTrigger.song2_lv2)
        {
            S2L2_effect.Play();
        }

        if (LaunchPadFingerTrigger.song3_lv1)
        {
            S3L1_effect.Play();
        }

        if (LaunchPadFingerTrigger.song3_lv2)
        {
            S3L2_effect.Play();
        }

        if (LaunchPadFingerTrigger.tutorial)
        {
            tutorial_effect.Play();
        }

        if (LaunchPadFingerTrigger.stop_button)
        {
            stop_effect.Play();
        }
    }
}
