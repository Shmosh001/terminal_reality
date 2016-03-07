using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerDataScript : Photon.MonoBehaviour {
	
	//PLAYER HEALTH//
	public int health = 100;
	public bool playerAlive = true;
	
	//PUBLIC MOVEMENT VARIABLES//
	public float movementSpeed = 6.5f;
	public float mouseSpeed = 3.0f;
	public float jumpSpeed = 5.0f;
	public float pushingPower = 2.5f;
	
	//PLAYER MOVEMENT STATES//
	public bool walking = true;
	public bool sprinting = false;
	public bool sneaking = false;
    public bool canHear = true;
	
	//PUBLIC WEAPON RELATED VARIABLES//	
	public bool pistolPickedUp = false;
	public bool machineGunPickedUp = false;
	public bool pistolEquipped = false;
	public bool machineGunEquipped = false;
	
	//WEAPONS GAME OBJECTS//
	public GameObject pistolGameObject;
	public GameObject machineGunGameObject;

	public bool inWaitingRoom = false;
	public bool inKeyRoom = false;
	
	//COUNTERS//
	public int kills = 0;

	
	//TORCH//
	public Light torch;


    //PLAYER HAS KEY
    public bool hasKey = false;



}