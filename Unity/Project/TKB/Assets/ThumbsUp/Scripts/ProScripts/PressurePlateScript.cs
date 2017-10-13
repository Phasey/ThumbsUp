using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates a class for the Pressure Plate Script 
public class PressurePlateScript : MonoBehaviour
{
	// Allows access to a Rigidbody
    public Rigidbody rigidBodyCube;

	// Allows access to an Animation
    public Animation anim;

	// Allows access to a GameObject
    public GameObject door;

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
		// If a GameObject with tag player is triggering function, then destroy door object
        if (other.gameObject.tag == "Player")
            Destroy(door);
    }
}
