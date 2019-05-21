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

    public AudioSource audio_source;

    //Very extensive list of epic songs
    public AudioClip default_song;
    public AudioClip battle_song;
    public AudioClip sea_shanty_song;

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
            SelectSong(SongList.DEFAULT);
            audio_source.Play();
        }

        if(scene.name == "SaloonScene")
        {
            SelectSong(SongList.BATTLE);
            audio_source.Play();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SelectSong(SongList.DEFAULT);
        audio_source.Play();
        DontDestroyOnLoad(gameObject);
    }
    
    // called when the game is terminated
    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SelectSong(SongList.DEFAULT);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            SelectSong(SongList.BATTLE);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            SelectSong(SongList.SEA_SHANTY);
        }
    }

    // Update is called once per frame
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
    }
}
