using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArrowDirection
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
    NONE
};

public enum ArrowType
{
    DEFAULT,
    INDICATED,
    INDICATED_PRESSED,
    NOT_INDICATED_PRESSED
};

public class ReticleScript : MonoBehaviour
{
    public Sprite[] reticle_sprites;
    public GameObject[] arrow_objects;
    public Sprite[] arrow_sprites;
    ArrowDirection indicated_arrow_1;
    ArrowDirection indicated_arrow_2;
    ArrowDirection wrong_arrow_1;
    ArrowDirection wrong_arrow_2;

    bool indicated_arrow_pressed_1;
    bool indicated_arrow_pressed_2;
    bool wrong_arrow_pressed_1;
    bool wrong_arrow_pressed_2;

    public bool directions_correct;
    public bool crosshair_correct;

    GameObject crosshair;

    void Start()
    {
        crosshair = GameObject.Find("Crosshair");
    }

    void Update() //Remove once InputHandler is implemented
    {
        if (Input.GetKeyDown(KeyCode.W))
            UpdateReticle(ArrowDirection.UP, true);

        if (Input.GetKeyUp(KeyCode.W))
            UpdateReticle(ArrowDirection.UP, false);

        if (Input.GetKeyDown(KeyCode.S))
            UpdateReticle(ArrowDirection.DOWN, true);

        if (Input.GetKeyUp(KeyCode.S))
            UpdateReticle(ArrowDirection.DOWN, false);

        if (Input.GetKeyDown(KeyCode.A))
            UpdateReticle(ArrowDirection.LEFT, true);

        if (Input.GetKeyUp(KeyCode.A))
            UpdateReticle(ArrowDirection.LEFT, false);

        if (Input.GetKeyDown(KeyCode.D))
            UpdateReticle(ArrowDirection.RIGHT, true);

        if (Input.GetKeyUp(KeyCode.D))
            UpdateReticle(ArrowDirection.RIGHT, false);
    }

    public void UpdateReticle(ArrowDirection dir, bool pressed)
    {
        if(pressed)
        {
            if (dir == indicated_arrow_1 || dir == indicated_arrow_2)
                arrow_objects[(int)dir].GetComponent<SpriteRenderer>().sprite = arrow_sprites[(int)(ArrowType.INDICATED_PRESSED)];

            else
                arrow_objects[(int)dir].GetComponent<SpriteRenderer>().sprite = arrow_sprites[(int)(ArrowType.NOT_INDICATED_PRESSED)];

        }

        else if (!pressed)
        {
            if (dir == indicated_arrow_1 || dir == indicated_arrow_2)
                arrow_objects[(int)dir].GetComponent<SpriteRenderer>().sprite = arrow_sprites[(int)(ArrowType.INDICATED)];

            else
                arrow_objects[(int)dir].GetComponent<SpriteRenderer>().sprite = arrow_sprites[(int)(ArrowType.DEFAULT)];
        }

        if (dir == indicated_arrow_1)
            indicated_arrow_pressed_1 = pressed;

        if (dir == indicated_arrow_2)
            indicated_arrow_pressed_2 = pressed;

        if (dir == wrong_arrow_1)
            wrong_arrow_pressed_1 = pressed;

        if (dir == wrong_arrow_2)
            wrong_arrow_pressed_2 = pressed;


        if (indicated_arrow_pressed_1 && indicated_arrow_pressed_2 &&
               !wrong_arrow_pressed_1 && !wrong_arrow_pressed_2)
        {
            if (!directions_correct)
            {
                directions_correct = true;
                crosshair.GetComponent<CrosshairScript>().reticles_correct++;
            }
        }

        else if (directions_correct)
        {
            directions_correct = false;
            crosshair.GetComponent<CrosshairScript>().reticles_correct--;
        }

            UpdateReticle();
    }

    public void InstantiateArrows(ArrowDirection dir1, ArrowDirection dir2) // When enemy is spawned set 2 arrows to indicated and enable reticle
    {
        indicated_arrow_1 = dir1;
        indicated_arrow_2 = dir2;

        for(int i = 0; i < 4; i++)
        {
            if (i != (int)dir1 && i != (int)dir2)
            {
                wrong_arrow_1 = (ArrowDirection)i;
                break;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (i != (int)dir1 && i != (int)dir2 && i != (int)wrong_arrow_1)
            {
                wrong_arrow_2 = (ArrowDirection)i;
                break;
            }
        }

        SetArrowSprite(dir1, ArrowType.INDICATED);
        SetArrowSprite(dir2, ArrowType.INDICATED);
        gameObject.SetActive(true);
    }

    void SetArrowSprite(ArrowDirection dir, ArrowType type)
    {
        switch (dir)
        {
            case ArrowDirection.UP:
                {
                    arrow_objects[(int)ArrowDirection.UP].GetComponent<SpriteRenderer>().sprite = arrow_sprites[(int)type];
                    break;
                }

            case ArrowDirection.DOWN:
                {
                    arrow_objects[(int)ArrowDirection.DOWN].GetComponent<SpriteRenderer>().sprite = arrow_sprites[(int)type];
                    break;
                }

            case ArrowDirection.LEFT:
                {
                    arrow_objects[(int)ArrowDirection.LEFT].GetComponent<SpriteRenderer>().sprite = arrow_sprites[(int)type];
                    break;
                }

            case ArrowDirection.RIGHT:
                {
                    arrow_objects[(int)ArrowDirection.RIGHT].GetComponent<SpriteRenderer>().sprite = arrow_sprites[(int)type];
                    break;
                }

            case ArrowDirection.NONE:
                {
                    break;
                }

            default:
                break;
        }
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
