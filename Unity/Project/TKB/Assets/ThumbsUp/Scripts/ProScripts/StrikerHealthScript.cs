using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikerHealthScript : MonoBehaviour
{
    public const int maxStrikerHealth = 25;

    public int currentStrikerHealth;
    public GameObject strikerPlayer;

    bool dead;
    bool damaged;

	// Use this for initialization
	void Awake()
    {
        currentStrikerHealth = maxStrikerHealth;
	}
	
	// Update is called once per frame
	void Update()
    {
        if (dead)
            Destroy(strikerPlayer);
    }

    public void TakeDamage(int damage)
    {
        damaged = true;
        currentStrikerHealth -= damage;

        if (currentStrikerHealth <= 0 && !dead)
            Death();
    }

    private void Death()
    {
        dead = true;
    }
}
