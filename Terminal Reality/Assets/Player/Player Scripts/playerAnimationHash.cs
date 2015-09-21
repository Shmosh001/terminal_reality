using UnityEngine;
using System.Collections;

public class playerAnimationHash : MonoBehaviour {

	//TRIGGERS//
	
	//arms//
	public static int noWeaponTrigger;	
	public static int pistolTrigger;
	public static int machineGunTrigger;
	public static int reloadTrigger;
	
	//movement//
	public static int standingTrigger;
	public static int walkingTrigger;
	public static int sprintingTrigger;
	public static int sneakingTrigger;
	
	//STATES//
	//arms//
	public static int noWeaponState;	
	public static int pistolState;
	public static int machineGunState;
	public static int reloadState;
	
	//movement//
	public static int standingState;
	public static int walkingState;
	public static int sprintingState;
	public static int sneakingState;

	void Awake()
	{
		//ANIMATOR PARAMETERS//
		
		//arms//
		noWeaponTrigger = Animator.StringToHash("NoWep");
		pistolTrigger = Animator.StringToHash("Pistol");
	
		//STATES//
		
		//arms - top//
		noWeaponState = Animator.StringToHash("Top.No weapon");
		pistolState = Animator.StringToHash("Top.Pistol Aim");
	}
}
