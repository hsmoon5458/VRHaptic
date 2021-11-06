using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveEffect : MonoBehaviour
{
    Material dissolveMaterial;
    private float t = 0;
    void Start()
    {
        dissolveMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        dissolveMaterial.SetFloat("_DissolveAmount", 1 - t);
        if(t >= 1)
        {
            this.enabled = false;
        }
    }
}
