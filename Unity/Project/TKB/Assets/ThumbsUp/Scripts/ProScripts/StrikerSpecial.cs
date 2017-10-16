using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using XboxCtrlrInput;

public class StrikerSpecial : MonoBehaviour
{
    public int hitForce = 10;
    public float specialTimer = 0.5f;

    private bool strikerSpecial = false;
    private float resetTimer;

    // Allows access to xbox controller buttons
    private XboxController Controller;
    public GameObject HitBox;

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

        if (attackButton && !strikerSpecial)
        {
            strikerSpecial = true;
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

    private void OnCollisionEnter(Collision Other)
    {
        if (strikerSpecial)
        {
            int layerMask = 1 << LayerMask.NameToLayer("Enemy");
            BoxCollider box = HitBox.GetComponent<BoxCollider>();

            Collider[] hitEnemies = Physics.OverlapBox(HitBox.transform.position, box.size * 0.5f, HitBox.transform.rotation, layerMask);
            for (int i = 0; i < hitEnemies.Length; ++i)
            {
                GameObject enemy = hitEnemies[i].gameObject;
                Rigidbody rb = enemy.GetComponent<Rigidbody>();
                NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
                BasicAIScript AI = enemy.GetComponent<BasicAIScript>();

                AI.enabled = false;
                agent.enabled = false;
                rb.isKinematic = false;

                Vector3 direction = enemy.transform.position - transform.position;
                direction.Normalize();
                rb.AddForce(direction * hitForce, ForceMode.Impulse);
            }
        }
    }

    private void ResetCoolDown()
    {
        specialTimer = resetTimer;
    }
}
