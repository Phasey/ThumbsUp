using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates a class for the Camera Script 
public class CameraScript : MonoBehaviour
{
	// Initialises public floats that Designers can adjust
	public float xMaxDistApart = 15f;
	public float zMaxDistApart = 15f;

	// Allows the transforms for both players to be accessed
    public Transform player1;
    public Transform player2;

	// Floats used to record the previous frame's x and z positions for both players
    private float prevPlayer1x;
    private float prevPlayer1z;
    private float prevPlayer2x;
    private float prevPlayer2z;

	// Sets Vector3s for the centre and offset
    private Vector3 centre;
    private Vector3 offset;

	//------------------------------------------------------------
	// Function is called when script first runs
	//------------------------------------------------------------
    void Awake()
    {
		// Sets default values
        prevPlayer1x = 0f;
        prevPlayer1z = 0f;
        prevPlayer2x = 0f;
        prevPlayer2z = 0f;

		// Records the centre of the screen upon starting
        centre = ((player2.position + player1.position) * 0.5f);

		// Records the offset upon starting
        offset = transform.position - centre;
    }

	//------------------------------------------------------------
	// Function is called once every frame
	//------------------------------------------------------------
    void Update()
    {
		// Local Vector3's store player 1 and 2's position for use in equations
        Vector3 player1Pos = player1.position;
        Vector3 player2Pos = player2.position;

		// Records the centre of the screen for every frame
        centre = ((player2.position + player1.position) * 0.5f);

		// Records where the camera should be positioned every frame
        transform.position = centre + offset;

		// Checks if both players are too far apart on the x
        if (player1.position.x - player2.position.x >= xMaxDistApart || 
            player1.position.x - player2.position.x <= -xMaxDistApart)
        {
			// If so, set both x positions back to the previous frames x
            player1Pos.x = prevPlayer1x;
            player2Pos.x = prevPlayer2x;
        }

        else
        {
			// If not, set the previous frames x to equal both player's current x position
            prevPlayer1x = player1Pos.x;
            prevPlayer2x = player2Pos.x;
        }

		// Checks if both players are too far apart on the z
        if (player1.position.z - player2.position.z >= zMaxDistApart ||
            player1.position.z - player2.position.z <= -zMaxDistApart)
        {
			// If so, set both z positions back to the previous frames z
            player1Pos.z = prevPlayer1z;
            player2Pos.z = prevPlayer2z;
        }


        else
        {
			// If not, set the previous frames z to equal both player's current z position
            prevPlayer1z = player1Pos.z;
            prevPlayer2z = player2Pos.z;
        }

		// Sets both players current position into local variable every frame
        player1.position = player1Pos;
        player2.position = player2Pos;
    }
}
