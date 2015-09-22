using UnityEngine;
using System.Collections;

public class soundControllerScript : MonoBehaviour {
	
	
	//PUBLIC PLAYER SOUND VARIABLES//
	public AudioClip pistolShotSound;
	public AudioClip machineGunShotSound;
	public AudioClip reloadSound;
	public AudioClip outOfAmmoSound;
	public AudioClip ammoAndWeaponPickupSound;
	public AudioClip torchButtonSound;
	public AudioClip lowHealthHeartBeat;
	
	
	//PUBLIC ZOMBIE SOUND VARIABLES//
	public AudioClip maleScreamingSound;
	public AudioClip femaleScreamingSound;
    public AudioClip bossZombieScream;
    public AudioClip bossZombieDeath;
	
	//PUBLIC LEVEL SOUNND VARIABLES
	public AudioClip sparkSound;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	//PLAY LOW HEALTH HEART BEAT//
	public void playLowHealthHeartBeat(AudioSource soundSource)
	{
		//play sound
		soundSource.PlayOneShot(lowHealthHeartBeat);
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
		print ("PISTOL SHOT!!!");
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
	
	//PLAY TORCH ON/OFF SOUND//
	public void playTorchSound(AudioSource soundSource)
	{
		//play sound
		soundSource.PlayOneShot(torchButtonSound);
	}
	
	//PLAY SCREAM FOR MALE ZOMBIE//
	public void playMaleScream(AudioSource soundSource){
		soundSource.PlayOneShot(maleScreamingSound);
	}
	
	//PLAY SCREAM FOR FEMALE ZOMBIE//
	public void playFemaleScream(AudioSource soundSource){
		soundSource.PlayOneShot(femaleScreamingSound);
	}
	
	//PLAY SCREAM FOR FEMALE ZOMBIE//
	public void playSparkSound(AudioSource soundSource){
		soundSource.PlayOneShot(sparkSound);
	}

    //PLAY SCREAM FOR BOSS ZOMBIE//
    public void playBossScreamSound(AudioSource soundSource) {
        soundSource.PlayOneShot(bossZombieScream);
    }

    //PLAY DEATH CLIP FOR BOSS ZOMBIE//
    public void playBossDeathSound(AudioSource soundSource) {
        soundSource.PlayOneShot(bossZombieDeath);
    }
}