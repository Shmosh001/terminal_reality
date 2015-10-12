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
	public GameObject soundController;
    private playerAnimatorSync animSync;

	public Texture crosshair;
	
	//COUNTERS//
	private int flareLoopCount = 0;
	
	
	// Use this for initialization
	void Start () {
		playerData = this.GetComponent<playerDataScript>();
        animSync = this.gameObject.GetComponent<playerAnimatorSync>();
        updateAmmoText(0,0);
		

    }
    
	// Draws two crosshairs in the center of each players screen.
	void OnGUI(){
		
		if (gameObject.tag == "Player1")
		{
			GUI.DrawTexture(new Rect(((Screen.width)/4)*3 - (crosshair.width/2),(Screen.height-crosshair.height)/2, crosshair.width, crosshair.height),crosshair);
		}
		else if (gameObject.tag == "Player2")
		{		
			GUI.DrawTexture(new Rect(((Screen.width)/4)*1 - (crosshair.width/2),(Screen.height-crosshair.height)/2, crosshair.width, crosshair.height),crosshair);
		}
		
		//instructionRight.transform.position = new Vector3 (Screen.width / 4 * 3, Screen.height/2 - blueCrosshair.height, 0);
		//instructionLeft.transform.position = new Vector3 (Screen.width / 4, Screen.height/2 - blueCrosshair.height, 0);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
		//////////////////////////////////////////////////////////
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
		//-----------PLAYER ONE----------PLAYER ONE------------//
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
		/////////////////////////////////////////////////////////
		if (gameObject.tag == "Player1")
		{
			//SINGLE FIRE//
			if (weapon != null && weapon.GetComponent<weaponDataScript>().singleFire && playerData.playerAlive)
			{
				coolDownTimer -= Time.deltaTime; // reduce cool down timer
				
				if (Input.GetMouseButtonDown(0))
				{
					if (weapon.GetComponent<weaponDataScript>().checkCanShoot()) //if there is a bullet in the clip
					{
						if (coolDownTimer <= 0)
						{    
						                    
	                       
							
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
			else if (weapon != null && !weapon.GetComponent<weaponDataScript>().singleFire && playerData.playerAlive)
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
			if (Input.GetKeyDown(KeyCode.R) && weapon != null && playerData.playerAlive)
			{
				//Can only reload if the clip is NOT full//
				if (weapon.GetComponent<weaponDataScript>().getRemainingClip() != weapon.GetComponent<weaponDataScript>().clipSize)
				{
					//if the gun reloads successfully...
					if (weapon.GetComponent<weaponDataScript>().reload()) 
					{
						coolDownTimer = 3.1f; //so can't start shooting while the sound is playing
						soundController.GetComponent<soundControllerScript>().playReload(transform.position); //play reload sound
						
						//RUN THE UPDATE AMMO HUD TEXT METHOD - method in ammoHUDScript//
						updateAmmoText(weapon.GetComponent<weaponDataScript>().getRemainingAmmo(), 
						               weapon.GetComponent<weaponDataScript>().getRemainingClip());
						
						//CHECK FOR RELOAD WARNING - method in ammoHUDScript//
						checkReloadWarning(weapon.GetComponent<weaponDataScript>().getRemainingClip(),
						                   weapon.GetComponent<weaponDataScript>().clipSize,
						                   weapon.GetComponent<weaponDataScript>().getRemainingAmmo());
	
	                    
	                    
	                    animSync.setTriggerP(playerAnimationHash.reloadTrigger);
	                    
	
	
	                }
	
				}
			}
		}
		
		//////////////////////////////////////////////////////////
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
		//-----------PLAYER TWO----------PLAYER TWO------------//
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
		/////////////////////////////////////////////////////////
		if (gameObject.tag == "Player2")
		{
		
			//SINGLE FIRE//
			if (weapon != null && weapon.GetComponent<weaponDataScript>().singleFire && playerData.playerAlive)
			{
				coolDownTimer -= Time.deltaTime; // reduce cool down timer
				
				if (Input.GetButtonDown("ShootC"))
				{
					if (weapon.GetComponent<weaponDataScript>().checkCanShoot()) //if there is a bullet in the clip
					{
						if (coolDownTimer <= 0)
						{    
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
							coolDownTimer = 0.2f;
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
			else if (weapon != null && !weapon.GetComponent<weaponDataScript>().singleFire && playerData.playerAlive)
			{
				coolDownTimer -= Time.deltaTime; //reduce cool down timer
				
				if (Input.GetAxis("ShootC")>0)
				{
					print ("BANG!");
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
			if (Input.GetButtonDown("ReloadC") && weapon != null && playerData.playerAlive)
			{
				//Can only reload if the clip is NOT full//
				if (weapon.GetComponent<weaponDataScript>().getRemainingClip() != weapon.GetComponent<weaponDataScript>().clipSize)
				{
					//if the gun reloads successfully...
					if (weapon.GetComponent<weaponDataScript>().reload()) 
					{
						coolDownTimer = 3.1f; //so can't start shooting while the sound is playing
						soundController.GetComponent<soundControllerScript>().playReload(transform.position); //play reload sound
						
						//RUN THE UPDATE AMMO HUD TEXT METHOD - method in ammoHUDScript//
						updateAmmoText(weapon.GetComponent<weaponDataScript>().getRemainingAmmo(), 
						               weapon.GetComponent<weaponDataScript>().getRemainingClip());
						
						//CHECK FOR RELOAD WARNING - method in ammoHUDScript//
						checkReloadWarning(weapon.GetComponent<weaponDataScript>().getRemainingClip(),
						                   weapon.GetComponent<weaponDataScript>().clipSize,
						                   weapon.GetComponent<weaponDataScript>().getRemainingAmmo());
						
						
						
						animSync.setTriggerP(playerAnimationHash.reloadTrigger);
						
						
						
					}
					
				}
			}
		}
		//SWITCHING WEAPONS//
		
		//pistol//
		if (Input.GetKeyDown(KeyCode.Alpha1) && playerData.playerAlive)
		{
			//if successfully switched to pistol...
			if (this.GetComponent<weaponSwitchScript>().switchToPistol())
			{
				loadNewWeapon("Pistol");				
			}
		}
		
		
		//machine gun//
		if (Input.GetKeyDown(KeyCode.Alpha2) && playerData.playerAlive)
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

            Debug.LogWarning(hitObject.tag + "  " + hitObject.name);

			
			//SHOOTING ENEMY//
			if (hitObject.CompareTag(Tags.ENEMY) || hitObject.CompareTag(Tags.BOSSENEMY))
			{
                Debug.Log("Enemy shot: " + hitObject.name );
                
                hitObject.GetComponent<EnemyHealthScript>().takeDamage((int)weapon.GetComponent<weaponDataScript>().damage, this.gameObject, hitPoint);
                
                
			}
            
		}
	}
}