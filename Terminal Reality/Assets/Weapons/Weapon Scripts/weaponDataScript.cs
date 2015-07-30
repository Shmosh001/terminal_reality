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
	public Light flareLight;

	//PRIVATE VARIABLES FOR THE WEAPON//
	private int ammoInClip;
	private bool equipped;

	//AUDIO VARIABLES//
	public AudioClip shotSound;
	public AudioClip reloadSound;
	private AudioSource soundSource;

	void Awake()
	{
		ammoInClip = clipSize;
		soundSource = this.GetComponent<AudioSource>();
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

	/*
	 * METHODS FOR PLAYING GUN RELATED SOUNDS
	 */
	 //PLAY EMPTY CLIP SOUND//
	public void playEmptyClip()
	{
	}

	//PLAY SHOOTING SOUND//
	public void playShot()
	{
		soundSource.PlayOneShot(shotSound);
	}

	//PLAY RELOAD SOUND//
	public void playReload()
	{
		soundSource.PlayOneShot(reloadSound);
	}

	//METHOD TO ENABLE AND DISABLE GUN FLARE//
	public void gunFlare(bool state)
	{
		flareLight.enabled = state;
	}



}
