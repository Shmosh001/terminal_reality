﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AmmoBoxScript : MonoBehaviour {

	
	//PUBLIC AMMO BOX VARIABLES//
	public int pistolAmmo;
	public int machineGunAmmo;
	
	private Text pushE;
	//private bool playerInRange;

    private GameObject pushETextObj;

    // Use this for initialization
    void Start () {
        pushETextObj = GameObject.FindGameObjectWithTag("PushE");
        if (pushETextObj != null) {
            pushE = pushETextObj.GetComponent<Text>();
        }
		
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (pushETextObj == null) {
            pushETextObj = GameObject.FindGameObjectWithTag("PushE");
            if (pushETextObj != null) {
                pushE = pushETextObj.GetComponent<Text>();
            }
        }
	}
	
	//WHEN SOMETHING ENTERS THE DOORS TRIGGER//
	void OnTriggerEnter (Collider other)
	{
		//IF A PLAYER ENTERS THE DOOR'S TRIGGER//
		if (other.tag == "Player")
		{
			// only show if player has a gun picked up
			if (other.GetComponent<playerDataScript>().pistolPickedUp || other.GetComponent<playerDataScript>().machineGunPickedUp)
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
        if (pushE != null) {
            pushE.enabled = false;
        }
	}
	

    [PunRPC]
    public void destroyObject() {
        turnOffText();
        Destroy(gameObject);
    }
	
}
