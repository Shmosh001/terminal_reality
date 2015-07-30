﻿using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	private Animator anim;
	public bool open = false;

	// Use this for initialization
	void Start () {
	
		anim = this.GetComponent<Animator> ();

	}

	//WHEN THE PLAYER INTERACTS WITH THE DOOR//
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

	//WHEN SOMETHING ENTERS THE DOORS TRIGGER//
	void OnTriggerEnter (Collider other)
	{
		//IF A PLAYER ENTERS THE DOOR'S TRIGGER//
		if (other.tag == "Player")
		{
			Debug.Log("PUSH E TO OPEN THE DOOR!");
		}
	}
}
