using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using XboxCtrlrInput;
using UnityEngine.EventSystems;

public class StartScript : MonoBehaviour
{
    public int levelScene = 1;
    public int creditsScene = 2;

    private XboxController Controller;

    public EventSystem eventSystem;

	public void StartOnButtonClicked()
    {
        SceneManager.LoadScene(levelScene);
    }

    public void CreditOnButtonClicked()
    {
        SceneManager.LoadScene(creditsScene);
    }

	public void QuitOnButtonClicked()
	{
		Application.Quit();
	}
}
