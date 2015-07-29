using UnityEngine;
using System.Collections;

public class weaponDataScript : MonoBehaviour 
{
	//PUBLIC VARIABLES FOR THE WEAPON//
	public float damage;
	public float rateOfFire;
	public bool singleFire;
	public int ammo;
	public int clipSize;

	//PRIVATE VARIABLES FOR THE WEAPON//
	private int ammoInClip;

	void Awake()
	{
		ammoInClip = clipSize;
	}

	//GET REMAINING AMMO//
	public int getRemainingAmmo()
	{
		return ammo;
	}

	//GET AMOUNT LEFT IN CLIP//
	public int getRemainingClip()
	{
		print (ammoInClip);
		return ammoInClip;
	}

	//REDUCE AMMO AFTER SHOT TAKEN//
	public void reduceAmmo()
	{
		ammoInClip--;
		ammo--;
	}

	//RELOADING//
	//return bool if reload successful
	public bool reload()
	{
		//if there is more ammo than the size of the clip
		//do a normal reload
		if (ammo > clipSize)
		{
			ammoInClip = clipSize;
			ammo -= clipSize;
			return true;
		}
		else
		{
			return false;
		}
	}

}
