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
        Cursor.visible = false;
        Invoke("LoadSaloon", (float) VP.length);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Input.GetKeyDown(KeyCode.Mouse1))
        {
            CancelInvoke("LoadSaloon");
            FindObjectOfType<MusicPlayer>().OnIntroSkip();
            LoadSaloon();
        }
    }

    void LoadSaloon()
    {
        FindObjectOfType<LevelFadeScript>().SwapLevel(Stages.SALOON);
    }
}
