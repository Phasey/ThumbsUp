﻿//-----------------------------------------------------------------------------
// Author: Matthew Le Nepveu.
//-----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Creates a class for the End Level Script 
public class EndLevel : MonoBehaviour
{
    // Used to store an array of enemies from the game
    public GameObject[] enemies;

    // Indicates the Closed Door as a GameObject
    public GameObject closedDoor;

    // Refers to the next level's scene number
    public int nextLevelScene;

    //--------------------------------------------------------------------------------
    // Function is called when script first runs.
    //--------------------------------------------------------------------------------
    void Awake() {}

    //--------------------------------------------------------------------------------
    // Function is called once every frame.
    //--------------------------------------------------------------------------------
    void Update()
    {
        // Finds all enemies in scene and stores in enemies array
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Deletes the closed door if there are no enemies left in scene
        if (enemies.Length <= 0)
            Destroy(closedDoor);
	}

    //--------------------------------------------------------------------------------
    // Function runs when a trigger is first detected.
    //
    // Param:
    // 		other: Refers to object of which Agent is triggering.
    //--------------------------------------------------------------------------------
    void OnTriggerEnter(Collider other)
    {
        // Runs next level's scene if a player collides with trigger
        if (other.tag == "Player")
            SceneManager.LoadScene(nextLevelScene);
    }
}
