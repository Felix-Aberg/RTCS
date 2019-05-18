using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public enum ArrowDirection
{
    UP,
    DOWN,
    LEFT,
    RIGHT
};

public enum ArrowType
{
    DEFAULT,
    INDICATED,
    INDICATED_PRESSED,
    NOT_INDICATED_PRESSED
};

//Make arrow class
//arrow class arrays
//replace stuff with "for each arrow in indicated_arrows" & "...in wrong_arrows"
//instantiate function

public class ReticleScript : MonoBehaviour
{
    public Sprite[] reticle_sprites;
    public GameObject[] arrow_objects;
    public Sprite[] arrow_sprites;

    //Instantiated by InstantiateArrows()
    public List<Arrow> all_arrows;
    public List<Arrow> indicated_arrows;
    public List<Arrow> wrong_arrows;

    public bool directions_correct;
    public bool crosshair_correct;
    
    GameObject crosshair;

    void Start()
    {
        FindCrosshair();

        if (SceneManager.GetActiveScene().name != "TutorialScene" && SceneManager.GetActiveScene().name != "ShootWallScene")
        {
            transform.localScale = Vector3.one;
            transform.localScale = new Vector3(1 / transform.lossyScale.x, 1 / transform.lossyScale.y, 1 / transform.lossyScale.z);
        }
    }

    void Update()
    {
        UpdateReticle();
    }

    void UpdateReticle()
    {
        if (!directions_correct && !crosshair_correct)
            GetComponent<SpriteRenderer>().sprite = reticle_sprites[0];

        if (directions_correct || crosshair_correct)
            GetComponent<SpriteRenderer>().sprite = reticle_sprites[1];

        if (directions_correct && crosshair_correct)
            GetComponent<SpriteRenderer>().sprite = reticle_sprites[2];
    }

    /// <summary>
    /// Called by the InputHandler to update all arrows accordingly with the input. Gives an arrowdirection and boolean at a time
    /// </summary>
    public void UpdateReticle(ArrowDirection dir, bool pressed)
    {
        //Find the corresponding indicated arrow and update it accordingly
        foreach (Arrow arrow in indicated_arrows)
        {
            if (pressed && dir == arrow.dir)
            {
                arrow_objects[(int)dir].GetComponent<SpriteRenderer>().sprite = arrow_sprites[(int)(ArrowType.INDICATED_PRESSED)];
                arrow.pressed = pressed;
            }
            else if (!pressed && dir == arrow.dir)
            {
                arrow_objects[(int)dir].GetComponent<SpriteRenderer>().sprite = arrow_sprites[(int)(ArrowType.INDICATED)];
                arrow.pressed = pressed;
            }
        }

        //Find the corresponding indicated arrow and update it accordingly
        foreach (Arrow arrow in wrong_arrows)
        {
            if (pressed && dir == arrow.dir)
            {
                arrow_objects[(int)dir].GetComponent<SpriteRenderer>().sprite = arrow_sprites[(int)(ArrowType.NOT_INDICATED_PRESSED)];
                arrow.pressed = pressed;
            }
            else if (!pressed && dir == arrow.dir)
            {
                arrow_objects[(int)dir].GetComponent<SpriteRenderer>().sprite = arrow_sprites[(int)(ArrowType.DEFAULT)];
                arrow.pressed = pressed;
            }
        }
        //*/

        //Check if directions_correct
        //Automatically set to true, can get disabled by the conitions below
        directions_correct = true;

        foreach (Arrow arrow in indicated_arrows)
        {
            //Check for errors.
            if (!arrow.pressed)
            {
                //Debug.Log("Player isn't pressing a indicated arrow.")
                directions_correct = false;
            }
        }

        foreach (Arrow arrow in wrong_arrows)
        {
            //Check for errors.
            if (arrow.pressed)
            {
                //Debug.Log("Player isn't pressing a indicated arrow.")
                directions_correct = false;
            }
        }
    }

    public void InstantiateArrows(ArrowDirection dir1, ArrowDirection dir2) // When enemy is spawned set 2 arrows to indicated and enable reticle
    {
        if (dir1 == dir2)
        {
            Debug.Log("Only instantiated one direction!");
            IndicateArrow(dir1);
        }
        else
        {
            IndicateArrow(dir1);
            IndicateArrow(dir2);
        }
        WrongArrow();
    }

    /// <summary>
    /// Puts an arrow into the indicated_arrows list
    /// </summary>
    void IndicateArrow(ArrowDirection dir)
    {
        indicated_arrows.Add(all_arrows[(int)dir]);
    }

    /// <summary>
    /// Sets all arrows that are not in the indicated_arrows list into the wrong_arrows list. Do this at the end of instantiating arrows.
    /// </summary>
    void WrongArrow()
    { 
        //Official order of arrows in arrays: up down left right
        foreach (Arrow arrow in all_arrows)
        {
            bool indicated = false;

            //Compare the current arrow with all indicated arrows
            foreach (Arrow indicated_arrow in indicated_arrows)
            {
                if (arrow == indicated_arrow)
                    indicated = true;
            }

            //If the arrow isn't in the indicated list
            if (!indicated)
            {
                //Add it to the wrong arrow list
                wrong_arrows.Add(arrow);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Crosshair")
        {
            crosshair_correct = true;
            other.GetComponent<CrosshairScript>().current_reticle = gameObject;
            other.GetComponent<CrosshairScript>().current_enemy = transform.parent.gameObject;
            other.GetComponent<CrosshairScript>().ChangeCrosshair(1);
            UpdateReticle();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Crosshair")
        {
            crosshair_correct = false;
            other.GetComponent<CrosshairScript>().current_reticle = null;
            other.GetComponent<CrosshairScript>().ChangeCrosshair(0);
            UpdateReticle();
        }
    }

    public void FindCrosshair()
    {
        crosshair = GameObject.Find("Crosshair");
    }
}
