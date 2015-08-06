using UnityEngine;
using System.Collections;

public class weaponSwitchScript : MonoBehaviour {

	playerDataScript playerData;
	GameObject pistolGameObject;
	GameObject machineGunGameObject;

	void Start()
	{
		playerData = this.GetComponent<playerDataScript>();
		pistolGameObject = GameObject.FindGameObjectWithTag("Pistol");
		machineGunGameObject = GameObject.FindGameObjectWithTag("MachineGun");
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
			weaponStr = "machineGun";
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
			machineGunGameObject.gameObject.SetActive(false);
			pistolGameObject.gameObject.SetActive(false);			
		}

		//If the pistol is equipped...
		else if (currentWeapon() == "Pistol")
		{
			print ("ENABLING PISTOL!");
			machineGunGameObject.SetActive(false);
			pistolGameObject.SetActive(true);
		}

		//If the machine gun is equipped...
		else if (currentWeapon() == "machineGun")
		{
			pistolGameObject.SetActive(false);
			machineGunGameObject.SetActive(true);
		}
	}
}
