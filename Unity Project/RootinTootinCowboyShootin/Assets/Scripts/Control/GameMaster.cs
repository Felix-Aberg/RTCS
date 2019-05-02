using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public List<GameObject> enemies;
    public List<Transform> spawnpoints_used;
    public float spawn_interval;
    public float bomb_timer;
    float spawn_time;
    public int special_waves;
    EnemySpawner es;
    uint events_spawned;
    public const uint nr_of_events = 20;
    Scene scene;

    void Start()
    {
        es = GetComponent<EnemySpawner>();
        spawn_time = Time.time + spawn_interval * 2;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("EmilScene");
        }
    }

    void FixedUpdate()
    {
        if (enemies.Count == 0 && Time.time < spawn_time)
        {
            spawn_time = Time.time - .1f;
        }

        if (events_spawned == nr_of_events)
        {
                Debug.Log("it's bomb");
        }

        if (Time.time > spawn_time)
        {
            spawn_time = Time.time + spawn_interval;

            if (spawnpoints_used.Count != es.spawn_points.Length)
            {
                if (events_spawned % special_waves == 0)
                {
                    SpawnPackage sp = es.SpawnSpecial();
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
