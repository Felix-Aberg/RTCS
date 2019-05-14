using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    /* //Default SpawnBasic
    //Cut due to being replaced by EventManager.cs
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
    }//

    public SpawnPackage SpawnBasic(GameObject enemy)
    {
        Transform spawnpoint = SelectSpawnPoint();

        GameObject clone = Instantiate(enemy,
                                       spawnpoint.position,
                                       Quaternion.identity);

        clone.GetComponent<EnemyBase>().SetPositions(spawnpoint.position, spawnpoint.GetChild(0).transform.position);

        SpawnPackage sp;
        sp.enemy = clone;
        sp.spawn_point = spawnpoint;
        return sp;
    }//

    public SpawnPackage SpawnBasic(GameObject enemy, SpawnPoint spawnpoint_index, ArrowDirection dir1, ArrowDirection dir2)
    {
        Transform spawnpoint = SelectSpawnPoint(spawnpoint_index);

        GameObject clone = Instantiate(enemy,
                                       spawnpoint.position,
                                       Quaternion.identity);

        clone.GetComponent<EnemyBase>().SetPositions(spawnpoint.position, spawnpoint.GetChild(0).transform.position);
        clone.GetComponent<EnemyBase>().reticle.GetComponent<ReticleScript>().InstantiateArrows(dir1, dir2);

        SpawnPackage sp;
        sp.enemy = clone;
        sp.spawn_point = spawnpoint;
        return sp;
    }//*/

    public SpawnPackage SpawnBasic(EnemyVariables enemy_variables)
    {
        Transform spawnpoint = SelectSpawnPoint(enemy_variables.spawnpoint);
        GameObject clone = Instantiate(enemy_variables.enemy,
                                       spawnpoint.position,
                                       Quaternion.identity);

        clone.GetComponent<EnemyBase>().SetPositions(spawnpoint.position, spawnpoint.GetChild(0).transform.position);
        clone.GetComponent<EnemyBase>().reticle.GetComponent<ReticleScript>().InstantiateArrows(enemy_variables.arrow_direction_1, enemy_variables.arrow_direction_2);
        SpawnPackage sp;
        sp.enemy = clone;
        sp.spawn_point = spawnpoint;
        return sp;
    }

    /* //SpawnSpecial
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
    //*/

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
