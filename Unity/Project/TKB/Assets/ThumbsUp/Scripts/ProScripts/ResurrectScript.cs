using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResurrectScript : MonoBehaviour {


    private GameObject[] Players;
    private HealthScript healthPlayer;

    public float DeathRad = 5f;
    public float DeathTimer = 5f;
    private float actualTimer = 0f;

    // Use this for initialization
    void Awake()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
        healthPlayer = GetComponent<HealthScript>();
    }
	
	// Update is called once per frame
	void Update()
    {
        if (healthPlayer.dead)
        {
            float dist = Vector3.Distance(Players[0].transform.position, Players[1].transform.position);

            if (dist < DeathRad)
            {
                actualTimer += Time.deltaTime;
                if (actualTimer >= DeathTimer)
                {
                    healthPlayer.dead = false;
                    
                    healthPlayer.currentHealth = healthPlayer.maxHealth;
                    if(healthPlayer.dead == false)
                    {
                        revive();
                    }

                    actualTimer = 0f;
                }
            }
            else
            {
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
    }
   
}
