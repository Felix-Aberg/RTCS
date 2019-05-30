﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


public enum ScoreEvent
{
    BASIC_KILL,
    SPECIAL_KILL,
    WALL_HOLE,
    BOMB_STOMP,
    BOMB_DEFUSED,
    TIME_LEFT
}

public class Score : MonoBehaviour
{
    public int[] high_score;
    [Tooltip("The number of high scores to save")]
    int max_high_scores = 10;

    public Text text_score;

    [Tooltip("Current player's score this run. Not to be confused with high-scores")]
    public int current_score;

    //Positive scoring events
    [Tooltip("Score given to the player for killing a basic enemy.")]
    public int score_kill_basic;

    //[Tooltip("Score given to the player for killing a special enemy.")]
    //public int score_kill_special;

    [Tooltip("Score given to the player for shooting a hole in the saloon wall.")]
    public int score_wall_hole;

    [Tooltip("Score given to the player everytime they step on the bomb while it's active.")]
    public int score_stomping_bomb;

    [Tooltip("Score given to the player when the bomb is finally defused.")]
    public int score_defused_bomb;

    [Tooltip("Score granted at the end of the level. \n" +
             "Multiplied by the time left!")]
    public int score_time_left_multiplier;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        //Debug.Log(mode); //4

        //if (scene.name == "WinScene")
        //{
        //    Debug.Log("Found HighScoreReader: " + !!GameObject.Find("Canvas").GetComponentInChildren<HighScoreReader>());
        //    GameObject.Find("Canvas").GetComponentInChildren<HighScoreReader>().RenderHighScores();
        //}
    }

    public void GiveScoreEnemy(float lifetime)
    {
        if (lifetime < 80)
            current_score += (int)(score_kill_basic * ((100 - lifetime)/100));

        else
            current_score += score_kill_basic * (int)(100 - 80);

        UpdateScore();
    }

    public void GiveScoreEvent(ScoreEvent se)
    {
        switch (se)
        {
            case (ScoreEvent.WALL_HOLE):
                {
                    current_score += score_wall_hole;
                    break;
                }

            case (ScoreEvent.BOMB_STOMP):
                {
                    current_score += score_stomping_bomb;
                    break;
                }

            case (ScoreEvent.BOMB_DEFUSED):
                {
                    current_score += score_defused_bomb;
                    break;
                }

            case (ScoreEvent.TIME_LEFT):
                {
                    current_score += (int)GameObject.Find("BombTimer").GetComponent<BombTimer>().bomb_timer * score_time_left_multiplier;
                    break;
                }

            default:
                break;
        }

        UpdateScore();
    }

    void UpdateScore()
    {
        text_score.text = current_score.ToString();
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("LastScore", current_score);
        UpdateHighScore(current_score);
    }

    void Start()
    {
        UpdateHighScore(0);

        //ClearHighScore();
        /* //Dummy values used for testing
        UpdateHighScore(1235);
        UpdateHighScore(152);
        UpdateHighScore(355);
        UpdateHighScore(332);
        UpdateHighScore(911);
        UpdateHighScore(1337);
        //*/
    }

    // ## High Score Functions ## //
    
    //Clears the saved high scores, used for debugging.
    void ClearHighScore()
    {
        for (int i = 0; i < max_high_scores; i++)
        {
            PlayerPrefs.SetInt("HighScore_" + i, 0);
        }
    }

    void UpdateHighScore(int score)
    {
        HighScorePull();

        high_score[max_high_scores] = score;
        Array.Sort(high_score);
        Array.Reverse(high_score);
        //high_score[max_high_scores] = 0;
        PlayerPrefs.Save();

        HighScorePush();
    }

    void HighScorePull()
    {
        for (int i = 0; i < max_high_scores; i++)
        {
            high_score[i] = PlayerPrefs.GetInt("HighScore_" + i, 0);
        }

        return;
    }

    void HighScorePush()
    {
        for(int i = 0; i < max_high_scores; i++)
        {
            PlayerPrefs.SetInt("HighScore_" + i, high_score[i]);
        }
        
        return;
    }
}
