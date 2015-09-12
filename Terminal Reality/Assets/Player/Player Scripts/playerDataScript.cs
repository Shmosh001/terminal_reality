﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerDataScript : MonoBehaviour {

	//PLAYER HEALTH//
	public int health = 100;
	public bool playerAlive = true;

	//PUBLIC MOVEMENT VARIABLES//
	public float movementSpeed = 6.5f;
	public float mouseSpeed = 3.0f;
	public float jumpSpeed = 5.5f;
	public float pushingPower = 2.5f;

	//PLAYER MOVEMENT STATES//
	public bool walking = true;
	public bool sprinting = false;
	public bool sneaking = false;
	
	//PUBLIC WEAPON RELATED VARIABLES//	
	public bool pistolPickedUp = false;
	public bool machineGunPickedUp = false;
	public bool pistolEquipped = false;
	public bool machineGunEquipped = false;
	
	//WEAPONS GAME OBJECTS//
	public GameObject pistolGameObject;
	public GameObject machineGunGameObject;
	
	//TORCH//
	public Light torch;


	//AUTOMATICALLY SEARCH FOR CERTAIN GAME OBJECTS AT START/AWAKE//
	void Awake()
	{
		pistolGameObject = GameObject.FindGameObjectWithTag("Pistol");
		machineGunGameObject = GameObject.FindGameObjectWithTag("MachineGun");
		torch = GameObject.FindGameObjectWithTag("torch").light;
	}

	/*
	public void receiveNetworkData (PhotonStream stream, PhotonMessageInfo info)
	{
		pistolEquipped = (bool)stream.ReceiveNext();
		machineGunEquipped = (bool)stream.ReceiveNext();
	}

	public void sendNetworkData (PhotonStream stream, PhotonMessageInfo info)
	{
		stream.SendNext(pistolEquipped);
		stream.SendNext(machineGunEquipped);
	}
	*/

}
