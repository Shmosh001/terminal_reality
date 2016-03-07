﻿using UnityEngine;
using System.Collections;

public class keyRoom : MonoBehaviour {

	public playerDataScript playerData;
	public GameObject soundController;

	int enemyInRoom = 0;

	// Use this for initialization
	void Start () {
		playerData = GameObject.FindGameObjectWithTag(Tags.PLAYER1).GetComponent<playerDataScript>();
		soundController = GameObject.FindGameObjectWithTag(Tags.SOUNDCONTROLLER);
	}

	void OnTriggerEnter(Collider other)
	{
		playerData.inKeyRoom = true;
	}
	
	void OnTriggerExit(Collider other)
	{
		playerData.inKeyRoom = false;
	}

	// Update is called once per frame
	void Update () 
	{
	
	}
}
