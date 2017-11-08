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
                    actualTimer = 0f;
                }
            }
            else
            {
                actualTimer = 0f;
            }
        }
    }

   
}
