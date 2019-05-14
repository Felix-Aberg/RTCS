using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ScoreEvent
{
    BASIC_KILL,
    SPECIAL_KILL,
    WALL_HOLE,
    DANCE_STEP,
    BOMB_STOMP,
    BOMB_DEFUSED,
    TIME_LEFT,
    GET_SHOT,
    SHOOT_FOOT
}

public class Score : MonoBehaviour
{
    public Text text_score;

    [Tooltip("Current player's score this run. Not to be confused with high-scores")]
    public int current_score;

    //Positive scoring events
    [Tooltip("Score given to the player for killing a basic enemy.")]
    public int score_kill_basic;

    [Tooltip("Score given to the player for killing a special enemy.")]
    public int score_kill_special;

    [Tooltip("Score given to the player for shooting a hole in the saloon wall.")]
    public int score_wall_hole;

    [Tooltip("Score given to the player for each step during dance event.")]
    public int score_dance_step;

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
    public void UpdateScore(ScoreEvent se)
    {
        switch(se)
        {
            case (ScoreEvent.BASIC_KILL):
                {
                    current_score += score_kill_basic;
                    break;
                }

            case (ScoreEvent.SPECIAL_KILL):
                {
                    current_score += score_kill_special;
                    break;
                }

            case (ScoreEvent.WALL_HOLE):
                {
                    current_score += score_wall_hole;
                    break;
                }

            case (ScoreEvent.DANCE_STEP):
                {
                    current_score += score_dance_step;
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

            case (ScoreEvent.GET_SHOT):
                {
                    current_score += score_get_shot;
                    break;
                }

            case (ScoreEvent.SHOOT_FOOT):
                {
                    current_score += score_shoot_yourself;
                    break;
                }

            default:
                break;
        }

        text_score.text = current_score.ToString();
    }
}
