 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Stages
{
    TUTORIAL,
    CUTSCENE,
    SALOON,
    WALLSHOOT,
    OUTSIDE,
    BOMBSTOMP,
    WIN,
    LOSE,
    ENDING
}

public class LevelFadeScript : MonoBehaviour
{
    private string scene;
    public float fade_time;
    public Image image;
    public AudioSource audio_source;
    private bool footsteps_enabled;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SwapLevel(Stages stage)
    {
        switch(stage)
        {
            case Stages.TUTORIAL:
                {
                    scene = "TutorialScene";
                    footsteps_enabled = false;
                    break;
                }

            case Stages.CUTSCENE:
                {
                    scene = "CutsceneScene";
                    footsteps_enabled = true;
                    break;
                }

            case Stages.SALOON:
                {
                    scene = "SaloonScene";
                    footsteps_enabled = false;
                    break;
                }

            case Stages.WALLSHOOT:
                {
                    scene = "ShootWallScene";
                    footsteps_enabled = true;
                    break;
                }

            case Stages.OUTSIDE:
                {
                    scene = "OutsideScene";
                    footsteps_enabled = true;
                    break;
                }

            case Stages.BOMBSTOMP:
                {
                    scene = "BombStompScene";
                    footsteps_enabled = true;
                    break;
                }

            case Stages.WIN:
                {
                    scene = "WinScene";
                    footsteps_enabled = false;
                    break;
                }

            case Stages.LOSE:
                {
                    scene = "GameOver";
                    footsteps_enabled = false;
                    break;
                }

            case Stages.ENDING:
                {
                    scene = "EndScene";
                    footsteps_enabled = false;
                    break;
                }
        }

        //Enable the fading coroutine
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        if (footsteps_enabled)
        {
            Invoke("PlayFootSteps", 0.15f);
        }

        for (float f = 0f; f <= 1; f += (Time.fixedDeltaTime / fade_time))
        {
            Color c = image.color;
            c.a = f;
            image.color = c;
            yield return null;
        }

        SceneManager.LoadScene(scene);
        


        for (float f = 1f; f >= 0; f -= (Time.fixedDeltaTime / fade_time))
        {
            Color c = image.color;
            c.a = f;
            image.color = c;
            yield return null;
        }
    }

    void PlayFootSteps()
    {
        audio_source.Play();
    }
}
