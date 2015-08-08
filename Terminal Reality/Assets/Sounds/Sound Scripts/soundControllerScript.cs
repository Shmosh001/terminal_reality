using UnityEngine;
using System.Collections;

public class soundControllerScript : MonoBehaviour {

	//PUBLIC SOUND VARIABLES//
	public AudioClip pistolShotSound;
	public AudioClip machineGunShotSound;
	public AudioClip reloadSound;
	public AudioClip outOfAmmoSound;
	public AudioClip ammoAndWeaponPickupSound;


	// Use this for initialization
	void Start () {
	
	}
	
	//PLAY EMPTY CLIP SOUND//
	public void playEmptyClip()
	{
		//Get the audio source for the current weapon
		AudioSource soundSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
		//play sound
		soundSource.PlayOneShot(outOfAmmoSound);
	}
	
	//PLAY PISTOL SHOOTING SOUND//
	public void playPistolShot()
	{
		//Get the audio source for the current weapon
		AudioSource soundSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
		//play sound
		soundSource.PlayOneShot(pistolShotSound);
	}
	
	//PLAY RELOAD SOUND//
	public void playReload()
	{
		//Get the audio source for the current weapon
		AudioSource soundSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
		//play sound
		soundSource.PlayOneShot(reloadSound);
	}
	
	//PLAY AMMO AND WEAPON PICKUP SOUND//
	public void playPickupSound()
	{
		//Get the audio source for the current weapon
		AudioSource soundSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
		//play sound
		soundSource.PlayOneShot(ammoAndWeaponPickupSound);
	}
}
