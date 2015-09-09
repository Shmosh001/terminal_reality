using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBoxScript : MonoBehaviour {

	private Text pushE;

	// Use this for initialization
	void Start () {
	
		pushE = GameObject.FindGameObjectWithTag("PushE").GetComponent<Text>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//WHEN SOMETHING ENTERS THE DOORS TRIGGER//
	void OnTriggerEnter (Collider other)
	{
		//IF A PLAYER ENTERS THE DOOR'S TRIGGER//
		if (other.tag == "Player")
		{
			// only show if the player does not have full health
			if (other.GetComponent<playerDataScript>().health < 100)
			{
				pushE.enabled = true;
			}
			
		}
	}
	
	//WHEN SOMETHING LEAVES THE DORR'S TRIGGER//
	void OnTriggerExit (Collider other)
	{
		//IF A PLAYER LEAVES THE DOOR'S TRIGGER//
		if (other.tag == "Player")
		{
			pushE.enabled = false;
		}
	}

	//METHOD TO TURN OFF TEXT JUST BEFORE OBJECT IS DESTROYED
	public void turnOffText()
	{
		pushE.enabled = false;
	}
}
