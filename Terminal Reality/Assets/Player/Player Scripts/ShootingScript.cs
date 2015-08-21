using UnityEngine;
using System.Collections;

public class ShootingScript : ammoHUDScript {

	//PUBLIC VARIABLES SHOOTING//
	public float shotRange = 100.0f;

	//PRIVATE VARIABLES SHOOTING//
	private Ray ray;
	private float coolDownTimer;
	private GameObject weapon;
	private GameObject soundController;

	//COUNTERS//
	private int flareLoopCount = 0;


	// Use this for initialization
	void Start () {
						
		updateAmmoText(0,0);
		soundController = GameObject.FindGameObjectWithTag("Sound Controller");

	}
	
	// Update is called once per frame
	void Update () 
	{
		//SINGLE FIRE//
		if (weapon != null && weapon.GetComponent<weaponDataScript>().singleFire)
		{
			coolDownTimer -= Time.deltaTime; // reduce cool down timer
		
			if (Input.GetMouseButtonDown(0))
			{
				if (weapon.GetComponent<weaponDataScript>().getRemainingClip() > 0) //if there is a bullet in the clip
				{
					if (coolDownTimer <= 0)
					{
						weapon.GetComponent<weaponDataScript>().reduceAmmo(); //reduce ammo
						soundController.GetComponent<soundControllerScript>().playPistolShot(this.GetComponent<AudioSource>()); //play sound of a pistol shot
						weapon.GetComponent<weaponDataScript>().gunFlare(true); //show gun flare
						flareLoopCount = 0;
	
						//RUN THE UPDATE AMMO HUD TEXT METHOD - method in ammoHUDScript//
						updateAmmoText(weapon.GetComponent<weaponDataScript>().getRemainingAmmo(), 
						               weapon.GetComponent<weaponDataScript>().getRemainingClip());
						//CHECK FOR RELOAD WARNING - method in ammoHUDScript//
						checkReloadWarning(weapon.GetComponent<weaponDataScript>().getRemainingClip(),
						                   weapon.GetComponent<weaponDataScript>().clipSize,
						                   weapon.GetComponent<weaponDataScript>().getRemainingAmmo());
	
						ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
						checkHit();
					}
				}
				else //if clip is empty
				{
					soundController.GetComponent<soundControllerScript>().playEmptyClip(this.GetComponent<AudioSource>()); //play empty clip sound
				}
			}
			else
			{
				if (flareLoopCount >= 2)
				{
					weapon.GetComponent<weaponDataScript>().gunFlare(false); //disable the gun flare
				}
				else
				{
					flareLoopCount++;
				}
			}
		}
		//AUTOMATIC FIRE//
		else if (weapon != null && !weapon.GetComponent<weaponDataScript>().singleFire)
		{
			coolDownTimer -= Time.deltaTime; //reduce cool down timer
			
			if (Input.GetMouseButton(0))
			{
				if (weapon.GetComponent<weaponDataScript>().getRemainingClip() > 0) //if there is a bullet in the clip
				{
					if (coolDownTimer <= 0) //can shoot
					{
						weapon.GetComponent<weaponDataScript>().reduceAmmo(); //reduce ammo
						soundController.GetComponent<soundControllerScript>().playPistolShot(this.GetComponent<AudioSource>()); //play sound of a pistol shot
						weapon.GetComponent<weaponDataScript>().gunFlare(true); //show gun flare
						

						//RUN THE UPDATE AMMO HUD TEXT METHOD - method in ammoHUDScript//
						updateAmmoText(weapon.GetComponent<weaponDataScript>().getRemainingAmmo(), 
						               weapon.GetComponent<weaponDataScript>().getRemainingClip());

						//CHECK FOR RELOAD WARNING - method in ammoHUDScript//
						checkReloadWarning(weapon.GetComponent<weaponDataScript>().getRemainingClip(),
						                   weapon.GetComponent<weaponDataScript>().clipSize,
						                   weapon.GetComponent<weaponDataScript>().getRemainingAmmo());

						ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
						//checkHit();
						Debug.LogError(weapon.GetComponent<weaponDataScript>().rateOfFire);
						coolDownTimer = weapon.GetComponent<weaponDataScript>().rateOfFire;						
					}
					else
					{
						weapon.GetComponent<weaponDataScript>().gunFlare(false); //disable the gun flare
					}
				}

				else //if clip is empty
				{
					if (coolDownTimer <= 0)
					{
						soundController.GetComponent<soundControllerScript>().playEmptyClip(this.GetComponent<AudioSource>()); //play empty clip sound
						coolDownTimer = 0.8f; //so sound doesn't play too fast.
					}
					weapon.GetComponent<weaponDataScript>().gunFlare(false); //disable the gun flare
				}
			}
			else
			{
				if (flareLoopCount >= 2)
				{
					weapon.GetComponent<weaponDataScript>().gunFlare(false); //disable the gun flare
				}
				else
				{
					flareLoopCount++;
				}
			}
		}

		//RELOAD//
		if (Input.GetKeyDown(KeyCode.R))
		{
			//Can only reload if the clip is NOT full//
			if (weapon.GetComponent<weaponDataScript>().getRemainingClip() != weapon.GetComponent<weaponDataScript>().clipSize)
			{
				//if the gun reloads successfully...
				if (weapon.GetComponent<weaponDataScript>().reload()) 
				{
					coolDownTimer = 1.3f; //so can't start shooting while the sound is playing
					soundController.GetComponent<soundControllerScript>().playReload(this.GetComponent<AudioSource>()); //play reload sound

					//RUN THE UPDATE AMMO HUD TEXT METHOD - method in ammoHUDScript//
					updateAmmoText(weapon.GetComponent<weaponDataScript>().getRemainingAmmo(), 
					               weapon.GetComponent<weaponDataScript>().getRemainingClip());

					//CHECK FOR RELOAD WARNING - method in ammoHUDScript//
					checkReloadWarning(weapon.GetComponent<weaponDataScript>().getRemainingClip(),
					                   weapon.GetComponent<weaponDataScript>().clipSize,
					                   weapon.GetComponent<weaponDataScript>().getRemainingAmmo());
				}
			}
		}

		//SWITCHING WEAPONS//

		//pistol//
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			//if successfully switched to pistol...
			if (this.GetComponent<weaponSwitchScript>().switchToPistol())
			{
				loadNewWeapon("Pistol");				
			}
		}


