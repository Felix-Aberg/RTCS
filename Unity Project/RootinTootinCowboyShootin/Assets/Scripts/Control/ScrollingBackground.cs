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

    public Vector3 pos_1;
    public Vector3 pos_2;

    public float width;
    private bool first_is_left = true;
    private float delta;
    private float scroll_edge;
    
    // Start is called before the first frame update
    void Start()
    {

        sprite_renderer_1 = transform_1.GetComponent<SpriteRenderer>();
        sprite_renderer_2 = transform_1.GetComponent<SpriteRenderer>();

        width = sprite_renderer_1.bounds.size.x;

        pos_1 = Vector3.zero;
        pos_2 = Vector3.zero;
        pos_2.x += width;

        scroll_edge = -width;
    }

    // Update is called once per frame
    void Update()
    {
        delta = Time.deltaTime;

        pos_1.x -= delta * scroll_speed;
        pos_2.x -= delta * scroll_speed;

        transform_1.position = pos_1;
        transform_2.position = pos_2;

        if (pos_1.x <= scroll_edge)
        {
            pos_1 = pos_2;
            pos_1.x += width;
            Debug.Log("pos 1 scroll edge triggered");
        }

        if (pos_2.x <= scroll_edge)
        {
            pos_2 = pos_1;
            pos_2.x += width;
            Debug.Log("pos 2 scroll edge triggered");
        }
    }
}
