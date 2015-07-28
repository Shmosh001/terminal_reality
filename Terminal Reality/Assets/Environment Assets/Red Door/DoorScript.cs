using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	private Animator anim;
	public bool open = false;

	// Use this for initialization
	void Start () {
	
		anim = this.GetComponent<Animator> ();

	}

	public void interaction()
	{
		//IF THE DOOR IS OPEN//
		if (open)
		{
			anim.SetBool("open", false);
			open = false;
		}
		//IF THE DOOR IS CLOSED//
		else
		{
			anim.SetBool("open", true);
			open = true;
		}
	}
}
