using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBase : MonoBehaviour
{
    public GameObject reticle;

    ArrowDirection indicated_arrow_1;
    ArrowDirection indicated_arrow_2;

    //public Vector2 hide_position; //Where the enemy hides
    //public Vector2 final_position; //Where the enemy jumps out


    // Start is called before the first frame update
    void Start()
    {
        //randomise arrows
        int rand = Random.Range(0, 4);
        indicated_arrow_1 = (ArrowDirection)rand;
        
        //randomise until a different value
        while (rand == (int)indicated_arrow_1)
        {
            rand = Random.Range(0, 4);
        }
        indicated_arrow_2 = (ArrowDirection)rand;

        //make reticle
        //reticle = Instantiate(reticle_prefab, reticle_position.position, Quaternion.identity);

        reticle.GetComponent<ReticleScript>().InstantiateArrows(indicated_arrow_1, indicated_arrow_2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
