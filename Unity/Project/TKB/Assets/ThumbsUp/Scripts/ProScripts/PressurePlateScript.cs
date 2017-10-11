using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateScript : MonoBehaviour
{
    public Rigidbody rigidBodyCube;
    public Animation anim;
    public GameObject door;

	// Use this for initialization
	void Start()
    {
        
	}

    // Update is called once per frame
	void Update()
    {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            Destroy(door);
    }
}
