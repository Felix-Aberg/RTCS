using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LossSceneScript : MonoBehaviour
{
    public string[] cowboy_advice;
    public Text text;

    void Start()
    {
        text.text = cowboy_advice[Random.Range(0, cowboy_advice.Length)];
    }
}
