using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCubeColor : MonoBehaviour
{

    public GameObject[] backgroundCubes;

    //[Range(0.1f, 2f)]
    public static float colorChangeTime = 2f;
    private float countTime;
    private int[] currentIndex, previousIndex;
    //[Range(1, 62)]
    public static int numberOfCubeColorChanged = 6;
    private Color defaultColor;
    void Start()
    {
        currentIndex = new int[numberOfCubeColorChanged];
        previousIndex = new int[numberOfCubeColorChanged];
        ColorUtility.TryParseHtmlString("#363637", out defaultColor);

        
    }
    void Update()
    {
        countTime += Time.deltaTime;
        if (countTime > colorChangeTime)
        {
            countTime = 0;
                        
            for (int i = 0; i < numberOfCubeColorChanged; i++)
            {
                try { backgroundCubes[previousIndex[i]].GetComponent<Renderer>().material.color = defaultColor; }
                catch{
                    currentIndex = new int[numberOfCubeColorChanged];
                    previousIndex = new int[numberOfCubeColorChanged];
                }
            }

            //initialize the first index, and times 10 for rest of the 5 with modulo 
            currentIndex[0] = Random.Range(0, backgroundCubes.Length); //random num

            //change the color
            backgroundCubes[currentIndex[0]].GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

            for (int i = 1; i < numberOfCubeColorChanged; i++) //randomly assign 6 index
            {
                currentIndex[i] = (currentIndex[0] + i*10) % backgroundCubes.Length;
                //change the color
                backgroundCubes[currentIndex[i]].GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            }

            //save index to make it default 
            previousIndex = currentIndex;
        }
    }
}
