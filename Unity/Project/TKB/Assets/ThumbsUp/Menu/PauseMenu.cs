using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using XboxCtrlrInput;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;

    private XboxController Controller;

    public GameObject resumeButton;
    public EventSystem eventSystem;

    //------------------------------------------------------------
    // Function is called when script first runs
    //------------------------------------------------------------
    void Awake()
    {
        Resume();
    }

    //------------------------------------------------------------
    // Function is called once every frame
    //------------------------------------------------------------
    void Update()
    {
        // Bool checks if the start button has been pressed
        bool start = XCI.GetButtonDown(XboxButton.Start, Controller);
        bool back = XCI.GetButtonDown(XboxButton.Back, Controller);

        if (start && !pauseCanvas.activeInHierarchy)
            Paused();

        else if (start && pauseCanvas.activeInHierarchy)
            Resume();

        else if (back && pauseCanvas.activeInHierarchy)
            Quit();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        eventSystem.SetSelectedGameObject(null);
        pauseCanvas.SetActive(false);
    }

    public void Paused()
    {
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);
        eventSystem.SetSelectedGameObject(resumeButton);
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
}
