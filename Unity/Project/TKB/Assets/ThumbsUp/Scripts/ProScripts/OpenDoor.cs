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
	public Animator animator;

	// Use this for initialization
	void Awake()
    {
    }
	
	// Update is called once per frame
	void Update()
    {
        bool triggersDown = IsTriggered();

		if (triggersDown) {
			animator.SetBool ("Open", true);
			StartCoroutine (Open ());
		}
        
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
	IEnumerator Open()
	{
		yield return new WaitForSeconds(4f);
		closedDoor.SetActive(false);

	}
}
