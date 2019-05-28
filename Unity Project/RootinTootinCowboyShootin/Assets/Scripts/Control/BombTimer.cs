using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BombTimer : MonoBehaviour
{
    public float bomb_timer;
    float time_left;

    public Image fuse;
    public Image fuse_fire;

    string current_scene_name;
    bool once;

    AudioSource AS;

    void Start()
    {
        AS = GetComponent<AudioSource>();
        AS.Play();
        time_left = bomb_timer;
    }

    void FixedUpdate()
    {
        time_left -= Time.fixedDeltaTime;

        if (time_left < 15f)
            AS.Play();

        if(time_left <= 0 && !once)
        {
            once = true;
            Debug.Log("Boom.");
            FindObjectOfType<LevelFadeScript>().SwapLevel(Stages.LOSE);
            Destroy(gameObject);
        }

        current_scene_name = SceneManager.GetActiveScene().name;

        if (current_scene_name == "BombStompScene")
        {
            Destroy(gameObject);
        }
        else
        {
            fuse.fillAmount = time_left / bomb_timer;
            Vector2 temppos = new Vector2(7 - (14 * fuse.fillAmount), fuse_fire.transform.position.y);
            fuse_fire.transform.position = temppos;
        }
    }
}