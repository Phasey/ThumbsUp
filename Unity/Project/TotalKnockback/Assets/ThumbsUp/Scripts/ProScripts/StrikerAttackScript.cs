using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class StrikerAttackScript : MonoBehaviour
{
    public XboxController Controller;

    // Use this for initialization
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StrikerAttack();
    }

    private void StrikerAttack()
    {
        float attackButton = XCI.GetAxis(XboxAxis.RightTrigger, Controller);

        if (attackButton > 0.15f)
        {
            print("Striker!!");
        }
    }
}
