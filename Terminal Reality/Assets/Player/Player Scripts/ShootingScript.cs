using UnityEngine;
using System.Collections;

public class ShootingScript : MonoBehaviour {

	//PUBLIC VARIABLES SHOOTING//
	public float shotRange = 100.0f;

	//PRIVATE VARIABLES SHOOTING//
	private Ray ray;
	private float damage;
	private float rateOfFire;
	private bool singleFire;
	private float coolDownTimer;
	private GameObject weapon;


	// Use this for initialization
	void Start () {
	
		weapon = GameObject.FindGameObjectWithTag("Pistol");

		damage = weapon.GetComponent<weaponDataScript>().damage;
		rateOfFire = weapon.GetComponent<weaponDataScript>().rateOfFire;
		singleFire = weapon.GetComponent<weaponDataScript>().singleFire;

		coolDownTimer = rateOfFire;

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

					ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
					checkHit();

					print ("AMMO : " + weapon.GetComponent<weaponDataScript>().ammo);
					print ("CLIP : " + weapon.GetComponent<weaponDataScript>().getRemainingClip());
				}
				else //if clip is empty
				{
					weapon.GetComponent<weaponDataScript>().playEmptyClip(); //play empty clip sound
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
			//if the gun reloads successfully...
			if (weapon.GetComponent<weaponDataScript>().reload()) 
			{
				weapon.GetComponent<weaponDataScript>().playReload(); //play reload sound
			}
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
