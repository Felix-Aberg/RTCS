using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class LossSceneScript : MonoBehaviour
{
    public VideoPlayer vp;
    public RawImage image;

    public string[] cowboy_advice;
    public Text text;

    void Start()
    {
        Invoke("EnableText", 2.5f);
        Invoke("DisableVideo", (float)vp.length);
        Invoke("GoToEndScene", (float)vp.length + 5);
        Score score = FindObjectOfType<Score>();
        if (score != null)
            score.SaveScore();
        else
            Debug.LogError("Scores not found when trying to update scores!!");
        Destroy(GameObject.Find("DontDestroy"));
    }

    void EnableText()
    {
        text.enabled = true;
        text.text = cowboy_advice[Random.Range(0, cowboy_advice.Length)];
    }

    void DisableVideo()
    {
        image.enabled = false;
    }

    void GoToEndScene()
    {
        FindObjectOfType<LevelFadeScript>().SwapLevel(Stages.ENDING);
    }
}
