using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Creates a class for the Basic AI Script 
public class BasicAIScript : MonoBehaviour
{
	// Initialises public floats for Designers to adjust
    public float enemyMovementSpeed = 10f;
	public float vision = 10f;
	public float AttackVision = 1f;
    public int enemyDamage = 5;

	// Gets access to a RigidBody
    Rigidbody rigidBody;

	// Allows the transform for both players to be accessed
    public Transform Player1;
    public Transform Player2;

	// Accesses both the Sweeper and Striker as game objects
    public GameObject enemy;


	// Gets the transform of points on the navmesh
    public Transform[] Points;

	// Initialises the destination private int to 0
    private int Dest = 0;

	// Sets a private NavMeshAgent for use in script 
    private NavMeshAgent Agent;
    
    //sets the cooldown timer for use in attacking
    public float CoolDownTimer = 2f;

    // Initialises CoolDown boolean to be false
    private bool CoolDown = false;

    // Sets AttackTime variable to be private
    private float AttackTime;

    //Add dead bool
   public bool dead = false;
    //put If(!dead) around all AI code
    public float deadTime = 2f;

    //------------------------------------------------------------
    // Function is called when script first runs
    //------------------------------------------------------------
    void Awake()
    {
        //checks for dead
        if(!dead)
            {
            // Gets a RigidBody component and stores it into rigidBody
            rigidBody = GetComponent<Rigidbody>();

            // Gets a NavMeshAgent component and stores it into Agent
            Agent = GetComponent<NavMeshAgent>();

            // Does not allow Agent to automatically brake
            Agent.autoBraking = false;
        }
    }
	
	//------------------------------------------------------------
	// Function is called once every frame
	//------------------------------------------------------------
	void Update()
    {
        //checks for dead
        if (!dead)
        {
            // Adds a forward force and speed to the rigidBody 
            rigidBody.AddForce(transform.forward * enemyMovementSpeed);

            // Calls Seek function every frame
            Seek();
        }
        if (dead)
        {
            deadTime -= Time.deltaTime;
            if (deadTime <= 0)
            {
                Destroy(enemy);
            }
        }

    }

	//------------------------------------------------------------
	// Function calculates the next point for NavMeshAgent to go
	//------------------------------------------------------------
	void NextPoint()
	{
        //checks for dead
        if (!dead)
        {
            if (Points.Length != 0)
            {
                // Agents destination refers to indexed point in points array
                Agent.destination = Points[Dest].position;

                // Ignores function if the length to the points equals zero
                if (Vector3.Distance(Points[Dest].position, transform.position) < 1)
                {
                    // Dest equals the Dest + 1 then the modulus of length in points 
                    Dest = (Dest + 1) % Points.Length;
                    return;
                }
            }
        }
	}

	//------------------------------------------------------------
	// Function makes the Agent seek the closest player
	//------------------------------------------------------------
    void Seek()
    {
		// Calculates distance to Player 1's position
        float Dist = Vector3.Distance(Player1.position, transform.position);

		// Calculates distance to Player 2's position
        float Dist2 = Vector3.Distance(Player2.position, transform.position);

		// Checks if distance to Player 1 is less than the Agent's vision
        if (Dist < vision)
        {
			// If so, make the NavMeshAgent seek Player 1
            GetComponent<NavMeshAgent>().destination = Player1.position;
        }

		// Checks if distance to Player 2 is less than the Agent's vision
        if (Dist2 < vision)
        {
			// If so, make the NavMeshAgent seek Player 1
            GetComponent<NavMeshAgent>().destination = Player2.position;
        }

		// If vision doesn't exceed distance to player 1 or 2
        else
        {
			// Calls NextPoint function
            NextPoint();

			// If path isn't pending and remaining distance is less than 0.5
            if (!Agent.pathPending && Agent.remainingDistance < 0.5f)
				// Call NextPoint function
                NextPoint();
        }
    }

	//------------------------------------------------------------
	// Function runs when collision is first detected
	//
	// Param:
	// 		other: Refers to object of which Agent is colliding
	// 		with
	//------------------------------------------------------------
    private void OnCollisionEnter(Collision other)
    {
        //checks for dead
        if (!dead)
        {
            // Checks for collision with any player
            if (other.gameObject.tag == "Player")
            {
                if (!CoolDown)
                {
                    other.gameObject.GetComponent<HealthScript>().TakeDamage(enemyDamage);
                    CoolDown = true;
                    //print("hit");
                }

                // If CoolDown boolean is true
                if (CoolDown)
                {
                    // If so, it decreases AttackTime by real time is seconds
                    AttackTime -= Time.deltaTime;

                    // Checks if AttackTime gets down to zero or below
                    if (AttackTime <= 0)
                    {
                        // If so, CoolDown is set to false and it cools ResetCoolDown function
                        CoolDown = false;
                        ResetCoolDown();
                    }
                }
            }
        }
    }

    //------------------------------------------------------------
    // Function sets ActiveTime to equal CoolDownTimer float
    //------------------------------------------------------------
    private void ResetCoolDown()
    {
        AttackTime = CoolDownTimer;
    }
}
