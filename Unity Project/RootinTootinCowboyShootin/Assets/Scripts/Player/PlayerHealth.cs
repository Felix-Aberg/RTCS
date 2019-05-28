﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int lives;
    public Image[] life_images;
    public Sprite health_empty;

    AudioSource AS;

    void Awake()
    {
        //AS = GetComponent<AudioSource>();
    }

    void CheckHealth()
    {
        if (lives <= 0)
            FindObjectOfType<LevelFadeScript>().SwapLevel(Stages.LOSE);
    }

    public void ShootPlayer()
    {
        lives--;
        //AS.Play();
        UpdateHealthbar();
        CheckHealth();
    }

    void UpdateHealthbar()
    {
        for (int i = 0; i < life_images.Length; i++)
        {
            if (i > lives - 1)
                life_images[i].sprite = health_empty;
        }
    }
}
