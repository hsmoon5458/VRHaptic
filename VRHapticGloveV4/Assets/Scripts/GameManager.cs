using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float completionTime, tempTime;
    private int gameStep = 0; //1 is creating, 2 is scaling, 3 rotating, 4 is positioning
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tempTime += Time.deltaTime;
    }

    public void LevelCompleted()
    {
        completionTime = tempTime;
        string minutes = Mathf.Floor(completionTime / 60).ToString("00");
        string seconds = (completionTime % 60).ToString("00");
    }
}
