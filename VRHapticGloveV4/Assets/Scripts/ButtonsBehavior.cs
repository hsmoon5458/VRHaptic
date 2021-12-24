using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class ButtonsBehavior : MonoBehaviour
{
    public UnityEvent onClick;

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.name == "L2Tip" || other.gameObject.name == "R2Tip")
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.green;
            onClick.Invoke();
        }
    }
}
