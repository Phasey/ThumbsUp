using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

// Creates a class for the Striker Attack Script 
public class StrikerAttackScript : MonoBehaviour
{
	// Allows access to xbox controller buttons
    public XboxController Controller;

	//------------------------------------------------------------
	// Function is called when script first runs
	//------------------------------------------------------------
    void Awake()
    {

    }

	//------------------------------------------------------------
	// Function is called once every frame
	//------------------------------------------------------------
    void Update()
    {
		// Calls StrikerAttack function every frame
        StrikerAttack();
    }

	//------------------------------------------------------------
	// Function allows Striker to use his regular attack
	//------------------------------------------------------------
    private void StrikerAttack()
    {
		// Gets the Right Trigger button from Xbox controller
        float attackButton = XCI.GetAxis(XboxAxis.RightTrigger, Controller);

		// If trigger is down a little bit, print Striker!! to console
        if (attackButton > 0.15f)
        {
            print("Striker!!");
        }
    }
}
