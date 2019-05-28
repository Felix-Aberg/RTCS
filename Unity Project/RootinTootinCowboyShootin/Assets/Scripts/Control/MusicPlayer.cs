﻿using System.Collections;
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

    private enum Transitioning
    {
        NONE,
        DEFAULT,
        BATTLE
    }

    public AudioSource audio_source_1;
    public AudioSource audio_source_2;

    //Very extensive list of epic songs
    public AudioClip default_song;
    public AudioClip battle_song;
    public AudioClip sea_shanty_song;

    private Transitioning transitioning;

    // called first
    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);

        if (scene.name =="WinScene" || scene.name == "GameOver")
        {
            /*
            SelectSong(SongList.DEFAULT);
            audio_source.Play();
            //*/

            //play default song
            //audio_source_1.volume = 1;
            //audio_source_2.volume = 0;
            transitioning = Transitioning.DEFAULT;
        }

        if(scene.name == "SaloonScene")
        {
            /*
            SelectSong(SongList.BATTLE);
            audio_source.Play();
            //*/

            //play battle song
            //audio_source_1.volume = 0;
            //audio_source_2.volume = 1;
            transitioning = Transitioning.BATTLE;
        }
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

    void resetDefaultSong()
    {
        if (audio_source_1.volume > 0.9f)
        {
            audio_source_1.Stop();
            audio_source_2.Stop();

            audio_source_1.Play();
            audio_source_2.Play();

            Invoke("resetDefaultSong", default_song.length);
            Invoke("resetBattleSong", battle_song.length);
        }
    }

    void resetBattleSong()
    {
        if (audio_source_2.volume > 0.9f)
        {
            audio_source_1.Stop();
            audio_source_2.Stop();

            audio_source_1.Play();
            audio_source_2.Play();

            Invoke("resetDefaultSong", default_song.length);
            Invoke("resetBattleSong", battle_song.length);
        }
    }

    private void Update()
    {
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
            else //Battle
            {
                audio_source_1.volume -= 0.012f;
                audio_source_2.volume += 0.012f;

                if (audio_source_2.volume > 0.99f)
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
        }//*/
    }

    // Update is called once per frame
    /*
    public void SelectSong(SongList song)
    {
        switch(song)
        {
            case SongList.DEFAULT:
                audio_source.clip = default_song;
                audio_source.Play();
                break;

            case SongList.BATTLE:
                audio_source.clip = battle_song;
                audio_source.Play();
                break;

            case SongList.SEA_SHANTY:
                audio_source.clip = sea_shanty_song;
                audio_source.Play();
                break;
        }
    }//*/
}
