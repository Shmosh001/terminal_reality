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
	public AudioClip doorCreekSound;
	public AudioClip glassBreakingSound;
    public AudioClip windSound;


	// Use this for initialization
	void Start () {
		
	}
	
	//PLAY LOW HEALTH HEART BEAT//
	public void playLowHealthHeartBeat(Vector3 position)
	{
        //play sound
        //soundSource.PlayOneShot(lowHealthHeartBeat);
        AudioSource.PlayClipAtPoint(lowHealthHeartBeat, position);
    }
	
	//PLAY EMPTY CLIP SOUND//
	public void playEmptyClip(Vector3 position)
	{
        //play sound
        //soundSource.PlayOneShot(outOfAmmoSound);
        AudioSource.PlayClipAtPoint(outOfAmmoSound, position);
    }

    //PLAY PISTOL SHOOTING SOUND//
    public void playPistolShot(Vector3 position)
	{
        //play sound
        //soundSource.PlayOneShot(pistolShotSound);
        AudioSource.PlayClipAtPoint(pistolShotSound, position);
    }

    //PLAY RELOAD SOUND//
    public void playReload(Vector3 position)
	{
        //play sound
        //soundSource.PlayOneShot(reloadSound);
        AudioSource.PlayClipAtPoint(reloadSound, position);
    }

    //PLAY AMMO AND WEAPON PICKUP SOUND//
    public void playPickupSound(Vector3 position)
	{
        //play sound
        //soundSource.PlayOneShot(ammoAndWeaponPickupSound);
        AudioSource.PlayClipAtPoint(ammoAndWeaponPickupSound, position);
    }

    //PLAY TORCH ON/OFF SOUND//
    public void playTorchSound(Vector3 position)
	{
        //play sound
        //soundSource.PlayOneShot(torchButtonSound);
        AudioSource.PlayClipAtPoint(torchButtonSound, position);
    }

    //PLAY SCREAM FOR MALE ZOMBIE//
    public void playMaleScream(Vector3 position){
        //soundSource.PlayOneShot(maleScreamingSound);
        AudioSource.PlayClipAtPoint(maleScreamingSound, position);
    }

    //PLAY SCREAM FOR FEMALE ZOMBIE//
    public void playFemaleScream(Vector3 position){
        //soundSource.PlayOneShot(femaleScreamingSound);
        AudioSource.PlayClipAtPoint(femaleScreamingSound, position);
    }

    //PLAY SCREAM FOR FEMALE ZOMBIE//
    public void playSparkSound(Vector3 position){
        //soundSource.PlayOneShot(sparkSound);
        AudioSource.PlayClipAtPoint(sparkSound, position);
    }


    //DOOR CREEK SOUND
    public void playDoorCreek(Vector3 position)
	{
        //soundSource.PlayOneShot(doorCreekSound);
        AudioSource.PlayClipAtPoint(doorCreekSound, position);
    }

    //GLASS BREAKING SOUND
    public void playGlassBreaking(Vector3 position)
	{
        //soundSource.PlayOneShot(glassBreakingSound);
        AudioSource.PlayClipAtPoint(glassBreakingSound, position);
    }

    //WIND SOUND
    public void playWindSound(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(windSound, position);
    }



    //PLAY SCREAM FOR BOSS ZOMBIE//
    public void playBossScreamSound(Vector3 position) {
        
        AudioSource.PlayClipAtPoint(bossZombieScream, position);
    }

    //PLAY DEATH CLIP FOR BOSS ZOMBIE//
    public void playBossDeathSound(Vector3 position) {
        //soundSource.PlayOneShot(bossZombieDeath);
        AudioSource.PlayClipAtPoint(bossZombieDeath, position);
    }
}

