using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using XboxCtrlrInput;

public class SweeperSpecial : MonoBehaviour
{
    public int hitForce = 10;
    public float upForce = 0.5f;
    public float radius = 5f;
    public float coolDownTimerMax = 5f;
    public Slider powerBar;

    // Allows access to xbox controller buttons
    private XboxController Controller;
    private bool coolDown = false;
    private float coolDownTimer;

    //------------------------------------------------------------
    // Function is called when script first runs
    //------------------------------------------------------------
    void Awake()
    {
        PlayerMove move = GetComponent<PlayerMove>();
        Controller = move.Controller;
        coolDownTimer = coolDownTimerMax;
        powerBar.value = coolDownTimerMax;
    }

    //------------------------------------------------------------
    // Function is called once every frame
    //------------------------------------------------------------
    void Update()
    {
        // Calls Special function every frame
        Special();

        powerBar.value = coolDownTimer;
    }

    //------------------------------------------------------------
    // Function allows Striker to use his regular attack
    //------------------------------------------------------------
    private void Special()
    {
        bool attackButton = XCI.GetButtonDown(XboxButton.RightBumper, Controller);

        if (attackButton && !coolDown)
        {
            int layerMask = 1 << LayerMask.NameToLayer("Enemy");

            Collider[] hitEnemies = Physics.OverlapSphere(transform.position, radius,  layerMask);
            for (int i = 0; i < hitEnemies.Length; ++i)
            {
                GameObject enemy = hitEnemies[i].gameObject;
                Rigidbody rb = enemy.GetComponent<Rigidbody>();
                NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
                BasicAIScript AI = enemy.GetComponent<BasicAIScript>();

                AI.dead = true;
                
                agent.enabled = false;
                rb.isKinematic = false;

                Vector3 direction = enemy.transform.position - transform.position;
                direction.Normalize();
                rb.AddForce(direction * hitForce + Vector3.up * upForce, ForceMode.Impulse);
            }

            coolDown = true;
            coolDownTimer = 0f;
        }

        if (coolDown)
        {
            coolDownTimer += Time.deltaTime;

            if (coolDownTimer >= coolDownTimerMax)
            {
                coolDown = false;
            }
        }
    }
}
