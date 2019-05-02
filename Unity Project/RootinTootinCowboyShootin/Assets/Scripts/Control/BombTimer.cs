using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BombTimer : MonoBehaviour
{

    public float bomb_timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bomb_timer -= Time.fixedDeltaTime;

        if(bomb_timer <= 0)
        {
            //Boom.
            SceneManager.LoadScene("GameOver");
        }
    }
}
