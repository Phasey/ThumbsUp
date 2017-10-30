using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemy;
    public int totalEnemies;
    public float spawnDelay;

    private float spawnTimer = 0f;
    private int enemiesSpawned = 0;

    private bool isEnabled = false;

	// Use this for initialization
	void Awake()
    {
		
	}
	
	// Update is called once per frame
	void Update()
    {
        if (!isEnabled)
            return;

        if (enemiesSpawned >= totalEnemies)
            return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnDelay)
        {
            Collider[] clearSpace = Physics.OverlapSphere(transform.position, 1);

            for (int i = 0; i < clearSpace.Length; ++i)
            {
                if (clearSpace[i].tag == "Player" || clearSpace[i].tag == "Enemy")
                    return;
            }

            Instantiate(enemy, transform.position, transform.rotation);
            ++enemiesSpawned;
            spawnTimer = 0f;
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            isEnabled = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            isEnabled = false;
    }
}
