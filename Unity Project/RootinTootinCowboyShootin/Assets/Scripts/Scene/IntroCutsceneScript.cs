using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCutsceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadSaloon", 3f);
    }

    void LoadSaloon()
    {
        SceneManager.LoadScene("SaloonScene");
    }
}
