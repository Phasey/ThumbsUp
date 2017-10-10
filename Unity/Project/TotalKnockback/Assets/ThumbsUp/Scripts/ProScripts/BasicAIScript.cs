using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicAIScript : MonoBehaviour
{
    public float enemyMovementSpeed = 10f;
    Rigidbody rigidBody;
    public float vision = 10f;
    public Transform Player1;
    public Transform Player2;
    public Transform[] Points;
    private int Dest = 0;
    private NavMeshAgent Agent;

    // Use this for initialization
    void Start()
    {

        rigidBody = GetComponent<Rigidbody>();
        Agent = GetComponent<NavMeshAgent>();

        Agent.autoBraking = false;

        
    }

    void NextPoint()
    {
        if (Points.Length == 0)
        {
            return;
        }
       
        Agent.destination = Points[Dest].position;

        Dest = (Dest + 1) % Points.Length;
    }
	
	// Update is called once per frame
	void Update()
    {
        rigidBody.AddForce(transform.forward * enemyMovementSpeed);
        seek();
	}

    void seek()
    {

        float Dist = Vector3.Distance(Player1.position, transform.position);
        float Dist2 = Vector3.Distance(Player2.position, transform.position);

        if (Dist < vision)
        {
            GetComponent<NavMeshAgent>().destination = Player1.position;
        }
        if(Dist2< vision)
        {
            GetComponent<NavMeshAgent>().destination = Player2.position;
        }
        else
        {
            NextPoint();
            if (!Agent.pathPending && Agent.remainingDistance < 0.5f)
                NextPoint();
        }



    }
}
