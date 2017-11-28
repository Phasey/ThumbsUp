//--------------------------------------------------------------------------------
// Author: Matthew Le Nepveu.
//--------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerMove : MonoBehaviour 
{
	// Initialises public floats so Designers can adjust
	public float movementSpeed = 10f;
	public float maxSpeed = 10f;
    public float minStepsVolume = 0.9f;
    public float maxStepsVolume = 1f;
    public float minStepsPitch = 1f;
    public float maxStepsPitch = 1.5f;

    // Indicates if striker is performing special and sets to false at awake
    public bool strikerDoingSpecial = false;

    // Bool determines if players are running or not
    public bool running;

    // Used to access the animator and its variables
    public Animator animator;

    // AudioSource used for the players footsteps when they move
    public AudioSource footsteps;

    // Represents the pause menu canvas that will be edited
    public GameObject pauseMenu;

    // Used to get the Striker's Rigidbody
    private Rigidbody rigidBody;

	// Allows access to xbox controller buttons
    public XboxController Controller;

	// Creates two private floats that record the previous x and z rotation
	private float prevRotateX;
	private float prevRotateZ;

    // Floats used to adjust pitch and volume by a random number every frame
    private float volumeValue;
    private float pitchValue;

    // Used to determine the player's picked up object and sets to null on awake
    private GameObject currentPickUp = null;

    // Variables used to access componments of the Pause Menu script
    private PauseMenu pauseScript;

    // Used to access components from the Sweeper Special script
    private SweeperSpecial sweeperSpecial;

    // Used to access components from the Striker Special script
    private StrikerSpecial strikerSpecial;

    //--------------------------------------------------------------------------------
    // Function is called when script first runs.
    //--------------------------------------------------------------------------------
    void Awake()
    {
		// Gets the Striker's Rigidbody and stores it in variable
        rigidBody = GetComponent<Rigidbody>();

        // Gets components from the Sweeper Special script
        sweeperSpecial = GetComponent<SweeperSpecial>();

        // Gets component from the Striker Special script
        strikerSpecial = GetComponent<StrikerSpecial>();

        // Sets previous rotation x and z to both be zero
        prevRotateX = 0f;
		prevRotateZ = 0f;
    }

    //--------------------------------------------------------------------------------
    // Function is called once every frame.
    //--------------------------------------------------------------------------------
    void Update()
    {
        // If statement runs if game is not paused
        if (!pauseMenu.GetComponent<PauseMenu>().paused)
        {
            // Calls all functions from the script but Awake
            Move();
            Rotate();
            PickUpBox();
            Footsteps();
        }

        // Bool checks if start button has been pressed
        bool start = XCI.GetButtonDown(XboxButton.Start, Controller);

        // Runs code in StartBtn function from Pause Script if start is pressed
        if (start)
            pauseMenu.GetComponent<PauseMenu>().StartBtn();

        // Determines a random value for the volume of footsteps
        volumeValue = Random.Range(minStepsVolume, maxStepsVolume);

        // Pitch of footsteps randomised and stored in pitch float
        pitchValue = Random.Range(minStepsPitch, maxStepsPitch);
    }

    //--------------------------------------------------------------------------------
    // Function checks when to play audio footsteps and when to stop it.
    //--------------------------------------------------------------------------------
    private void Footsteps()
	{
        // Code in braces runs if players are running and audio isn't playing
		if (running && !footsteps.isPlaying)
        {
            // Sets the pitch of footsteps to equal the random pitch value
            footsteps.pitch = pitchValue;

            // Random volume value is applied to the footsteps audio
            footsteps.volume = volumeValue;

            // Plays audio for the footsteps
			footsteps.Play();
		}

        // Stops playing audio for footsteps if player isn't running and audio is playing
		if (!running && footsteps.isPlaying)
			footsteps.Stop();
	}

    //--------------------------------------------------------------------------------
    // Function allows for the Striker to move
    //--------------------------------------------------------------------------------
    private void Move()
    {
        // If statement runs if striker isn't doing special
        if (!strikerDoingSpecial)
        {
            // Both floats get direction of the Xbox controller's left stick
            float axisX = XCI.GetAxisRaw(XboxAxis.LeftStickX, Controller);
            float axisZ = XCI.GetAxisRaw(XboxAxis.LeftStickY, Controller);

            // Checks if left control stick is not in the centre
			if (axisX != 0 || axisZ != 0)
            {
                // Sets speed bool in animator to be true
				animator.SetBool("Speed", true);

                // Running bool is set to true
				running = true;
			}

            // Otherwise runs code below if the left control stick is in the centre
			else
            {
                // Speed bool from animator is reset to false
                animator.SetBool("Speed", false);

                // Running bool is set back to false
				running = false;
			}

            // Creates a "new" Vector3 to allow movement
            Vector3 movement = new Vector3(axisX, 0, axisZ) * movementSpeed;

            // Applies movement physics to the player's Rigidbody
            rigidBody.MovePosition(rigidBody.position + movement * Time.deltaTime);

            // Sets the Striker's velocity to be zero
            rigidBody.velocity = Vector3.zero;
            
            // Sets axisX to equal previous Rotation's X value if axisX is zero
            if (axisX == 0f)
                axisX = prevRotateX;

            // Otherwise the previous Rotation X records the axisX
            else
                prevRotateX = axisX;

            // Sets axisZ to equal previous Rotation's Z value if axisZ is zero
            if (axisZ == 0f)
                axisZ = prevRotateZ;

            // Otherwise the previous Rotation Z records the axisZ
            else
                prevRotateZ = axisZ;

            // Runs code in braces if the left control stick is not in the centre
            if (axisX != 0 || axisZ != 0)
            {
                // Creates a "new" direction Vector3 of the left control sticks direction
                Vector3 directionVector = new Vector3(axisX, 0, axisZ);

                // Makes the player look in direction of the directionVector
                transform.rotation = Quaternion.LookRotation(directionVector);
            }

            // Angular velocity of player is set to be a zero Vector
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

            // Otherwise store current rotate x into the previous frame rotate x
            else
                prevRotateX = rotateAxisX;

            // Checks if the right stick is at default position on the y axis
            if (rotateAxisZ == 0f)
                // If so, set the rotation z to be the previous frame's rotation z
                rotateAxisZ = prevRotateZ;

            // Otherwise store current rotate z into the previous frame rotate z
            else
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

                    // Disables any use of attack whilst a box is picked up
                    GetComponent<PlayerAttack>().enabled = false;

                    // Disables functions from Sweeper Special if it exists
                    if (sweeperSpecial)
                        sweeperSpecial.enabled = false;

                    // Disables functions from Striker Special if it exists
                    if (strikerSpecial)
                        strikerSpecial.enabled = false;
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

                // Reenables attack if no box is picked up
                GetComponent<PlayerAttack>().enabled = true;

                // Reenables functions from Sweeper Special if it exists
                if (sweeperSpecial)
                    sweeperSpecial.enabled = true;

                // Reenables functions from Striker Special if it exists
                if (strikerSpecial)
                    strikerSpecial.enabled = true;
            }
        }
    }
}
