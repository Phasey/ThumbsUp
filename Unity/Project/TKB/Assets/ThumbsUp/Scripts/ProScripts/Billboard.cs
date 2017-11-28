//--------------------------------------------------------------------------------
// Author: Nick Barnett
//--------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    //--------------------------------------------------------------------------------
    // Function is called once every frame.
    //--------------------------------------------------------------------------------
    void Update() 
	{
        // Makes billboard look down from the camera
		transform.LookAt(Camera.main.transform.position, -Vector3.up);
	}
}
