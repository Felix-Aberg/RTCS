using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemyBase : MonoBehaviour
{
    public GameObject reticle;
    Animator animator;
    SpriteRenderer sr;

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
    Color temp_color;

    public float jump_speed = 10;
    public float shoot_time = 3f;
    public float shoot_time_saloon = 6.5f;
    public float shoot_time_outside = 4.5f;

    AudioSource AS;
    public AudioClip yeehaw;
    public AudioClip oof;
    public AudioClip shoot;

    bool once;


    // Start is called before the first frame update
    void Start()
    {
        shoot_time = 3f;
        shoot_time_saloon = 6.5f;
        shoot_time_outside = 4.5f;


        AS = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        if (SceneManager.GetActiveScene().name == "SaloonScene")
        {
            shoot_time = shoot_time_saloon;
        }
        else
        {
            shoot_time = shoot_time_outside;
        }

        Invoke("StartJump", jump_time);
        temp_color = Color.white;
    }

    void StartJump()
    {
        animator.SetBool("Walking", true);
        jumping = true;
    }

    void Shoot()
    {
        if (!dead)
        {
            FindObjectOfType<PlayerHealth>().ShootPlayer();
            AS.PlayOneShot(shoot);
            animator.SetBool("Shooting", false);
            StartReturn();
        }
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
                transform.parent.position = Vector2.MoveTowards(transform.parent.position, final_position, jump_speed / 100);

                //To disable jumping
                if (Vector2.Distance(transform.parent.position, final_position) < 0.000002f)
                {
                    jumping = false;
                    animator.SetBool("Walking", false);
                    animator.SetBool("Shooting", true);
                    Invoke("Shoot", shoot_time);
                }
            }

            else if (returning)
            {
                transform.parent.position = Vector2.MoveTowards(transform.parent.position, hide_position, jump_speed / 100);

                if (Vector2.Distance(transform.parent.position, hide_position) < 0.000002f)
                {
                    returning = false;
                    animator.SetBool("Walking", false);
                    Invoke("StartJump", jump_time);
                }
            }
        }
        else if (dead)
        {
            temp_color.a -= Time.deltaTime / 1.2f;
            sr.color = temp_color;
        }
    }

    public void SetPositions(Vector2 spawn_point, Vector2 jump_point)
    {
        hide_position = spawn_point;
        final_position = jump_point;
    }

    public void OnDeath()
    {
        if (!dead)
        {
            AS.Stop();
            AS.PlayOneShot(oof);
            FindObjectOfType<Score>().GiveScoreEnemy(life_time);
            dead = true;
            animator.SetTrigger("Dead");
            reticle.SetActive(false);
            GetComponent<CapsuleCollider2D>().enabled = false;
            GameObject.Find("Crosshair").GetComponent<CrosshairScript>().ChangeCrosshair(0);
            Invoke("Dead", 1.2f);
        }
    }

    void Dead()
    {
        //Debug.Log("void Dead() called");
        GameObject.Find("GameMaster").GetComponent<GameMaster>().ClearSpawn(transform.parent.gameObject);
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
            if (!once)
            {
                once = true;
                AS.PlayOneShot(yeehaw);
            }

            reticle.SetActive(true);
        }
    }
}
