using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResurrectScript : MonoBehaviour {


	public GameObject[] Players;
    private HealthScript healthPlayer;

    public float DeathRad = 5f;
    public float DeathTimer = 5f;
    private float actualTimer = 0f;
    public Image resbar = null;
	public float dist;

    private Animator animator;

    // Use this for initialization
    void Awake()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
        healthPlayer = GetComponent<HealthScript>();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update()
    {
        if (healthPlayer.dead)
        {
            float dist = Vector3.Distance(Players[1].transform.position, Players[2].transform.position);

            if (dist < DeathRad)
            {
                actualTimer += Time.deltaTime;

                resbar.fillAmount = actualTimer * 0.2f;

                if (actualTimer >= DeathTimer)
                {
                    healthPlayer.dead = false;
                    
                    
                    healthPlayer.currentHealth = healthPlayer.maxHealth;
                    if(healthPlayer.dead == false)
                    {
                        revive();
                    }

                    resbar.fillAmount = 0;
                    actualTimer = 0f;
                }
            }
            else
            {
                resbar.fillAmount = 0;
                actualTimer = 0f;
            }
        }
    }

    void revive()
    {
        // Gets Sweeper and Striker's Special components
        SweeperSpecial specialSweeper = GetComponent<SweeperSpecial>();
        StrikerSpecial specialStriker = GetComponent<StrikerSpecial>();

        // Gets Player Move and Player Attack components
        PlayerMove PlayMove = GetComponent<PlayerMove>();

        PlayerAttack playAttack = GetComponent<PlayerAttack>();

        

        
        healthPlayer.body.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        

        //Players[0].transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        //Players[1].transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        // If there is a Striker Special then disable the component
        if (specialStriker)
            specialStriker.enabled = true;

        // If there is a Sweeper Special then disable the component
        if (specialSweeper)
            specialSweeper.enabled = true;

        // If there is a Player Move then disable the component
        if (PlayMove)
            PlayMove.enabled = true;

        // If there is a Player Attack then disable the component
        if (playAttack)
            playAttack.enabled = true;

        animator.SetBool("Dead", false);
    }
   
}
