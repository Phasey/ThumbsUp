using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAIScript : MonoBehaviour
{
    public float enemyMovementSpeed = 10f;
    Rigidbody rigidBody;

	// Use this for initialization
	void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update()
    {
        rigidBody.AddForce(transform.forward * enemyMovementSpeed);
	}
}
