using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpecialEnemies
{
    BIGIRON
};

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] basic_enemies;
    public GameObject[] special_enemies; //Add enemies in enum order

    public Transform[] spawn_points;
    
    public void SpawnBasic()
    {
        GameObject clone = Instantiate(basic_enemies[Random.Range(0, basic_enemies.Length - 1)],
                                       spawn_points[Random.Range(0, basic_enemies.Length - 1)].position,
                                       Quaternion.identity);
    }

    public void SpawnSpecial(SpecialEnemies special_enemy)
    {
        GameObject clone = Instantiate(basic_enemies[(int)special_enemy],
                                       spawn_points[Random.Range(0, basic_enemies.Length - 1)].position,
                                       Quaternion.identity);
    }

}
