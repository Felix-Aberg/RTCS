using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int lives;
    public Image[] life_images;

    void CheckHealth()
    {
        if (lives <= 0)
            SceneManager.LoadScene("GameOver");
    }

    public void ShootPlayer()
    {
        lives--;
        UpdateHealthbar();
        CheckHealth();
    }

    void UpdateHealthbar()
    {
        for (int i = 0; i < life_images.Length; i++)
        {
            if (i > lives - 1)
                life_images[i].enabled = false;
        }
    }
}
