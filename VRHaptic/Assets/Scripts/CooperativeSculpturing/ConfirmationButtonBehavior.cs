using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConfirmationButtonBehavior : MonoBehaviour
{
    public static bool confirmButtonFlag; // this will be true by RoomGameManager when confirmation flag is ture.
    public  bool leftConfirmation, rightConfirmation;
    private bool corutineCheck = false;
    void Update()
    {
        if(leftConfirmation && rightConfirmation && !corutineCheck)
        {
            confirmButtonFlag = true;
            corutineCheck = true;
            StartCoroutine(ConfirmTure());
        }
        /*
        else if (leftConfirmation && !rightConfirmation) this.gameObject.GetComponent<Renderer>().material.color = Color.yellow;

        else if (!leftConfirmation && rightConfirmation) this.gameObject.GetComponent<Renderer>().material.color = Color.yellow;

        else this.gameObject.GetComponent<Renderer>().material.color = Color.white;
        */
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "L2TipNetwork") leftConfirmation = true;
        if (other.gameObject.name == "R2Tip") rightConfirmation = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "L2TipNetwork") leftConfirmation = false;
        if (other.gameObject.name == "R2Tip") rightConfirmation = false;
    }
    IEnumerator ConfirmTure()
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.green;
        yield return new WaitForSeconds(1.5f);
        leftConfirmation = false;
        rightConfirmation = false;
        corutineCheck = false; // to avoid excute this function multiple times
        this.gameObject.GetComponent<Renderer>().material.color = Color.white;
        //confirmButtonFlag = false; //falsed in RoomGameManager
    }

}
