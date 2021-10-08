using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CubeHit : MonoBehaviour
{
    //public static bool cube1_triggered, cube2_triggered, cube3_triggered, cube4_triggered, cube5_triggered;
    public ParticleSystem effect1, effect2, effect3, effect4, effect5;
    public TextMesh score, combo;
    public GameObject[] destination;
    public static string song_title = null; // song title
    public static int hit_count = 0; // hit count
    public static int combo_count = 0; // combo count
    public static float score_calculated = 0; // calculated score based on hit count and deviation
    public static float[] deviation = new float[200]; // deviation for each block in array
    public static float deviation_sum = 0; // to aggregate the deviation to get the score
    public static int deviation_index = 0; // to count the deviation
    private SongScore[] logs = new SongScore[7];
    float lastPressedTime = 0;

    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            writeAllSongData();
        }

        if (Input.GetKeyDown("s") && Time.time != lastPressedTime)
        {
            lastPressedTime = Time.time;
            //Debug.Log("we ran the if statement");
            saveDataToObject();
        }
        

        score_calculated = Mathf.Round((hit_count * 100f) - (deviation_sum * 100f)); //calculate the score
        score.text = score_calculated.ToString(); //save as a textmesh
        combo.text = combo_count.ToString(); // save as a textmesh
    }
    void OnTriggerStay(Collider other)
        {
        if (other.gameObject.name.Contains("cube1") && LaunchPadFingerTrigger.button1)
        {
            effect1.Play(); //effect
            deviation[deviation_index] = Vector3.Distance(this.gameObject.transform.position, other.gameObject.transform.position); // get the distance and save in the array
            deviation_sum = deviation_sum + deviation[deviation_index]; // to calculate the score, need to add up all the deviation.
            //Debug.Log(deviation[deviation_index]);
            deviation_index++; // increase the index
            //Debug.Log(deviation_index);
            Destroy(other.gameObject);
            hit_count++;
            combo_count++;
        }
        if (other.gameObject.name.Contains("cube2") && LaunchPadFingerTrigger.button2)
        {
            effect2.Play();
            deviation[deviation_index] = Vector3.Distance(this.gameObject.transform.position, other.gameObject.transform.position);
            deviation_sum = deviation_sum + deviation[deviation_index];
            //Debug.Log(deviation[deviation_index]);
            deviation_index++;
            //Debug.Log(deviation_index);
            Destroy(other.gameObject);
            hit_count++;
            combo_count++;
        }
        if (other.gameObject.name.Contains("cube3") && LaunchPadFingerTrigger.button3)
        {   
            effect3.Play();
            deviation[deviation_index] = Vector3.Distance(this.gameObject.transform.position, other.gameObject.transform.position);
            deviation_sum = deviation_sum + deviation[deviation_index];
            //Debug.Log(deviation[deviation_index]);
            deviation_index++;
            //Debug.Log(deviation_index);
            Destroy(other.gameObject);
            hit_count++;
            combo_count++;
        }
        if (other.gameObject.name.Contains("cube4") && LaunchPadFingerTrigger.button4)
        {
            effect4.Play();
            deviation[deviation_index] = Vector3.Distance(this.gameObject.transform.position, other.gameObject.transform.position);
            deviation_sum = deviation_sum + deviation[deviation_index];
            //Debug.Log(deviation[deviation_index]);
            deviation_index++;
            //Debug.Log(deviation_index);
            Destroy(other.gameObject);
            hit_count++;
            combo_count++;
        }
        if (other.gameObject.name.Contains("cube5") && LaunchPadFingerTrigger.button5)
        {
            effect5.Play();
            deviation[deviation_index] = Vector3.Distance(this.gameObject.transform.position, other.gameObject.transform.position);
            deviation_sum = deviation_sum + deviation[deviation_index];
            //Debug.Log(deviation[deviation_index]);
            deviation_index++;
            //Debug.Log(deviation_index);
            Destroy(other.gameObject);
            hit_count++;
            combo_count++;
        }
    }

    private bool saveDataToObject() //creates a new SongScore instance and populates it with data to templorarily store before
    {
        //Debug.Log("called Save Function");
        int i = 0;
        for(i = 0; i< 7; ++i)
        {
            if (logs[i] == null)
            {
                break;
            }
        }
        if (i > 6)
        {
            return false;
        }
        logs[i] = new SongScore();
        logs[i].setDeviationArray(deviation);
        logs[i].setHitCount(hit_count);
        logs[i].setSongTitle(song_title);
        float copy = score_calculated;
        logs[i].setSumScore(copy);
        logs[i].setTotalNotes(deviation_index);
        
        Debug.Log(logs[i].ToString());
        return true;
    }
   
    private bool writeAllSongData()
    {
        Debug.Log("Started Data Write");
        string baseDir = "C:\\Users\\mhss5458\\Documents\\SongData\\recentParticipantSongData.csv";
        try
        {
            StreamWriter writer;
            try
            {
                writer = new StreamWriter(baseDir);
            }
            catch(Exception e)
            {
                return false;
            }
            writer.WriteLine("SongTitle, Condition, Score, HitCount, TotalNotes");
            foreach (SongScore s in logs)
            {
                if (s != null)
                {
                    writer.WriteLine(s.getSongTitle().ToString() + ", , " + s.getSumScore().ToString() + ", " + s.getHitCount().ToString() + ", " + s.getTotalNotes().ToString());
                }
            }
            writer.WriteLine(); //buffer between the two
            writer.WriteLine("SongTitle, DeviationList");

            string line = "";
            foreach (SongScore s in logs)
            {
                if (s != null) {
                line = String.Concat(s.getSongTitle(), ", ");
                float[] dev = s.getDeviationArray();
                foreach (float f in dev)
                {
                    line = String.Concat(line, f.ToString(), ", ");
                        //Debug.Log(String.Concat("Writing List Entry: ", f.ToString()));
                }
                }
                writer.WriteLine(line);
                line = "";
            }
            writer.Close();

        }
        catch (Exception e)
        {
            Debug.Log("Ran into error writing data into a csv file");
            Debug.Log(e.ToString());
            return false;
        }
        Debug.Log("Successfully wrote data to CSV");
        return true;

    }

}

