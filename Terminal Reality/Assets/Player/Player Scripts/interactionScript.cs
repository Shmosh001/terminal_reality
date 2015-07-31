using UnityEngine;
using System.Collections;

public class interactionScript : MonoBehaviour {

	private playerDataScript playerData;

	//PRIVATE VARIABLES INTERACTION//
	private Ray ray;
	private bool inRangeOfAmmo;
	private bool inRangeOfHealth;
	private Collider interactingCollider; //the collider of the object the player was last in

	// Use this for initialization
	void Start () {
		playerData = this.GetComponent<playerDataScript>();
	}
	
	// Update is called once per frame
	void Update () {

		//WHEN THE PLAYER PUSHES E TO INTERACT WITH THE ENVIRONMENT//
		if (Input.GetKeyDown(KeyCode.E))
		{
			ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

			RaycastHit hitInfo; //to store what the ray hit

			//IF RAY HITS AN OBJECT IN RANGE//
			if (Physics.Raycast(ray, out hitInfo, 2.5f))
			{
				Vector3 hitPoint = hitInfo.point; //point where the collision happened
				GameObject hitObject = hitInfo.collider.gameObject; //get the game object which the ray hits

				//IF THE RAY HIT A DOOR//
				if (hitObject.CompareTag("Door"))
				{
					DoorScript ds = hitObject.GetComponent<DoorScript>();
					ds.interaction();
				}								
				
			}
			
			//IF THE PLAYER IS IN RANGE OF AMMO - PICK IT UP
			if (inRangeOfAmmo)
			{	
				//If the player has a pistol//
				if (playerData.pistolPickedUp)
				{
					GameObject weapon = GameObject.FindGameObjectWithTag("Pistol"); //find the pistol object
					//add ammo to the pistol - get ammo amount from the parent of the collider (Ammobox) and get the amount of pistol ammo it is holding.
					weapon.GetComponent<weaponDataScript>().ammoPickup(interactingCollider.GetComponentInParent<AmmoBoxScript>().pistolAmmo);

				}

				//If the player has a machine gun//
				if (playerData.machineGunPickedUp)
				{

					//TODO: MACHINE GUN LATER WHEN HAVE MACHINE GUN OBJECT//
				}
			}
			
			//IF THE PLAYER IS IN RANGE OF HEALTH - PICK IT UP
			if (inRangeOfHealth)
			{
				this.GetComponent<playerHealthScript>().fullPlayerHealth();
			}
		}
	
	}
	
	//PLAYER ENTERS AN OBJECTS TRIGGER//
	void OnTriggerEnter (Collider other)
	{
		//IF PLAYER IN RANGE OF AN AMMO BOX
		if (other.tag == "AmmoBox")
		{
			inRangeOfAmmo = true;
			interactingCollider = other;
		}
		
		//IF PLAYER IN RANGE OF AN HEALTH BOX
		if (other.tag == "HealthBox")
		{
			inRangeOfHealth = true;
			interactingCollider = other;
		}
	}
	
	//PLAYER EXITS AN OBJECTS TRIGGER//
	void OnTriggerExit (Collider other)
	{
		//IF PLAYER IN RANGE OF AN AMMO BOX
		if (other.tag == "AmmoBox")
		{
			inRangeOfAmmo = false;
		}
		
		//IF PLAYER IN RANGE OF AN HEALTH BOX
		if (other.tag == "HealthBox")
		{
			inRangeOfHealth = false;
		}
	}
}
