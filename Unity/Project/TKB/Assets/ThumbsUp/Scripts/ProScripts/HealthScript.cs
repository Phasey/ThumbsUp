using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates a class for the Health Script 
public class HealthScript : MonoBehaviour
{
	// Creates public integers for both the max health and current health
    public int maxHealth = 50;
    public int currentHealth;

	// Creates public GameObject so Designers can assign both players to use this script
    public GameObject player;

	// Both variables used to make players flash red when on low health
    public Renderer rend;
    public Color FlashColour;

	// Creates booleans to check if players are being damaged or are alive
    bool dead;
    bool damaged;

	//------------------------------------------------------------
	// Function is called when script first runs
	//------------------------------------------------------------
	void Awake()
    {
		// Sets current health to equal the max health upon startup
        currentHealth = maxHealth;
	}
	
	//------------------------------------------------------------
	// Function is called once every frame
	//------------------------------------------------------------
	void Update()
    {
		// If player is dead, then destroy the player from game
        if (dead)
            Destroy(player);
    }

	//------------------------------------------------------------
	// Function allows players to take damage
	//
	// Param:
	// 		damage: Refers to how much damade the players take
	//------------------------------------------------------------
    public void TakeDamage(int damage)
    {
		// Sets damaged bool to be true
        damaged = true;

		// Deducts health by the amount of damage taken
        currentHealth -= damage;

		// If players health is zero or less and they aren't dead yet, then call Death function
        if (currentHealth <= 0 && !dead)
            Death();
    }

	//------------------------------------------------------------
	// Function sets dead bool to be true when called
	//------------------------------------------------------------
    private void Death()
    {
        dead = true;
    }

	//------------------------------------------------------------
	// Function allows player to flash red when on low health
	//------------------------------------------------------------
    private void Flash()
    {
		// Gets renderer component and stores it into rend
        rend = GetComponent<Renderer>();

		// Finds the shader the player will use and stores it into rend
        rend.material.shader = Shader.Find("lambert1");

		// Sets the rend colour to be whatever the FlashColour is set to
        rend.material.color = FlashColour;

        //After timer, renderer sets colour back
    }

	//------------------------------------------------------------
	// Function runs when collision is first detected
	//
	// Param:
	// 		other: Refers to object of which Agent is colliding
	// 		with
	//------------------------------------------------------------
    private void OnCollisionEnter(Collision other)
    {
		// If object collides with an enemy, then call Flash function
        if (other.gameObject.tag == "Enemy")
        {
            Flash();
        }
    }
}
