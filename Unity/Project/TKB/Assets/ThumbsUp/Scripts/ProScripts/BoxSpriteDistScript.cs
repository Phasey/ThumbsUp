//---------------------------------------------------------------
//Author: Liam Knights.
//---------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoxSpriteDistScript : MonoBehaviour
{
    //createss a players obejects
    private PlayerMove[] players;

    //game object for the cube sprite
    public GameObject CubeSprite;

    //vision box can see players
    public float BoxVision = 5f;

    // Use this for initialization
    void Start ()
    {
        //finds the player pbjects in playermove
        players = FindObjectsOfType<PlayerMove>();
    }
	
	// Update is called once per frame
	void Update ()
    {

        //gets the distance to the player
        float Dist = Vector3.Distance(players[0].transform.position, transform.position);
        //gets the distance to the player
        float Dist2 = Vector3.Distance(players[1].transform.position, transform.position);
        
        //checks the distance between the playres and the box
        if (Dist > BoxVision && Dist2 > BoxVision)
        {
            //set the cube sprite to false while the players are far enough away
            CubeSprite.SetActive(false);
        }
        //checks the distance between the playres and the box
        else if (Dist < BoxVision && Dist < Dist2)
        {
            //set the cube sprite to true when the player gets close enough
            CubeSprite.SetActive(true);
        }
        //checks the distance between the playres and the box
        else if (Dist2 < BoxVision)
        {
            //set the cube sprite to true when the player gets close enough
            CubeSprite.SetActive(true);
        }
      
    }
}
