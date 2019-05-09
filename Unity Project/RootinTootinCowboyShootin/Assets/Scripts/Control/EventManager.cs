using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Events
{
    ENEMY,
    TWINENEMIES,
    SPECIALEVENT
}

public enum SpecialEvents
{
    SHOOTAHOLE,
    DANCEFORME,
    BOMBSTOMP
}

public class EventManager : MonoBehaviour
{
        // REFERENCES //

    private GameMaster game_master;
    //private EnemySpawner enemy_spawner; //No longer used


    // ARRAY VARIABLES //

    public MultiList[] test_array;


    [Tooltip("The list of all events happening in the game in order. \n" +
        "It will then access the next event in one of the event type arrays.")]
    public Events[] events;

    [Tooltip("Singular enemies appearing one at a time.")]
    public EnemyVariables[] enemies;

    [Tooltip("Two enemies appearing at the same time, grouped in pairs.")]
    public GameObject[] twin_enemies;

    [Tooltip("Special events like stomping the bomb and such.")]
    public SpecialEvents[] special_events;

    //Index counters to keep track of progress through all the arrays. 
    //POTENTIAL FUTURE BUG: Loading the next area (scene) will reset these values, ensure that they are persistent yet only in a playthrough.
    private int events_array_index;
    private int enemies_array_index;
    private int twin_enemies_array_index;
    private int special_event_array_index;

        // OTHER VARIABLES //


    void Start()
    {
        //Set variables and such
        game_master = gameObject.GetComponent<GameMaster>();
    }

    void Update()
    {
        
    }

    ///<summary>
    ///Checks the next event in the array and checks the conditions
    ///</summary>
    ///<returns>Returns true if it's ready to spawn the next event</returns>
    //Thought: Should it instead maybe just spawn the event instead of saying "yeah its ready"??
    bool CheckForNextEvent()
    {
        switch (events[events_array_index + 1])
        {
            case Events.ENEMY:
                {
                    //Check if an enemy can be spawned
                    break;
                }

            case Events.TWINENEMIES:
                {
                    //Check if two enemies can be spawned
                    break;
                }

            case Events.SPECIALEVENT:
                {
                    //Check if a special event can start (just no enemies(?))
                    break;
                }
        }
        return false;
    }

    ///<summary>
    ///Starts the next event in the event array index. Does NOT check if you can/should start the next event.
    ///</summary>
    void StartEvent()
    {
        switch (events[events_array_index])
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

        //Increment for future calls
        events_array_index++;
    }


    ///<summary>
    ///Checks the next event in the array and checks the conditions
    ///</summary>
    ///<returns>Returns true if it's ready to spawn the next event</returns>
    void SpawnEnemy()
    {
        game_master.SpawnEnemy(enemies[enemies_array_index].enemy);
        enemies_array_index++;
    }

    void SpawnTwinEnemies()
    {
        game_master.SpawnEnemy(twin_enemies[twin_enemies_array_index]);
        twin_enemies_array_index++;

        game_master.SpawnEnemy(twin_enemies[twin_enemies_array_index]);
        twin_enemies_array_index++;

    }

    void SpawnSpecialEvent()
    {
        switch (special_events[special_event_array_index])
        {
            case SpecialEvents.DANCEFORME:
                {
                    //Load a scene? Do some DontDestroyOnLoad?
                    break;
                }

            case SpecialEvents.SHOOTAHOLE:
                {
                    //Load a scene? Do some DontDestroyOnLoad?
                    break;
                }

            case SpecialEvents.BOMBSTOMP:
                {
                    //Load a scene? Do some DontDestroyOnLoad?+
                    break;
                }
        }

        special_event_array_index++;
    }

    ///<summary>
    ///Resets all array index values. Call this whenever the game restarts/ends. Exists to avoid presistent bugs
    ///</summary>
    void ResetGame()
    {
        events_array_index = 0;
        enemies_array_index = 0;
        twin_enemies_array_index = 0;
        special_event_array_index = 0;
    }
}