		//machine gun//
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			if (this.GetComponent<weaponSwitchScript>().switchToMachineGun())
			{
				loadNewWeapon("MachineGun");			
			}
		}
	
	}
	
	//LOAD A NEW WEAPON INTO THE WEAPON GAMEOBJECT
	//ALSO UPDATE ALL THE STATS TO THOSE OF THE WEAPON
	public void loadNewWeapon(string weaponTag)
	{
		if (weaponTag == "Pistol")
		{
			weapon = this.GetComponent<playerDataScript>().pistolGameObject;
		}
		else if (weaponTag == "MachineGun")
		{
			weapon = this.GetComponent<playerDataScript>().machineGunGameObject;
		}
		
	}

	void checkHit()
	{
		RaycastHit hitInfo; //to store what the ray hit

		if (Physics.Raycast(ray, out hitInfo, shotRange))
		{
			Vector3 hitPoint = hitInfo.point; //point where the collision happened
			GameObject hitObject = hitInfo.collider.gameObject; //get the game object which the ray hits

			//TEST SHOOTING ON SPHERE
/*			if (hitObject.CompareTag("Sphere"))
			{
				Color c = new Color(Random.value, Random.value, Random.value, 1.0f);

				hitObject.renderer.material.color = c;
			}*/

			//SHOOTING ENEMY//
			if (hitObject.CompareTag(Tags.ENEMY))
			{
				Debug.Log("Enemy shot");
				hitObject.GetComponent<EnemyHealthScript>().takeDamage((int)weapon.GetComponent<weaponDataScript>().damage, this.gameObject);
			}
		}
	}
}
