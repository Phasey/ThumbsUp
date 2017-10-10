using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweeperHealthScript : MonoBehaviour
{
    public const int maxSweeperHealth = 100;

    public int currentSweeperHealth;
    public GameObject sweeperPlayer;

    bool dead;
    bool damaged;

    // Use this for initialization
    void Awake()
    {
        currentSweeperHealth = maxSweeperHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
            Destroy(sweeperPlayer);
    }

    public void TakeDamage(int damage)
    {
        damaged = true;
        currentSweeperHealth -= damage;

        if (currentSweeperHealth <= 0 && !dead)
            Death();
    }

    private void Death()
    {
        dead = true;
    }
}
