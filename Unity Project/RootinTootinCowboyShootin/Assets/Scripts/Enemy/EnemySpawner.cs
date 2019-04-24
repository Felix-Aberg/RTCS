﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpecialEnemies
{
    BIGIRON
};

public struct SpawnPackage
{
    public GameObject enemy;
    public Transform spawn_point;
}

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] basic_enemies;
    public GameObject[] special_enemies; //Add enemies in enum order

    public Transform[] spawn_points;
    
    public SpawnPackage SpawnBasic()
    {
        Transform spawnpoint = SelectSpawnPoint();

        GameObject clone = Instantiate(basic_enemies[Random.Range(0, basic_enemies.Length)],
                                       spawnpoint.position,
                                       Quaternion.identity);

        clone.GetComponent<EnemyBase>().SetPositions(spawnpoint.position, spawnpoint.GetComponentInChildren<Transform>().position);

        SpawnPackage sp;
        sp.enemy = clone;
        sp.spawn_point = spawnpoint;
        return sp;
    }

    public SpawnPackage SpawnSpecial(SpecialEnemies special_enemy)
    {
        Transform spawnpoint = SelectSpawnPoint();

        GameObject clone = Instantiate(special_enemies[(int)special_enemy],
                                       spawnpoint.position,
                                       Quaternion.identity);

        clone.GetComponent<EnemyBase>().SetPositions(spawnpoint.position, spawnpoint.GetComponentInChildren<Transform>().position);

        SpawnPackage sp;
        sp.enemy = clone;
        sp.spawn_point = spawnpoint;
        return sp;
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
