using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBase : MonoBehaviour
{
    public GameObject reticle;

    public ArrowDirection indicated_arrow_1;
    public ArrowDirection indicated_arrow_2;

    public Vector2 hide_position; //Where the enemy hides
    public Vector2 final_position; //Where the enemy jumps out

    public float jump_time;
    bool jumping;

    public float jump_speed;

    // Start is called before the first frame update
    void Start()
    {
        //randomise arrows if at least one of them isn't set
        if (indicated_arrow_1 == ArrowDirection.NONE || indicated_arrow_2 == ArrowDirection.NONE)
        {
            int rand = Random.Range(0, 4);
            indicated_arrow_1 = (ArrowDirection)rand;

            //randomise until a different value
            while (rand == (int)indicated_arrow_1)
            {
                rand = Random.Range(0, 4);
            }
            indicated_arrow_2 = (ArrowDirection)rand; 
        }

        Invoke("StartJump", jump_time);

        //make reticle
        //reticle = Instantiate(reticle_prefab, reticle_position.position, Quaternion.identity);

        reticle.GetComponent<ReticleScript>().InstantiateArrows(indicated_arrow_1, indicated_arrow_2);
    }

    void StartJump()
    {
        jumping = true;
    }


    void FixedUpdate()
    {
        if (jumping)
        {
            Vector2.MoveTowards(hide_position, final_position, jump_speed / 100);

            //To disable jumping
            if (Vector2.Distance(transform.position, final_position) < 0.02f)
            {
                jumping = false;
                reticle.gameObject.SetActive(true);
            }
        }
    }

    public void SetPositions(Vector2 spawn_point, Vector2 jump_point)
    {
        hide_position = spawn_point;
        final_position = jump_point;
    }
}
