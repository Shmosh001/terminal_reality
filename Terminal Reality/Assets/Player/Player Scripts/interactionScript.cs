using UnityEngine;
using System.Collections;

public class interactionScript : Photon.MonoBehaviour {

	private playerDataScript playerData;
	
	//the animator
	private Animator animator;
    private PhotonView playerPView;
    private playerAnimatorSync animSync;

	//PRIVATE VARIABLES INTERACTION//
	private Ray ray;
	private bool inRangeOfAmmo;
	private bool inRangeOfHealth;
	private bool inRangeOfPistol;
	private bool inRangeOfMachineGun;
	private bool inRangeOfKeys;
	private Collider interactingCollider; //the collider of the object the player was last in
	private GameObject soundController;




	// Use this for initialization
	void Start () 
	{
		playerData = this.GetComponent<playerDataScript>();
		soundController = GameObject.FindGameObjectWithTag(Tags.SOUNDCONTROLLER);
		animator = this.gameObject.GetComponent<Animator>();
        playerPView = this.gameObject.GetComponent<PhotonView>();
        animSync = this.gameObject.GetComponent<playerAnimatorSync>();
    }
	
	// Update is called once per frame
	void Update () 
	{

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
				if (hitObject.CompareTag(Tags.DOOR))
				{

					//DoorScript ds = hitObject.GetComponentInParent<DoorScript>();
					//ds.interaction();



                    PhotonView pView = hitObject.GetComponent<PhotonView>();

                    if (pView == null) {
                        pView = hitObject.GetComponentInParent<PhotonView>();
                        if (pView == null) {
                            Debug.LogError("No PhotonView component found on " + hitObject);
                        }
                        else {
                            if (PhotonNetwork.offlineMode) {
                                DoorScript ds = hitObject.GetComponentInParent<DoorScript>();
                                ds.interaction();
                            }
                            else {
                                pView.RPC("interaction", PhotonTargets.AllBuffered);
                            }
                        }
                    }
                    else {
                        if (PhotonNetwork.offlineMode) {
                            DoorScript ds = hitObject.GetComponent<DoorScript>();
                            ds.interaction();
                        }
                        else {
                            pView.RPC("interaction", PhotonTargets.AllBuffered);
                        }
                    }
                }

				//IF THE RAY HIT A DOOR//				
				if (hitObject.CompareTag(Tags.DOUBLEDOOR) && playerData.hasKey)
				{
					//DDScript dds = hitObject.GetComponentInParent<DDScript>();
					//dds.interaction();


                    PhotonView pView = hitObject.GetComponent<PhotonView>();

                    if (pView == null) {
                        pView = hitObject.GetComponentInParent<PhotonView>();
                        if (pView == null) {
                            Debug.LogError("No PhotonView component found on " + hitObject);
                        }
                        else {
                            if (PhotonNetwork.offlineMode) {
                                DDScript dds = hitObject.GetComponentInParent<DDScript>();
                                dds.interaction();
                            }
                            else {
								DDScript dds = hitObject.GetComponentInParent<DDScript>();
								dds.interaction();
                                pView.RPC("interaction", PhotonTargets.AllBufferedViaServer);
                            }
                        }
                    }
                    else {
                        if (PhotonNetwork.offlineMode) {
                            DDScript dds = hitObject.GetComponentInParent<DDScript>();
                            dds.interaction();
                        }
                        else {
							DDScript dds = hitObject.GetComponentInParent<DDScript>();
							dds.interaction();
                            pView.RPC("interaction", PhotonTargets.AllBufferedViaServer);
                        }
                    }


                }

				//DoorScript ds = hitObject.GetComponentInParent<DoorScript>();

                
                    
			}	

