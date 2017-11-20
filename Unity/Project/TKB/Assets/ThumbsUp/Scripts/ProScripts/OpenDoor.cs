using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public List<GameObject> triggers;
    public GameObject closedDoor;

    private int switchTotal;

	// Use this for initialization
	void Awake()
    {
        switchTotal = 0;
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
