using UnityEngine;
using System.Collections;

public class weaponSwitchScript : ammoHUDScript {

	playerDataScript playerData;

	void Start()
	{
		playerData = this.GetComponent<playerDataScript>();
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
			weaponStr = "Pistol";
		}
		else if (playerData.machineGunEquipped)
		{
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
