﻿using UnityEngine;
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
		noWeaponBool = Animator.StringToHash("NoWeapon");
		pistolTrigger = Animator.StringToHash("Pistol");
		
		//movement//
		forwardSpeedFloat = Animator.StringToHash("Speed");
	
		//STATES//
		
		//arms - top//
		noWeaponState = Animator.StringToHash("Top.No weapon");
		pistolState = Animator.StringToHash("Top.Pistol Aim");
		
		//movement//
		standingState = Animator.StringToHash("Base Layer.stand_idle");
		walkingState = Animator.StringToHash("Base Layer.walk");
	}
}