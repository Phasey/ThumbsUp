using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CharacterType
{
    SWEEPER,
    SWEEPERALT,
    STRIKER,
    STRIKERALT
}

public class SpawnManagerScript : MonoBehaviour
{
    public static CharacterType Player1;
    public static CharacterType Player2;

    public GameObject SpawnPoint1;
    public GameObject SpawnPoint2;

    public GameObject SweeperR;
    public GameObject StrikerB;
    public GameObject SweeperB;
    public GameObject StrikerR;

    // Use this for initialization
    void Awake()
    {
        if(Player1 == CharacterType.SWEEPER)
        {

            GameObject p1 = Instantiate(SweeperR, SpawnPoint1.transform.position, SpawnPoint1.transform.rotation);
            //Get PlayerMove component and set Controller to first
        }
        if(Player1 == CharacterType.STRIKERALT)
        {
            GameObject p1 = Instantiate(StrikerR, SpawnPoint1.transform.position, SpawnPoint1.transform.rotation);

        }

        if(Player2 == CharacterType.SWEEPERALT)
        {

            GameObject p2 = Instantiate(SweeperB, SpawnPoint2.transform.position, SpawnPoint2.transform.rotation);
            //Get PlayerMove component and set Controller to second
        }
        if(Player2 == CharacterType.STRIKER)
        {
            GameObject p2 = Instantiate(StrikerB, SpawnPoint2.transform.position, SpawnPoint2.transform.rotation);

        }
    }
	
	// Update is called once per frame
	void Update()
    {

		
	}
}
