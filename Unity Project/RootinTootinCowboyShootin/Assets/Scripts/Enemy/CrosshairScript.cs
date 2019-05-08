using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairScript : MonoBehaviour
{
    public Transform crosshair_tracker;
    public GameObject current_reticle;
    public GameObject current_enemy;
    public int reticles_correct = 0; //How many reticles that are correct
    GameObject game_master;

    void Start()
    {
        Cursor.visible = false;
        game_master = GameObject.Find("GameMaster");
    }

    void Update()
    {
        if (game_master.GetComponent<InputHandler>().shooting)
        {
            if (current_reticle != null)
            {
                Debug.Log("reticle exists");
                if (current_reticle.GetComponent<ReticleScript>().directions_correct)
                {
                    Debug.Log("directions correct");
                    if (current_reticle.GetComponent<ReticleScript>().crosshair_correct)
                    {
                        Debug.Log("crosshair correct");
                        Shoot();
                    }
                }
            }

            if (reticles_correct < 1)
                ShootFoot();
        }
    }

    void LateUpdate()
    {
        Vector2 temppos = crosshair_tracker.position;
        transform.position = temppos;
    }

    void Shoot()
    {
        GameObject.Find("GameMaster").GetComponent<GameMaster>().ClearSpawn(current_enemy);
    }

    void ShootFoot()
    {

        Debug.Log("Ouch owie my footsie");
    }
}
