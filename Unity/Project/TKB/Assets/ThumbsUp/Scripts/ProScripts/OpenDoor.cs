using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------------
// Author: Matthew Le Nepveu.
//--------------------------------------------------------------------------------

public class OpenDoor : MonoBehaviour
{
    // Creates a list so multiple triggers can be placed in
    public List<GameObject> triggers;

    // GameObject used for the Closed Door prefab
    public GameObject closedDoor;
	
    // GameObject used for the Open Door prefab
	public GameObject doorOpen;

    // Initialized to access the Animator
    public Animator animator;

    //--------------------------------------------------------------------------------
    // Function is called when script first runs.
    //--------------------------------------------------------------------------------
    void Awake() {}

    //--------------------------------------------------------------------------------
    // Function is called once every frame.
    //--------------------------------------------------------------------------------
    void Update()
    {
        // Gets return result from function and stores it in local bool
        bool triggersDown = IsTriggered();

        // Checks if local bool is true before running code in braces
		if (triggersDown)
        {
            // Sets Open bool in animator to be true
			animator.SetBool("Open", true);

            // Sets the Open Door to be true
			doorOpen.SetActive(true);

            // Yields a result from Open function at the same time as Open
            StartCoroutine(Open());
		}
    }

    //--------------------------------------------------------------------------------
    // Function checks if all triggers have been pressed down
    //
    // Return:
    //      Returns a bool determining if triggers have been pressed
    //--------------------------------------------------------------------------------
    private bool IsTriggered()
    {
        // Runs following code for all triggers in list
        for (int i = 0; i < triggers.Count; ++i)
        {
            // Returns false if current selected trigger is false
            if (!triggers[i].GetComponent<PressurePlateScript>().triggered)
                return false;
        }

        // Returns true as all triggers must be true if code here runs
        return true;
    }

    //-----------------------------------------------------------------------------
    // Function waits 4 seconds until it sets closed door to false.
    //-----------------------------------------------------------------------------
    IEnumerator Open()
	{
		yield return new WaitForSeconds(4f);
		closedDoor.SetActive(false);
	}
}
