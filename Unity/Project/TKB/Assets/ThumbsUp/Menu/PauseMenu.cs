using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XboxCtrlrInput;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;

    private XboxController Controller;

    public Button resumeButton;


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
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    resumeButton.Select();
        //    Debug.Log("hi");
        //}

        // Bool checks if the start button has been pressed
        bool start = XCI.GetButtonDown(XboxButton.Start, Controller);

        if (start && !pauseCanvas.activeInHierarchy)
        {
            Paused();
            resumeButton.Select();
        }

        else if (start && pauseCanvas.activeInHierarchy)
            Resume();
	}

    public void Resume()
    {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
    }

    public void Paused()
    {
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);


        //EventSystem EVRef = EventSystem.current; // get the current event system
        //EVRef.SetSelectedGameObject(resumeButton.gameObject);   // set current selected button
        //                                                //    Hack: Move up to the button above us (so we can hack a highlight on it)
        //Button bref = EVRef.currentSelectedGameObject.GetComponent<Button>();
        //EVRef.SetSelectedGameObject(bref.navigation.selectOnUp.gameObject, null);
    }
}
