using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroCutsceneScript : MonoBehaviour
{
    public VideoPlayer VP;

    void Start()
    {
        Invoke("LoadSaloon", (float) VP.length);
    }

    void LoadSaloon()
    {
        FindObjectOfType<LevelFadeScript>().SwapLevel(Stages.SALOON);
    }
}
