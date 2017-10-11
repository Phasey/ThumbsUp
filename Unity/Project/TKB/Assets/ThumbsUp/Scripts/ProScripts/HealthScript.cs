using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public GameObject player;
    public Renderer rend;
    public Color FlashColour;

    bool dead;
    bool damaged;

	// Use this for initialization
	void Awake()
    {
        currentHealth = maxHealth;
        
	}
	
	// Update is called once per frame
	void Update()
    {
        if (dead)
            Destroy(player);
    }

    public void TakeDamage(int damage)
    {
        damaged = true;
        currentHealth -= damage;

        if (currentHealth <= 0 && !dead)
            Death();
    }

    private void Death()
    {
        dead = true;
    }

    private void Flash()
    {
        rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("lambert1");
        rend.material.color = FlashColour;

        //After timer, renderer sets colour back
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Flash();
        }
    }
}
