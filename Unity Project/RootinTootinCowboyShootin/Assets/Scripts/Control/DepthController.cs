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
        sr = GetComponentInChildren<SpriteRenderer>();

        if (sr == null)
        {
            sr = GetComponentInParent<SpriteRenderer>();

            if (sr == null)
            {
                Debug.LogError("ERROR! Depthcontroller Doesn't know what sprite to handle depth for!");
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        sort_order = transform.position.y * -1000;
        sr.sortingOrder = (int)sort_order;
    }
}
