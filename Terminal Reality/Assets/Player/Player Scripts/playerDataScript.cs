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
    public bool canHear = false;
	
	//PUBLIC WEAPON RELATED VARIABLES//	
	public bool pistolPickedUp = false;
	public bool machineGunPickedUp = false;
	public bool pistolEquipped = false;
	public bool machineGunEquipped = false;
	
	//WEAPONS GAME OBJECTS//
	public GameObject pistolGameObject;
	public GameObject machineGunGameObject;
    public GameObject torch2;
    
	
	//TORCH//
	public Light torch;
    public GameObject torchObj;

    //PLAYER HAS KEY
    public bool hasKey = false;


    //AUTOMATICALLY SEARCH FOR CERTAIN GAME OBJECTS AT START/AWAKE//
    void Awake()
	{
		pistolGameObject = GameObject.FindGameObjectWithTag(Tags.PISTOL);
		pistolGameObject.SetActive(false);
		machineGunGameObject = GameObject.FindGameObjectWithTag(Tags.MACHINEGUN);

        //fx torch
        torch2 = GameObject.FindGameObjectWithTag(Tags.P2TORCH);

        //main torch
        torchObj = GameObject.FindGameObjectWithTag(Tags.TORCH);

		machineGunGameObject.SetActive(false);

	}
	
	
    void Update() {
        if (pistolGameObject == null) {
            pistolGameObject = GameObject.FindGameObjectWithTag(Tags.PISTOL);
        }
        if (machineGunGameObject == null) {
            machineGunGameObject = GameObject.FindGameObjectWithTag(Tags.MACHINEGUN);
        }
        if (torch2 == null) {
            torch2 = GameObject.FindGameObjectWithTag(Tags.P2TORCH);
        }

        //main torch
        if (torchObj == null) {
            torchObj = GameObject.FindGameObjectWithTag(Tags.TORCH);
        }
        else {
            torch = torchObj.light;
        }


    }


	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			stream.SendNext(playerAlive);
            //stream.SendNext(torchON);
		}
		else {
			playerAlive = (bool)stream.ReceiveNext();
           // torchON = (int)stream.ReceiveNext();
		}
	}
}