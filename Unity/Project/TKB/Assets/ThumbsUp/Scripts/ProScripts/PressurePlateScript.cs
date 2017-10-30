﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates a class for the Pressure Plate Script 
public class PressurePlateScript : MonoBehaviour
{
	// Creates two new GameObjects that are used in this script
    public GameObject doorClosed;
    public GameObject doorOpen;

    //------------------------------------------------------------
    // Function is called when script first runs
    //------------------------------------------------------------
    void Awake() {}

	//------------------------------------------------------------
	// Function is called once every frame
	//------------------------------------------------------------
	void Update() {}

	//------------------------------------------------------------
	// Function runs when a trigger is first detected
	//
	// Param:
	// 		other: Refers to object of which Agent is triggering
	//------------------------------------------------------------
    void OnTriggerEnter(Collider other)
    {
        // Checks if a GameObject with tag "Crate" is triggering function
        if (other.gameObject.tag == "Crate")
        {
            // Instantiates the open door Game Object
            Instantiate(doorOpen, doorClosed.transform.position, Quaternion.identity);

            // Destroys the closed door Game Object
            Destroy(doorClosed);
        }
    }  
}
