using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

// Creates a class for the Striker Script 
public class PlayerMove : MonoBehaviour 
{
	// Initialises public floats so Designers can adjust
	public float movementSpeed = 10f;
	public float maxSpeed = 10f;

	// Used to get the Striker's Rigidbody
    private Rigidbody rigidBody;

	// Allows access to xbox controller buttons
    public XboxController Controller;

	// Creates two private floats that record the previous x and z rotation
	private float prevRotateX;
	private float prevRotateZ;

    public bool doingSpecial = false;

	//------------------------------------------------------------
	// Function is called when script first runs
	//------------------------------------------------------------
    void Awake()
    {
		// Gets the Striker's Rigidbody and stores it in variable
        rigidBody = GetComponent<Rigidbody>();

		// Sets previous rotation x and z to both be zero
		prevRotateX = 0f;
		prevRotateZ = 0f;
    }

	//------------------------------------------------------------
	// Function is called once every frame
	//------------------------------------------------------------
    void Update()
    {
		// Calls both MoveStriker and RotateStriker functions every frame
        MoveStriker();
        RotateStriker();
    }

	//------------------------------------------------------------
	// Function allows for the Striker to move
	//------------------------------------------------------------
    private void MoveStriker()
    {
        if (!doingSpecial)
        {

            // Both floats get direction of the Xbox controller's left stick
            float axisX = XCI.GetAxisRaw(XboxAxis.LeftStickX, Controller);
            float axisZ = XCI.GetAxisRaw(XboxAxis.LeftStickY, Controller);

            // Creates a "new" Vector3 to allow movement
            Vector3 movement = new Vector3(axisX, 0, axisZ);

            // Equation used to allow Striker to move
            rigidBody.MovePosition(rigidBody.position + movement * movementSpeed * Time.deltaTime);

            // Sets the Striker's velocity to be zero
            rigidBody.velocity = Vector3.zero;
        }
    }

	//------------------------------------------------------------
	// Function allows for the Striker to rotate
	//------------------------------------------------------------
    private void RotateStriker()
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

			// Allows for the striker to look in same direction as right stick's position
            transform.rotation = Quaternion.LookRotation(directionVector);
        }

		// Sets the angular velocity of the Striker to be zero
        rigidBody.angularVelocity = Vector3.zero;
    }
}
