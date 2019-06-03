using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingArrow_TEMP : MonoBehaviour
{
    public Sprite arrow_on;
    public Sprite arrow_off;
    bool flashing = false;

    // Start is called before the first frame update

    void Start()
    {
        ArrowOn();
    }

    void ArrowOn()
    {
        GetComponent<SpriteRenderer>().sprite = arrow_on;
        Invoke("ArrowOff", 0.25f);
    }

    void ArrowOff()
    {
        GetComponent<SpriteRenderer>().sprite = arrow_off;
        Invoke("ArrowOn", 0.25f);
    }
}

