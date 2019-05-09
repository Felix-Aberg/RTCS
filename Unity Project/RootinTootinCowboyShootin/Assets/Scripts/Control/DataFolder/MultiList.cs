using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MultiList
{
    public enum Status
    {
        ENEMY,
        TWINENEMIES,
        EVENT
    }

    public Status type_of_event;

    public EnemyVariables enemy_variables;

    public EnemyVariables enemy_variables_twin;

    public SpecialEvents special_event;

    public int test_integer;
     
    public bool controllable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
