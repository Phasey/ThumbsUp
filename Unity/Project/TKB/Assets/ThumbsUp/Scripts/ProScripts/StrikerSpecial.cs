using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using XboxCtrlrInput;

// Creates a class for the Striker Special Script 
public class StrikerSpecial : MonoBehaviour
{
    // Sets and initiases public floats so the designers can adjust them
    public float hitForce = 10f;
    public float specialTimer = 0.5f;
    public float coolDownTimerMax = 5f;
    public float dist = 25;
    public float width = 5;
    public float upForce = 0.5f;
    public float speedIncrease = 30;

    // Creates a public power bar so it can be set in unity
    public Slider powerBar;

    // Creates a public power bar colour so it can be set in unity
    public Image powerBarColour;

    // Initialises the colour for when power bar is both full and not full
    public Color powerFullColour = Color.yellow;
    public Color powerNotFullColour = Color.blue;

    // Sets and initialises private bools
    private bool strikerSpecial = false;
    private bool coolDown = false;

    // Sets private floats
    private float resetTimer;
    private float coolDownTimer;

    // Sets two Vector3s to determine striker's position at start and end of special
    private Vector3 startPos;
    private Vector3 endPos;

    // Allows access to xbox controller buttons
    private XboxController Controller;

    private Animator animator;
	public AudioSource Dash;

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

        // Sets the reset timer to equal the special timer
        resetTimer = specialTimer;

        // Sets the cool down timer to equal the max cool down timer value
        coolDownTimer = coolDownTimerMax;

        // Sets the power bar value to equal the max cool down timer value
        powerBar.value = coolDownTimerMax;
    }

    //------------------------------------------------------------
    // Function is called once every frame
    //------------------------------------------------------------
    void Update()
    {
        // Calls Special function every frame
        Special();

        // Sets the power bar value to equal the cool down timer value
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
        float leftTrigger = XCI.GetAxis(XboxAxis.LeftTrigger, Controller);
        bool attackButton = XCI.GetButtonDown(XboxButton.RightBumper, Controller);
        bool yButton = XCI.GetButtonDown(XboxButton.Y, Controller);

        // Checks if right bumper has been pressed down and the striker is not in cool down mode
        if (leftTrigger > 0.15f || attackButton || yButton)
        {
            if (!coolDown)
            {
                animator.SetBool("Special", true);

                if (!Dash.isPlaying)
                {
                    Dash.Play();
                }

                // Sets the strikerSpecial bool to be true
                strikerSpecial = true;

                // Sets the start position as the striker's current location
                startPos = transform.position;

                // Predicts the end position where the striker will end their special
                endPos = startPos + transform.forward * dist;

                // Calls Knockback function
                Knockback();

                // Sets cool down bool to be true and cool down timer to equal zero
                coolDown = true;
                coolDownTimer = 0f;
            }
        }
        else
            animator.SetBool("Special", false);

        // Checks if Cool Down bool is true
        if (coolDown)
        {
            // Adds deltaTime to the cool down timer
            coolDownTimer += Time.deltaTime;

            // If cool down timer equals or exceeds the max value, then make cool down false
            if (coolDownTimer >= coolDownTimerMax)
            {
                coolDown = false;
            }
        }

        // Checks if strikerSpecial bool is true
        if (strikerSpecial)
        {
            // Gets the Player Move component
            PlayerMove pm = gameObject.GetComponent<PlayerMove>();

            // Increases the striker's movement temporarily
            pm.movementSpeed = speedIncrease;

            // Sets bool to be true to disable controller input
            pm.strikerDoingSpecial = true;

            // Decreases timer by deltaTime seconds
            specialTimer -= Time.deltaTime;

            // Sets velocity for the striker's Rigidbody
            GetComponent<Rigidbody>().velocity = transform.forward * pm.movementSpeed;

            // Checks if the special timer goes below or equals zero
            if (specialTimer <= 0)
            {
                // Sets bools back to false
                strikerSpecial = false;
                pm.strikerDoingSpecial = false;

                // Resets the movement speed
                pm.movementSpeed = pm.maxSpeed;

                // Calls ResetCoolDown function
                ResetCoolDown();
            }
        }
    }

    //------------------------------------------------------------
    // Function applies knockback physics to enemies
    //------------------------------------------------------------
    private void Knockback()
    {
        // Gets the layer mask of an enemy and stores it into local int
        int layerMask = 1 << LayerMask.NameToLayer("Enemy");

        // Vector3 used to determine the dash direction
        Vector3 dashDir = endPos - startPos;

        // Dash Direction Vector3 normalised
        dashDir.Normalize();

        // Defines the centre Vector3 as a point half the distance between start and end position
        Vector3 centre = (startPos + endPos) * 0.5f;

        // Creates a "new" Vector3 which is half the size of hitbox
        Vector3 halfSize = new Vector3(width * 0.5f, 2, dist * 0.5f);

        // Stores all enemies near striker during dash into a local array
        Collider[] hitEnemies = Physics.OverlapBox(centre, halfSize, transform.rotation, layerMask);

        // Runs a for loop for all enemies in local array
        for (int i = 0; i < hitEnemies.Length; ++i)
        {
            // Stores current enemy as a GameObject
            GameObject enemy = hitEnemies[i].gameObject;

            // Gets the Rigidbody component of the current enemy
            Rigidbody rb = enemy.GetComponent<Rigidbody>();

            // Gets the NavMeshAgent component of the current enemy
            NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();

            // Gets the AI script of the current enemy
            BasicAIScript AI = enemy.GetComponent<BasicAIScript>();

            // Sets the enemy to be dead in AI script
            AI.dead = true;

            // Disables the NavMeshAgent of the current enemy
            agent.enabled = false;

            // Disables kinematic of the enemy's Rigidbody
            rb.isKinematic = false;

            // Enables gravity of the enemy's Rigidbody
            rb.useGravity = true;

            // Determines how far the enemy will fly back and normalises the Vector3
            Vector3 enemyDir = enemy.transform.position - startPos;
            enemyDir.Normalize();

            // Creates a "new" Vector3 which will be used to add force
            Vector3 rightAngle = new Vector3(dashDir.z, 0, -dashDir.x);

            // Gets the dot product of rightAngle and enemyDir
            float dot = Vector3.Dot(rightAngle, enemyDir);

            // If enemy is to the right of player, add a force to make the enemy fly right
            if (dot > 0.0f)
                rb.AddForce(rightAngle * hitForce + Vector3.up * upForce, ForceMode.Impulse);

            // Otherwise, add a force that makes the enemy fly left
            else
                rb.AddForce(-rightAngle * hitForce + Vector3.up * upForce, ForceMode.Impulse);
        }
    }

    //------------------------------------------------------------
    // Function sets the Special Timer back to default when called
    //------------------------------------------------------------
    private void ResetCoolDown()
    {
        specialTimer = resetTimer;
    }
}
