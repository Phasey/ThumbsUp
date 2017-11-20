using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using XboxCtrlrInput;

public class PauseMenu : MonoBehaviour
{
    public int mainMenuScene = 0;
    public bool paused;

    private XboxController Controller;

    public GameObject resumeButton;
    public EventSystem eventSystem;

    //------------------------------------------------------------
    // Function is called when script first runs.
    //
    // Author: Matthew Le Nepveu.
    //------------------------------------------------------------
    void Awake()
    {
        Resume();
        paused = false;
    }

    //------------------------------------------------------------
    // Function is called once every frame
    //------------------------------------------------------------
    void Update()
    {
        // Bool checks if the start button has been pressed
        bool start = XCI.GetButtonDown(XboxButton.Start, Controller);
        bool back = XCI.GetButtonDown(XboxButton.Back, Controller);

        if (start && !gameObject.activeInHierarchy)
            Paused();

        else if (start && gameObject.activeInHierarchy)
            Resume();

        else if (back && gameObject.activeInHierarchy)
            Quit();
    }

    public void StartBtn()
    {
        if (!gameObject.activeInHierarchy)
            Paused();

        else if (gameObject.activeInHierarchy)
            Resume();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        eventSystem.SetSelectedGameObject(null);
        paused = false;
    }

    public void Paused()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
        eventSystem.SetSelectedGameObject(resumeButton);
        paused = true;
    }

    public void Quit()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}
