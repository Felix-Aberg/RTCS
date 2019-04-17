using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairScript : MonoBehaviour
{
    public GameObject current_reticle;
    public GameObject current_enemy;
    public int reticles_correct; //How many reticles that are correct

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
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
        Vector2 mousepos; mousepos = Input.mousePosition;
        mousepos = Camera.main.ScreenToWorldPoint(mousepos);
        transform.position = mousepos;
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
