using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BombFoot : MonoBehaviour
{

    public GameObject bomb;
    SpriteRenderer sr;

    public Sprite foot_step_lit;
    public Sprite foot_unstep_lit;
    public Sprite foot_step_defused;
    public Sprite foot_unstep_defused;
    public Sprite bomb_defused;

    public bool stepping;
    public bool stepping_last_frame;

    public int bomb_hp;

    public Vector2 step_position;
    public Vector2 unstep_position;

    public bool bool_bomb_defused;

    void Start()
    {
        bomb = transform.parent.gameObject;
        sr = GetComponent < SpriteRenderer>();

        unstep_position = transform.position;
        step_position = transform.GetChild(0).position;
    }

    // Update is called once per frame
    void Update()
    {
        CheckBomb();
        CheckStep();
    }

    void CheckStep()
    {
        stepping_last_frame = stepping;
        stepping = Input.GetButton("DanceMatUp");

        if (stepping && !stepping_last_frame)
        {
            if(!bool_bomb_defused)
                sr.sprite = foot_step_lit;
            else
                sr.sprite = foot_step_defused;

            transform.position = step_position;

            bomb_hp--;
            FindObjectOfType<Score>().GiveScoreEvent(ScoreEvent.BOMB_STOMP);
        }
        else if (!stepping && stepping_last_frame)
        {
            if (!bool_bomb_defused)
                sr.sprite = foot_unstep_lit;
            else
                sr.sprite = foot_unstep_defused;
            transform.position = unstep_position;
        }
    }

    void CheckBomb()
    {
        // Bomb HP
        if (bomb_hp <= 0)
        {
            BombDefused();
        }
    }

    void BombDefused()
    {
        FindObjectOfType<Score>().GiveScoreEvent(ScoreEvent.BOMB_DEFUSED);
        bomb.GetComponent<Animator>().SetTrigger("BombDefused");
        bool_bomb_defused = true;
        Debug.Log("the bomb has been defused");
        Invoke("GoToWinScene", 2f);
    }

    void GoToWinScene()
    { 
        SceneManager.LoadScene("WinScene");
    }
}
