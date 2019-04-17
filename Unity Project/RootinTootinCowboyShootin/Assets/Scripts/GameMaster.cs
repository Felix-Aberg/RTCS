using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public List<GameObject> enemies;
    public List<Transform> spawnpoints_used;
    public float spawn_interval;
    public int special_waves;
    EnemySpawner es;
    float spawn_time;
    uint events_spawned;
    public const uint nr_of_events = 20;

    void Start()
    {
        es = GetComponent<EnemySpawner>();
        spawn_time = Time.time + spawn_interval * 2;
    }

    void FixedUpdate()
    {
        if (enemies.Count == 0 && Time.time < spawn_time)
        {
            spawn_time = Time.time - .1f;
        }

        if (Time.time > spawn_time)
        {
            spawn_time = Time.time + spawn_interval;

            if (spawnpoints_used.Count != 4)
            {
                if (events_spawned == nr_of_events)
                {
                    Debug.Log("it's bomb");
                }

                else if (events_spawned % special_waves == 0)
                {
                    SpawnPackage sp = es.SpawnSpecial(SpecialEnemies.BIGIRON);
                    enemies.Add(sp.enemy);
                    spawnpoints_used.Add(sp.spawn_point);
                }

                else
                {
                    SpawnPackage sp = es.SpawnBasic();
                    enemies.Add(sp.enemy);
                    spawnpoints_used.Add(sp.spawn_point);
                }
            }

            events_spawned++;
        }
    }

    public void ClearSpawn(GameObject dead_enemy) //Call after enemy death anim
    {
        int index = 0;

        foreach (GameObject enemy in enemies)
        {
            if (enemy == dead_enemy)
            {
                Destroy(enemies[index]);
                enemies.Remove(enemies[index]);
                spawnpoints_used.Remove(spawnpoints_used[index]);
                return;
            }

            index++;
        }
    }
}
