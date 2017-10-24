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
        SpawnManagerScript.Player1 = CharacterType.SWEEPER;
    }

    public void Player1striker()
    {
        SpawnManagerScript.Player1 = CharacterType.STRIKER;
    }

    public void Player2Sweeper()
    {
        SpawnManagerScript.Player1 = CharacterType.SWEEPER;
    }

    public void Player2striker()
    {
        SpawnManagerScript.Player1 = CharacterType.STRIKER;
    }

}
