using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates a class for the Camera Script 
public class CameraScript : MonoBehaviour
{
    // Vector3 allows are offset to be set when program starts
    public Vector3 startOffset;

	// Allows the transforms for both players to be accessed
    public Transform player1;
    public Transform player2;

    // Indicates how closes the camera can be to centre on the z
    public float minZoom = 15f;

    // Makes sure the camera does not zoom too heavily
    public float zoomScale = 0.2f;

    // Indicates the centre position the camera points at
    private Vector3 centre;

    // Refers to the camera's offset from the centre position 
    private Vector3 offset;

    //--------------------------------------------------------------------------------
    // Function is called when script first runs.
    //
    // Author: Matthew Le Nepveu.
    //--------------------------------------------------------------------------------
    void Awake()
    {
		// Records the centre of the screen upon starting
        centre = ((player2.position + player1.position) * 0.5f);

        // Camera sets up behind players, depending on their location
        transform.position = centre + startOffset;

		// Records the offset upon starting
        offset = transform.position - centre;
        offset.Normalize();
    }

    //--------------------------------------------------------------------------------
    // Function is called once every frame.
    //
    // Author: Matthew Le Nepveu.
    //--------------------------------------------------------------------------------
    void Update()
    {
        // Local Vector3's store player 1 and 2's position for use in equations
        Vector3 player1Pos = player1.position;
        Vector3 player2Pos = player2.position;

        // Records the centre of the screen for every frame
        centre = ((player2.position + player1.position) * 0.5f);

        // Determines thye distance between player 1 and 2 and stores it in local float
        float dist = Vector3.Distance(player1Pos, player2Pos);

        // Gets half the field of view used to determine the camera's angle
        float halfFOV = Camera.main.fieldOfView * 0.5f;

        // Identifies the camera distance then stores in a local float
        float camDist = Mathf.Abs(Mathf.Tan(halfFOV)) * dist * zoomScale;

        // Ensures the camera distance never goes below the minimum zoom value
        if (camDist < minZoom)
            camDist = minZoom;

        // Records where the camera should be positioned every frame
        transform.position = centre + offset * camDist;
    }
}
