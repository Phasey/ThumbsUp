using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum GameStates
{
    MAINMENU,
    CHARACTERSELECT,
    GAMEPLAY,
    PAUSE,
    DEATH,
    COMPLETION
}

public class StartScript : MonoBehaviour
{
    GameStates gs;
    
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
        SceneManager.LoadScene(1);
    }
}
