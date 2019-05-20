using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Events
{
    ENEMY,
    TWINENEMIES,
    SPECIALEVENT
}

public enum SpecialEvents
{
    NONE,
    SHOOTAHOLE,
    DANCEFORME,
    BOMBSTOMP
}

public class EventManager : MonoBehaviour
{

    // OTHER VARIABLES //

    [Tooltip("Enables debugs stating why an enemy wasn't spawned. Floods the console log when enabled.")]
    public bool spawn_rejection_debugs;

    [Tooltip("Maximum enemies allowed on screen simultaneously. \n" +
             "Will not spawn more enemies if that will make it exceed this number.")]
    public int max_enemies_on_screen;

    // REFERENCES //
    
    private GameMaster game_master;
    private EnemySpawner enemy_spawner;

    //private EnemySpawner enemy_spawner; //No longer used


    // ARRAY VARIABLES //

    public MultiList[] event_array;

    //Index counters to keep track of progress through all the arrays. 
    //POTENTIAL FUTURE BUG: Loading the next area (scene) will reset these values, ensure that they are persistent yet only in a playthrough.
    //*/

    private int event_array_index;

    void Start()
    {
        //Set variables and such
        game_master = gameObject.GetComponent<GameMaster>();
        enemy_spawner = gameObject.GetComponent<EnemySpawner>();
    }

    ///<summary>
    ///Checks the next event in the array and checks the conditions
    ///</summary>
    ///<returns>Returns true if it's ready to spawn the next event</returns>
    //Thought: Should it instead maybe just spawn the event instead of saying "yeah its ready"??
    public bool NextEventReady()
    {
        if (game_master.in_event)
        {
            return false;
        }

        switch (event_array[event_array_index].type_of_event)
        {
            case Events.ENEMY:
                {
                    //Insert fail conditions here! If it makes it past all checks it will spawn

                    //Check if the next enemy exists
                    if (event_array[event_array_index].enemy_variables.enemy == null)
                    {
                        if (spawn_rejection_debugs)
                            Debug.Log("Prevented enemy spawn. Enemy gameobject doesn't exist.");

                        return false;
                    }

                    //Check if there are too many enemies
                    if (game_master.spawnpoints_used.Count > max_enemies_on_screen - 1)
                    {
                        if(spawn_rejection_debugs)
                            Debug.Log("Prevented enemy spawn. Not enough space.");

                        return false;
                    }

                    //Check if the spawnpoint is occupied
                    foreach (Transform sp_used in game_master.spawnpoints_used)
                    {
                        if (enemy_spawner.SelectSpawnPoint(event_array[event_array_index].enemy_variables.spawnpoint) == sp_used)
                        {
                            if (spawn_rejection_debugs)
                                Debug.Log("Prevented enemy spawn. Spawnpoint is occupied!");

                            return false;
                        }
                    }

                    //If it makes it past all the checks
                    return true;
                }

            case Events.TWINENEMIES:
                {
                    //Insert fail conditions here! If it makes it past all checks it will spawn

                    //Check if the next enemy exists
                    if (event_array[event_array_index].enemy_variables.enemy == null || event_array[event_array_index].enemy_variables_twin.enemy == null)
                    {
                        if (spawn_rejection_debugs)
                            Debug.Log("Prevented twin enemy spawn. One of the enemy gameobjects doesn't exist.");

                        return false;
                    }

                    //Check if there are too many enemies
                    if (game_master.spawnpoints_used.Count > max_enemies_on_screen - 2)
                    {
                        if (spawn_rejection_debugs)
                            Debug.Log("Prevented twin enemy spawn. Not enough space.");

                        return false;
                    }

                    //Check if the spawnpoint is occupied
                    foreach (Transform sp_used in game_master.spawnpoints_used)
                    {
                        if (enemy_spawner.SelectSpawnPoint(event_array[event_array_index].enemy_variables.spawnpoint) == sp_used ||
                            enemy_spawner.SelectSpawnPoint(event_array[event_array_index].enemy_variables_twin.spawnpoint) == sp_used)
                        {
                            if (spawn_rejection_debugs)
                                Debug.Log("Prevented twin enemy spawn. Spawnpoint is occupied!");

                            return false;
                        }
                    }

                    //If it makes it past all the checks
                    return true;
                }

            case Events.SPECIALEVENT:
                {
                    //Insert fail conditions here! If it makes it past all checks it will spawn

                    if (game_master.spawnpoints_used.Count > 0)
                    {
                        Debug.Log("Prevented event spawn. Not enough space.");
                        return false;
                    }

                    //If it makes it past all the checks
                    return true;
                }
        }
        return false;
    }

    ///<summary>
    ///Checks what kind of event the next event in the array is.
    ///</summary>
    ///<returns>Returns the next event as an enum value.</returns>

    public Events CheckNextEvent()
    {
        return event_array[event_array_index].type_of_event;
    }

    ///<summary>
    ///Starts the next event in the event array index. Does NOT check if you can/should start the next event.
    ///</summary>
    public void StartEvent()
    {
        if (NextEventReady())
        {
            switch (event_array[event_array_index].type_of_event)
            {
                case Events.ENEMY:
                    {
                        SpawnEnemy();
                        break;
                    }

                case Events.TWINENEMIES:
                    {
                        SpawnTwinEnemies();
                        break;
                    }

                case Events.SPECIALEVENT:
                    {
                        SpawnSpecialEvent();
                        break;
                    }
            }
        }

        //Increment for future calls
        event_array_index++;
    }


    ///<summary>
    ///Checks the next event in the array and checks the conditions
    ///</summary>
    ///<returns>Returns true if it's ready to spawn the next event</returns>
    void SpawnEnemy()
    {
        game_master.SpawnEnemy(event_array[event_array_index].enemy_variables);
        Debug.Log("Spawn: " + Time.time);
    }

    void SpawnTwinEnemies()
    {
        game_master.SpawnEnemy(event_array[event_array_index].enemy_variables);
        game_master.SpawnEnemy(event_array[event_array_index].enemy_variables_twin);
    }

    void SpawnSpecialEvent()
    {
        switch (event_array[event_array_index].special_event)
        {
            case SpecialEvents.NONE:
                {
                    Debug.Log("Error! Called a non-existent event!");
                    break;
                }

            case SpecialEvents.DANCEFORME:
                {
                    //Load a scene? Do some DontDestroyOnLoad?
                    break;
                }

            case SpecialEvents.SHOOTAHOLE:
                {
                    DontDestroyOnLoad(GameObject.Find("DontDestroy"));
                    SceneManager.LoadScene("ShootWallScene");
                    break;
                }

            case SpecialEvents.BOMBSTOMP:
                {
                    //Load a scene? Do some DontDestroyOnLoad?+
                    //DontDestroyOnLoad(GameObject.Find("BombTimer")); Not needed because done previously??
                    SceneManager.LoadScene("BombStompScene");
                    break;
                }
        }
    }

    ///<summary>
    ///Resets all array index values. Call this whenever the game restarts/ends. Exists to avoid presistent bugs
    ///</summary>
    void ResetGame()
    {
        event_array_index = 0;
    }
}