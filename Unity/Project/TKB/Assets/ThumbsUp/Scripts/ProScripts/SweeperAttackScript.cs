using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

// Creates a class for the Sweeper Attack Script 
public class SweeperAttackScript : MonoBehaviour
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
		// Calls SweeperAttack function every frame
        SweeperAttack();
    }

	//------------------------------------------------------------
	// Function allows Sweeper to use his regular attack
	//------------------------------------------------------------
    private void SweeperAttack()
    {
		// Gets the Right Trigger button from Xbox controller
        float attackButton = XCI.GetAxis(XboxAxis.RightTrigger, Controller);

		// If trigger is down a little bit, print Sweeper!! to console
        if (attackButton > 0.15f)
        {
            print("Sweeper!!");
        }
    }
}
