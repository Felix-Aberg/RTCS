using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SpawnPackage
{
    public GameObject enemy;
    public Transform spawn_point;
}

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] basic_enemies;
    public List<GameObject> special_enemies;

    public Transform[] spawn_points;

    [Tooltip("Height offset of enemy spawning compared to spawnpoint depth controller. Higher value = further up.")]
    public float offset = 0.1f;

    public SpawnPackage SpawnBasic()
    {
        Transform spawnpoint = SelectSpawnPoint();

        GameObject clone = Instantiate(basic_enemies[Random.Range(0, basic_enemies.Length)],
                                       spawnpoint.position,
                                       Quaternion.identity);

        clone.GetComponent<EnemyBase>().SetPositions(spawnpoint.position, spawnpoint.GetChild(0).transform.position);

        SpawnPackage sp;
        sp.enemy = clone;
        sp.spawn_point = spawnpoint;
        return sp;
    }

    public SpawnPackage SpawnSpecial()
    {
        //return SpawnBasic();
        Transform spawnpoint = SelectSpawnPoint();

        if (special_enemies[0] != null )
        {
            GameObject clone = Instantiate(special_enemies[0],
                                       spawnpoint.position,
                                       Quaternion.identity);

            special_enemies.Remove(special_enemies[0]);
            clone.GetComponent<EnemyBase>().SetPositions(spawnpoint.position, spawnpoint.GetChild(0).transform.position);

            SpawnPackage sp;
            sp.enemy = clone;
            sp.spawn_point = spawnpoint;
            return sp;
        }
        else
        {
            return SpawnBasic();
        }

        
    }

    Vector2 AdjustSpawnHeight(GameObject enemy, Transform spawnpoint)
    {
        /*
         * 1 Get position of both enemy depthcontroller and sp depthcontroller
         * 2 check enemy depthcontroller local position
         * 3 set the enemy's hide position & position to spawnpoint.position subtracted by the enemy depthcontrollers localposition
         * 4 return it
         */

        Vector2 foot_offset = enemy.transform.Find("Prefab_Depthcontroller").localPosition;
        Vector2 sdc = spawnpoint.transform.Find("Prefab_Depthcontroller").position;

        Vector2 adjusted_position = sdc - foot_offset - new Vector2 (0, offset);

        return adjusted_position;
    }

    Transform SelectSpawnPoint()
    {
        Transform spawnpoint;
        RandomSpawn:
        spawnpoint = spawn_points[Random.Range(0, spawn_points.Length)];
        foreach (Transform sp in GetComponent<GameMaster>().spawnpoints_used)
        {
            if (spawnpoint == sp)
            goto RandomSpawn;
        }

        return spawnpoint;
    }
}
