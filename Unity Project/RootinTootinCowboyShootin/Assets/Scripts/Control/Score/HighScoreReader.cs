using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreReader : MonoBehaviour
{
    public Text text;
    bool once = false;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        Debug.Log("Text: " + GetComponent<Text>());
        text.text += "instantiate \n test";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RenderHighScores(int[] score, int length)
    {

        if (!once)
        {
            Debug.Log("Rendering high scores??");

            once = true;
            //Reset text
            text.text = "asdf123";
            text.text += "\n tesdt" + 2;

            /*
            for (int i = 0; i < length; i++)
            {
                text.text += (i + 1) + ". " + score[i] + "\n";
            }//*/
        }
    }
}
