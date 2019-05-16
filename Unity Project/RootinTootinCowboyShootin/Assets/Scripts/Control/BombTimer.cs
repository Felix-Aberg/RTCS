using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BombTimer : MonoBehaviour
{
    public float bomb_timer;
    float time_left;

    public Image fuse;
    public Image fuse_fire;

    void Start()
    {
        time_left = bomb_timer;
    }

    void FixedUpdate()
    {
        time_left -= Time.fixedDeltaTime;

        if(time_left <= 0)
        {
            Debug.Log("Boom.");
            SceneManager.LoadScene("GameOver");
            Destroy(gameObject);
        }

        if (SceneManager.GetActiveScene().name != "BombStompScene") 
        {
            fuse.fillAmount = time_left / bomb_timer;
            Vector2 temppos = new Vector2(690 * fuse.fillAmount, fuse_fire.transform.position.y);
            fuse_fire.transform.position = temppos;
        }
    }
}