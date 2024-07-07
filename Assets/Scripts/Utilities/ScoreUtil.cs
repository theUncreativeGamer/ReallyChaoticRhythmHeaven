using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreUtil 
{
    public static int GetScore(this MusicTrack track)
    {
        return PlayerPrefs.GetInt(track.name + " High Score", -9999999);
    }

    public static void SetScore(this MusicTrack track, int score)
    {
        PlayerPrefs.SetInt(track.name + " High Score", score);
    }

    public static char GetRank(this MusicTrack track, int score)
    {
        float scorePercentage = (float)score / track.MaxScore;
        char rank;
        if (scorePercentage > 0.8)
            rank = 'A';
        else if (scorePercentage > 0.6)
            rank = 'B';
        else if (scorePercentage > 0.4)
            rank = 'C';
        else if (scorePercentage > 0.2)
            rank = 'D';
        else if (scorePercentage > 0.0)
            rank = 'E';
        else
            rank = 'F';

        return rank;
    }
}
