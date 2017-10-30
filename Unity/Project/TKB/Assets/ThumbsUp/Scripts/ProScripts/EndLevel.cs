using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject closedDoor;
    public GameObject openDoor;

	// Use this for initialization
	void Awake()
    {
        
	}
	
	// Update is called once per frame
	void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length <= 0)
        {
            Instantiate(openDoor, closedDoor.transform.position, closedDoor.transform.rotation);
            Destroy(closedDoor);
        }
	}
}
