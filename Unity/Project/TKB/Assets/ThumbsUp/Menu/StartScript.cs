//-----------------------------------------------------------------------------
// Author: Matthew Le Nepveu.
//-----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using XboxCtrlrInput;
using UnityEngine.EventSystems;

public class StartScript : MonoBehaviour
{
    // Refers to the Main Level scene number
    public int levelScene = 1;

    // Int refers to the Credits scene number
    public int creditsScene = 2;

    // Refers to the Event System this script will use
    public EventSystem eventSystem;

    //--------------------------------------------------------------------------------
    // Function loads the Main Level scene when called.
    //--------------------------------------------------------------------------------
    public void StartOnButtonClicked()
    {
        SceneManager.LoadScene(levelScene);
    }

    //--------------------------------------------------------------------------------
    // Loads the Credits scene when function is called.
    //--------------------------------------------------------------------------------
    public void CreditOnButtonClicked()
    {
        SceneManager.LoadScene(creditsScene);
    }

    //--------------------------------------------------------------------------------
    // Quits the game when the function is called.
    //--------------------------------------------------------------------------------
    public void QuitOnButtonClicked()
	{
		Application.Quit();
	}
}
