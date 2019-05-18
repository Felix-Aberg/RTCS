using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWall : MonoBehaviour
{
    public Sprite wall_hole;

    public ReticleScript[] targets;


    void Start()
    {
        foreach (ReticleScript reticle in targets)
        {
            reticle.InstantiateArrows(ArrowDirection.LEFT, ArrowDirection.RIGHT);
        }
    }

    void FixedUpdate()
    {

    }
}
