using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class SweeperAttackScript : MonoBehaviour
{
    public XboxController Controller;

    // Use this for initialization
    void Awake()
    {
       
	}
	
	// Update is called once per frame
	void Update()
    {
        SweeperAttack();
    }

    private void SweeperAttack()
    {
        float attackButton = XCI.GetAxis(XboxAxis.RightTrigger, Controller);

        if (attackButton > 0.15f)
        {
            print("Sweeper!!");
        }
    }
}
