using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBase : MonoBehaviour
{
    public GameObject reticle;
    Animator animator;

    public ArrowDirection indicated_arrow_1;
    public ArrowDirection indicated_arrow_2;

    [Tooltip("Don't touch this, public for the sake of passing reference ;w;")]
    public float life_time;

    public Vector2 hide_position; //Where the enemy hides
    public Vector2 final_position; //Where the enemy jumps out

    public float jump_time;
    bool jumping;
    bool returning;
    bool dead;

    public float jump_speed = 10;
    public float shoot_time = 5;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Invoke("StartJump", jump_time);
    }

    void StartJump()
    {
        jumping = true;
    }

    void Shoot()
    {
        //Play shooting anim
        GameObject.Find("Prefab_Player").GetComponent<PlayerHealth>().ShootPlayer();
        animator.SetBool("Shooting", false);
        StartReturn();
    }

   void StartReturn()
    {
        animator.SetBool("Walking", true);
        returning = true;
    }


    void FixedUpdate()
    {
        if (!dead)
        {
            life_time += Time.fixedDeltaTime;

            if (jumping)
            {
                transform.position = Vector2.MoveTowards(transform.position, final_position, jump_speed / 100);

                //To disable jumping
                if (Vector2.Distance(transform.position, final_position) < 0.000002f)
                {
                    jumping = false;
                    animator.SetBool("Walking", false);
                    animator.SetBool("Shooting", true);
                    Invoke("Shoot", shoot_time);
                }
            }

            else if (returning)
            {
                transform.position = Vector2.MoveTowards(transform.position, hide_position, jump_speed / 100);

                if (Vector2.Distance(transform.position, hide_position) < 0.000002f)
                {
                    returning = false;
                    animator.SetBool("Walking", false);
                    Invoke("StartJump", jump_time);
                }
            }
        }
    }

    public void SetPositions(Vector2 spawn_point, Vector2 jump_point)
    {
        hide_position = spawn_point;
        final_position = jump_point;
    }

    public void OnDeath()
    {
        dead = true;
        animator.SetBool("Dead", true);
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
