using System;
using System.Collections;
using System.Collections.Generic;

public class SongScore
{
    private float sumScore; //total sum of the score for this song
    private string songTitle; //title of this song's level data
    private float[] deviationIndex; //deviations for all of the blocks that are hit (misses are logged as -1?)
    private int hitCount;
    private int totalNotes;

    public void setSumScore(float toSet) //set sumScore to toSet
    {
        sumScore = toSet;
    }
    public float getSumScore() //get the value of the total summed up score
    {
        return sumScore;
    }

    public void setSongTitle(string n) //set songTitle to n
    {
        songTitle = n;
    }

    public string getSongTitle() //get the string representation of the song's title
    {
        return songTitle;
    }

    public float getDeviationIndex(int indexNum) //returns the deviation at a deviationIndex[indexNum] if it is a valid index. If invalid a console message is sent and -1 is returned.
    {
        try
        {
            return deviationIndex[indexNum];
        }
        catch //illegal bounds
        {
            Console.WriteLine("Error accessing desired index in DeviationIndex for get");
            return -1;
        }
    }
    public bool setDeviationIndex(int indexNum, float value) //set deviationIndex[indexNum] to value. throws no error if illegal memory access ,logs to console if fails and returns false
    {
        try
        {
            deviationIndex[indexNum] = value;
            return true;
        }
        catch  //illegal bounds
        {
            Console.WriteLine("Error accessing desired index in DeviationIndex for set");
            return false;
        }
    }
    public float[] getDeviationArray()
    {
        return deviationIndex;
    }
    public void setDeviationArray(float[] completeIndex)
    {

        deviationIndex = completeIndex;
    }
    public void setHitCount(int toSet) //set the value of hitCount to toSet
    {
        hitCount = toSet;
    }

    public int getHitCount() //return the hitCount variable
    {
        return hitCount;
    }
    public void setTotalNotes(int toSet) //set the value of totalNotes to toSet
    {
        totalNotes = toSet;
    }
    public int getTotalNotes() //return the value of totalNotes
    {
        return totalNotes;
    }
    public override string ToString()
    {
        return String.Concat("SongTitle:", songTitle, " |  sumScore:", sumScore, " | hitCount:", hitCount, " | totalNotes:", totalNotes);
    }
}