			//IF THE PLAYER IS IN RANGE OF AMMO - PICK IT UP
			if (inRangeOfAmmo)
			{	
				//player can only pick up ammo if they have a weapon.
				if(playerData.pistolPickedUp || playerData.machineGunPickedUp)
				{
					//animator.SetTrigger(playerAnimationHash.pickupTrigger);


                    if (PhotonNetwork.offlineMode) {
                        animator.SetTrigger(playerAnimationHash.pickupTrigger);
                    }
                    else {
                        playerPView.RPC("setTriggerP", PhotonTargets.AllViaServer, playerAnimationHash.pickupTrigger);
                    }


                    //If the player has a pistol//
                    if (playerData.pistolPickedUp)
					{
						soundController.GetComponent<soundControllerScript>().playPickupSound(transform.position);
						GameObject weapon = GameObject.FindGameObjectWithTag("Pistol"); //find the pistol object
	
						//add ammo to the pistol - get ammo amount from the parent of the collider (Ammobox) and get the amount of pistol ammo it is holding.
						playerData.pistolGameObject.GetComponent<weaponDataScript>().ammoPickup(interactingCollider.GetComponentInParent<AmmoBoxScript>().pistolAmmo);
	
					}
	
					//If the player has a machine gun//
					if (playerData.machineGunPickedUp)
					{
						soundController.GetComponent<soundControllerScript>().playPickupSound(transform.position);
						//add ammo to the machine gun - get ammo amount from the parent of the collider (Ammobox) and get the amount of machine gun ammo it is holding.
						playerData.machineGunGameObject.GetComponent<weaponDataScript>().ammoPickup(interactingCollider.GetComponentInParent<AmmoBoxScript>().machineGunAmmo);
					}
	
	
					//After picking up ammo destroy the ammobox game object//
					//Make in range false - because collider is destroy, therefore you cannot exit it to remove text//
					//interactingCollider.GetComponentInParent<AmmoBoxScript>().turnOffText();//handled in script
	                //PhotonNetwork.Destroy(interactingCollider.gameObject); worked but only for master client
	
	                PhotonView pView = interactingCollider.GetComponentInParent<PhotonView>();
	                if (pView == null) {
	                    Debug.LogError("No PhotonView component found");
	                }
	                else {
	                    if (PhotonNetwork.offlineMode) {
	                        Destroy(interactingCollider.gameObject);
	                    }
	                    else {
	                        pView.RPC("destroyObject", PhotonTargets.AllBuffered);
	                    }
	                    
	                }
	                
					inRangeOfAmmo = false;
				}
			}
	
			
			if (inRangeOfHealth) {
                if (playerData.health < 100) {
                    //animator.SetTrigger(playerAnimationHash.pickupTrigger);

                    if (PhotonNetwork.offlineMode) {
                        animator.SetTrigger(playerAnimationHash.pickupTrigger);
                    }
                    else {
                        playerPView.RPC("setTriggerP", PhotonTargets.AllViaServer, playerAnimationHash.pickupTrigger);
                    }


                    this.GetComponent<playerHealthScript>().fullPlayerHealth();

                    //Destroy health box after picking it up//
                    interactingCollider.GetComponentInParent<HealthBoxScript>().turnOffText();

                    PhotonView pView = interactingCollider.GetComponentInParent<PhotonView>();
                    if (pView == null) {
                        Debug.LogError("No PhotonView component found");
                    }
                    else {
                        if (PhotonNetwork.offlineMode) {
                            Destroy(interactingCollider.gameObject);
                        }
                        else {
                            pView.RPC("destroyObject", PhotonTargets.AllBuffered);
                        }

                    }

                    //Destroy(interactingCollider.gameObject);
                    inRangeOfHealth = false;
                }
            }
			//can only pick up health if player's health is not full
			
	
			//IF THE PLAYER IS IN RANGE OF PISTOL - PICK IT UP
			if (inRangeOfPistol)
			{
				//If the player already has a pistol, just pickup pistol ammo.
				if (playerData.pistolPickedUp)
				{
					soundController.GetComponent<soundControllerScript>().playPickupSound(transform.position);
					GameObject pistol = GameObject.FindGameObjectWithTag(Tags.PISTOL); //find the pistol object
	
					//pickup ammo for the pistol
					//amount randomly generate - from 10 - 30 bullets picked up
					playerData.pistolGameObject.GetComponent<weaponDataScript>().ammoPickup(Random.Range(10, 30)); 
				}
				else 
				{
					playerData.pistolPickedUp = true;	
					soundController.GetComponent<soundControllerScript>().playPickupSound(transform.position);				
	
					//if this is the only gun that the player now has - enable it
					if (!playerData.machineGunPickedUp)
					{
						playerData.pistolEquipped = true;
						this.GetComponent<weaponSwitchScript>().enableWeapon();
						this.GetComponent<ShootingScript>().loadNewWeapon("Pistol");
					}
				}

                //Destroy the pistol game object//				
                //interactingCollider.GetComponentInParent<weaponOnMapScript>().turnOffText();//handled in script
                //Destroy(interactingCollider.gameObject);
               


                PhotonView pView = interactingCollider.GetComponentInParent<PhotonView>();
	            if (pView == null) {
	                Debug.LogError("No PhotonView component found");
	            }
	            else {
	                if (PhotonNetwork.offlineMode) {
	                    Destroy(interactingCollider.gameObject);

	                }
	                else {
	                    pView.RPC("destroyObject", PhotonTargets.AllBuffered);
                        
	                }
	            }

                //this calls the fx rpc for the other client
                /*if (gameObject.tag == Tags.PLAYER1) {
                    GameObject player2 = GameObject.FindGameObjectWithTag(Tags.PLAYER2);
                    if (player2 != null) {
                        player2.GetComponent<PhotonView>().RPC("pistolEquipped", PhotonTargets.OthersBuffered);
                    }
                }
                else if (gameObject.tag == Tags.PLAYER2) {
                    GameObject player1 = GameObject.FindGameObjectWithTag(Tags.PLAYER1);
                    if (player1 != null) {
                        player1.GetComponent<PhotonView>().RPC("pistolEquipped", PhotonTargets.OthersBuffered);
                    }
                }*/

                
                gameObject.GetComponent<PhotonView>().RPC("pistolEquipped", PhotonTargets.OthersBuffered);
                    

                inRangeOfPistol = false;
			}
	
