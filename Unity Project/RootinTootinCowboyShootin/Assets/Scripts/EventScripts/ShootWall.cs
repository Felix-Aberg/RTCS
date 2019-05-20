using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShootWall : MonoBehaviour
{
    public Sprite wall_hole;

    public GameObject[] targets;
        
    void Start()
    {
        foreach (GameObject reticle in targets)
        {
            reticle.GetComponent<ReticleScript>().InstantiateArrows(ArrowDirection.LEFT, ArrowDirection.RIGHT);
        }
    }

    void FixedUpdate()
    {

    }

    public void CheckReticles()
    {
        int i = 0;

        foreach (GameObject reticle in targets)
        {
            if (!reticle.activeInHierarchy)
                i++;
        }

        if (i == targets.Length)
            EventComplete();
    }

    void EventComplete()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = wall_hole;
        StartCoroutine(GoToNextScene(1.5f));
    }

    IEnumerator GoToNextScene(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("OutsideScene");
    }
}
