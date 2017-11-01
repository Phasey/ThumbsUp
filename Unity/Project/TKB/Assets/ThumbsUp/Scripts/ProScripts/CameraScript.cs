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

	// Sets Vector3s for the centre and offset
    private Vector3 centre;
    private Vector3 offset;

    public float minZoom = 0.0f;
    public float zoomScale = 0.2f;

	//------------------------------------------------------------
	// Function is called when script first runs
	//------------------------------------------------------------
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

	//------------------------------------------------------------
	// Function is called once every frame
	//------------------------------------------------------------
    void Update()
    {
        HealthScript p1 = player1.GetComponent<HealthScript>();
        HealthScript p2 = player2.GetComponent<HealthScript>();

        // Local Vector3's store player 1 and 2's position for use in equations
        Vector3 player1Pos = player1.position;
        Vector3 player2Pos = player2.position;

        // Records the centre of the screen for every frame
        centre = ((player2.position + player1.position) * 0.5f);

        float dist = Vector3.Distance(player1Pos, player2Pos);
        float halfFOV = Camera.main.fieldOfView * 0.5f;
        float camDist = Mathf.Abs(Mathf.Tan(halfFOV)) * dist * zoomScale;

        if (camDist < minZoom)
            camDist = minZoom;

        // Records where the camera should be positioned every frame
        transform.position = centre + offset * camDist;
    }
}
