using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<Score>().SaveScore();
        Destroy(GameObject.Find("DontDestroy"));
        Invoke("GoToEndScene",2f);
    }

    void GoToEndScene()
    {
        SceneManager.LoadScene("EndScene");
    }
}
