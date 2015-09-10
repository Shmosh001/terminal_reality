using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerDataScript : MonoBehaviour {

	//PLAYER HEALTH//
	public int health;

	//PUBLIC MOVEMENT VARIABLES//
	public float movementSpeed = 6.5f;
	public float mouseSpeed = 3.0f;
	public float jumpSpeed = 5.5f;
	public float pushingPower = 2.5f;
	
	//PUBLIC WEAPON RELATED VARIABLES//	
	public bool pistolPickedUp;
	public bool machineGunPickedUp;
	public bool pistolEquipped;
	public bool machineGunEquipped;
	
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
	}

}
