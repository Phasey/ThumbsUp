using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using XboxCtrlrInput;

public class StrikerSpecial : MonoBehaviour
{
    public int hitForce = 10;
    public float specialTimer = 0.5f;

    private bool attacking = false;
    private float resetTimer;

    // Allows access to xbox controller buttons
    private XboxController Controller;

    //------------------------------------------------------------
    // Function is called when script first runs
    //------------------------------------------------------------
    void Awake()
    {
        PlayerMove move = GetComponent<PlayerMove>();
        Controller = move.Controller;
        resetTimer = specialTimer;
    }

    //------------------------------------------------------------
    // Function is called once every frame
    //------------------------------------------------------------
    void Update()
    {
        // Calls Special function every frame
        Special();
    }

    //------------------------------------------------------------
    // Function allows Striker to use his regular attack
    //------------------------------------------------------------
    private void Special()
    {
        bool attackButton = XCI.GetButtonDown(XboxButton.RightBumper, Controller);

        if (attackButton && !attacking)
        {
            attacking = true;
        }

        if (attacking)
        {
            PlayerMove pm = gameObject.GetComponent<PlayerMove>();
            pm.movementSpeed = 30;
            specialTimer -= Time.deltaTime;

            if (specialTimer <= 0)
            {
                attacking = false;
                pm.movementSpeed = 10;
                ResetCoolDown();
            }
        }
    }

    private void ResetCoolDown()
    {
        specialTimer = resetTimer;
    }
}
