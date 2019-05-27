using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeFrame : MonoBehaviour
{
    bool frozen;

    /// <summary>
    /// Freeze screen for time given in inspector
    /// </summary>
    /// <param name="time">Freeze duration</param>
    public void Freeze(float time)
    {
        if (!frozen)
            StartCoroutine(FreezeForDuration(time));
    }

    IEnumerator FreezeForDuration(float time)
    {
        frozen = true;
        float original_timescale = Time.timeScale;
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = original_timescale;
        frozen = false;
    }
}
