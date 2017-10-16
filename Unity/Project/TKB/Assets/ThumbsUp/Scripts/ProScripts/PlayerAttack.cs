using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using XboxCtrlrInput;

// Creates a class for the Striker Attack Script 
public class PlayerAttack : MonoBehaviour
{
    public int hitForce = 10;
    public float upForce = 0.5f;

    // Allows access to xbox controller buttons
    private XboxController Controller;
    public GameObject HitBox;

	//------------------------------------------------------------
	// Function is called when script first runs
	//------------------------------------------------------------
    void Awake()
    {
        PlayerMove move = GetComponent<PlayerMove>();
        Controller = move.Controller;
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

		// If trigger is down a little bit, print Striker!! to console
        if (attackButton > 0.15f)
        {
            int layerMask = 1 << LayerMask.NameToLayer("Enemy");
            BoxCollider box = HitBox.GetComponent<BoxCollider>();

            Collider[] hitEnemies = Physics.OverlapBox(HitBox.transform.position, box.size * 0.5f, HitBox.transform.rotation, layerMask);
            for(int i = 0; i < hitEnemies.Length; ++i)
            {
                GameObject enemy = hitEnemies[i].gameObject;
                Rigidbody rb = enemy.GetComponent<Rigidbody>();
                NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
                BasicAIScript AI = enemy.GetComponent<BasicAIScript>();

                AI.enabled = false;
                agent.enabled = false;
                rb.isKinematic = false;

                Vector3 direction = enemy.transform.position - transform.position;
                direction.Normalize();
                rb.AddForce(direction * hitForce + Vector3.up * upForce, ForceMode.Impulse);
            }
        }
    }
}
