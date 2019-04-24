using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    WiiMote wiimote;

    bool[] arrows = new bool[4];
    bool[] arrows_last_frame = new bool[4];
    int enemies_last_frame;

    bool shoot;
    bool shoot_last_frame;
    public bool shooting;

    void Start()
    {
        wiimote = GetComponent<WiiMote>();
    }

    void Update()
    {
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

        UpdateArrows();

        // WIIMOTE SHOOTY
        // do code
        shoot_last_frame = shoot;
        shoot = wiimote.btn_b_down;

        if ((!shoot_last_frame) && shoot)
            shooting = true;
        else
            shooting = false;
    }

    void LateUpdate()
    {
        enemies_last_frame = GetComponent<GameMaster>().enemies.Count;
    }

    public void UpdateArrows()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (arrows_last_frame[i] != arrows[i] || GetComponent<GameMaster>().enemies.Count > enemies_last_frame)
            {
                //delta detected!!!
                //update the appropriate arrow on all enemies reticles
                foreach (GameObject enemy in GetComponent<GameMaster>().enemies)
                {
                    if (enemy.GetComponent<ReticleScript>().isActiveAndEnabled)
                    {
                        enemy.GetComponentInChildren<ReticleScript>().UpdateReticle((ArrowDirection)i, arrows[i]);
                    }
                }
            }
        }
    }
}
