using UnityEngine;
using System.Collections;

public class interactionScript : MonoBehaviour {

	private playerDataScript playerData;

	//PRIVATE VARIABLES INTERACTION//
	private Ray ray;
	private bool inRangeOfAmmo;
	private bool inRangeOfHealth;
	private bool inRangeOfPistol;
	private bool inRangeOfMachineGun;
	private Collider interactingCollider; //the collider of the object the player was last in

	//PUBLIC VARIABLES FOR INTERACTION//
	public AudioClip weaponPickup;

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
					playWeaponPickupSound();

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

			//IF THE PLAYER IS IN RANGE OF PISTOL - PICK IT UP
			if (inRangeOfPistol)
			{
				//If the player already has a pistol, just pickup pistol ammo.
				if (playerData.pistolPickedUp)
				{
					GameObject pistol = GameObject.FindGameObjectWithTag("Pistol"); //find the pistol object
					//pickup ammo for the pistol
					//amount randomly generate - from 10 - 30 bullets picked up
					pistol.GetComponent<weaponDataScript>().ammoPickup(Random.Range(10, 30)); 
				}
				else 
				{
					playerData.pistolPickedUp = true;					

					//if this is the only gun that the player now has - enable it
					if (!playerData.machineGunPickedUp)
					{
						playerData.pistolEquipped = true;
						this.GetComponent<weaponSwitchScript>().enableWeapon();
						this.GetComponent<ShootingScript>().loadNewWeapon("Pistol");
					}
				}
			}

			//IF THE PLAYER IS IN RANGE OF MACHINE GUN - PICK IT UP
			if (inRangeOfMachineGun)
			{
				//If the player already has a machine gun, just pickup the ammo
				if (playerData.machineGunPickedUp)
				{
					GameObject machineGun = GameObject.FindGameObjectWithTag("MachineGun"); //find the pistol object					
					//pickup ammo for the machine gun
					//amount randomly generate - from 10 - 50 bullets picked up
					machineGun.GetComponent<weaponDataScript>().ammoPickup(Random.Range(10, 50)); 
				}
				else
				{
					playerData.machineGunPickedUp = true;
					
					//if this is the only gun that the player now has - enable it
					if (!playerData.pistolPickedUp)
					{
						playerData.machineGunEquipped = true;
						this.GetComponent<weaponSwitchScript>().enableWeapon();
						this.GetComponent<ShootingScript>().loadNewWeapon("MachineGun");
					}
				}
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

		//IF PLAYER IN RANGE OF PISTOL
		if (other.tag == "pistolPickup")
		{
			inRangeOfPistol = true;
			interactingCollider = other;
		}

		//IF PLAYER IN RANGE OF MACHINE GUN
		if (other.tag == "machineGunPickup")
		{
			inRangeOfMachineGun = true;
			interactingCollider = other;
		}
	}
	
	//PLAYER EXITS AN OBJECTS TRIGGER//
	void OnTriggerExit (Collider other)
	{
		//IF PLAYER NOT IN RANGE OF AN AMMO BOX
		if (other.tag == "AmmoBox")
		{
			inRangeOfAmmo = false;
		}
		
		//IF PLAYER NOT IN RANGE OF AN HEALTH BOX
		if (other.tag == "HealthBox")
		{
			inRangeOfHealth = false;
		}

		//IF PLAYER NOT IN RANGE OF PISTOL
		if (other.tag == "pistolPickup")
		{
			inRangeOfPistol = false;
		}
		
		//IF PLAYER NOT IN RANGE OF MACHINE GUN
		if (other.tag == "machineGunPickup")
		{
			inRangeOfMachineGun = false;
		}
	}

	//WEAPON RELATED PICKUP SOUND - sound that plays for weapon pickup and ammo pickup//
	void playWeaponPickupSound()
	{
		interactingCollider.GetComponentInParent<AudioSource>().PlayOneShot(weaponPickup);
	}
}
