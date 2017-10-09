using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateScript : MonoBehaviour
{
    public Rigidbody rigidBodyCube;
    public Animation anim;

	// Use this for initialization
	void Start ()
    {
        
	}

    // Update is called once per frame
	void Update ()
    {
		
	}

    private void PressurePlate()
    {
        GameObject[] plate = GameObject.FindGameObjectsWithTag("Switch");

        foreach (GameObject collide in plate)
        {
            rigidBodyCube = collide.GetComponent<Rigidbody>();

            if (GameObject.FindGameObjectWithTag("Box"))
            {
                anim.Play("DoorAnimation");
            }
        }
    }
}
