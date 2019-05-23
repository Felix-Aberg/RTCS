using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BombFoot : MonoBehaviour
{

    public GameObject bomb;
    SpriteRenderer sr;

    public Sprite foot_step;
    public Sprite foot_unstep;
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
            sr.sprite = foot_step;
            transform.position = step_position;

            bomb_hp--;
        }
        else if (!stepping && stepping_last_frame)
        {
            sr.sprite = foot_unstep;
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
        bool_bomb_defused = true;
        //yeah idk u win
        Debug.Log("the bomb has been defused");
        bomb.GetComponent<SpriteRenderer>().sprite = bomb_defused;
        Invoke("GoToWinScene", 2f);
    }

    void GoToWinScene()
    {
        Destroy(GameObject.Find("DontDestroy"));
        SceneManager.LoadScene("WinScene");
    }
}
