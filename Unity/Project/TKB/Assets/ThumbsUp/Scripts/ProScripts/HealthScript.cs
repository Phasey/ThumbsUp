using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Creates a class for the Health Script 
public class HealthScript : MonoBehaviour
{
	// Creates public integers for both the max health and current health
    public int maxHealth = 50;
    public int currentHealth;

	// Both variables used to make players flash red when on low health
    public Renderer rend;
    public Color FlashColour;

	// Creates booleans to check if players are being damaged or are alive
    public bool dead;
    bool damaged;

    //sets the cooldown timer for use in attacking
    public float CoolDownTimer = 0.5f;

    // Initialises CoolDown boolean to be false
    private bool CoolDown = false;

    // Sets AttackTime variable to be private
    private float FlashTime = 0.0f;

    public Slider healthBar;

    //------------------------------------------------------------
    // Function is called when script first runs
    //------------------------------------------------------------
    void Awake()
    {
		// Sets current health to equal the max health upon startup
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
    }
	
	//------------------------------------------------------------
	// Function is called once every frame
	//------------------------------------------------------------
	void Update()
    {
        Flash();
        healthBar.value = currentHealth;
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
        if (currentHealth <= 0)
            Death();
    }

	//------------------------------------------------------------
	// Function sets dead bool to be true when called
	//------------------------------------------------------------
    private void Death()
    {
        dead = true;
        SweeperSpecial specialSweeper = GetComponent<SweeperSpecial>();
        StrikerSpecial specialStriker = GetComponent<StrikerSpecial>();
        PlayerMove PlayMove = GetComponent<PlayerMove>();
        PlayerAttack playAttack = GetComponent<PlayerAttack>();

        if(specialStriker)
            specialStriker.enabled = false;

        if(specialSweeper)
            specialSweeper.enabled = false;

        if(PlayMove)
            PlayMove.enabled = false;

        if(playAttack)
            playAttack.enabled = false;
    }

	//------------------------------------------------------------
	// Function allows player to flash red when on low health
	//------------------------------------------------------------
    private void Flash()
    {
        // Ignores following code if the flash time is less than zero
        if (FlashTime <= 0.0f)
            return;

		if(!CoolDown)
        {
            // Gets renderer component and stores it into rend
            rend = GetComponentInChildren<Renderer>();
           
            // Finds the shader the player will use and stores it into rend
            //rend.material.shader = Shader.Find("Black");

            // Sets the rend colour to be whatever the FlashColour is set to
            rend.material.color = FlashColour;

            CoolDown = true;
        }

        // Checks if CoolDown boolean is true
        if (CoolDown)
        {
            // If so, it decreases AttackTime by real time is seconds
            FlashTime -= Time.deltaTime;

            // Checks if AttackTime gets to exactly 1
            if (FlashTime <= 0)
            {
                rend = GetComponentInChildren<Renderer>();

                // Sets the rend colour to be whatever the FlashColour is set to
                rend.material.color = Color.black;
                CoolDown = false;
            }
        }
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
            ResetCoolDown();
            Flash();
        }
    }

    //------------------------------------------------------------
    // Function sets ActiveTime to equal CoolDownTimer float
    //------------------------------------------------------------
    private void ResetCoolDown()
    {
        FlashTime = CoolDownTimer;
    }
}
