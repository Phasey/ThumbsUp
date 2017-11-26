using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//---------------------------------------------------------------
//Author: Liam Knights
//---------------------------------------------------------------

// Creates a class for the Basic AI Script 
public class BasicAIScript : MonoBehaviour
{
	// Initialises public floats for Designers to adjust
    public float enemyMovementSpeed = 10f;
    public float vision = 10f;
    public float enemyHealth = 1f;
    public float enemyDamage = 5f;
	public Animator animator;
	public GameObject boneParticle;
	public bool attacking;

    public Renderer rend;
    public Color FlashColour;

    private bool isFlashing = false;

    // Sets AttackTime variable to be private
    private float FlashTime = 0.0f;

    // Gets access to a RigidBody
    Rigidbody rigidBody;

    // Allows the transform for both players to be accessed
    private PlayerMove[] players;

    // Accesses the AI as a game object
    public GameObject enemy;

    //public GameObject sweeperObject;
    //public GameObject strikerObject;

    // Gets the transform of points on the navmesh
    public Transform[] Points;

	// Initialises the destination private int to 0
    private int Dest = 0;

	// Sets a private NavMeshAgent for use in script 
    private NavMeshAgent Agent;
    
    // Sets the cooldown timer for use in attacking
    public float CoolDownTimer = 2f;
		
    // Initialises CoolDown boolean to be false
    private bool CoolDown = false;

    // Sets AttackTime variable to be private
    private float AttackTime;

    // Bool determines if the enemy is dead or not
    public bool dead = false;

    // Determines how long an enemy will be visible before disappearing
    public float deadTime = 2f;

    public int CircleRange = 3;

	public AudioSource growl;
	public bool growled;

    public bool IsBoss = true;

    //------------------------------------------------------------
    // Function is called when script first runs
    //------------------------------------------------------------
    void Awake()
    {
        // Code inside runs if the enemy is not dead
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

    void Start()
    {
        players = FindObjectsOfType<PlayerMove>();
    }
	
	//------------------------------------------------------------
	// Function is called once every frame
	//------------------------------------------------------------
	void Update()
    {
        // Code inside runs if the enemy is not dead
        if (!dead)
        {
            // Adds a forward force and speed to the rigidBody 
            rigidBody.AddForce(transform.forward * enemyMovementSpeed);

            // Calls Seek function every frame
            Seek();
        }

        // Code inside runs if the enemy is dead
        if (dead)
        {
			boneParticle.SetActive (true);
            Physics.IgnoreCollision(players[0].GetComponent<Collider>(), GetComponent<Collider>());
            Physics.IgnoreCollision(players[1].GetComponent<Collider>(), GetComponent<Collider>());
			animator.SetBool ("Dead", true);
            // Timer begins to count down
            deadTime -= Time.deltaTime;

            // If the timer hits zero or below, then the enemy gets destroyed
            if (deadTime <= 0)
            {
                Destroy(enemy);
            }
        }
		if (CoolDown)
		{
			// If so, it decreases AttackTime by real time is seconds
			AttackTime -= Time.deltaTime;

			// Checks if AttackTime gets down to zero or below
			if (AttackTime <= 1.5f) {
				animator.SetBool ("Attack", false);
				if (AttackTime <= 0) {
					// If so, CoolDown is set to false and it cools ResetCoolDown function

					CoolDown = false;
					ResetCoolDown ();
				}
			}
		}

        Flash();
    }

    //------------------------------------------------------------
    // Function calculates the next point for NavMeshAgent to go
    //------------------------------------------------------------
    void NextPoint()
    {
        // Code inside runs if the enemy is not dead
        if (!dead)
        {
            // Code inside only runs 
            if (Points.Length != 0)
            {
                Vector3 destpos = Points[Dest].position;



                int angle = Random.Range(0, 360);
                // Agents destination refers to indexed point in points array

                destpos.x += CircleRange * Mathf.Cos(angle);
                destpos.y += CircleRange * Mathf.Sin(angle);

                Agent.destination = destpos;



                // Ignores function if the length to the points equals zero
                if (Vector3.Distance(Agent.destination, transform.position) < 1)
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
        float Dist = Vector3.Distance(players[0].transform.position, transform.position);

		// Calculates distance to Player 2's position
        float Dist2 = Vector3.Distance(players[1].transform.position, transform.position);

		// Checks if distance to Player 1 is less than the Agent's vision
        if (Dist < vision && Dist < Dist2)
        {
			// If so, make the NavMeshAgent seek Player 1
            GetComponent<NavMeshAgent>().destination = players[0].transform.position;
			animator.SetBool ("Speed", true);
			if (!growl.isPlaying && !growled) {
				growl.Play ();
				growled = true;
			}


        }

		// Checks if distance to Player 2 is less than the Agent's vision
        else if (Dist2 < vision)
        {
			// If so, make the NavMeshAgent seek Player 2
            GetComponent<NavMeshAgent>().destination = players[1].transform.position;
			animator.SetBool ("Speed", true);
			if (!growl.isPlaying && !growled) {
				growl.Play ();
				growled = true;
			}

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
    private void OnCollisionStay(Collision other)
    {
        // Code inside runs if the enemy is not dead
        if (!dead)
        {
            // Checks for collision with any player
            if (other.gameObject.tag == "Player")
            {
				
				if (!CoolDown)
                {
                    other.gameObject.GetComponent<HealthScript>().TakeDamage(enemyDamage);
					animator.SetBool ("Attack", true);
                    CoolDown = true;

                }

                // If CoolDown boolean is true
                
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


    public void Flash()
    {
        // Ignores following code if the flash time is less than zero
        if (!isFlashing)
            return;

        if (!CoolDown)
        {
            // Gets renderer component and stores it into rend
            //rend = GetComponent<Renderer>();

            rend.material.EnableKeyword("_EMISSION");
            // Sets the rend colour to be whatever the FlashColour is set to
            rend.material.SetColor("_EmissionColor", FlashColour);

           

            CoolDown = true;
        }

        // Checks if CoolDown boolean is true
        if (CoolDown)
        {
            // If so, it decreases AttackTime by real time is seconds
            FlashTime -= Time.deltaTime;

            // Checks if AttackTime gets to exactly 1
            if (FlashTime <= 0)
            {
                // rend = GetComponent<Renderer>();

                // Sets the rend colour to be whatever the FlashColour is set to
                rend.material.DisableKeyword("_EMISSION");
                
                CoolDown = false;
                isFlashing = false;
            }
        }
    }
    public void ResetFlashCoolDown()
    {
        FlashTime = CoolDownTimer;
        isFlashing = true;
    }
}
