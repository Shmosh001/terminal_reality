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


	// Use this for initialization
	void Start () {
	
		//TODO: CHANGE: we will get this information from the gun object which the player is using***
		damage = 50.0f;
		rateOfFire = 10.0f;
		singleFire = true;

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
				ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
				checkHit();
			}
		}
		//AUTOMATIC FIRE//
		else
		{
			if (Input.GetMouseButton(0))
			{
				coolDownTimer -= Time.deltaTime;

				if (coolDownTimer <= 0) //can shoot
				{
					ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
					checkHit();
					coolDownTimer = rateOfFire;
				}
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
