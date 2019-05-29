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
        Score score = FindObjectOfType<Score>();
        if (score != null)
            score.SaveScore();
        else
            Debug.LogError("Scores not found when trying to update scores!!");
        Destroy(GameObject.Find("DontDestroy"));
        Invoke("GoToEndScene",2f);
    }

    void GoToEndScene()
    {
        SceneManager.LoadScene("EndScene");
    }
}
