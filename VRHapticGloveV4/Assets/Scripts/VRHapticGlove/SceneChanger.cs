using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public ParticleSystem scenechange_effect;
    public Animator transition;
    private bool button_pushed = false;

    void Update()
    {
        if (button_pushed || Input.GetKeyDown("c"))
        {
            scenechange_effect.Play();

            StartCoroutine(LoadLevel());

            button_pushed = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        button_pushed = true;
    }

    IEnumerator LoadLevel ()
    {
        transition.SetTrigger("Start");

        //wait for 2 seconds
        yield return new WaitForSeconds(2f);

        //get the current index and load the scene
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        if (activeScene == 0) { SceneManager.LoadScene(1); }
        if (activeScene == 1) { SceneManager.LoadScene(0); }
    }
}
