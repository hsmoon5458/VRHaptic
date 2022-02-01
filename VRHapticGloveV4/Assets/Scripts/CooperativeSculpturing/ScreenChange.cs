using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScreenChange : MonoBehaviour
{
    public GameObject environments;
    public GameObject quadBackground, quadLogo;
    public Material backgroundMat;

    private Material logoMat;

    public UnityEvent invoke;
    private void Start()
    {
        logoMat = quadLogo.GetComponent<Material>();
    }

    public void ScreenFadeOut()
    {
        environments.SetActive(false);

        quadBackground.SetActive(true);
        quadLogo.SetActive(true);

        StartCoroutine(SceneChangeAfterColorFade());
    }
    IEnumerator SceneChangeAfterColorFade()
    {
        float ElapsedTime = 0;
        while (ElapsedTime < 2.5f)
        {
            ElapsedTime += Time.deltaTime;
            backgroundMat.color = Color.Lerp(new Vector4(255, 255, 255, 0), new Vector4(255, 255, 255, 1), (ElapsedTime / 2.5f));
            //logoMat.color = Color.Lerp(new Vector4(255, 255, 255, 0), new Vector4(255, 255, 255, 1), (ElapsedTime / 2.5f));
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        invoke.Invoke();
    }
}


