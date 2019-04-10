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

public class Reticle : MonoBehaviour
{
    public GameObject[] arrow_objects;
    public Sprite[] arrow_sprites;
    ArrowDirection indicated_arrow_1;
    ArrowDirection indicated_arrow_2;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject arrow in arrow_objects) //Set all arrows to default sprite
            arrow.GetComponent<SpriteRenderer>().sprite = arrow_sprites[(int)ArrowType.DEFAULT];
    }

    public void UpdateArrow(ArrowDirection dir, bool pressed)
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
    }

    public void InstantiateArrows(ArrowDirection dir1, ArrowDirection dir2) // When enemy is spawned set 2 arrows to indicated and enable reticle
    {
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

    
}
