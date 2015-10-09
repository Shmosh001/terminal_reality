using UnityEngine;
using System.Collections;

public class ShootingScript : ammoHUDScript {
	
	playerDataScript playerData;
	
	//PUBLIC VARIABLES SHOOTING//
	public float shotRange = 100.0f;
	
	//PRIVATE VARIABLES SHOOTING//
	private Ray ray;
	private float coolDownTimer;
	private GameObject weapon;
	private GameObject soundController;
    private playerAnimatorSync animSync;
    private PhotonView pView;
	
	//COUNTERS//
	private int flareLoopCount = 0;
	
	
	// Use this for initialization
	void Start () {
		playerData = this.GetComponent<playerDataScript>();
        animSync = this.gameObject.GetComponent<playerAnimatorSync>();
        updateAmmoText(0,0);
		soundController = GameObject.FindGameObjectWithTag(Tags.SOUNDCONTROLLER);
        pView = gameObject.GetComponent<PhotonView>();
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
				if (weapon.GetComponent<weaponDataScript>().checkCanShoot()) //if there is a bullet in the clip
				{
					if (coolDownTimer <= 0)
					{    
					                    
                        if (!PhotonNetwork.offlineMode) {
                            pView.RPC("gunShot", PhotonTargets.Others);
                       }
						
                        animSync.setTriggerP(playerAnimationHash.shootTrigger);

                        weapon.GetComponent<weaponDataScript>().reduceAmmo(); //reduce ammo
						soundController.GetComponent<soundControllerScript>().playPistolShot(transform.position); //play sound of a pistol shot
						weapon.GetComponent<weaponDataScript>().gunFlare(true); //show gun flare
						flareLoopCount = 0;
						
						if (playerData.pistolEquipped == true)
						{
							//RUN THE UPDATE AMMO HUD TEXT METHOD - method in ammoHUDScript//
							updateAmmoText(weapon.GetComponent<weaponDataScript>().getRemainingAmmo(), 
							               weapon.GetComponent<weaponDataScript>().getRemainingClip());
							//CHECK FOR RELOAD WARNING - method in ammoHUDScript//
							checkReloadWarning(weapon.GetComponent<weaponDataScript>().getRemainingClip(),
							                   weapon.GetComponent<weaponDataScript>().clipSize,
							                   weapon.GetComponent<weaponDataScript>().getRemainingAmmo());
						}
						
						ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
						checkHit();
					}
				}
				else //if clip is empty
				{
					soundController.GetComponent<soundControllerScript>().playEmptyClip(transform.position); //play empty clip sound
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
				if (weapon.GetComponent<weaponDataScript>().checkCanShoot()) //if there is a bullet in the clip
				{
					if (coolDownTimer <= 0) //can shoot
					{
						weapon.GetComponent<weaponDataScript>().reduceAmmo(); //reduce ammo
						soundController.GetComponent<soundControllerScript>().playPistolShot(transform.position); //play sound of a pistol shot
						weapon.GetComponent<weaponDataScript>().gunFlare(true); //show gun flare
						
						
						//RUN THE UPDATE AMMO HUD TEXT METHOD - method in ammoHUDScript//
						updateAmmoText(weapon.GetComponent<weaponDataScript>().getRemainingAmmo(), 
						               weapon.GetComponent<weaponDataScript>().getRemainingClip());
						
						//CHECK FOR RELOAD WARNING - method in ammoHUDScript//
						checkReloadWarning(weapon.GetComponent<weaponDataScript>().getRemainingClip(),
						                   weapon.GetComponent<weaponDataScript>().clipSize,
						                   weapon.GetComponent<weaponDataScript>().getRemainingAmmo());
						
						ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
						checkHit();
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
						soundController.GetComponent<soundControllerScript>().playEmptyClip(transform.position); //play empty clip sound
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
					soundController.GetComponent<soundControllerScript>().playReload(transform.position); //play reload sound
					
					//RUN THE UPDATE AMMO HUD TEXT METHOD - method in ammoHUDScript//
					updateAmmoText(weapon.GetComponent<weaponDataScript>().getRemainingAmmo(), 
					               weapon.GetComponent<weaponDataScript>().getRemainingClip());
					
					//CHECK FOR RELOAD WARNING - method in ammoHUDScript//
					checkReloadWarning(weapon.GetComponent<weaponDataScript>().getRemainingClip(),
					                   weapon.GetComponent<weaponDataScript>().clipSize,
					                   weapon.GetComponent<weaponDataScript>().getRemainingAmmo());

                    PhotonView pView = this.gameObject.GetComponent<PhotonView>();
                    if (PhotonNetwork.offlineMode) {
                        animSync.setTriggerP(playerAnimationHash.reloadTrigger);
                    }
                    else {
                        animSync.setTriggerP(playerAnimationHash.reloadTrigger);
                        pView.RPC("setTriggerP", PhotonTargets.Others, playerAnimationHash.reloadTrigger);
                    }


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
		
		/*
		if (weapon != null)
		{
			//RUN THE UPDATE AMMO HUD TEXT METHOD - method in ammoHUDScript//
			updateAmmoText(weapon.GetComponent<weaponDataScript>().getRemainingAmmo(), 
			               weapon.GetComponent<weaponDataScript>().getRemainingClip());
		}*/
		
	}
	
	//LOAD A NEW WEAPON INTO THE WEAPON GAMEOBJECT
	//ALSO UPDATE ALL THE STATS TO THOSE OF THE WEAPON
	public void loadNewWeapon(string weaponTag)
	{
		if (weaponTag == Tags.PISTOL)
		{
			weapon = this.GetComponent<playerDataScript>().pistolGameObject;
		}
		else if (weaponTag == Tags.MACHINEGUN)
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
			if (hitObject.CompareTag(Tags.ENEMY) || hitObject.CompareTag(Tags.BOSSENEMY))
			{
                //Debug.Log("Enemy shot");
                //hitObject.GetComponent<EnemyHealthScript>().takeDamage((int)weapon.GetComponent<weaponDataScript>().damage, this.gameObject);

                PhotonView enemypView = hitObject.GetComponent<PhotonView>();

                //hitObject.GetComponent<EnemyHealthScript>().takeDamage((int)weapon.GetComponent<weaponDataScript>().damage, this.gameObject.tag);
                if (PhotonNetwork.offlineMode) {
                    //Debug.LogWarning((int)weapon.GetComponent<weaponDataScript>().damage);
                    hitObject.GetComponent<EnemyHealthScript>().takeDamageN((int)weapon.GetComponent<weaponDataScript>().damage, this.gameObject.tag, hitPoint, hitObject.GetComponent<PhotonView>().viewID);
                }
                else {
                    if (enemypView == null) {
                        Debug.LogError("No PhotonView component found on " + hitObject);
                    }
                    else {
                        //hitObject.GetComponent<EnemyHealthScript>().takeDamage((int)weapon.GetComponent<weaponDataScript>().damage, this.gameObject.tag);
                        enemypView.RPC("takeDamageN", PhotonTargets.AllViaServer, (int)weapon.GetComponent<weaponDataScript>().damage, this.gameObject.tag, hitPoint,hitObject.GetComponent<PhotonView>().viewID);
                    }
                }
                
			}
            
		}
	}
}