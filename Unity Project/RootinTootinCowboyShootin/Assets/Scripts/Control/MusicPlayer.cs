using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    public enum SongList
    {
        DEFAULT,
        BATTLE,
        SEA_SHANTY
    }

    public enum Transitioning
    {
        NONE,
        DEFAULT,
        BATTLE,
        KICKDOOR,
        SILENCE
    }

    public AudioSource audio_source_1;
    public AudioSource audio_source_2;
    public AudioSource audio_source_door;

    //Very extensive list of epic songs
    public AudioClip default_song;
    public AudioClip battle_song;
    public AudioClip sea_shanty_song;
    public AudioClip door_song;

    public Transitioning transitioning;

    // called first
    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void Win()
    {
        transitioning = Transitioning.DEFAULT;

        audio_source_1.Stop();
        audio_source_2.Stop();

        audio_source_1.Play();
        audio_source_2.Play();

        Invoke("ResetDefaultSong", default_song.length);
        Invoke("ResetBattleSong", battle_song.length);
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);

        if (scene.name == "EndScene" || scene.name == "WinScene")
        {
            transitioning = Transitioning.DEFAULT;

            audio_source_1.Stop();
            audio_source_2.Stop();

            audio_source_1.Play();
            audio_source_2.Play();

            Invoke("ResetDefaultSong", default_song.length);
            Invoke("ResetBattleSong", battle_song.length);
        }

        if (scene.name == "GameOver")
        {
            transitioning = Transitioning.SILENCE;
        }
        /*
        if(scene.name == "SaloonScene")
        {
            /*
            //SelectSong(SongList.BATTLE);
            //audio_source.Play();
            

            //play battle song
            //audio_source_1.volume = 0;
            //audio_source_2.volume = 1;
            transitioning = Transitioning.BATTLE;
        }
        //*/
    }

    public void OnRestart()
    {
        CancelInvoke();

        transitioning = Transitioning.DEFAULT;

        audio_source_1.Stop();
        audio_source_2.Stop();

        audio_source_1.Play();
        audio_source_2.Play();

        Invoke("ResetDefaultSong", default_song.length);
        Invoke("ResetBattleSong", battle_song.length);
    }

    public void OnIntroSkip()
    {
        CancelInvoke();

        transitioning = Transitioning.BATTLE;
    }

    // Start is called before the first frame update
    void Start()
    {
        audio_source_1.clip = default_song;
        audio_source_2.clip = battle_song;
        audio_source_1.Play();
        audio_source_2.Play();
        audio_source_1.volume = 1;
        audio_source_2.volume = 0;
    }
    
    // called when the game is terminated
    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void ResetDefaultSong()
    {
        if (audio_source_1.volume > 0.9f)
        {
            audio_source_1.Stop();
            audio_source_2.Stop();

            audio_source_1.Play();
            audio_source_2.Play();

            Invoke("ResetDefaultSong", default_song.length);
            Invoke("ResetBattleSong", battle_song.length);
        }
    }

    void ResetBattleSong()
    {
        if (audio_source_2.volume > 0.9f)
        {
            audio_source_1.Stop();
            audio_source_2.Stop();

            audio_source_1.Play();
            audio_source_2.Play();

            Invoke("ResetDefaultSong", default_song.length);
            Invoke("ResetBattleSong", battle_song.length);
        }
    }

    public void ForceStartBattleTheme()
    {
        transitioning = Transitioning.KICKDOOR;
        Invoke("KickTheDoor", 0.45f);
        audio_source_door.PlayDelayed(0.05f);
    }

    void KickTheDoor()
    {
        audio_source_1.volume = 0;
        audio_source_2.volume = 1;

        audio_source_1.Stop();
        audio_source_2.Stop();

        audio_source_1.Play();
        audio_source_2.Play();

        FindObjectOfType<ScreenShake>().StartShake(100f, 1f);

        Invoke("ResetDefaultSong", default_song.length);
        Invoke("ResetBattleSong", battle_song.length);
    }

    private void FixedUpdate()
    {
        //redo as switch?
        if (transitioning != Transitioning.NONE)
        {
            if (transitioning == Transitioning.DEFAULT)
            {
                audio_source_1.volume += 0.012f;
                audio_source_2.volume -= 0.012f;

                if (audio_source_1.volume > 0.99f)
                {
                    transitioning = Transitioning.NONE;
                }
            }
            else if (transitioning == Transitioning.BATTLE)
            {
                audio_source_1.volume -= 0.012f;
                audio_source_2.volume += 0.012f;

                if (audio_source_2.volume > 0.99f)
                {
                    transitioning = Transitioning.NONE;
                }
            }
            else if (transitioning == Transitioning.KICKDOOR)//Kicking open the saloon door
            {
                audio_source_1.volume -= 0.016f;

                if (audio_source_2.volume > 0.99f)
                {
                    audio_source_1.volume = 0f;
                    transitioning = Transitioning.NONE;
                }
            }
            else if (transitioning == Transitioning.SILENCE)//Kicking open the saloon door
            {
                Debug.Log("transitioning: silence");
                audio_source_1.volume -= 0.02f;
                audio_source_2.volume -= 0.02f;

                if (audio_source_1.volume < 0.02f)
                {
                    audio_source_1.volume = 0f;
                }

                if (audio_source_2.volume < 0.02f)
                {
                    audio_source_2.volume = 0f;
                }

                if (audio_source_1.volume < 0.02f && audio_source_2.volume < 0.02f)
                {
                    transitioning = Transitioning.NONE;
                }
                    
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            transitioning = Transitioning.DEFAULT;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            transitioning = Transitioning.BATTLE;
        }                   //*/
    }
}
