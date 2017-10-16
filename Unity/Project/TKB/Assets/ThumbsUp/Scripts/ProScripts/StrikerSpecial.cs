using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using XboxCtrlrInput;

public class StrikerSpecial : MonoBehaviour
{
    public int hitForce = 1;
    public float specialTimer = 0.5f;

    private bool strikerSpecial = false;
    private float resetTimer;

    // Allows access to xbox controller buttons
    private XboxController Controller;
    //public GameObject HitBox;

    private Vector3 startPos;
    private Vector3 endPos;
    public float dist = 25;
    public float width = 5;
    public float upForce = 0.5f;

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

        if (attackButton)
        {
            strikerSpecial = true;
            startPos = transform.position;
            endPos = startPos + transform.forward * dist;
            Knockback();
        }

        if (strikerSpecial)
        {
            PlayerMove pm = gameObject.GetComponent<PlayerMove>();
            pm.movementSpeed = 30;
            pm.doingSpecial = true;
            specialTimer -= Time.deltaTime;
            GetComponent<Rigidbody>().velocity = transform.forward * pm.movementSpeed;

            if (specialTimer <= 0)
            {
                strikerSpecial = false;
                pm.doingSpecial = false;
                pm.movementSpeed = 10;
                ResetCoolDown();
            }
        }
    }

    private void Knockback()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Enemy");
        //BoxCollider box = HitBox.GetComponent<BoxCollider>();

        Vector3 dashDir = endPos - startPos;
        dashDir.Normalize();
        Vector3 centre = (startPos + endPos) * 0.5f;
        Vector3 halfSize = new Vector3(width * 0.5f, 2, dist * 0.5f);

        Collider[] hitEnemies = Physics.OverlapBox(centre, halfSize, transform.rotation, layerMask);
        for (int i = 0; i < hitEnemies.Length; ++i)
        {
            GameObject enemy = hitEnemies[i].gameObject;
            Rigidbody rb = enemy.GetComponent<Rigidbody>();
            NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
            BasicAIScript AI = enemy.GetComponent<BasicAIScript>();

            AI.enabled = false;
            agent.enabled = false;
            rb.isKinematic = false;
            rb.useGravity = true;

            Vector3 enemyDir = enemy.transform.position - startPos;
            enemyDir.Normalize();

            Vector3 rightAngle = new Vector3(dashDir.z, 0, -dashDir.x);
            float dot = Vector3.Dot(rightAngle, enemyDir);

            if(dot > 0.0f)
                rb.AddForce(rightAngle * hitForce + Vector3.up * upForce, ForceMode.Impulse);
            else
                rb.AddForce(-rightAngle * hitForce + Vector3.up * upForce, ForceMode.Impulse);
        }
    }

    private void ResetCoolDown()
    {
        specialTimer = resetTimer;
    }
}
