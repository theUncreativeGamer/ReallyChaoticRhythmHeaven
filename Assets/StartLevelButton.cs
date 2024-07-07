using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLevelButton : MonoBehaviour
{
    public MusicTrack levelToLoad;
    public string sceneToLoad;
    public TMPro.TextMeshProUGUI scoreDisplay;
    public TMPro.TextMeshProUGUI rankDisplay;

    private void Start()
    {
        int score = levelToLoad.GetScore();
        if(score<0)
        {
            scoreDisplay.enabled = false;
            rankDisplay.enabled = false;
            return;
        }

        scoreDisplay.text = score.ToString();
        rankDisplay.text = levelToLoad.GetRank(score).ToString();
    }


    public void StartLevel()
    {
        MusicTrackLoader.Instance.musicToLoad = levelToLoad;
        SceneManager.LoadScene(sceneToLoad);
    }
}
