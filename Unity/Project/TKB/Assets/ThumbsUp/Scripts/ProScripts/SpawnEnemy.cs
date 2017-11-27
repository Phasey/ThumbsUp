using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------------
// Author: Matthew Le Nepveu.
//--------------------------------------------------------------------------------

public class SpawnEnemy : MonoBehaviour
{
    // Initialises an int determining all enemies and a float for the spawn delay time
    public int totalEnemies;
    public float spawnDelay;

    // Represents the enemy will will be spawned
    public GameObject enemy;

    // Private float determines the time between spawning
    private float spawnTimer = 0f;

    // Int keeps track of how many enemies have been spawned and starts at zero
    private int enemiesSpawned = 0;

    // Bool checks if spawning is enabled and is initialised to false
    private bool isEnabled = false;

    //--------------------------------------------------------------------------------
    // Function is called when script first runs.
    //--------------------------------------------------------------------------------
    void Awake() {}

    //--------------------------------------------------------------------------------
    // Function is called once every frame.
    //--------------------------------------------------------------------------------
    void Update()
    {
        // Spawning is ignored if spawner is not triggered
        if (!isEnabled)
            return;

        // Igm=nores spawning if enemies spawned exceeds the total enemies somehow
        if (enemiesSpawned >= totalEnemies)
            return;

        // Adds spawn timer by real time seconds
        spawnTimer += Time.deltaTime;

        // Runs code if the spawn timer exceeds the spawn delay
        if (spawnTimer >= spawnDelay)
        {
            // Creates an array of colliders to check if space is vacant
            Collider[] clearSpace = Physics.OverlapSphere(transform.position, 1);

            // Runs a for loop of the array of clear space
            for (int i = 0; i < clearSpace.Length; ++i)
            {
                // Ignores the rest of the if statement if space is vacant by a player or enemy
                if (clearSpace[i].tag == "Player" || clearSpace[i].tag == "Enemy")
                    return;
            }

            // Instatiates a new enemy at the spawn point
            Instantiate(enemy, transform.position, transform.rotation);

            // Adds one to the total number of enemies spawned
            ++enemiesSpawned;

            // Resets spawn timer back to zero
            spawnTimer = 0f;
        }
	}

    //--------------------------------------------------------------------------------
    // Function runs if an object has entered the trigger
    //
    // Param:
    //      other: Is the collider of the object that has entered trigger
    //--------------------------------------------------------------------------------
    void OnTriggerEnter(Collider other)
    {
        // isEnabled bool is set to true if a player has entered the trigger
        if (other.tag == "Player")
            isEnabled = true;
    }

    //--------------------------------------------------------------------------------
    // Function runs if an object has exited the trigger
    //
    // Param:
    //      other: Is the collider of the object that has exited trigger
    //--------------------------------------------------------------------------------
    void OnTriggerExit(Collider other)
    {
        // isEnabled bool is set to false if a player has exited the trigger
        if (other.tag == "Player")
            isEnabled = false;
    }
}
