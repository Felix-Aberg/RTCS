using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float scroll_speed = 5;

    public Transform transform_1;
    public Transform transform_2;

    private SpriteRenderer sprite_renderer_1;
    private SpriteRenderer sprite_renderer_2;


    // Start is called before the first frame update
    void Start()
    {
        sprite_renderer_1 = transform_1.GetComponent<SpriteRenderer>();
        sprite_renderer_2 = transform_1.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
