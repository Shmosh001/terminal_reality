using UnityEngine;
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
	
	//PLAYER HAS KEY
	public bool hasKey = false;
	
	
	//AUTOMATICALLY SEARCH FOR CERTAIN GAME OBJECTS AT START/AWAKE//
	void Awake()
	{
		pistolGameObject = GameObject.FindGameObjectWithTag(Tags.PISTOL);
		machineGunGameObject = GameObject.FindGameObjectWithTag(Tags.MACHINEGUN);
		torch = GameObject.FindGameObjectWithTag(Tags.TORCH).light;
	}
	
	
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			//Debug.Log("writing");
			stream.SendNext(pistolEquipped);
			stream.SendNext(machineGunEquipped);
			stream.SendNext(playerAlive);
		}
		else {
			//Debug.Log("receiving");
			pistolEquipped = (bool)stream.ReceiveNext();
			machineGunEquipped = (bool)stream.ReceiveNext();
			playerAlive = (bool)stream.ReceiveNext();
		}
	}
}