using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BombTimer : MonoBehaviour
{
    public float bomb_timer;
    float time_left;

    public Image fuse;
    public Image fusefire;

    // Start is called before the first frame update
    void Start()
    {
        time_left = bomb_timer;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time_left -= Time.fixedDeltaTime;
        fuse.fillAmount = time_left / bomb_timer;
        fusefire.transform.position = new Vector2(8.5f - (17 * fuse.fillAmount), fusefire.transform.position.y);

        if(time_left <= 0)
        {
            fusefire.gameObject.SetActive(false);
            Debug.Log("Boom.");
            SceneManager.LoadScene("GameOver");
        }
    }
}
