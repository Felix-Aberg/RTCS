using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEffect : MonoBehaviour
{
    public float display_time;
    public AudioClip sound_effect;
    AudioSource a_s;

    void Start()
    {
        //a_s = GetComponent<AudioSource>(); enable when audio is implemented
        //StartCoroutine(DestroyObject(sound_effect.length)); enable when audio is implemented
        StartCoroutine(DisableSprite(display_time));
        StartCoroutine(DestroyObject(display_time)); // delete when audio is implemented;
        //a_s.PlayOneShot(sound_effect);
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
