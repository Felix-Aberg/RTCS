using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [Tooltip("Current player's score this run. Not to be confused with high-scores")]
    public int current_score;

    //Positive scoring events
    [Tooltip("Score given to the player for killing a basic enemy.")]
    public int score_kill_basic;

    [Tooltip("Score given to the player for killing a special enemy.")]
    public int score_kill_special;

    [Tooltip("Score given to the player everytime they step on the bomb while it's active.")]
    public int score_stomping_bomb;

    [Tooltip("Score given to the player when the bomb is finally defused.")]
    public int score_defused_bomb;

    [Tooltip("Score granted at the end of the level. \n" +
             "Multiplied by the time left!")]
    public int score_time_left_multiplier;

    //Negative scoring events
    [Tooltip("Score taken from the player for getting shot. \n" +
             "USE NEGATIVE NUMBERS!")]
    public int score_get_shot;

    [Tooltip("Score taken from the player for getting shot. \n" +
             "USE NEGATIVE NUMBERS!")]
    public int score_shoot_yourself;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Vector3.positiveInfinity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
