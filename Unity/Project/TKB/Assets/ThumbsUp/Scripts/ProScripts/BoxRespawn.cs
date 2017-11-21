using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates a class for the Box Respawn
public class BoxRespawn : MonoBehaviour
{
    // Public Vector3 that indicates the start position of a box
    public Vector3 startPos;

    // Refers to the lowest Y value a box can be positioned
    public float lowestYPos = -10;

    //--------------------------------------------------------------------------------
    // Function is called when script first runs.
    //
    // Author: Matthew Le Nepveu.
    //--------------------------------------------------------------------------------
    void Awake()
    {
        // Sets a boxes' position to be located at the start position
        transform.position = startPos;
	}

    //--------------------------------------------------------------------------------
    // Function is called once every frame.
    //
    // Author: Matthew Le Nepveu.
    //--------------------------------------------------------------------------------
    void Update()
    {
        // Runs if the boxes' Y position equals or goes below the lowest Y pos float
        if (transform.position.y <= lowestYPos)
        {
            // Resets its position back to the start position
            transform.position = startPos;

            // Resets its rotation to equal the default rotation (identity)
            transform.rotation = Quaternion.identity;

            // Resets its velocity back to zero
            GetComponent<Rigidbody>().velocity = Vector3.zero;

            // Resets its angular velocity back to zero
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}
