using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject closedDoor;
    public GameObject openDoor;

    public int sceneNumber;

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

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            SceneManager.LoadScene(sceneNumber);
    }
}
