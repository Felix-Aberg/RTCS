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
        //AS = GetComponent<AudioSource>(); enable when audio is implemented
        StartCoroutine(DisableSprite(display_time));
        StartCoroutine(DestroyObject(display_time)); // delete when audio is implemented;
        //int r = Random.Range(0, sound_effects.Length);
        //AS.PlayOneShot(sound_effects[r]);
        //StartCoroutine(DestroyObject(sound_effects[r].length)); enable when audio is implemented
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
