using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using XboxCtrlrInput;

//--------------------------------------------------------------------------------
// Author: Matthew Le Nepveu.
//--------------------------------------------------------------------------------

// Creates a class for the Player Attack Script 
public class PlayerAttack : MonoBehaviour
{
    // Initialises public floats for designers to adjust
    public float hitForce = 50f;
    public float upForce = 20f;
    public float coolDownMaxTime = 0.5f;
    public float damage = 50;
    public float attackTime = 0f;
    public float particleTimer = 5f;
    public float minSwingVolume = 0.9f;
    public float maxSwingVolume = 1f;
    public float minSwingPitch = 1f;
    public float maxSwingPitch = 1.5f;

    // Added audio for when the player attacks
	public AudioSource Swing;

    // GameObject used to set the hitbox for both players
    public GameObject HitBox;

    // Adds Bone Particles for when the players hit enemies
    public GameObject BoneParticle;

    // Adds particles to the weapons when attacking
    public GameObject AxeParticle;

    // Accesses the animator to adjust its variables
    public Animator animator;

    // Checks if right trigger is down and initialises to false on awake
    private bool wasTriggerDown = false;

    // Floats used to randomise pitch and volume of attack
    private float volumeValue;
    private float pitchValue;

    // Bool used for cool down in this and other scripts
    public bool coolDown;

    // Bool used to check if a sound has been player or not
	public bool soundPlayed;

    // Allows access to xbox controller buttons
    private XboxController Controller;

    //--------------------------------------------------------------------------------
    // Function is called when script first runs.
    //--------------------------------------------------------------------------------
    void Awake()
    {
        // Accesses Player Move script and gets its component
        PlayerMove move = GetComponent<PlayerMove>();

        // Allows for movement in the game
        Controller = move.Controller;

        // Initialises cool down bool to false on awake
        coolDown = false;
        //sets the particle effect to false so it doesnt play
        AxeParticle.SetActive(false);
    }

    //--------------------------------------------------------------------------------
    // Function is called once every frame.
    //--------------------------------------------------------------------------------
    void Update()
    {
		// Calls StrikerAttack function every frame
        Attack();
        

        // Makes the volume a random number between min and max volume floats
        volumeValue = Random.Range(minSwingVolume, maxSwingVolume);

        // Determines a randomised float every frame to be used for pitch
        pitchValue = Random.Range(minSwingPitch, maxSwingPitch);
    }

    //--------------------------------------------------------------------------------
    // Function allows Striker to use his regular attack.
    //--------------------------------------------------------------------------------
    private void Attack()
    {
		// Gets the Right Trigger button from Xbox controller and returns how much it has been pressed
        float attackButton = XCI.GetAxis(XboxAxis.RightTrigger, Controller);

        // Checks if the X Button has been pressed
        bool xButton = XCI.GetButtonDown(XboxButton.X, Controller);

        // Bool determines if an animation is playing
        bool attacking = animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");

        // Checks if trigger is down a little and trigger wasn't down or if X button has been pressed
        if ((attackButton > 0.15f && !wasTriggerDown) || xButton)
        {
            // Sets the trigger down bool to be true
            wasTriggerDown = true;

            // Runs code in braces if player isn't attacking
            if (!attacking)
            {
                // Sets Attack bool in animator to be true
                animator.SetBool("Attack", true);

                // Axe Particles are set to active
                AxeParticle.SetActive(true);
                
                // Code runs if swing audio is not playing
                if (!Swing.isPlaying)
                {
                    // Sets the random pitch and volume values
                    Swing.pitch = pitchValue;
                    Swing.volume = volumeValue;

                    // Plays the swing audio
                    Swing.Play();
                }
            }
        }

        // Otherwise if no X button or trigger is pressed
        else
        {
            // Sets trigger down bool to false if trigger is not pressed enough
            if (attackButton < 0.15f)
                wasTriggerDown = false;

            // Checks if the attacking animation is in transition
            if (attacking)
            {
                // Initialises Attack bool to false in animator
                animator.SetBool("Attack", false);

                
               
            }
        }
        //turns off the particle effect
        ResetAxe();
    }

    //--------------------------------------------------------------------------------
    // Function determines events when an object stays in a trigger.
    //
    // Param:
    //      other: Is the collider of the object triggering the function.
    //--------------------------------------------------------------------------------
    void OnTriggerStay(Collider other)
    {
        // Bool determines if an animation is playing
        bool attacking = animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !animator.IsInTransition(0);

        // Ignores function if the collider of object doesn't have a tag of "Enemy"
        if (other.tag != "Enemy")
            return;

        // Ignores function if player is not attacking
        if (!attacking)
            return;

        // Sets the current enemy as a GameObject
        GameObject enemy = other.gameObject;

        // Gets the Rigidbody of the enemy
        Rigidbody rb = enemy.GetComponent<Rigidbody>();

        // Gets the NavMeshAgent of the enemy
        NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();

        // Gets the enemy's BasicAIScript
        BasicAIScript AI = enemy.GetComponent<BasicAIScript>();

        BoneParticle.SetActive(true);

        // Disables NavMeshAgent for enemy
        agent.enabled = false;

        // Disables Kinematic for the enemies Rigidbody
        rb.isKinematic = false;

        // Direction Vector3 used for direction enemy will be knocked back
        Vector3 direction = enemy.transform.position - transform.position;

        // Direction Vector3 normalised
        direction.Normalize();

        // Adds a knockback force that gets applied to the enemy's Rigidbody
        rb.AddForce(direction * hitForce + Vector3.up * upForce, ForceMode.Impulse);

        // Decreases the enemies health by how much damage was dealt
        AI.enemyHealth -= damage;

        // Checks if enemies health is equal to or goes below zero
        if (AI.enemyHealth <= 0)
        {
            // If so, set the dead bool in AI script to be true for enemy 
            AI.dead = true;
            rb.detectCollisions = false;
        }

        // Else if the enemies health is above zero
        else
        {
            // Re-enables the NavMeshAgent and its Rigidbody
            agent.enabled = true;
            rb.isKinematic = true;
            
            // Resets enemy's cool down and allows it to flash
            AI.ResetFlashCoolDown();
            AI.Flash();
        }
    }

    //--------------------------------------------------------------------------------
    // Function created to turn off the particle effect 
    //--------------------------------------------------------------------------------
    private void ResetAxe()
    {
        // Disallows any weapon particles
        AxeParticle.SetActive(false);

    }
}
