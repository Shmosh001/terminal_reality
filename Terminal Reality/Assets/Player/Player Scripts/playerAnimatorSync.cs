﻿using UnityEngine;
using System.Collections;

public class playerAnimatorSync : MonoBehaviour {

	//here we simply force the photon view to sync the seperate layers as we can't set this before run time
    //because the animator is not instaiated at this time


    //the animator
    private Animator animator;




    /// <summary>
    /// initialization
    /// </summary>
    void Awake() {
        animator = this.gameObject.GetComponent<Animator>();
       
    }

    /// <summary>
    /// sets a trigger 
    /// </summary>
    /// <param name="name">
    /// hash of the trigger
    /// </param>

    public void setTriggerP(int name) {
        animator.SetTrigger(name);
    }






    /// <summary>
    /// sets a boolean to true/false
    /// </summary>
    /// <param name="name">
    /// hash of boolean
    /// </param>
    /// <param name="value">
    /// value
    /// </param>
 
    public void setBooleanP(int name, bool value) {
        animator.SetBool(name, value);
    }

   




    /// <summary>
    /// sets and integer parameter to a specified value
    /// </summary>
    /// <param name="name">
    /// param hash
    /// </param>
    /// <param name="value">
    /// value
    /// </param>

    public void setIntegerP(int name, int value) {
        animator.SetInteger(name, value);
    }

    /// <summary>
    /// sets and float parameter to a specified value
    /// </summary>
    /// <param name="name">
    /// param hash
    /// </param>
    /// <param name="value">
    /// value
    /// </param>

    public void setFloatP(int name, float value) {
        animator.SetFloat(name, value);

    }



    /// <summary>
    /// checks if the animator is in a state
    /// </summary>
    /// <param name="stateID">
    /// state we are checking for
    /// </param>
    /// <returns>
    /// true/false based on above
    /// </returns>
	public bool checkAnimationStateP(int stateID) {
        return animator.GetCurrentAnimatorStateInfo(0).nameHash == stateID;
    }

    /// <summary>
    /// force the animator to change to a animation
    /// </summary>
    /// <param name="animation">
    /// animation hash
    /// </param>

    public void forceAnimationP(int animation) {
        animator.Play(animation);
    }

    /// <summary>
    /// turns off root motion of animator
    /// </summary>
	public void turnOffRMP() {
        animator.applyRootMotion = false;
    }




}
