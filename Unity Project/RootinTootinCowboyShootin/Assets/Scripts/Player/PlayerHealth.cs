using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int lives;

    void CheckHealth()
    {
        if (lives <= 0)
            Debug.Log("Game Over"); //lose game
    }

    public void ShootPlayer()
    {
        lives--;
        CheckHealth();
    }
}
