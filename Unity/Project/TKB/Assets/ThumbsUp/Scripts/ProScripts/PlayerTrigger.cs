using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerTrigger : MonoBehaviour 
{
	public XboxController Controller;

	public float radius = 2.0F;
    public float power = 20.0f;
    public float UpPower = 1.5f;
    public float CooldownTimer = 2f;
    private float AttackTime;
    private bool CoolDown = false;


    public ParticleSystem explodeParticles;

	// Use this for initialization
	void Start() 
	{
		
	}
	
	// Update is called once per frame
	void Update() 
	{
		PlayerExplode();
	}

	private void Explode()
	{
		Vector3 explosionPos = transform.position;
        //Physics.OverlapSphere() //Use for Sweeper Special Attack (Create seperate script)
        //Physics.OverlapCapsule() //Use for Striker Special Attack (Create seperate script)
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");

		foreach (GameObject hit in Enemies)
		{
			Rigidbody rb = hit.GetComponent<Rigidbody>();

			if (rb != null) 
			{
				rb.AddExplosionForce (power, explosionPos, radius, UpPower, ForceMode.Impulse);
			}
		}
			
	}

    private void PlayerExplode()
    {
        float attackButton = XCI.GetAxis(XboxAxis.RightTrigger, Controller);

        if (attackButton > 0.15f && !CoolDown)
        {
            Explode();

            CoolDown = true;
        }

        if (CoolDown)
        {
            AttackTime -= Time.deltaTime;
            if (AttackTime <= 0)
            {
                CoolDown = false;
                ResetCoolDown();
            }
        }
    }

    private void ResetCoolDown()
    {
        AttackTime = CooldownTimer;
    }
}

