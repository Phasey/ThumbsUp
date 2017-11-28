using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------------
// Author: Matthew Le Nepveu.
//--------------------------------------------------------------------------------

public class PressurePlateScript : MonoBehaviour
{
    // Bool used to check for a trigger
    public bool triggered;

    //--------------------------------------------------------------------------------
    // Function is called when script first runs
    //--------------------------------------------------------------------------------
    void Awake()
    {
        triggered = false;
    }

    //--------------------------------------------------------------------------------
    // Function is called once every frame
    //--------------------------------------------------------------------------------
    void Update() {}

    //--------------------------------------------------------------------------------
    // Function runs when a trigger is first detected
    //
    // Param:
    // 		other: Refers to object of which Agent is triggering
    //--------------------------------------------------------------------------------
    void OnTriggerEnter(Collider other)
    {
        // Checks if a GameObject with tag "Crate" is triggering function
        if (other.gameObject.tag == "Crate")
        {
            // If so, set triggered bool to be true
            triggered = true;
        }
    }

    //--------------------------------------------------------------------------------
    // Function runs when a trigger is first detected exitting
    //
    // Param:
    // 		other: Refers to object of which Agent is triggering the exit
    //--------------------------------------------------------------------------------
    void OnTriggerExit(Collider other)
    {
        // Checks if a GameObject with tag "Crate" exits trigger
        if (other.gameObject.tag == "Crate")
        {
            // If so, set triggered bool to be false
            triggered = false;
        }
    }
}