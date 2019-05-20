using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    WiiMote wiimote;

    bool[] arrows = new bool[4];
    bool[] arrows_last_frame = new bool[4];
    int enemies_last_frame;

    bool shoot;
    bool shoot_last_frame;
    public bool shooting;

    public bool use_mouse;

    public ReticleScript[] special_targets;

    void Start()
    {

        if (PlayerPrefs.GetInt("MouseAiming") == 0)
            use_mouse = false;

        else if (PlayerPrefs.GetInt("MouseAiming") == 1)
            use_mouse = true;
        wiimote = GameObject.Find("WiimoteHandler").GetComponent<WiiMote>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!use_mouse)
            {
                PlayerPrefs.SetInt("MouseAiming", 1);
                use_mouse = true;
            }

            else if (use_mouse)
            {
                PlayerPrefs.SetInt("MouseAiming", 0);
                use_mouse = false;
            }
        }

        // DANCE MAT INPUT

        //set arrows last frame to arrows
        for (int i = 0; i < arrows.Length; i++)
        {
            arrows_last_frame[i] = arrows[i];
        }

        //check dance mat input, update arrows
        arrows[(int)ArrowDirection.UP] = Input.GetButton("DanceMatUp"); //fix these later, dummy names
        arrows[(int)ArrowDirection.DOWN] = Input.GetButton("DanceMatDown");
        arrows[(int)ArrowDirection.LEFT] = Input.GetButton("DanceMatLeft");
        arrows[(int)ArrowDirection.RIGHT] = Input.GetButton("DanceMatRight");

        if(GameObject.Find("SceneVariables").GetComponent<SceneScript>().is_core_loop_scene == true)
            UpdateArrows();

        // WIIMOTE SHOOTY
        // do code
        shoot_last_frame = shoot;

        if (!use_mouse)
            shoot = wiimote.btn_b_down;

        else if (use_mouse)
            shoot = Input.GetButton("Fire1");

        if ((!shoot_last_frame) && shoot)
            shooting = true;
        else
            shooting = false;
    }

    public void UpdateArrows()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (SceneManager.GetActiveScene().name != "ShootWallScene")
            {
                //update the appropriate arrow on all enemies reticles
                foreach (GameObject enemy in GetComponent<GameMaster>().enemies)
                {
                    if (enemy.transform.GetChild(0).gameObject.activeInHierarchy)
                    {
                        enemy.GetComponentInChildren<ReticleScript>().UpdateReticle((ArrowDirection)i, arrows[i]);
                    }
                }
            }

            else
            {
                foreach (ReticleScript reticle in special_targets)
                {
                    reticle.UpdateReticle((ArrowDirection)i, arrows[i]);
                }
            }
        }
    }
}
