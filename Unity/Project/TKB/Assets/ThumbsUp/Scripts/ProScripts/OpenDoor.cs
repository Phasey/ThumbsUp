using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------------
// Author: Matthew Le Nepveu.
//--------------------------------------------------------------------------------

public class OpenDoor : MonoBehaviour
{
    public List<GameObject> triggers;
    public GameObject closedDoor;

	// Use this for initialization
	void Awake()
    {
    }
	
	// Update is called once per frame
	void Update()
    {
        bool triggersDown = IsTriggered();

        closedDoor.SetActive(!triggersDown);
    }

    private bool IsTriggered()
    {
        for (int i = 0; i < triggers.Count; ++i)
        {
            if (!triggers[i].GetComponent<PressurePlateScript>().triggered)
                return false;
        }

        return true;
    }
}
