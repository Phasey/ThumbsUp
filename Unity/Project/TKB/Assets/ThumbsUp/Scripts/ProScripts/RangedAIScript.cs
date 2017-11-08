using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedAIScript : MonoBehaviour {

    // Initialises public floats for Designers to adjust
    public float enemyMovementSpeed = 10f;
    public float vision = 10f;
    public float DistFromPlayer = 3f;
    public float AttackVision = 1f;
    public float enemyHealth = 1f;
    public float enemyDamage = 5f;
    public float ArrowImpulse = 20.0f;

    // Gets access to a RigidBody
    Rigidbody rigidBody;

    // Allows the transform for both players to be accessed
    private PlayerMove[] players;

    // Accesses the AI as a game object
    public GameObject enemy;

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


    public Rigidbody ProjectedArrow;

    public int CircleRange = 3;

    private bool folowing = true;

    //------------------------------------------------------------
    // Function is called when script first runs
    //------------------------------------------------------------
    void Awake()
    {
        // Code inside runs if the enemy is not dead
        if (!dead)
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
            Physics.IgnoreCollision(players[0].GetComponent<Collider>(), GetComponent<Collider>());
            Physics.IgnoreCollision(players[1].GetComponent<Collider>(), GetComponent<Collider>());

            // Timer begins to count down
            deadTime -= Time.deltaTime;

            // If the timer hits zero or below, then the enemy gets destroyed
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




    void AttackArrow()
    {
        if (!CoolDown)
        {
            Rigidbody Arrow = (Rigidbody)Instantiate(ProjectedArrow, transform.position + transform.forward, transform.rotation);
            Arrow.AddForce(transform.forward * ArrowImpulse, ForceMode.Impulse);

            Destroy(Arrow.gameObject, 2);

            CoolDown = true;
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
        if (Dist < vision && Dist < Dist2 )
        {
            transform.LookAt(players[0].transform.position);
            // If so, make the NavMeshAgent seek Player 1

            if (Dist >= DistFromPlayer)
            {
                GetComponent<NavMeshAgent>().destination = players[0].transform.position;
            }
            else if(Dist == DistFromPlayer)
            {
                GetComponent<NavMeshAgent>().destination = enemy.transform.position;
            }
            else if (Dist2 < DistFromPlayer)
            {
                GetComponent<NavMeshAgent>().destination = players[0].transform.TransformDirection(transform.forward * -1);
            }

            AttackArrow();
        }

        // Checks if distance to Player 2 is less than the Agent's vision
        else if (Dist2 < vision)
        { 

            transform.LookAt(players[1].transform.position);
            // If so, make the NavMeshAgent seek Player 2
            if (Dist2 >= DistFromPlayer)
            {
                GetComponent<NavMeshAgent>().destination = players[1].transform.position;
            }
            else if(Dist2 == DistFromPlayer)
            {
                GetComponent<NavMeshAgent>().destination = enemy.transform.position;
            }
            else if (Dist2 < DistFromPlayer)
            {
                GetComponent<NavMeshAgent>().destination = players[1].transform.TransformDirection(transform.forward * -1);
            }

            AttackArrow();
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
        // Code inside runs if the enemy is not dead
        if (!dead)
        {
            // Checks for collision with any player
            if (other.gameObject.tag == "Arrow")
            {
                if (!CoolDown)
                {
                    other.gameObject.GetComponent<HealthScript>().TakeDamage(enemyDamage);
                    CoolDown = true;
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
