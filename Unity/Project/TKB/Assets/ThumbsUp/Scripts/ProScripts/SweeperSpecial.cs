using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using XboxCtrlrInput;

public class SweeperSpecial : MonoBehaviour
{
    public int hitForce = 10;

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
            //int layerMask = 1 << LayerMask.NameToLayer("Enemy");
            //CapsuleCollider box = HitBox.GetComponent<CapsuleCollider>();

            //Collider[] hitEnemies = Physics.OverlapCapsule(HitBox.transform.position, box.radius * 0.5, HitBox.transform.rotation, layerMask);
            //for (int i = 0; i < hitEnemies.Length; ++i)
            //{
            //    GameObject enemy = hitEnemies[i].gameObject;
            //    Rigidbody rb = enemy.GetComponent<Rigidbody>();
            //    NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
            //    BasicAIScript AI = enemy.GetComponent<BasicAIScript>();

            //    AI.enabled = false;
            //    agent.enabled = false;
            //    rb.isKinematic = false;

            //    Vector3 direction = enemy.transform.position - transform.position;
            //    direction.Normalize();
            //    rb.AddForce(direction * hitForce, ForceMode.Impulse);
            //}
        }
    }
}
