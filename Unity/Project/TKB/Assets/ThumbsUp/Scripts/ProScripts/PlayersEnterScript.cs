using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------------------------------------------------------------
//Author: Liam Knights
//---------------------------------------------------------------

public class PlayersEnterScript : MonoBehaviour
{
    public GameObject[] Players;
    public GameObject DoorAfterEnter;

    // Use this for initialization
    void Awake()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
    }
	
	// Update is called once per frame
	void Update()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other == Players[0] && other == Players[1])
        {
            DoorAfterEnter.SetActive(true);
        }
    }
}
