using UnityEngine;
using System.Collections;

public class ZombieAnimationController : MonoBehaviour {

	private Animator animator;
	private EnemyHashScript hash;


	public bool debug;

	// Use this for initialization
	void Awake () {
		animator = this.gameObject.GetComponent<Animator>();
		hash = this.gameObject.GetComponent<EnemyHashScript>();

	}
	
	
	public void setTrigger(int name){
		if (debug)Debug.Log("trigger " + name + " activated at " + Time.timeSinceLevelLoad);
		animator.SetTrigger(name);
	}


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

	public void chooseStartingState(){
		int choice = getRandomInt(4);
		//need to handle dead body spawn if we are in biting state
		setInteger(hash.stateDInt,choice);
		if (debug)Debug.Log("random starting state set to" + choice + " at " + Time.timeSinceLevelLoad);
	}

	public void setStartState(int value){
		setInteger(hash.stateDInt,value);
		if (debug)Debug.Log("starting state set to" + value+ " at " + Time.timeSinceLevelLoad);
	}


	public void setBoolean(int name, bool value){
		animator.SetBool(name, value);
		if (debug)Debug.Log("boolean " + name + " set to " + value+ " at " + Time.timeSinceLevelLoad);
	}

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
	
	public int setRandomInteger(int name, int max){
		int choice = getRandomInt(max);
		animator.SetInteger(name, choice);
		if (debug)Debug.Log("random int " + name + " set to " + choice+ " at " + Time.timeSinceLevelLoad);
		return choice;
	}

	public void setInteger(int name, int value){
		animator.SetInteger(name, value);
		if (debug)Debug.Log("int " + name + " set to " + value+ " at " + Time.timeSinceLevelLoad);
	}

	public void setFloat(int name, float value){
		animator.SetFloat(name, value);
		if (debug)Debug.Log("float " + name + " set to " + value+ " at " + Time.timeSinceLevelLoad);

	}


	//gets a random number between 0 and max-1
	public int getRandomInt(int max){
		return Random.Range(0,max);
	}

	public void resetBooleans(){
		if (debug)Debug.Log("booleans reset"+" at " + Time.timeSinceLevelLoad);
		animator.SetBool(hash.searchingBool,false);
		animator.SetBool(hash.wanderingBool,false);
		animator.SetBool(EnemyHashScript.attackingBool,false);
	}


	public bool checkAnimationState(int stateID){
		return animator.GetCurrentAnimatorStateInfo(0).nameHash == stateID;
	}


}
