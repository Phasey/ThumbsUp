﻿//--------------------------------------------------------------------------------
// Author: Liam Knights
//--------------------------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Creates a class for the Player Death Script 
public class PlayerDeath : MonoBehaviour
{
    // Scene number can be adjusted for final build so it sends player back to right scene
    public int sceneNumber = 0;

    // Initialises two new GameObjects for both players
    public GameObject Player1;
    public GameObject Player2;

    //------------------------------------------------------------
    // Function is called when script first runs
    //------------------------------------------------------------
    void Awake() {}

    //------------------------------------------------------------
    // Function is called once every frame
    //------------------------------------------------------------
    void Update()
    {
        // Gets the Health Scripts for both players
        HealthScript P1Health = Player1.GetComponent<HealthScript>();
        HealthScript P2Health = Player2.GetComponent<HealthScript>();

        // If both players are dead, then load the menu screen
        if (P1Health.dead && P2Health.dead)
        {
            //loads whatever scene is placed in scene number
            SceneManager.LoadScene(sceneNumber);
        }
	}
}
