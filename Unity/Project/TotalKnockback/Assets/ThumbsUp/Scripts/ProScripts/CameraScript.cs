using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player1;
    public Transform player2;

    private float prevPlayer1x;
    private float prevPlayer1z;
    private float prevPlayer2x;
    private float prevPlayer2z;

    private Vector3 centre;
    private Vector3 offset;
    private Vector3 player1Pos;
    private Vector3 player2Pos;

    // Use this for initialization
    void Start()
    {
        prevPlayer1x = 0f;
        prevPlayer1z = 0f;
        prevPlayer2x = 0f;
        prevPlayer2z = 0f;

        centre = ((player2.position + player1.position) * 0.5f);
        offset = transform.position - centre;

        player1Pos = new Vector3(player1.position.x, player1.position.y, player1.position.z);
        player2Pos = new Vector3(player2.position.x, player2.position.y, player2.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        centre = ((player2.position + player1.position) * 0.5f);
        transform.position = centre + offset;

        if (player1.position.x - player2.position.x >= 15f || 
            player1.position.x - player2.position.x <= -15f)
        {
            player1Pos.x = prevPlayer1x;
            player2Pos.x = prevPlayer2x;
        }
        else
        {
            prevPlayer1x = player1Pos.x;
            prevPlayer2x = player2Pos.x;
        }

        if (player1.position.z - player2.position.z >= 15f ||
            player1.position.z - player2.position.z <= -15f)
        {
            player1Pos.z = prevPlayer1z;
            player2Pos.z = prevPlayer2z;
        }
        else
        {
            prevPlayer1z = player1Pos.z;
            prevPlayer2z = player2Pos.z;
        }

    }
}
