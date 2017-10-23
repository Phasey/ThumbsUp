using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum CharacterType
{
    Sweeper,
    Striker
}

public class SpawnManagerScript : MonoBehaviour
{
    static CharacterType Sweeper;
    static CharacterType Striker;

    public GameObject SpawnPoint1;
    public GameObject SpawnPoint2;

    public GameObject player1;
    public GameObject player2;

    // Use this for initialization
    void Awake()
    {


       Instantiate(player1, SpawnPoint1.transform.position, SpawnPoint1.transform.rotation);
       Instantiate(player2, SpawnPoint2.transform.position, SpawnPoint2.transform.rotation);

    }
	
	// Update is called once per frame
	void Update()
    {

		
	}
}
