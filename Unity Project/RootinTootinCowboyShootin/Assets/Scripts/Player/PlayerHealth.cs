using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int lives;
    public int max_lives;
    public Image[] life_images;
    public Sprite heart_empty;
    public Sprite heart_full;

    AudioSource AS;

    public Image image;
    public float fade_in_time;
    public float fade_out_time;

    void Awake()
    {
        AS = GetComponent<AudioSource>();
        max_lives = lives;
    }

    void CheckHealth()
    {
        if (lives <= 0)
            FindObjectOfType<LevelFadeScript>().SwapLevel(Stages.LOSE);
    }

    public void ShootPlayer()
    {
        lives--;
        StartCoroutine(Fade());

        GameMaster GM = FindObjectOfType<GameMaster>();
        foreach (GameObject enemy in GM.enemies)
            enemy.GetComponentInChildren<EnemyBase>().CancelShoot();

        AS.Play();
        UpdateHealthbar();
        CheckHealth();
    }

    public void GiveLife()
    {
        lives++;

        foreach  (Image img in life_images)
        {
            if (img.sprite == heart_empty)
            {
                img.sprite = heart_full;
                return;
            }
        }
    }

    void UpdateHealthbar()
    {
        for (int i = 0; i < life_images.Length; i++)
        {
            if (i > lives - 1)
                life_images[i].sprite = heart_empty;
        }
    }

    IEnumerator Fade()
    {
        for (float f = 0f; f <= 1; f += (Time.fixedDeltaTime / fade_in_time))
        {
            Color c = image.color;
            c.a = f;
            image.color = c;
            yield return null;
        }

        for (float f = 1f; f >= 0; f -= (Time.fixedDeltaTime / fade_out_time))
        {
            Color c = image.color;
            c.a = f;
            image.color = c;
            yield return null;
        }
    }
}
