using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour {

    public GameObject PLayer1;
    public GameObject PLayer2;


    // Use this for initialization
    void Awake()
    {
        
    }
	
	// Update is called once per frame
	void Update()
    {
        HealthScript P1Health = PLayer1.GetComponent<HealthScript>();
        HealthScript P2Health = PLayer2.GetComponent<HealthScript>();

        if (P1Health.dead && P2Health.dead)
        {
            SceneManager.LoadScene("Menu");
        }
	}
}
