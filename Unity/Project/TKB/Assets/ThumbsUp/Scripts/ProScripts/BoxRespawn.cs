using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRespawn : MonoBehaviour
{
    public Vector3 startPos;
    public float lowestYPos = -10;

	// Use this for initialization
	void Awake()
    {
        transform.position = startPos;
	}
	
	// Update is called once per frame
	void Update()
    {
        if (transform.position.y <= lowestYPos)
        {
            transform.position = startPos;
            transform.rotation = Quaternion.identity;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}
