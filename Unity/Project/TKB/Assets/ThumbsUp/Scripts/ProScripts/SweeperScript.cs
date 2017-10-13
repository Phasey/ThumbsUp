using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

// Creates a class for the Sweeper Script 
public class SweeperScript : MonoBehaviour
{
	// Initialises public floats so Designers can adjust
	public float movementSpeed = 10f;
	public float maxSpeed = 10f;

	// Used to get the Sweeper's Rigidbody
    public Rigidbody SweeperBody;

	// Allows access to xbox controller buttons
    public XboxController Controller;

	// Creates two private floats that record the previous x and z rotation
	private float prevRotateX;
	private float prevRotateZ;

	//------------------------------------------------------------
	// Function is called when script first runs
	//------------------------------------------------------------
    void Awake()
    {
		// Gets the Sweeper's Rigidbody and stores it in variable
        SweeperBody = GetComponent<Rigidbody>();

		// Sets previous rotation x and z to both be zero
		prevRotateX = 0f;
		prevRotateZ = 0f;
    }

	//------------------------------------------------------------
	// Function is called once every frame
	//------------------------------------------------------------
    void Update()
    {
		// Calls both MoveSweeper and RotateSweeper functions every frame
        MoveSweeper();
        RotateSweeper();
    }

	//------------------------------------------------------------
	// Function allows for the Sweeper to move
	//------------------------------------------------------------
    private void MoveSweeper()
    {
		// Both floats get direction of the Xbox controller's left stick
        float axisX = XCI.GetAxisRaw(XboxAxis.LeftStickX, Controller);
        float axisZ = XCI.GetAxisRaw(XboxAxis.LeftStickY, Controller);

		// Creates a "new" Vector3 to allow movement
        Vector3 movement = new Vector3(axisX, 0, axisZ);

		// Equation used to allow Sweeper to move
        SweeperBody.MovePosition(SweeperBody.position + movement * movementSpeed * Time.deltaTime);

		// Sets the Sweeper's velocity to be zero
        SweeperBody.velocity = Vector3.zero;
    }

	//------------------------------------------------------------
	// Function allows for the Sweeper to rotate
	//------------------------------------------------------------
    private void RotateSweeper()
    {
		// Both floats get direction of the Xbox controller's right stick
		float rotateAxisX = XCI.GetAxisRaw(XboxAxis.RightStickX, Controller);
		float rotateAxisZ = XCI.GetAxisRaw(XboxAxis.RightStickY, Controller);

		// Checks if the right stick is at default position on the x axis
		if (rotateAxisX == 0f)
			// If so, set the rotation x to be the previous frame's rotation x
			rotateAxisX = prevRotateX;

		else
			// Otherwise store current rotate x into the previous frame rotate x
			prevRotateX = rotateAxisX;

		// Checks if the right stick is at default position on the y axis
		if (rotateAxisZ == 0f)
			// If so, set the rotation z to be the previous frame's rotation z
			rotateAxisZ = prevRotateZ;

		else
			// Otherwise store current rotate z into the previous frame rotate z
			prevRotateZ = rotateAxisZ;

		// Checks if either rotate x isn't zero or rotate z isn't zero
		if (rotateAxisX != 0 || rotateAxisZ != 0)
		{
			// If one variable isn't zero, create a "new" Vector3 refering to looking direction
			Vector3 directionVector = new Vector3(rotateAxisX, 0, rotateAxisZ);

			// Allows for the sweeper to look in same direction as right stick's position
			transform.rotation = Quaternion.LookRotation(directionVector);
		}

		// Sets the angular velocity of the Striker to be zero
        SweeperBody.angularVelocity = Vector3.zero;
    }
}
