using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpriteDistScript : MonoBehaviour {

    private PlayerMove[] players;
    public GameObject CubeSprite;
    public float BOxVision = 5f;

    // Use this for initialization
    void Start ()
    {
        players = FindObjectsOfType<PlayerMove>();
    }
	
	// Update is called once per frame
	void Update ()
    {

        float Dist = Vector3.Distance(players[0].transform.position, transform.position);
        float Dist2 = Vector3.Distance(players[1].transform.position, transform.position);

        if (Dist > BOxVision && Dist2 > BOxVision)
        {
            CubeSprite.SetActive(false);
        }
        else if (Dist < BOxVision && Dist < Dist2)
        {
            CubeSprite.SetActive(true);
        }
        else if(Dist2 < BOxVision)
        {
            CubeSprite.SetActive(true);
        }
      
    }
}
