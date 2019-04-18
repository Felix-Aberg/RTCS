﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    GameObject crosshair;

    bool[] arrows = new bool[4];
    bool[] arrows_last_frame = new bool[4];

    void Start()
    {
        crosshair = GameObject.Find("Crosshair");
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
        arrows[(int)ArrowDirection.UP] = Input.GetKey(KeyCode.W); //fix these later, dummy names
        arrows[(int)ArrowDirection.DOWN] = Input.GetKey(KeyCode.S);
        arrows[(int)ArrowDirection.LEFT] = Input.GetKey(KeyCode.A);
        arrows[(int)ArrowDirection.RIGHT] = Input.GetKey(KeyCode.D);

        //compare input
        for (int i = 0; i < arrows.Length; i++)
        {
            if(arrows_last_frame[i] != arrows[i])
            {
                //delta detected!!!
                //update the appropriate arrow on all enemies reticles
                foreach(GameObject enemy in GetComponent<GameMaster>().enemies)
                {
                    enemy.GetComponentInChildren<ReticleScript>().UpdateReticle((ArrowDirection)i, arrows[i]);
                }
            }
        }

        // WIIMOTE INPUT
        // do code
    }
}
