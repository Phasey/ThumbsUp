using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Creates a class for the Health Script 
public class HealthScript : MonoBehaviour
{
	// Creates public integers for both the max health and current health
    public float maxHealth = 50;
    public float currentHealth;

	// Both variables used to make players flash red when on low health
    private Renderer rend;
    public Color FlashColour;

	// Creates booleans to check if players are being damaged or are alive
    public bool dead;

    //sets the cooldown timer for use in attacking
    public float CoolDownTimer = 0.5f;

    // Initialises CoolDown boolean to be false
    private bool CoolDown = false;

    private bool isFlashing = false;

    // Sets AttackTime variable to be private
    private float FlashTime = 0.0f;

    // Gets access to the health bar so health bar value can decrease according to health
    public Slider healthBar;

	public Animator animator;
	public Rigidbody body;
    public GameObject helpMe;

    public bool inSpecial;

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
        // Calls the flash function every frame
        Flash();

        // Makes the value of the health bar equal the player's health
        healthBar.value = currentHealth;
    }

	//------------------------------------------------------------
	// Function allows players to take damage
	//
	// Param:
	// 		damage: Refers to how much damage the players take
	//------------------------------------------------------------
    public void TakeDamage(float damage)
    {
        if (!CoolDown && !inSpecial)
        {
            // Deducts health by the amount of damage taken
            currentHealth -= damage;
            ResetCoolDown();
            //Flash();
        }

        // If players health is zero or less and they aren't dead yet, then call Death function
        if (currentHealth <= 0)
            Death();
    }

	//------------------------------------------------------------
	// Function limits actions for player when called
	//------------------------------------------------------------
    private void Death()
    {
        // Dead bool is set to true
        dead = true;
		animator.SetBool("Dead", true);

		body.constraints = RigidbodyConstraints.FreezeAll;

        helpMe.SetActive(true);
        // Gets Sweeper and Striker's Special components
        SweeperSpecial specialSweeper = GetComponent<SweeperSpecial>();
        StrikerSpecial specialStriker = GetComponent<StrikerSpecial>();

        // Gets Player Move and Player Attack components
        PlayerMove PlayMove = GetComponent<PlayerMove>();

        PlayerAttack playAttack = GetComponent<PlayerAttack>();

        // If there is a Striker Special then disable the component
        if (specialStriker)
            specialStriker.enabled = false;

        // If there is a Sweeper Special then disable the component
        if (specialSweeper)
            specialSweeper.enabled = false;

        // If there is a Player Move then disable the component
        if (PlayMove)
            PlayMove.enabled = false;

        // If there is a Player Attack then disable the component
        if (playAttack)
            playAttack.enabled = false;
    }

	//------------------------------------------------------------
	// Function allows player to flash red when on low health
	//------------------------------------------------------------
    private void Flash()
    {
        // Ignores following code if the flash time is less than zero
        if (!isFlashing)
            return;

		if(!CoolDown)
        {
            // Gets renderer component and stores it into rend
            rend = GetComponentInChildren<Renderer>();

            // Sets the rend colour to be whatever the FlashColour is set to
            rend.material.color = FlashColour;

			animator.SetBool("Damage", true);

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
				animator.SetBool("Damage", false);
                CoolDown = false;
                isFlashing = false;
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
  //  private void OnCollisionEnter(Collision other)
  //  {
		//// If object collides with an enemy, then call Flash function and ResetCoolDown
  //      if (other.gameObject.tag == "Enemy")
  //      {
            
  //          Flash();
  //      }
  //  }

    //------------------------------------------------------------
    // Function sets ActiveTime to equal CoolDownTimer float
    //------------------------------------------------------------
    private void ResetCoolDown()
    {
        FlashTime = CoolDownTimer;
        isFlashing = true;
    }
}
