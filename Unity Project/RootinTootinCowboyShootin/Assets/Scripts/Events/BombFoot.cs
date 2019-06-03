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

    Animator bombanim;

    AudioSource[] AS; //two audiosources. index 0 fuse sound; index 1 stomp sound;

    void Start()
    {
        AS = GetComponents<AudioSource>();
        AS[0].Play();

        bomb = transform.parent.gameObject;
        bombanim = GetComponentInParent<Animator>();
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
            FindObjectOfType<ScreenShake>().StartShake(.05f, 1);
            bomb_hp--;

            AS[1].Play();
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
        if (bomb_hp <= 0 && !bool_bomb_defused)
        {
            BombDefused();
        }
    }

    void BombDefused()
    {
        AS[0].Stop();
        bombanim.SetTrigger("BombDefused");
        FindObjectOfType<Score>().GiveScoreEvent(ScoreEvent.BOMB_DEFUSED);
        bool_bomb_defused = true;
        Debug.Log("the bomb has been defused");
        Invoke("GoToEndScene", 3.5f);
    }

    void GoToEndScene()
    {
        Score score = FindObjectOfType<Score>();
        if (score != null)
            score.SaveScore();
        else
            Debug.LogError("Scores not found when trying to update scores!!");
        Destroy(GameObject.Find("DontDestroy"));

        FindObjectOfType<LevelFadeScript>().SwapLevel(Stages.ENDING);
    }
}
