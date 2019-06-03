using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneScript : MonoBehaviour
{
    public float restart_time;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Restart", restart_time);
    }

    void Update()
    {
        if (FindObjectOfType<InputHandler>().shooting)
        {
            CancelInvoke("Restart");
            Restart();
        }
    }

    void Restart()
    {
        FindObjectOfType<LevelFadeScript>().SwapLevel(Stages.TUTORIAL);
    }
}