			//IF THE PLAYER IS IN RANGE OF MACHINE GUN - PICK IT UP
			if (inRangeOfMachineGun)
			{
				//If the player already has a machine gun, just pickup the ammo
				if (playerData.machineGunPickedUp)
				{
	
					soundController.GetComponent<soundControllerScript>().playPickupSound(transform.position);
					GameObject machineGun = GameObject.FindGameObjectWithTag("MachineGun"); //find the pistol object					
	
					//pickup ammo for the machine gun
					//amount randomly generate - from 10 - 50 bullets picked up
					playerData.machineGunGameObject.GetComponent<weaponDataScript>().ammoPickup(Random.Range(10, 50)); 
				}
				else
				{
					playerData.machineGunPickedUp = true;
					soundController.GetComponent<soundControllerScript>().playPickupSound(transform.position);
						
					//if this is the only gun that the player now has - enable it
					if (!playerData.pistolPickedUp)
					{
						playerData.machineGunEquipped = true;
						this.GetComponent<weaponSwitchScript>().enableWeapon();
						this.GetComponent<ShootingScript>().loadNewWeapon("MachineGun");
					}
				}
	
				//Destroy the machine gun game object//
				interactingCollider.GetComponentInParent<weaponOnMapScript>().turnOffText();
				Destroy(interactingCollider.gameObject);
				inRangeOfMachineGun = false;
			}
			
			//IF THE PLAYER IS IN RANGE OF THE KEYS - PICK THEM UP
			if (inRangeOfKeys)
			{
				playerData.hasKey = true;
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
		
		//IF PLAYER IN RANGE OF KEYS
		if (other.tag == "Keys")
		{
			inRangeOfKeys = true;
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
		
		//IF PLAYER NOT IN RANGE OF KEYS
		if (other.tag == "Keys")
		{
			inRangeOfKeys = false;
		}
	}

}
