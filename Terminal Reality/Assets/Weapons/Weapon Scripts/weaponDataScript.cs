using UnityEngine;
using System.Collections;

public class weaponDataScript : ammoHUDScript
{
	//PUBLIC VARIABLES FOR THE WEAPON//
	public float damage;
	public float rateOfFire;
	public bool singleFire;
	public int ammo;
	public int clipSize;	
	public Light flareLight;
	public int maxAmmo;

	//PRIVATE VARIABLES FOR THE WEAPON//
	private int ammoInClip;
	private bool equipped;

//	//AUDIO VARIABLES//
//	public AudioClip shotSound;
//	public AudioClip reloadSound;
//	public AudioClip outOfAmmoSound;
//	public AudioSource soundSource;

	void Awake()
	{
		ammoInClip = clipSize;
		//soundSource = this.GetComponent<AudioSource>();
	}

	//GET REMAINING AMMO//
	public int getRemainingAmmo()
	{
		return ammo;
	}

	//GET AMOUNT LEFT IN CLIP//
	public int getRemainingClip()
	{
		return ammoInClip;
	}

	//REDUCE AMMO AFTER SHOT TAKEN//
	public void reduceAmmo()
	{
		ammoInClip--;
	}

	//RELOADING//
	//return bool if reload successful
	public bool reload()
	{
		//if there is more ammo than the size of the clip
		// and the clip is empty
		//do a normal reload
		if (ammo >= clipSize && ammoInClip == 0)
		{
			ammoInClip = clipSize;
			ammo -= clipSize;
			return true;
		}
		//if there is ammo, but not enough to fill the clip
		//and the clip is empty
		else if (ammo > 0 && ammo < clipSize && ammoInClip == 0)
		{
			ammoInClip = ammo;
			ammo = 0;
			return true;
		}
		//if there is more ammo than the size of the clip
		// and the clip is NOT empty
		else if (ammo >= clipSize && ammoInClip > 0)
		{
			int diff = clipSize - ammoInClip;
			ammoInClip = clipSize;
			ammo -= diff;
			return true;
		}
		//if there is ammo, but not enough to fill the clip
		//and the clip is empty
		else if (ammo > 0 && ammo < (clipSize - ammoInClip) && ammoInClip > 0)
		{
			ammoInClip += ammo;
			ammo = 0;
			return true;
		}
		else
		{
			return false;
		}
	}
	
	//WHEN PICKING UP AMMO AND ADDING IT TO TOTAL AMMO//
	public void ammoPickup (int pickupAmount)
	{
		//If there is more ammo in box than there is to full to max ammo
		if (pickupAmount > (maxAmmo - ammo))
		{
			ammo = maxAmmo; //set ammo to max ammo
		}

		//if there is less ammo in the ammo box than there is needed to full max ammo
		if (pickupAmount < (maxAmmo - ammo))
		{
			ammo += pickupAmount;
		}

		//RUN THE UPDATE AMMO HUD TEXT METHOD - method in ammoHUDScript//
		updateAmmoText(ammo, ammoInClip);
	}


	//METHOD TO ENABLE AND DISABLE GUN FLARE//
	public void gunFlare(bool state)
	{
		flareLight.enabled = state;
	}



}
