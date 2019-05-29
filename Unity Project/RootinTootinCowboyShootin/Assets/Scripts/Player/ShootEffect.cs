using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEffect : MonoBehaviour
{
    public float display_time;
    public AudioClip[] sound_effects;
    AudioSource AS;

    void Start()
    {
        AS = GetComponent<AudioSource>(); 
        StartCoroutine(DisableSprite(display_time));
        int r = Random.Range(0, sound_effects.Length);
        AS.PlayOneShot(sound_effects[r]);
        StartCoroutine(DestroyObject(sound_effects[r].length));
    }

    IEnumerator DisableSprite(float time)
    {
        yield return new WaitForSeconds(time);
        GetComponent<SpriteRenderer>().enabled = false;
    }

    IEnumerator DestroyObject(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
