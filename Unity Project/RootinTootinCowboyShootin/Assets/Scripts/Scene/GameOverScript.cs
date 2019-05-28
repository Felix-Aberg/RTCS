using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverScript : MonoBehaviour
{
    AudioSource AS;
    public AudioClip boom;

    void Start()
    {
        //AS = GetComponent<AudioSource>();

        //if (SceneManager.GetActiveScene().name == "GameOver")
        //{
        //    AS.PlayOneShot(boom);
        //}

        FindObjectOfType<Score>().SaveScore();
        Destroy(GameObject.Find("DontDestroy"));
        Invoke("GoToEndScene",2f);
    }

    void GoToEndScene()
    {
        SceneManager.LoadScene("EndScene");
    }
}
