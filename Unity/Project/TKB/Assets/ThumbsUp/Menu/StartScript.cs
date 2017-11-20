﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using XboxCtrlrInput;
using UnityEngine.EventSystems;

public class StartScript : MonoBehaviour
{
    public int mainMenu = 0;
    public int levelScene = 1;
    public int creditsScene = 2;

    private XboxController Controller;

    public EventSystem eventSystem;

    void Awake()
    {
        SceneManager.LoadScene(mainMenu);
    }

	public void Start()
    {
        SceneManager.LoadScene(levelScene);
    }

    public void Credits()
    {
        SceneManager.LoadScene(creditsScene);
    }

	public void Quit()
	{
		Application.Quit();
	}
}
