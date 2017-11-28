//-----------------------------------------------------------------------------
// Author: Matthew Le Nepveu. Edited by: Liam Knights.
//-----------------------------------------------------------------------------

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
    public Renderer rend;
    public Color FlashColour;

	// Creates booleans to check if players are being damaged or are alive
    public bool dead;

    // Sets the cooldown timer for use in attacking
    public float CoolDownTimer = 0.5f;

    // Initialises CoolDown boolean to be false
    private bool CoolDown = false;

	// Bool checks if player is flashing
    private bool isFlashing = false;

    // Sets FlashTime variable to be private 
    private float FlashTime = 0.0f;

    // Gets access to the health bar so health bar value can decrease according to health
    public Slider healthBar;

	// Used so Death Animations can be placed on player's
	public Animator animator;

	// Used to allow Rigidbody's to be put on player's
	public Rigidbody body;

    // Used to place a "!" above a dead player
    public GameObject helpMe;

    // Bool is used to specify if a player is using their special or not
    public bool inSpecial;

    //-----------------------------------------------------------------------------
    // Function is called when script first runs
    //-----------------------------------------------------------------------------
    void Awake()
    {
		// Sets current health to equal the max health upon startup
        currentHealth = maxHealth;

        // Assigns the max health to the health bar
        healthBar.maxValue = maxHealth;

        // Yields a result from Damage function at the same time as Damage
        StartCoroutine(Damage());
    }

    //-----------------------------------------------------------------------------
    // Function is called once every frame
    //-----------------------------------------------------------------------------
    void Update()
    {
        // Calls the flash function every frame
        Flash();

        // Makes the value of the health bar equal the player's health
        healthBar.value = currentHealth;
    }

    //-----------------------------------------------------------------------------
    // Function allows players to take damage
    //
    // Param:
    // 		damage: Refers to how much damage the players take
    //-----------------------------------------------------------------------------
    public void TakeDamage(float damage)
    {
        // Code below runs if not in Cool Down and Special not in use
        if (!CoolDown && !inSpecial)
        {
            // Deducts health by the amount of damage taken
            currentHealth -= damage;

            // Sets damage bool in animator to be true
            animator.SetBool("Damage", true);

            // Yields a result from Damage function at the same time as Damage
            StartCoroutine(Damage());

            // Calls Reset Cool Down function to reset variables
            ResetCoolDown();
            
            // Calls Flash function to execute flashing on enemies
            Flash();
        } 

        // If players health is zero or less and they aren't dead yet, then call Death function
        if (currentHealth <= 0)
            Death();
    }

    //-----------------------------------------------------------------------------
    // Function limits actions for player when called
    //-----------------------------------------------------------------------------
    private void Death()
    {
        // Dead bool is set to true
        dead = true;

        // Animator Dead bool is set to true
		animator.SetBool("Dead", true);

        // Freezes all body constraints on dead player
		body.constraints = RigidbodyConstraints.FreezeAll;

        // Adds the "!" above the dead player
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

    //-----------------------------------------------------------------------------
    // Function allows player to flash red when on low health
    //-----------------------------------------------------------------------------
    private void Flash()
    {
        // Ignores following code if the flash time is less than zero
        if (!isFlashing)
            return;

        // Code in braces runs if cool down is not active
		if (!CoolDown)
        {
            // Sets a Shader Keyword to change the colour
            rend.material.EnableKeyword("_EMISSION");
            
            // Sets the colour to be the Flash Colour
            rend.material.SetColor("_EmissionColor", FlashColour);

            // Sets Cool Down variable to be true
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
                // Disables a Shader Keyword so it can be reused again
                rend.material.DisableKeyword("_EMISSION");

                // Sets Cool Down and isFlashing to be false
                CoolDown = false;
                isFlashing = false;
            }
        }
    }

    //-----------------------------------------------------------------------------
    // Function waits 0.5 seconds until it sets damage bool to false.
    //-----------------------------------------------------------------------------
    IEnumerator Damage()
    {
        yield return new WaitForSeconds(.5f);
        animator.SetBool("Damage", false);
    }

    //-----------------------------------------------------------------------------
    // Function resets Flash Time and isFlashing variables.
    //-----------------------------------------------------------------------------
    private void ResetCoolDown()
    {
        FlashTime = CoolDownTimer;
        isFlashing = true;
    }
}
