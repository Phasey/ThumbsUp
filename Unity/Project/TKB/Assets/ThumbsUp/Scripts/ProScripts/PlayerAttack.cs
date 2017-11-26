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
    public float hitForce = 50f;
    public float upForce = 20f;
    public float coolDownMaxTime = 0.5f;
    public float damage = 50;
    public float attackTime = 0f;
    public float particleTimer = 5f;
	public AudioSource Swing;
    //public float animationSpeed = 1.5f;

    private float timer = 0f;
    public bool coolDown;
	public bool soundPlayed;
    // Allows access to xbox controller buttons
    private XboxController Controller;
    public GameObject HitBox;
    public GameObject BoneParticle;
    public GameObject AxeParticle;

    public Animator animator;



    //------------------------------------------------------------
    // Function is called when script first runs
    //------------------------------------------------------------
    void Awake()
    {
        PlayerMove move = GetComponent<PlayerMove>();
        Controller = move.Controller;

        coolDown = false;
        //animator.speed = animationSpeed;
    }

	//------------------------------------------------------------
	// Function is called once every frame
	//------------------------------------------------------------
    void Update()
    {
		// Calls StrikerAttack function every frame
        Attack();
    }

	//------------------------------------------------------------
	// Function allows Striker to use his regular attack
	//------------------------------------------------------------
    private void Attack()
    {
		// Gets the Right Trigger button from Xbox controller
        float attackButton = XCI.GetAxis(XboxAxis.RightTrigger, Controller);
        bool xButton = XCI.GetButtonDown(XboxButton.X, Controller);
        bool attacking = animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !animator.IsInTransition(0);

        // Checks if trigger is down a little bit
        if (attackButton > 0.15f || xButton)
        {
            if (!attacking)
            {
                animator.SetBool("Attack", true);
                AxeParticle.SetActive(true);

                if (!Swing.isPlaying && !coolDown)
                {
                    Swing.Play();
                }

                coolDown = true;
            }
        }
        else
        {
            animator.SetBool("Attack", false);
            AxeParticle.SetActive(false);
        }

        if (coolDown)
        {
            timer += Time.deltaTime;

            if (timer >= coolDownMaxTime)
            {
                coolDown = false;

                timer = 0f;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        bool attacking = animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !animator.IsInTransition(0);

        if (other.tag != "Enemy")
            return;

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

        //if(AI.IsBoss)
        //{
        //    AI.ResetFlashCoolDown();
        //    AI.Flash();
        //}

        // Checks if enemies health is equal to or goes below zero
        if (AI.enemyHealth <= 0)
        {
            // If so, set the dead bool in AI script to be true for enemy 
            AI.dead = true;
        }

        else
        {
            agent.enabled = true;
            rb.isKinematic = true;

            AI.ResetFlashCoolDown();
            AI.Flash();
        }
    }
}
