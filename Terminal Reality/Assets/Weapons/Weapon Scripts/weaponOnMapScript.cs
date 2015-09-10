using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class weaponOnMapScript : MonoBehaviour {

	private Text pushE;

	// Use this for initialization
	void Start () {

		pushE = GameObject.FindGameObjectWithTag("PushE").GetComponent<Text>();
	
	}

	//WALKING INTO THE TRIGGER SURROUNDING WEAPON//
	void OnTriggerEnter(Collider other)
	{
		//If player walks into trigger...
		if (other.tag == "Player")
		{
			pushE.enabled = true;

		}
	}

	//WALKING OUT OF THE TRIGGER SURROUNDING WEAPON//
	void OnTriggerExit(Collider other)
	{
		//If player walks into trigger...
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
