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
    bool returning;

    public float jump_speed = 10;
    public float shoot_time = 5;

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

        reticle.GetComponent<ReticleScript>().InstantiateArrows(indicated_arrow_1, indicated_arrow_2);
    }

    void StartJump()
    {
        jumping = true;
    }

    void Shoot()
    {
        //Play shooting anim
        GameObject.Find("Prefab_Player").GetComponent<PlayerHealth>().ShootPlayer();
        StartReturn();
    }

   void StartReturn()
    {
        returning = true;
    }


    void FixedUpdate()
    {
        if (jumping)
        {
            transform.position = Vector2.MoveTowards(transform.position, final_position, jump_speed/100);

            //To disable jumping
            if (Vector2.Distance(transform.position, final_position) < 0.000002f)
            {
                jumping = false;
                Invoke("Shoot", shoot_time);
            }
        }

        else if (returning)
        {
            transform.position = Vector2.MoveTowards(transform.position, hide_position, jump_speed / 100);

            if(Vector2.Distance(transform.position, hide_position) < 0.000002f)
            {
                returning = false;
                Invoke("StartJump", jump_time);
            }
        }
    }

    public void SetPositions(Vector2 spawn_point, Vector2 jump_point)
    {
        hide_position = spawn_point;
        final_position = jump_point;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Cover")
        {
            reticle.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Cover")
        {
            reticle.SetActive(true);
        }
    }
}
