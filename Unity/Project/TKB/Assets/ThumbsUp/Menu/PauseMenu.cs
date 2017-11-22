using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using XboxCtrlrInput;

//-----------------------------------------------------------------------------
// Author: Matthew Le Nepveu.
//-----------------------------------------------------------------------------

public class PauseMenu : MonoBehaviour
{
    // Refers to the main menu's scene number
    public int mainMenuScene = 0;

    // Bool determines whether the game is in a paused state or not
    public bool paused;

    // Refers to the resume button as a Game Object
    public GameObject resumeButton;

    // Refers to the Event System that the Pause Canvas will use
    public EventSystem eventSystem;

    // Gets the Xbox Controller, used in input functions
    private XboxController Controller;

    //--------------------------------------------------------------------------------
    // Function is called when script first runs.
    //--------------------------------------------------------------------------------
    void Awake()
    {
        // Calls Resume function immediately and states paused is false
        Resume();
        paused = false;
    }

    //--------------------------------------------------------------------------------
    // Function is called once every frame.
    //--------------------------------------------------------------------------------
    void Update()
    {
        // Bools check if either the start or back button have been pressed
        bool start = XCI.GetButtonDown(XboxButton.Start, Controller);
        bool back = XCI.GetButtonDown(XboxButton.Back, Controller);

        // If start button has been pressed and game is not paused, run Pause function
        if (start && !gameObject.activeInHierarchy)
            Paused();

        // If start button has been pressed and game is paused, run Resume function
        else if (start && gameObject.activeInHierarchy)
            Resume();

        // If back button has been pressed and game is paused, run Quit function
        else if (back && gameObject.activeInHierarchy)
            Quit();
    }

    //--------------------------------------------------------------------------------
    // Function determines whether the game is currently paused.
    //--------------------------------------------------------------------------------
    public void StartBtn()
    {
        // Calls Paused function if game is not paused already
        if (!gameObject.activeInHierarchy)
            Paused();

        // Otherwise call Resume function if game isn't paused
        else if (gameObject.activeInHierarchy)
            Resume();
    }

    //--------------------------------------------------------------------------------
    // Resumes the game when function is called.
    //--------------------------------------------------------------------------------
    public void Resume()
    {
        // Enables deltaTime and other time variables to run as normal
        Time.timeScale = 1;

        // Sets the Pause canvas to be false
        gameObject.SetActive(false);

        // Does not select a button in pause menu
        eventSystem.SetSelectedGameObject(null);

        // Tells other scripts the game is not paused
        paused = false;
    }

    //--------------------------------------------------------------------------------
    // Pauses the game when function is called.
    //--------------------------------------------------------------------------------
    public void Paused()
    {
        // Disables time variables to stop the gameplay
        Time.timeScale = 0;

        // Sets the Pause canvas to run
        gameObject.SetActive(true);

        // Selects the Resume button to be selected immediately after pausing
        eventSystem.SetSelectedGameObject(resumeButton);

        // Tells other scripts the game is paused
        paused = true;
    }

    //--------------------------------------------------------------------------------
    // Sends the player back to the Main Menu scene when function is called.
    //--------------------------------------------------------------------------------
    public void Quit()
    {
        // Loads the Main Menu scene
        SceneManager.LoadScene(mainMenuScene);
    }
}
