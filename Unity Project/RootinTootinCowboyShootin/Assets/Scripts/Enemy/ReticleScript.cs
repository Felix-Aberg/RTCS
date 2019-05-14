using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ArrowDirection
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
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
    /*
    public ArrowDirection indicated_arrow_1; //outdated, D E L E T E
    public ArrowDirection indicated_arrow_2; //outdated, D E L E T E
    public ArrowDirection wrong_arrow_1; //outdated, D E L E T E
    public ArrowDirection wrong_arrow_2; //outdated, D E L E T E ⁾:

    bool indicated_arrow_pressed_1; //outdated, D E L E T E
    bool indicated_arrow_pressed_2; //outdated, D E L E T E
    bool wrong_arrow_pressed_1; //outdated, D E L E T E
    bool wrong_arrow_pressed_2; //outdated, D E L E T E
    //*/
    public bool directions_correct;
    public bool crosshair_correct;
    
    GameObject crosshair;

    void Start()
    {
        crosshair = GameObject.Find("Crosshair");
        transform.localScale = Vector3.one;
        transform.localScale = new Vector3(1 / transform.lossyScale.x, 1 / transform.lossyScale.y, 1 / transform.lossyScale.z);
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

    public void UpdateReticle(ArrowDirection dir, bool pressed)
    {
        if(pressed)
        {
            foreach (Arrow arrow in indicated_arrows)
            {
                if (dir == arrow.dir)
                {
                    arrow_objects[(int)dir].GetComponent<SpriteRenderer>().sprite = arrow_sprites[(int)(ArrowType.INDICATED_PRESSED)];
                    arrow.pressed = pressed;
                }
                else
                {
                    arrow_objects[(int)dir].GetComponent<SpriteRenderer>().sprite = arrow_sprites[(int)(ArrowType.NOT_INDICATED_PRESSED)];
                    arrow.pressed = pressed;
                }
            }
        }
        else if (!pressed)
        {
            foreach (Arrow arrow in indicated_arrows)
            {
                if (dir == arrow.dir)
                    arrow_objects[(int)dir].GetComponent<SpriteRenderer>().sprite = arrow_sprites[(int)(ArrowType.INDICATED)];
                else
                    arrow_objects[(int)dir].GetComponent<SpriteRenderer>().sprite = arrow_sprites[(int)(ArrowType.DEFAULT)];
            }
        }

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
            UpdateReticle();
    }

    public void InstantiateArrows(ArrowDirection dir1, ArrowDirection dir2) // When enemy is spawned set 2 arrows to indicated and enable reticle
    {
        if (dir1 == dir2)
        {
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
        //indicated_arrows.Add(all_arrows[(int)dir]);
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
            crosshair.GetComponent<CrosshairScript>().current_reticle = gameObject;
            crosshair.GetComponent<CrosshairScript>().current_enemy = transform.parent.gameObject;
            UpdateReticle();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Crosshair")
        {
            crosshair_correct = false;
            crosshair.GetComponent<CrosshairScript>().current_reticle = null;
            UpdateReticle();
        }
    }
}
