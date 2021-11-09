using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class ButtonsBehavior : MonoBehaviour
{
    public UnityEvent onClick;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        

    }
    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.name == "L2Tip" || other.gameObject.name == "R2Tip")
        {
            onClick.Invoke();
        }
    }
}
