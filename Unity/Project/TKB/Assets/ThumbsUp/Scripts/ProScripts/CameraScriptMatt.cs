using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates a class for the Camera Script 
public class CameraScriptMatt : MonoBehaviour
{
    // Vector3 allows are offset to be set when program starts
    public Vector3 offset;

	// Allows the transforms for both players to be accessed
    public Transform player1;
    public Transform player2;

	// Sets Vector3s for the centre and offset
    private Vector3 centre;

	//------------------------------------------------------------
	// Function is called when script first runs
	//------------------------------------------------------------
    void Awake()
    {
		
    }

	//------------------------------------------------------------
	// Function is called once every frame
	//------------------------------------------------------------
    void Update()
    {
        centre = ((player2.position + player1.position) * 0.5f);
        transform.position = centre + offset;
    }
}
