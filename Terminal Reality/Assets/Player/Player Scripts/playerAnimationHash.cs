using UnityEngine;
using System.Collections;

public class playerAnimationHash : MonoBehaviour {

	//TRIGGERS//
	
	//arms//
	public static int noWeaponBool;	
	public static int pistolBoolean;
	public static int machineGunTrigger;
	public static int reloadTrigger;
	
	//movement//
	public static int forwardSpeedFloat;
	public static int jumpTrigger;
	
	//pickup//
	public static int pickupTrigger;
	
	//dying//
	public static int dieTrigger;
	

	void Awake()
	{
		//ANIMATOR PARAMETERS//
		
		//arms//
		noWeaponBool = Animator.StringToHash("noWeapon");
		pistolBoolean = Animator.StringToHash("Pistol");
		machineGunTrigger = Animator.StringToHash("MachineGun");
		
		//movement//
		forwardSpeedFloat = Animator.StringToHash("Speed");
		jumpTrigger = Animator.StringToHash("jump");
		
		//pickup//
		pickupTrigger = Animator.StringToHash("pickup");
		
		//dying//
		dieTrigger = Animator.StringToHash("died");

        reloadTrigger = Animator.StringToHash("Reload");
	
	}
}
