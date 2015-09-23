using UnityEngine;
using System.Collections;

public class weaponSwitchScript : ammoHUDScript {

	playerDataScript playerData;
	
	//the animator
	private Animator animator;
    private PhotonView pView;
    private playerAnimatorSync animSync;

	void Start()
	{
		playerData = this.GetComponent<playerDataScript>();
	    animator = this.gameObject.GetComponent<Animator>();
        animSync = this.gameObject.GetComponent<playerAnimatorSync>();
        pView = this.gameObject.GetComponent<PhotonView>();
        enableWeapon();
	}

	//1 PRESSED - SWITCH TO PISTOL//
	public bool switchToPistol()
	{
		bool successfulSwitch = true;

		//if the player has a pistol to switch to
		if (playerData.pistolPickedUp)
		{
			//if the pistol is not currently equipped
			if (!playerData.pistolEquipped)
			{
				playerData.pistolEquipped = true; 

				if (playerData.machineGunEquipped)
				{
					playerData.machineGunEquipped = false;
				}
				
				enableWeapon(); //activate the pistol

			}
			else //weapon is already equipped and no need to switch
			{
				successfulSwitch = false;
			}
		}
		else //play a sound when player doesn't have a weapon
		{
			successfulSwitch = false;
		}

		return successfulSwitch;
	}

	//2 PRESSED - SWITCH TO MACHINE GUN//
	public bool switchToMachineGun()
	{
		bool successfulSwitch = true;

		//if the player has a machine gun to switch to
		if (playerData.machineGunPickedUp)
		{
			//if the machine gun is not currently equipped
			if (!playerData.machineGunEquipped)
			{
				playerData.machineGunEquipped = true;

				if (playerData.pistolEquipped)
				{
					playerData.pistolEquipped = false;
				}
				
				enableWeapon(); //activate the machine gun
			}
			else //weapon is already equipped and no need to switch
			{
				successfulSwitch = false;
			}
		}
		else //play a sound when player doesn't have a weapon
		{
			successfulSwitch = false;
		}

		return successfulSwitch;
	}

	//RETURNS A STRING OF THE CURRENT WEAPON EQUIPPED BY THE PLAYER//
	public string currentWeapon()
	{
		string weaponStr = "";
		
		if (playerData.pistolEquipped)
		{
			//animator.SetTrigger(playerAnimationHash.pistolTrigger);
			//animator.SetBool(playerAnimationHash.noWeaponBool, false);
            if (PhotonNetwork.offlineMode) {
                animator.SetTrigger(playerAnimationHash.pistolTrigger);
            }
            else {
                pView.RPC("setTriggerP", PhotonTargets.AllViaServer, playerAnimationHash.pistolTrigger);
            }

            if (PhotonNetwork.offlineMode) {
                animator.SetBool(playerAnimationHash.noWeaponBool, false);
            }
            else {
                pView.RPC("setBooleanP", PhotonTargets.AllViaServer, playerAnimationHash.noWeaponBool, false);
            }

            weaponStr = "Pistol";
		}
		else if (playerData.machineGunEquipped)
		{
			//animator.SetTrigger(playerAnimationHash.machineGunTrigger);
			//animator.SetBool(playerAnimationHash.noWeaponBool, false);

            if (PhotonNetwork.offlineMode) {
                animator.SetTrigger(playerAnimationHash.machineGunTrigger);
            }
            else {
                pView.RPC("setTriggerP", PhotonTargets.AllViaServer, playerAnimationHash.machineGunTrigger);
            }

            if (PhotonNetwork.offlineMode) {
                animator.SetBool(playerAnimationHash.noWeaponBool, false);
            }
            else {
                pView.RPC("setBooleanP", PhotonTargets.AllViaServer, playerAnimationHash.noWeaponBool, false);
            }




            weaponStr = "MachineGun";
		}
		else if (!playerData.pistolEquipped && !playerData.machineGunEquipped)
		{
			weaponStr = "nothing";
		}
		
		return weaponStr;
	}

	//MAKE THE WEAPON SWITCHED TO VISIBLE//
	public void enableWeapon()
	{
		
		//If no weapon is equipped...
		if (currentWeapon() == "nothing")
		{
           
            playerData.machineGunGameObject.gameObject.SetActive(false);
            
            playerData.pistolGameObject.gameObject.SetActive(false);
			
			//After switch, update ammo HUD
			updateAmmoText(0,0);			
		}

		//If the pistol is equipped...
		else if (currentWeapon() == "Pistol")
		{
			playerData.machineGunGameObject.SetActive(false);
			playerData.pistolGameObject.SetActive(true);
			
			//After switch, update ammo HUD
            if (playerData.pistolGameObject == null) {
                Debug.LogError("Pistol gameobject is null");
                return;
            }

            if (playerData.pistolGameObject.GetComponent<weaponDataScript>() == null) {
                Debug.LogError("Pistol gameobject.weapondatascript is null");
                return;
            }

            

            print(playerData.pistolGameObject.GetComponent<weaponDataScript>().getRemainingClip());
            print(playerData.pistolGameObject.GetComponent<weaponDataScript>().getRemainingAmmo());

            updateAmmoText(playerData.pistolGameObject.GetComponent<weaponDataScript>().getRemainingAmmo(), 
			               playerData.pistolGameObject.GetComponent<weaponDataScript>().getRemainingClip());

			//check reload warnings
			checkReloadWarning(playerData.pistolGameObject.GetComponent<weaponDataScript>().getRemainingClip(),
			                   playerData.pistolGameObject.GetComponent<weaponDataScript>().clipSize,
			                   playerData.pistolGameObject.GetComponent<weaponDataScript>().getRemainingAmmo());
		}

		//If the machine gun is equipped...
		else if (currentWeapon() == "MachineGun")
		{
			playerData.pistolGameObject.SetActive(false);
			playerData.machineGunGameObject.SetActive(true);
			
			//After switch, update ammo HUD
			updateAmmoText(playerData.machineGunGameObject.GetComponent<weaponDataScript>().getRemainingAmmo(), 
			               playerData.machineGunGameObject.GetComponent<weaponDataScript>().getRemainingClip());

			//check reload warnings
			checkReloadWarning(playerData.machineGunGameObject.GetComponent<weaponDataScript>().getRemainingClip(),
			                   playerData.machineGunGameObject.GetComponent<weaponDataScript>().clipSize,
			                   playerData.machineGunGameObject.GetComponent<weaponDataScript>().getRemainingAmmo());
		}
		
	}
}
