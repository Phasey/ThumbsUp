using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Credits : MonoBehaviour {

    public float speed = 2f;
    public GameObject credits;
    public float Timer = 10f;
    float actualTime = 0f;
    public int sceneNumber;
    

	// Use this for initialization
	void Awake()
    {
        
        

	}
	
	// Update is called once per frame
	void Update()
    {
        //credits.GetComponent<RectTransform>().position += Vector2.up * Time.deltaTime * speed;
        credits.GetComponent<RectTransform>().anchoredPosition += Vector2.up * Time.deltaTime * speed;
        //credits.transform.position = Vector3.up * Time.deltaTime * speed;
        actualTime += Time.deltaTime;

        if(actualTime >= Timer)
        {
            SceneManager.LoadScene(sceneNumber);
        }

	}
}
