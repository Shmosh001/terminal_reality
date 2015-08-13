using UnityEngine;
using System.Collections;

public class soundControllerScript : MonoBehaviour {
	

	//PUBLIC PLAYER SOUND VARIABLES//
	public AudioClip pistolShotSound;
	public AudioClip machineGunShotSound;
	public AudioClip reloadSound;
	public AudioClip outOfAmmoSound;
	public AudioClip ammoAndWeaponPickupSound;


	//PUBLIC ZOMBIE SOUND VARIABLES//
	public AudioClip maleScreamingSound;
	public AudioClip femaleScreamingSound;


	// Use this for initialization
	void Start () {

	}
	
	//PLAY EMPTY CLIP SOUND//
	public void playEmptyClip(AudioSource soundSource)
	{
		//play sound
		soundSource.PlayOneShot(outOfAmmoSound);
	}
	
	//PLAY PISTOL SHOOTING SOUND//
	public void playPistolShot(AudioSource soundSource)
	{
		//play sound
		soundSource.PlayOneShot(pistolShotSound);
	}
	
	//PLAY RELOAD SOUND//
	public void playReload(AudioSource soundSource)
	{
		//play sound
		soundSource.PlayOneShot(reloadSound);
	}
	
	//PLAY AMMO AND WEAPON PICKUP SOUND//
	public void playPickupSound(AudioSource soundSource)
	{
		//play sound
		soundSource.PlayOneShot(ammoAndWeaponPickupSound);
	}

	//PLAY SCREAM FOR MALE ZOMBIE//
	public void playMaleScream(AudioSource soundSource){
		soundSource.PlayOneShot(maleScreamingSound);
	}

	//PLAY SCREAM FOR FEMALE ZOMBIE//
	public void playFemaleScream(AudioSource soundSource){
		soundSource.PlayOneShot(femaleScreamingSound);
	}
}
