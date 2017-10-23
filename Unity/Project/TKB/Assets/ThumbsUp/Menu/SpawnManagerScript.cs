﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CharacterType
{
    Sweeper,
    Striker
}

public class SpawnManagerScript : MonoBehaviour
{
    public static CharacterType Player1;
    public static CharacterType Player2;

    public GameObject SpawnPoint1;
    public GameObject SpawnPoint2;

    public GameObject Sweeper;
    public GameObject Striker;

    // Use this for initialization
    void Awake()
    {
        if (Player1 == CharacterType.Sweeper)
        {

            GameObject p1 = Instantiate(Sweeper, SpawnPoint1.transform.position, SpawnPoint1.transform.rotation);
            //Get PlayerMove component and set Controller to first
        }
        else
        {
            Instantiate(Striker, SpawnPoint1.transform.position, SpawnPoint1.transform.rotation);

        }

        if (Player2 == CharacterType.Sweeper)
        {

            Instantiate(Sweeper, SpawnPoint2.transform.position, SpawnPoint2.transform.rotation);
            //Get PlayerMove component and set Controller to second
        }
        else
        {
            Instantiate(Striker, SpawnPoint2.transform.position, SpawnPoint2.transform.rotation);

        }
    }
	
	// Update is called once per frame
	void Update()
    {

		
	}

}
