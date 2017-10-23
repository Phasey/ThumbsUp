using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerSelect : MonoBehaviour
{

	// Use this for initialization
	void Awake()
    {
		
	}
	
	// Update is called once per frame
	void Update()
    {
		
	}

    public void Player1Sweeper()
    {
        SpawnManagerScript.Player1 = CharacterType.Sweeper;
    }

    public void Player1striker()
    {
        SpawnManagerScript.Player1 = CharacterType.Striker;
    }

    public void Player2Sweeper()
    {
        SpawnManagerScript.Player1 = CharacterType.Sweeper;
    }

    public void Player2striker()
    {
        SpawnManagerScript.Player1 = CharacterType.Striker;
    }

}
