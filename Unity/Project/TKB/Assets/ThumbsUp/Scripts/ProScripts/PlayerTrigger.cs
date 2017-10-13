using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

// Creates a class for the Player Trigger Script
public class PlayerTrigger : MonoBehaviour 
{
	// Allows access to xbox controller buttons
	public XboxController Controller;

	// Initialises public variables so designers can adjust them
	public float radius = 2.0F;
    public float power = 20.0f;
    public float UpPower = 1.5f;
    public float CoolDownTimer = 2f;

	// Sets AttackTime variable to be private
    private float AttackTime;

	// Initialises CoolDown boolean to be false
    private bool CoolDown = false;

	// Creates a ParticleSystem variable
    public ParticleSystem explodeParticles;

	//------------------------------------------------------------
	// Function is called when script first runs
	//------------------------------------------------------------
	void Awake() {}
	
	//------------------------------------------------------------
	// Function is called once every frame
	//------------------------------------------------------------
	void Update() 
	{
		// Calls PlayerExplode function every frame
		PlayerExplode();
	}

	//------------------------------------------------------------
	// Function allows for objects to explode
	//------------------------------------------------------------
	private void Explode()
	{
		// Sets the explosion position to equal the transform position
		Vector3 explosionPos = transform.position;
        
		// Finds all GameObjects with Enemy tag and stores them into local array
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");

		// Runs for every GameObject in Enemies array
		foreach (GameObject hit in Enemies)
		{
			// Gets current enemy's RigidBody
			Rigidbody rb = hit.GetComponent<Rigidbody>();

			// If there is a RigidBody on enemy, then add an explosion force to it
			if (rb != null) 
			{
				rb.AddExplosionForce (power, explosionPos, radius, UpPower, ForceMode.Impulse);
			}
		}
	}

	//------------------------------------------------------------
	// Function checks for player input to allow explosion
	//------------------------------------------------------------
    private void PlayerExplode()
    {
		// Gets the Right Trigger button from Xbox controller
        float attackButton = XCI.GetAxis(XboxAxis.RightTrigger, Controller);

		// Checks if trigger is down a little bit and CoolDown mode is not active
        if (attackButton > 0.15f && !CoolDown)
        {
			// If not, Explode function is called and CoolDown mode activates
            Explode();
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
	// Function sets ActiveTime to equal CoolDownTimer float
	//------------------------------------------------------------
    private void ResetCoolDown()
    {
        AttackTime = CoolDownTimer;
    }
}

//Physics.OverlapSphere() //Use for Sweeper Special Attack (Create seperate script)
//Physics.OverlapCapsule() //Use for Striker Special Attack (Create seperate script)
