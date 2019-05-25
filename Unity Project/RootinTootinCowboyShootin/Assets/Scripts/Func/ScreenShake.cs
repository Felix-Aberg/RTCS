using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    float ShakeAmount;
    Vector3 original_camerapos;

    /// <summary>
    /// Shake the screen for time in seconds
    /// </summary>
    /// <param name="amount">Shake amount</param>
    /// <param name="time">Shake duration</param>
    public void StartShake(float amount, float time)
    {
        original_camerapos = Camera.main.transform.position;

        ShakeAmount = amount;

        InvokeRepeating("Shake", 0, 0.01f);
        Invoke("StopShake", time);
    }

    void Shake()
    {
        if (ShakeAmount > 0)
        {
            Vector3 camerapos = Camera.main.transform.position;

            float offsetX = Random.value * ShakeAmount * 2 - ShakeAmount;
            float offsety = Random.value * ShakeAmount * 2 - ShakeAmount;
            camerapos.x += offsetX;
            camerapos.y += offsety;

            Camera.main.transform.position = camerapos;
        }
    }

    void StopShake()
    {
        CancelInvoke("Shake");
        Camera.main.transform.position = original_camerapos;
    }
}
