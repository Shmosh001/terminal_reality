using UnityEngine;
using System.Collections;

public class ShootingScript : ammoHUDScript {

	//PUBLIC VARIABLES SHOOTING//
	public float shotRange = 100.0f;

	//PRIVATE VARIABLES SHOOTING//
	private Ray ray;
	private float damage;
	private float rateOfFire;
	private bool singleFire;
	private float coolDownTimer;
	private GameObject weapon;

	//COUNTERS//
	private int flareLoopCount = 0;


	// Use this for initialization
	void Start () {
	
		weapon = GameObject.FindGameObjectWithTag("Pistol");

		damage = weapon.GetComponent<weaponDataScript>().damage;
		rateOfFire = weapon.GetComponent<weaponDataScript>().rateOfFire;
		singleFire = weapon.GetComponent<weaponDataScript>().singleFire;

		coolDownTimer = rateOfFire;

		//RUN THE UPDATE AMMO HUD TEXT METHOD - method in ammoHUDScript//
		updateAmmoText(weapon.GetComponent<weaponDataScript>().getRemainingAmmo(), 
		               weapon.GetComponent<weaponDataScript>().getRemainingClip());

	}
	
	// Update is called once per frame
	void Update () 
	{
		//SINGLE FIRE//
		if (singleFire)
		{
			if (Input.GetMouseButtonDown(0))
			{
				if (weapon.GetComponent<weaponDataScript>().getRemainingClip() > 0) //if there is a bullet in the clip
				{
					weapon.GetComponent<weaponDataScript>().reduceAmmo(); //reduce ammo
					weapon.GetComponent<weaponDataScript>().playShot(); //play sound of a pistol shot
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
				else //if clip is empty
				{
					weapon.GetComponent<weaponDataScript>().playEmptyClip(); //play empty clip sound
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
		else
		{
			if (Input.GetMouseButton(0))
			{
				if (weapon.GetComponent<weaponDataScript>().getRemainingClip() > 0) //if there is a bullet in the clip
				{
					coolDownTimer -= Time.deltaTime;

					if (coolDownTimer <= 0) //can shoot
					{
						weapon.GetComponent<weaponDataScript>().reduceAmmo(); //reduce ammo

						//RUN THE UPDATE AMMO HUD TEXT METHOD - method in ammoHUDScript//
						updateAmmoText(weapon.GetComponent<weaponDataScript>().getRemainingAmmo(), 
						               weapon.GetComponent<weaponDataScript>().getRemainingClip());

						//CHECK FOR RELOAD WARNING - method in ammoHUDScript//
						checkReloadWarning(weapon.GetComponent<weaponDataScript>().getRemainingClip(),
						                   weapon.GetComponent<weaponDataScript>().clipSize,
						                   weapon.GetComponent<weaponDataScript>().getRemainingAmmo());

						ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
						checkHit();
						coolDownTimer = rateOfFire;
					}
				}

				else //if clip is empty
				{
					weapon.GetComponent<weaponDataScript>().playEmptyClip(); //play empty clip sound
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
					weapon.GetComponent<weaponDataScript>().playReload(); //play reload sound

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
			print("ONE! - pistol");
		}


		//machine gun//
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			print("TWO! - machine gun");
		}
	
	}

	void checkHit()
	{
		RaycastHit hitInfo; //to store what the ray hit

		if (Physics.Raycast(ray, out hitInfo, shotRange))
		{
			Vector3 hitPoint = hitInfo.point; //point where the collision happened
			GameObject hitObject = hitInfo.collider.gameObject; //get the game object which the ray hits

			if (hitObject.CompareTag("Sphere"))
			{
				Color c = new Color(Random.value, Random.value, Random.value, 1.0f);

				hitObject.renderer.material.color = c;
			}
		}
	}
}
