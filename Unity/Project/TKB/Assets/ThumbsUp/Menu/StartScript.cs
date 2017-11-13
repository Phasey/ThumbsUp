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



	public void StartButtonClicked()
    {
        SceneManager.LoadScene(sceneNumber);
    }

	public void QuitButtonClicked()
	{
		Application.Quit();
	}
}
