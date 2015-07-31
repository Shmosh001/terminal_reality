using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AmmoBoxScript : MonoBehaviour {

	
	//PUBLIC AMMO BOX VARIABLES//
	public int pistolAmmo;
	
	private Text pushE;
	private bool playerInRange;

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
			playerInRange = true;
			pushE.enabled = true;
			
		}
	}
	
	//WHEN SOMETHING LEAVES THE DORR'S TRIGGER//
	void OnTriggerExit (Collider other)
	{
		//IF A PLAYER LEAVES THE DOOR'S TRIGGER//
		if (other.tag == "Player")
		{
			playerInRange = false;
			pushE.enabled = false;
		}
	}
	
	
}
