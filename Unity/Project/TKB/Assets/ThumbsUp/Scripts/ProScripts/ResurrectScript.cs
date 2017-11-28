//---------------------------------------------------------------
//Author: Liam Knights
//---------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResurrectScript : MonoBehaviour {

    //gets the players
	public GameObject[] Players;
    //gets the players health from healthscript 
    private HealthScript healthPlayer;

    //public variables for desighners to change if need be
    public float DeathRad = 5f;
    public float DeathTimer = 5f;
    private float actualTimer = 0f;
    public Image resbar = null;
    
    //gets an animator
    private Animator animator;

    // Use this for initialization
    void Awake()
    {
        //checks if players have the player tag
        Players = GameObject.FindGameObjectsWithTag("Player");

        //gets the healthscript component for health players
        healthPlayer = GetComponent<HealthScript>();
        //gets the Animator component for animator
        animator = GetComponent<Animator>();
       
    }
	
	// Update is called once per frame
	void Update()
    {
        //checks if player os dead
        if (healthPlayer.dead)
        {
            //gets the distance between the players
            float dist = Vector3.Distance(Players[0].transform.position, Players[1].transform.position);


            //checks distance of players against the death radius
            if (dist < DeathRad)
            {
                //sets the actual timer to delta time
                actualTimer += Time.deltaTime;
                //shows the timer as the resbar
                resbar.fillAmount = actualTimer * 0.2f;

                //checks if actual timer is greater then death timer 
                if (actualTimer >= DeathTimer)
                {
                    //then ressurects the player
                    healthPlayer.dead = false;
                    
                    //and sets the players health back to their max
                    healthPlayer.currentHealth = healthPlayer.maxHealth;

                    //checks if the player is still dead
                    if(healthPlayer.dead == false)
                    {
                        //runs revive
                        revive();
                    }

                    //if not sets the resbar back to 0 
                    resbar.fillAmount = 0;
                    //and then sets the timer back to 0
                    actualTimer = 0f;
                }
            }
            else
            {
                //sets the resbar back to 0 
                resbar.fillAmount = 0;
                //and then sets the timer back to 0
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

        

        //sets the players rigid body constraints back to waht they where when they lived
        healthPlayer.body.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        

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
		healthPlayer.helpMe.SetActive(false); //sets the players help me icon to false
        animator.SetBool("Dead", false); //sets the players death animation to false
    }
   
}
