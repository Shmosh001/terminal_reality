using UnityEngine;
using System.Collections;

/// <summary>
/// controls all parts of the animation transitions and parameter assignment
/// also handles randomization of animations
/// </summary>
public class ZombieAnimationController : MonoBehaviour {

    //the animator
	private Animator animator;
    //debug
	public bool debug;

    /// <summary>
    /// initialization
    /// </summary>
    void Awake () {
		animator = this.gameObject.GetComponent<Animator>();
	}
	
	/// <summary>
    /// sets a trigger 
    /// </summary>
    /// <param name="name">
    /// hash of the trigger
    /// </param>
	public void setTrigger(int name){
		if (debug)Debug.Log("trigger " + name + " activated at " + Time.timeSinceLevelLoad);
		animator.SetTrigger(name);
	}

    /// <summary>
    /// sets a trigger randomly
    /// </summary>
    /// <param name="name">
    /// hash of the trigger
    /// </param>
    /// <returns>
    /// if it set (true) or not (false)
    /// </returns>
	public bool setRandomTrigger(int name){
		int choice = getRandomInt(2);
		if (choice == 0){
			animator.SetTrigger(name);
			if (debug)Debug.Log("random trigger  set to " + true + " at " + Time.timeSinceLevelLoad);
			return true;
		}
		else{
			if (debug)Debug.Log("random trigger set to " + false+ " at " + Time.timeSinceLevelLoad);
			return false;
		}
	}

    /// <summary>
    /// choses a random starting state
    /// </summary>
	public void chooseStartingState(){
		int choice = getRandomInt(4);
		//TODO need to handle dead body spawn if we are in biting state
		setInteger(EnemyHashScript.stateDInt,choice);
		setRandomInteger(EnemyHashScript.idleDInt,6);
		if (debug)Debug.Log("random starting state set to" + choice + " at " + Time.timeSinceLevelLoad);
	}

    /// <summary>
    /// sets a specific starting state
    /// </summary>
    /// <param name="value">
    /// value to be set
    /// </param>
	public void setStartState(int value){
		setInteger(EnemyHashScript.stateDInt,value);
		if (debug)Debug.Log("starting state set to" + value+ " at " + Time.timeSinceLevelLoad);
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
	public void setBoolean(int name, bool value){
		animator.SetBool(name, value);
		if (debug)Debug.Log("boolean " + name + " set to " + value+ " at " + Time.timeSinceLevelLoad);
	}

    /// <summary>
    /// set a boolean randomly
    /// </summary>
    /// <param name="name">
    /// hash of the boolean
    /// </param>
    /// <returns>
    /// what it was set too
    /// </returns>
	public bool setRandomBoolean(int name){
		int choice = getRandomInt(2);
		if (choice == 0){
			animator.SetBool(name, true);
			if (debug)Debug.Log("random boolean " + name + " set to " + true+ " at " + Time.timeSinceLevelLoad);
			return true;
		}
		else{
			animator.SetBool(name, false);
			if (debug)Debug.Log("random boolean " + name + " set to " + false+ " at " + Time.timeSinceLevelLoad);
			return false;
		}

	}
	
    /// <summary>
    /// sets an integer randomly
    /// </summary>
    /// <param name="name">
    /// hash of the parameter
    /// </param>
    /// <param name="max">
    /// max value of the random number
    /// </param>
    /// <returns>
    /// number it was set to
    /// </returns>
	public int setRandomInteger(int name, int max){
		int choice = getRandomInt(max);
		animator.SetInteger(name, choice);
		if (debug)Debug.Log("random int " + name + " set to " + choice+ " at " + Time.timeSinceLevelLoad);
		return choice;
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
	public void setInteger(int name, int value){
		animator.SetInteger(name, value);
		if (debug)Debug.Log("int " + name + " set to " + value+ " at " + Time.timeSinceLevelLoad);
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
	public void setFloat(int name, float value){
		animator.SetFloat(name, value);
		if (debug)Debug.Log("float " + name + " set to " + value+ " at " + Time.timeSinceLevelLoad);

	}


    /// <summary>
    /// gets a random number between 0 and max-1
    /// </summary>
    /// <param name="max">
    /// max amount
    /// </param>
    /// <returns>
    /// the random int
    /// </returns>
    public int getRandomInt(int max){
		return Random.Range(0,max);
	}

    /// <summary>
    /// resets all booleans to false
    /// </summary>
	public void resetBooleans(){
		if (debug)Debug.Log("booleans reset"+" at " + Time.timeSinceLevelLoad);
		animator.SetBool(EnemyHashScript.wanderingBool,false);
		animator.SetBool(EnemyHashScript.attackingBool,false);
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
	public bool checkAnimationState(int stateID){
		return animator.GetCurrentAnimatorStateInfo(0).nameHash == stateID;
	}

    /// <summary>
    /// force the animator to change to a animation
    /// </summary>
    /// <param name="animation">
    /// animation hash
    /// </param>
	public void forceAnimation(int animation){
		animator.Play(animation);
	}

    /// <summary>
    /// turns off root motion of animator
    /// </summary>
	public void turnOffRM(){
		animator.applyRootMotion = false;
	}

}
