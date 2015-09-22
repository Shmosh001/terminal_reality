using UnityEngine;
using System.Collections;

public class playerAnimationHash : MonoBehaviour {

	//TRIGGERS//
	
	//arms//
	public static int noWeaponBool;	
	public static int pistolTrigger;
	public static int machineGunTrigger;
	public static int reloadTrigger;
	
	//movement//
	public static int forwardSpeedFloat;
	
	//pickup//
	public static int pickupTrigger;
	

	void Awake()
	{
		//ANIMATOR PARAMETERS//
		
		//arms//
		noWeaponBool = Animator.StringToHash("NoWeapon");
		pistolTrigger = Animator.StringToHash("Pistol");
		
		//movement//
		forwardSpeedFloat = Animator.StringToHash("Speed");
		
		//pickup//
		pickupTrigger = Animator.StringToHash("pickup");
	
	}
}
