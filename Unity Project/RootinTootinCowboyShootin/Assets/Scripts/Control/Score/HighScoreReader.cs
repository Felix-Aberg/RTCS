using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreReader : MonoBehaviour
{
    public Text[] score_displays;
    public int[] high_score;
    public int max_high_scores;
    bool once;

    // Start is called before the first frame update
    void Start()
    {
        HighScorePull();
        RenderHighScores();
    }

    void RenderHighScores()
    {
        for (int i = 0; i < max_high_scores; i++)
        {
            if (i > score_displays.Length)
                return;

            if (PlayerPrefs.GetInt("LastScore") == high_score[i] && !once)
            {
                score_displays[i].color = Color.white;
                once = true;
            }
            score_displays[i].text = high_score[i].ToString();
        }
    }

    void HighScorePull()
    {
        for (int i = 0; i < max_high_scores; i++)
        {
            high_score[i] = PlayerPrefs.GetInt("HighScore_" + i, 0);
        }

        return;
    }
}
