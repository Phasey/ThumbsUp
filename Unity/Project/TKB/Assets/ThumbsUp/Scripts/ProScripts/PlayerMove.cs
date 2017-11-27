﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

//--------------------------------------------------------------------------------
// Author: Matthew Le Nepveu.
//--------------------------------------------------------------------------------

public class PlayerMove : MonoBehaviour 
{
	// Initialises public floats so Designers can adjust
	public float movementSpeed = 10f;
	public float maxSpeed = 10f;
    public float minStepsVolume = 0.9f;
    public float maxStepsVolume = 1f;

    // Used to get the Striker's Rigidbody
    private Rigidbody rigidBody;

	// Allows access to xbox controller buttons
    public XboxController Controller;

	// Creates two private floats that record the previous x and z rotation
	private float prevRotateX;
	private float prevRotateZ;

    private float volumeValue;

    public bool strikerDoingSpecial = false;
    private GameObject currentPickUp = null;

    public Animator animator;
	public bool running;
	public AudioSource footsteps;

    public GameObject pauseMenu;

    private PauseMenu pauseScript;

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
        if (!pauseMenu.GetComponent<PauseMenu>().paused)
        {
            Move();
            Rotate();
            PickUpBox();
            Footsteps();
        }

        bool start = XCI.GetButtonDown(XboxButton.Start, Controller);

        if (start)
            pauseMenu.GetComponent<PauseMenu>().StartBtn();

        volumeValue = Random.Range(minStepsVolume, maxStepsVolume);
    }

	private void Footsteps()
	{
		if (running && !footsteps.isPlaying)
        {
            footsteps.volume = volumeValue;
			footsteps.Play();
		}

		if (!running && footsteps.isPlaying)
        {
			footsteps.Stop();
		}
	}

	//------------------------------------------------------------
	// Function allows for the Striker to move
	//------------------------------------------------------------
    private void Move()
    {
        if (!strikerDoingSpecial)
        {
            // Both floats get direction of the Xbox controller's left stick
            float axisX = XCI.GetAxisRaw(XboxAxis.LeftStickX, Controller);
            float axisZ = XCI.GetAxisRaw(XboxAxis.LeftStickY, Controller);

			if (axisX != 0 || axisZ != 0)
            {
				animator.SetBool("Speed", true);
				running = true;
			}

			else
            {
                animator.SetBool("Speed", false);
				running=false;
			}

            // Creates a "new" Vector3 to allow movement
            Vector3 movement = new Vector3(axisX, 0, axisZ) * movementSpeed;

            rigidBody.MovePosition(rigidBody.position + movement * Time.deltaTime);

            // Sets the Striker's velocity to be zero
            rigidBody.velocity = Vector3.zero;
            
            if (axisX == 0f)
                axisX = prevRotateX;
            else
                prevRotateX = axisX;

            if (axisZ == 0f)
                axisZ = prevRotateZ;
            else
                prevRotateZ = axisZ;

            if (axisX != 0 || axisZ != 0)
            {
                Vector3 directionVector = new Vector3(axisX, 0, axisZ);

                transform.rotation = Quaternion.LookRotation(directionVector);
            }

            rigidBody.angularVelocity = Vector3.zero;
        }
    }

	//------------------------------------------------------------
	// Function allows for the Striker to rotate
	//------------------------------------------------------------
    private void Rotate()
    {
        // Only runs if the striker is not performing their special
        if (!strikerDoingSpecial)
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

    //------------------------------------------------------------
    // Function allows players to pick up boxes
    //------------------------------------------------------------
    private void PickUpBox()
    {
        // Bool gets if the A button on an Xbox controller is pushed down
        bool aButton = XCI.GetButtonDown(XboxButton.A, Controller);


        // Checks if the A button has been pressed down
        if (aButton)
        {
            // Checks if the player does not have anything picked up
            if (currentPickUp == null)
            {
                // Gets the layer mask of the box
                int layerMask = 1 << LayerMask.NameToLayer("PickUp");

                // BoxSize Vector3 used to determine the hit box of the player
                Vector3 boxSize = new Vector3(3, 3, 3);

                // Stores all boxes that are found in the players hit box into a local array
                Collider[] boxPickUp = Physics.OverlapBox(transform.position + transform.forward, boxSize * 0.5f, transform.rotation, layerMask);

                // Checks if there are any boxes in local array
                if (boxPickUp.Length > 0)
                {
                    // Picks up the first box in the array
                    currentPickUp = boxPickUp[0].gameObject;

                    // Makes the box a child of the parent
                    currentPickUp.transform.parent = transform;

                    // Gets the Rigidbody of the box
                    Rigidbody rb = currentPickUp.GetComponent<Rigidbody>();

                    // Sets Kinematic to be true on Boxes' Rigidbody
                    rb.isKinematic = true;

                    // Positions the box to be in front of the player's chest
                    currentPickUp.transform.localPosition = Vector3.forward + Vector3.forward + Vector3.up;

                    // Sets the boxes' rotation back to default
                    currentPickUp.transform.localRotation = Quaternion.identity;
                    
                    // Gets the box collider of the box and disables it
                    BoxCollider bc = currentPickUp.GetComponent<BoxCollider>();
                    bc.enabled = false;
                }
            }

            // Else if the player has something picked up
            else
            {
                // Sets the pick up to have no parent again
                currentPickUp.transform.parent = null;

                // Gets the Rigidbody of the box
                Rigidbody rb = currentPickUp.GetComponent<Rigidbody>();

                // Sets kinematic to be false on the Rigidbody
                rb.isKinematic = false;

                // Gets the box collider of the box and enables it back
                BoxCollider bc = currentPickUp.GetComponent<BoxCollider>();
                bc.enabled = true;

                // Sets currentPickUp to be null
                currentPickUp = null;
            }
        }
    }
}
