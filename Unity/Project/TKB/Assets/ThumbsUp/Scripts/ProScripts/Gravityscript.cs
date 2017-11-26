using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------------
// Script is not being utilised in final game.
//
// Author: Liam Knights.
//--------------------------------------------------------------------------------

public class Gravityscript : MonoBehaviour
{
    public Rigidbody public1;
    public Rigidbody public2;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        BasicAIScript AI = GetComponent<BasicAIScript>();

        if (AI.dead)
        {
            public1.useGravity = true;
            public2.useGravity = true;
        }
    }
}
