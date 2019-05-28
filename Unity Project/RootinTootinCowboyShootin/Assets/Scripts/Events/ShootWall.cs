using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShootWall : MonoBehaviour
{
    public Sprite wall_hole;

    public GameObject[] targets;
    public GameObject big_bang;

    bool last_target_hit;
        
    void Start()
    {
        foreach (GameObject reticle in targets)
        {
            reticle.GetComponent<ReticleScript>().InstantiateArrows(ArrowDirection.LEFT, ArrowDirection.RIGHT);
        }
    }

    public void CheckReticles()
    {
        int i = 0;

        foreach (GameObject reticle in targets)
        {
            if (!reticle.activeInHierarchy)
                i++;
        }

        if (i == targets.Length && !last_target_hit)
        {
            targets[targets.Length - 1].SetActive(true);
            last_target_hit = true;
        }

        else if (i == targets.Length && last_target_hit)
            EventComplete();
    }

    void EventComplete()
    {
        StartCoroutine(ToggleGameObject(big_bang, true, 0f));
        StartCoroutine(ToggleGameObject(big_bang, false, .4f));
        gameObject.GetComponent<SpriteRenderer>().sprite = wall_hole;
        StartCoroutine(GoToNextScene(1.5f));
    }

    IEnumerator GoToNextScene(float time)
    {
        yield return new WaitForSeconds(time);
        FindObjectOfType<LevelFadeScript>().SwapLevel(Stages.OUTSIDE);
    }
    IEnumerator ToggleGameObject(GameObject GO, bool enabled, float time) 
    {
        yield return new WaitForSeconds(time);
        GO.SetActive(enabled);
    }
}
