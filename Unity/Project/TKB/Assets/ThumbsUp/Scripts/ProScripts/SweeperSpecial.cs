using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using XboxCtrlrInput;

public class SweeperSpecial : MonoBehaviour
{
    // Sets and initiases public floats so the designers can adjust them
    public float hitForce = 10;
    public float upForce = 0.5f;
    public float radius = 5f;
    public float damage = 5f;
    public float coolDownTimerMax = 5f;

    // Creates a public power bar so it can be set in unity
    public Slider powerBar;

    // Creates a public power bar colour so it can be set in unity
    public Image powerBarColour;

    // Initialises the colour for when power bar is both full and not full
    public Color powerFullColour = Color.yellow;
    public Color powerNotFullColour = Color.blue;

    // Sets both a private bool to be false and a private float
    private bool coolDown = false;
    private float coolDownTimer;

    // Allows access to xbox controller buttons
    private XboxController Controller;

    private Animator animator;

    //------------------------------------------------------------
    // Function is called when script first runs
    //------------------------------------------------------------
    void Awake()
    {
        // Gets the Player Move component
        PlayerMove move = GetComponent<PlayerMove>();
        animator = GetComponent<Animator>();

        // Sets the controller to be the same as controller used in PlayerMove script
        Controller = move.Controller;

        // Sets the cool down timer to equal the max cool down timer value
        coolDownTimer = coolDownTimerMax;

        // Sets the power bar value to equal the max cool down timer value
        powerBar.maxValue = coolDownTimerMax;
    }

    //------------------------------------------------------------
    // Function is called once every frame
    //------------------------------------------------------------
    void Update()
    {
        // Calls Special function every frame
        Special();

        // Sets the power bar value to equal the value of the cool down timer
        powerBar.value = coolDownTimer;

        // If power bar value equals max, then make its colour equal the full colour
        if (powerBar.value == coolDownTimerMax)
            powerBarColour.color = powerFullColour;

        // Else, make the power bar's colour equal the not full colour
        else
            powerBarColour.color = powerNotFullColour;
    }

    //------------------------------------------------------------
    // Function allows Striker to use his regular attack
    //------------------------------------------------------------
    private void Special()
    {
        // Bool checks if the right bumper has been pressed
        bool attackButton = XCI.GetButtonDown(XboxButton.RightBumper, Controller);

        // Checks if right bumper has been pressed down and sweeper is not in cool down
        if (attackButton && !coolDown)
        {
            animator.SetBool("Special", true);

            // Gets the layer mask of enemy and stores it in local variable
            //int layerMask = 1 << LayerMask.NameToLayer("Enemy");

            // Stores all enemies near sweeper into a local array
            //Collider[] hitEnemies = Physics.OverlapSphere(transform.position, radius,  layerMask);

            //// Runs for loop for every enemy in local array
            //for (int i = 0; i < hitEnemies.Length; ++i)
            //{
            //    // Stores current enemy as a GameObject
            //    GameObject enemy = hitEnemies[i].gameObject;

            //    // Gets the Rigidbody component of the current enemy
            //    Rigidbody rb = enemy.GetComponent<Rigidbody>();

            //    // Gets the NavMeshAgent component of the current enemy
            //    NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();

            //    // Gets the AI script of the current enemy
            //    BasicAIScript AI = enemy.GetComponent<BasicAIScript>();

            //    // Disables the NavMeshAgent of the current enemy
            //    agent.enabled = false;

            //    // Disables kinematic of the enemy's Rigidbody
            //    rb.isKinematic = false;

            //    // Vector3 determines the direction the enemy will fly and normalises the variable
            //    Vector3 direction = enemy.transform.position - transform.position;
            //    direction.Normalize();

            //    // Adds force to make the enemy fly back
            //    rb.AddForce(direction * hitForce + Vector3.up * upForce, ForceMode.Impulse);

            //    // Decreases the enemies health by how much damage was dealt
            //    AI.enemyHealth -= damage;

            //    // Checks if enemies health is equal to or goes below zero
            //    if (AI.enemyHealth <= 0)
            //    {
            //        // If so, set the dead bool in AI script to be true for enemy 
            //        AI.dead = true;
            //    }

            //    else
            //    {
            //        agent.enabled = true;
            //        rb.isKinematic = true;
            //    }
            //}

            // Sets cool down bool to be true and sets timer to equal zero
            coolDown = true;
            coolDownTimer = 0f;
        }
        else
            animator.SetBool("Special", false);

        // Checks if cool down bool equals true
        if (coolDown)
        {
            // Cool Down Timer is increased by deltaTime
            coolDownTimer += Time.deltaTime;

            // If the timer exceeds or equals the max timer value, then set cool down back to false
            if (coolDownTimer >= coolDownTimerMax)
            {
                coolDown = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        bool attacking = animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !animator.IsInTransition(0);

        if (other.tag != "Enemy")
            return;

        if (!attacking)
            return;

        // Stores current enemy as a GameObject
        GameObject enemy = other.gameObject;

        // Gets the Rigidbody component of the current enemy
        Rigidbody rb = enemy.GetComponent<Rigidbody>();

        // Gets the NavMeshAgent component of the current enemy
        NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();

        // Gets the AI script of the current enemy
        BasicAIScript AI = enemy.GetComponent<BasicAIScript>();

        // Disables the NavMeshAgent of the current enemy
        agent.enabled = false;

        // Disables kinematic of the enemy's Rigidbody
        rb.isKinematic = false;

        // Vector3 determines the direction the enemy will fly and normalises the variable
        Vector3 direction = enemy.transform.position - transform.position;
        direction.Normalize();

        // Adds force to make the enemy fly back
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
}
