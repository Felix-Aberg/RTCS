using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthController : MonoBehaviour
{
    SpriteRenderer sr;
    float sort_order;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponentInParent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sort_order = gameObject.transform.position.y * -1000;
        sr.sortingOrder = (int)sort_order;
    }
}
