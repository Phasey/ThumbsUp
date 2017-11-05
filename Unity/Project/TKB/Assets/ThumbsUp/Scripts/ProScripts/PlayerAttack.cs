using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using XboxCtrlrInput;

// Creates a class for the Striker Attack Script 
public class PlayerAttack : MonoBehaviour
{
    public float hitForce = 50f;
    public float upForce = 20f;
    public float coolDownMaxTime = 0.5f;
    public float damage = 50;

    private float timer = 0f;
    public bool coolDown;

    // Allows access to xbox controller buttons
    private XboxController Controller;
    public GameObject HitBox;

    public Animator animator;

    //------------------------------------------------------------
    // Function is called when script first runs
    //------------------------------------------------------------
    void Awake()
    {
        PlayerMove move = GetComponent<PlayerMove>();
        Controller = move.Controller;

        coolDown = false;
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

        // Checks if trigger is down a little bit
        if (attackButton > 0.15f && !coolDown)
        {
            animator.SetBool("Attack", true);

            // Gets the layer mask of an enemy and stores it in local int
            int layerMask = 1 << LayerMask.NameToLayer("Enemy");

            // Gets the Box Collider Component of the player 
            BoxCollider box = HitBox.GetComponent<BoxCollider>();

            // Stores all enemies found in player's hitbox into local array
            Collider[] hitEnemies = Physics.OverlapBox(HitBox.transform.position, box.size * 0.5f, HitBox.transform.rotation, layerMask);

            // Runs for loop for all enemies in the local array
            for (int i = 0; i < hitEnemies.Length; ++i)
            {
                // Sets the current enemy as a GameObject
                GameObject enemy = hitEnemies[i].gameObject;

                // Gets the Rigidbody of the enemy
                Rigidbody rb = enemy.GetComponent<Rigidbody>();

                // Gets the NavMeshAgent of the enemy
                NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();

                // Gets the enemy's BasicAIScript
                BasicAIScript AI = enemy.GetComponent<BasicAIScript>();

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
                }

                else
                {
                    agent.enabled = true;
                    rb.isKinematic = true;
                }
            }

            coolDown = true;
        }
        else
            animator.SetBool("Attack", false);

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
}
