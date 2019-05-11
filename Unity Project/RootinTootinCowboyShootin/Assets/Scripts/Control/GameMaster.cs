using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    [Tooltip("A list of all actively in-game enemies.")]
    public List<GameObject> enemies;

    [Tooltip("A list of the spawnpoints used by the current enemies.")]
    public List<Transform> spawnpoints_used;

    [Tooltip("A queue of the time it took to kill the last three enemies. \n" +
             "Used to delay spawns for slower players.")]
    public Queue<float> death_queue;

    [Tooltip("Time until the first enemy spawns.")]
    public float first_spawn_delay;

    [Tooltip("MINIMUM time until the next normal enemy spawns. \n" +
             "Spawn velocity is added to the spawn time so it only makes up a part of it.")]
    public float single_spawn_delay;

    [Tooltip("MINIMUM time until the next pair of twins spawns. \n" +
         "Spawn velocity is added to the spawn time so it only makes up a part of it.")]
    public float twin_spawn_delay;

    [Tooltip("Work In Progress!!")] //Work in progress!!
    public float event_spawn_delay;

    public float[] first_death_queue_values;

    public bool in_event;

    private float death_queue_total;
    private float spawn_time;

    EventManager em;
    EnemySpawner es;

    Scene scene;

    void Start()
    {
        es = GetComponent<EnemySpawner>();
        em = GetComponent<EventManager>();
        spawn_time = Time.time + first_spawn_delay;

        death_queue_total += first_death_queue_values[0];
        death_queue_total += first_death_queue_values[1];
        death_queue_total += first_death_queue_values[2];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //Reloads the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void FixedUpdate()
    {
        if (em.CheckNextEvent() != Events.SPECIALEVENT && enemies.Count == 0 && Time.time < spawn_time)
        {
            spawn_time = Time.time - .1f;
        }

        if (Time.time > spawn_time)
        {
            //When the time comes to try and spawn an event
            if (em.NextEventReady())
            {
                //Start next event
                em.StartEvent();

                //Set the time for the next event to happen
                spawn_time = Time.time;

                switch(em.CheckNextEvent())
                {
                    case Events.ENEMY:
                        {
                            spawn_time += single_spawn_delay;
                            break;
                        }

                    case Events.TWINENEMIES:
                        {
                            spawn_time += twin_spawn_delay;
                            break;
                        }

                    case Events.SPECIALEVENT:
                        {
                            spawn_time += event_spawn_delay;
                            break;
                        }
                }

                //Add velocity to non-special events
            }
        }
    }

    public void SpawnEnemy(GameObject enemy)
    {
        SpawnPackage sp = es.SpawnBasic(enemy);
        enemies.Add(sp.enemy);  
        spawnpoints_used.Add(sp.spawn_point);
    }

    public void SpawnEnemy(GameObject enemy, SpawnPoint enum_spawnpoint, ArrowDirection dir1, ArrowDirection dir2)
    {
        SpawnPackage sp = es.SpawnBasic(enemy, enum_spawnpoint, dir1, dir2);
        enemies.Add(sp.enemy);
        spawnpoints_used.Add(sp.spawn_point);
    }

    public void SpawnEnemy(EnemyVariables enemy_variables)
    {
        SpawnPackage sp = es.SpawnBasic(enemy_variables);
        enemies.Add(sp.enemy);
        spawnpoints_used.Add(sp.spawn_point);
    }

    void LoadBombScene()
    {
        SceneManager.LoadScene("BombStompScene");
    }

    public void ClearSpawn(GameObject dead_enemy) //Call after enemy death anim
    {
        int index = 0;

        foreach (GameObject enemy in enemies)
        {
            if (enemy == dead_enemy)
            {
                RequeDeath(enemy.GetComponent<EnemyBase>().life_time);
                Destroy(enemies[index]);
                enemies.Remove(enemies[index]);
                spawnpoints_used.Remove(spawnpoints_used[index]);
                return;
            }

            index++;
        }
    }

    ///<summary>
    ///Call this whenever an enemy dies via the enemy.
    ///</summary>
    public void RequeDeath(float death_time)
    {
        EnqueDeath(death_time);
        DequeDeath();
    }

    ///<summary>
    ///Call this at the start of gamemaster and whenever an enemy dies.
    ///</summary>
    void EnqueDeath(float death_time)
    {
        death_queue_total += death_time;
        death_queue.Enqueue(death_time);
    }

    void DequeDeath()
    {
        float death_time = death_queue.Dequeue();
        death_queue_total -= death_time;
    }
}
