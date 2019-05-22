using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrosshairScript : MonoBehaviour
{
    public Transform crosshair_tracker;
    public GameObject current_reticle;
    public GameObject current_enemy;

    public int reticles_correct = 0; //How many reticles that are correct
    GameObject game_master;

    public float lerp_bias;
    int lerp_preset_nr = 2;
    WiiMote wiimote;

    bool left_pressed_last_frame;
    bool right_pressed_last_frame;

    public Sprite[] crosshair_sprites;
    public GameObject[] hit_feedback_objects;
    public GameObject[] miss_feedback_objects;

    void Start()
    {
        Cursor.visible = false;
        game_master = GameObject.Find("GameMaster");
        wiimote = GameObject.Find("WiimoteHandler").GetComponent<WiiMote>();
    }

    void Update()
    {
        if (!game_master.GetComponent<InputHandler>().use_mouse)
        {
            if (wiimote.btn_left_down && !left_pressed_last_frame)
            {
                if (lerp_preset_nr > 0)
                {
                    lerp_preset_nr--;
                    ChangeLerpBias();
                }
            }

            else if (wiimote.btn_right_down  && !right_pressed_last_frame)
            {
                if (lerp_preset_nr < 3)
                {
                    lerp_preset_nr++;
                    ChangeLerpBias();
                }
            }
        }

        if (game_master.GetComponent<InputHandler>().shooting)
        {
            if (current_reticle != null)
            {
                if (SceneManager.GetActiveScene().name == "TutorialScene")
                    GameObject.Find("Dancemat").GetComponent<Tutorial>().TutorialComplete();

                else if (current_reticle.GetComponent<ReticleScript>().directions_correct)
                {
                    Debug.Log("directions correct + reticle");
                    if (current_reticle.GetComponent<ReticleScript>().crosshair_correct)
                    {
                        Debug.Log("everything correct, shooting");

                        if (SceneManager.GetActiveScene().name == "ShootWallScene")
                        {
                            current_reticle.SetActive(false);
                            ShowHitFeedback();
                            GameObject.Find("Outline").GetComponent<ShootWall>().CheckReticles();
                        }

                        else
                            Shoot();
                    }
                }

                else if (!current_reticle.GetComponent<ReticleScript>().directions_correct)
                {
                    ShowMissFeedback();
                }
            }

            else
                ShowMissFeedback();

            if (reticles_correct < 1)
                ShootFoot();
        }

        left_pressed_last_frame = wiimote.btn_left_down;
        right_pressed_last_frame = wiimote.btn_right_down;
    }

    void LateUpdate()
    {
        Vector2 temppos = transform.position;

        if (!game_master.GetComponent<InputHandler>().use_mouse)
        {
            temppos.x = Mathf.Lerp(transform.position.x, crosshair_tracker.transform.position.x, lerp_bias);
            temppos.y = Mathf.Lerp(transform.position.y, crosshair_tracker.transform.position.y, lerp_bias);
        }

        else if (game_master.GetComponent<InputHandler>().use_mouse)
            temppos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = temppos;
    }

    void Shoot()
    {
        ShowHitFeedback();
        current_enemy.GetComponent<EnemyBase>().OnDeath();
    }

    void ShootFoot()
    {
        Debug.Log("Ouch owie my footsie");
    }

    public void ChangeCrosshair(int sprite_index)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = crosshair_sprites[sprite_index];
    }

    void ChangeLerpBias()
    {
        switch (lerp_preset_nr)
        {
            case (0):
                {
                    lerp_bias = .25f;
                    wiimote.UpdateLEDs(true, false, false, false);
                    break;
                }

            case (1):
                {
                    lerp_bias = .5f;
                    wiimote.UpdateLEDs(true, true, false, false);
                    break;
                }

            case (2):
                {
                    lerp_bias = .75f;
                    wiimote.UpdateLEDs(true, true, true, false);
                    break;
                }

            case (3):
                {
                    lerp_bias = 1;
                    wiimote.UpdateLEDs(true, true, true, true);
                    break;
                }


            default:
                break;

        }
    }

    void ShowHitFeedback()
    {
        int r = Random.Range(0, hit_feedback_objects.Length);
        Instantiate(hit_feedback_objects[r], current_reticle.transform.position, Quaternion.identity);
    }
    void ShowMissFeedback()
    {
        int r = Random.Range(0, miss_feedback_objects.Length);
        Instantiate(miss_feedback_objects[r], transform.position, Quaternion.identity);
    }
}

