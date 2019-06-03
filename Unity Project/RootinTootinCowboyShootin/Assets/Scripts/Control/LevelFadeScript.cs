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
    public float fade_time_gameover;
    public Image image;
    public AudioSource audio_source;
    private bool footsteps_enabled;

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
                    footsteps_enabled = false;
                    FindObjectOfType<MusicPlayer>().Invoke("ForceStartBattleTheme", 4.55f);
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

        float fade_in;
        float fade_out;

        if (scene != "GameOver")
        {
            fade_in = fade_time;
        }
        else
        {
            fade_in = fade_time_gameover;
        }

        if (SceneManager.GetActiveScene().name != "GameOver")
        {
            fade_out = fade_time;
        }
        else
        {
            fade_out = fade_time_gameover;
        }

        for (float f = 0f; f <= 1; f += (Time.fixedDeltaTime / fade_out))
        {
            Color c = image.color;
            c.a = f;
            image.color = c;
            yield return null;
        }

        SceneManager.LoadScene(scene);
        


        for (float f = 1f; f >= 0; f -= (Time.fixedDeltaTime / fade_in))
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
