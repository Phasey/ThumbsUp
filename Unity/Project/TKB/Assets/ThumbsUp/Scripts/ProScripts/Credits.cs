//---------------------------------------------------------------
//Author: Liam Knights
//---------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Credits : MonoBehaviour {

    //sets the speed of the credits
    public float speed = 2f;
    //gets the credits object
    public GameObject credits;

    //time set untill the credits are change scene
    public float Timer = 10f;
    //the timer
    float actualTime = 0f;
    //scene number chosen
    public int sceneNumber;
    

	// Use this for initialization
	void Awake()
    {
        
        

	}
	
	// Update is called once per frame
	void Update()
    {
        //moves the object upwards on the screen

        //credits.GetComponent<RectTransform>().position += Vector2.up * Time.deltaTime * speed;
        credits.GetComponent<RectTransform>().anchoredPosition += Vector2.up * Time.deltaTime * speed;
        //credits.transform.position = Vector3.up * Time.deltaTime * speed;

        //sets the timer to delta time
        actualTime += Time.deltaTime;

        //checks if moving timer(actualtimer) is greater then the time set(timer)
        if(actualTime >= Timer)
        {
            //loads the scene chosen once the credits are done
            SceneManager.LoadScene(sceneNumber);
        }

	}
}
