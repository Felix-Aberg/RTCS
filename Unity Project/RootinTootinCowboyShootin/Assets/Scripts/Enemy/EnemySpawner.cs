using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SpawnPoint
{
    UP_LEFT,
    UP_RIGHT,
    DOWN_LEFT,
    DOWN_RIGHT,
    RANDOM
}

public struct SpawnPackage
{
    public GameObject enemy;
    public Transform spawn_point;
}

public class EnemySpawner : MonoBehaviour
{
    //Cut due to being replaced by EventManager.cs
    //public GameObject[] basic_enemies;
    //public List<GameObject> special_enemies;

    public Transform[] spawn_points;

    [Tooltip("Height offset of enemy spawning compared to spawnpoint depth controller. Higher value = further up.")]
    public float offset = 0.1f;
    bool is_outside_scene;

    public SpawnPackage SpawnBasic(EnemyVariables enemy_variables)
    {
        Transform spawnpoint = SelectSpawnPoint(enemy_variables.spawnpoint);
        GameObject clone = Instantiate(enemy_variables.enemy,
                                       spawnpoint.position,
                                       Quaternion.identity);

        EnemyBase eb = clone.GetComponentInChildren<EnemyBase>();
        eb.SetPositions(spawnpoint.position, spawnpoint.GetChild(0).position);
        eb.reticle.GetComponent<ReticleScript>().InstantiateArrows(enemy_variables.arrow_direction_1, enemy_variables.arrow_direction_2);

        SpawnPackage sp;
        sp.enemy = clone;
        sp.spawn_point = spawnpoint;

        Debug.Log("Enemy spawned");
        if (enemy_variables.spawnpoint == SpawnPoint.UP_LEFT || enemy_variables.spawnpoint == SpawnPoint.UP_RIGHT)
        {
            Debug.Log("scale adjusted");
            clone.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
        }

        return sp;
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

    public Transform SelectSpawnPoint()
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

    public Transform SelectSpawnPoint(SpawnPoint spawnpoint)
    {
    switch (spawnpoint)
        {
            case SpawnPoint.UP_LEFT:
                return spawn_points[0];

            case SpawnPoint.UP_RIGHT:
                return spawn_points[1];

            case SpawnPoint.DOWN_LEFT:
                return spawn_points[2];

            case SpawnPoint.DOWN_RIGHT:
                return spawn_points[3];

            case SpawnPoint.RANDOM: //currently broken :(
                return SelectSpawnPoint();
        }

    return null;
    }
}
