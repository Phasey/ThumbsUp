//---------------------------------------------------------------
//script not going to be used in final cut
//
//Author: Liam Knights
//---------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayersEnterScript : MonoBehaviour
{
    //gets the players
    public GameObject[] Players;
    //gets the game object that will be the spawned door
    public GameObject DoorAfterEnter;

    // Use this for initialization
    void Awake()
    {
        //checks id players has the player tag
        Players = GameObject.FindGameObjectsWithTag("Player");
    }
	
	// Update is called once per frame
	void Update()
    {
		
	}

    //-------------------------------------------------------------
    //on trigger event that checks for other 
    //-------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        //checks if other is both player one and player two
        if(other == Players[0] && other == Players[1])
        {
            //yes it activates the door when the players enter
            DoorAfterEnter.SetActive(true);
        }
    }
}
