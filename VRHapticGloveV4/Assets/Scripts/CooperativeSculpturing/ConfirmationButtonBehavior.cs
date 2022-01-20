using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConfirmationButtonBehavior : MonoBehaviour
{
    public bool confirmButtonFlag; // this will be true by RoomGameManager when confirmation flag is ture.
    public static bool leftConfirmation, rightConfirmation;
    void Update()
    {
        if(leftConfirmation && rightConfirmation)
        {
            confirmButtonFlag = true;
            leftConfirmation = false;
            rightConfirmation = false;
            StartCoroutine(ConfirmTure());
        }
        else if (leftConfirmation && !rightConfirmation) this.gameObject.GetComponent<Renderer>().material.color = Color.yellow;

        else if (!leftConfirmation && rightConfirmation) this.gameObject.GetComponent<Renderer>().material.color = Color.yellow;

        else if(!leftConfirmation && !rightConfirmation) this.gameObject.GetComponent<Renderer>().material.color = Color.white;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "L2TipNetwork") leftConfirmation = true;
        if (other.gameObject.name == "R2Tip") rightConfirmation = true;
    }

    IEnumerator ConfirmTure()
    {
        Debug.Log("True");
        this.gameObject.GetComponent<Renderer>().material.color = Color.green;
        yield return new WaitForSeconds(1f);
        this.gameObject.GetComponent<Renderer>().material.color = Color.white;
        confirmButtonFlag = false;
    }

}
