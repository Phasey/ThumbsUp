using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using XboxCtrlrInput;
using UnityEngine.EventSystems;

public class StartScript : MonoBehaviour
{
    public int sceneNumber = 1;

    private XboxController Controller;

    public EventSystem eventSystem;


    // Use this for initialization
    void Awake()
    {
        
	}
	
	// Update is called once per frame
	void Update()
    {
    
    }

    public void StartButtonClicked()
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
