using UnityEngine;
using System.Collections;

public class ZombieAnimationController : MonoBehaviour {

	private Animator animator;
	private EnemyHashScript hash;


	// Use this for initialization
	void Start () {
		animator = this.gameObject.GetComponent<Animator>();
		hash = this.gameObject.GetComponent<EnemyHashScript>();
		resetBooleans();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void chooseStartingState(){
		int choice = getRandomInt(4);
		//need to handle dead body spawn if we are in biting state
		setInteger(hash.stateDInt,choice);
		Debug.Log("random starting state set to" + choice + " at " + Time.timeSinceLevelLoad);
	}

	public void setStartState(int value){
		setInteger(hash.stateDInt,value);
		Debug.Log("starting state set to" + value+ " at " + Time.timeSinceLevelLoad);
	}


	public void setBoolean(int name, bool value){
		animator.SetBool(name, value);
		Debug.Log("boolean " + name + " set to " + value+ " at " + Time.timeSinceLevelLoad);
	}

	public bool setRandomBoolean(int name){
		int choice = getRandomInt(2);
		if (choice == 0){
			animator.SetBool(name, true);
			Debug.Log("random boolean " + name + " set to " + true+ " at " + Time.timeSinceLevelLoad);
			return true;
		}
		else{
			animator.SetBool(name, false);
			Debug.Log("random boolean " + name + " set to " + false+ " at " + Time.timeSinceLevelLoad);
			return false;
		}

	}
	
	public int setRandomInteger(int name, int max){
		int choice = getRandomInt(max);
		animator.SetInteger(name, choice);
		Debug.Log("random int " + name + " set to " + choice+ " at " + Time.timeSinceLevelLoad);
		return choice;
	}

	public void setInteger(int name, int value){
		animator.SetInteger(name, value);
		Debug.Log("int " + name + " set to " + value+ " at " + Time.timeSinceLevelLoad);
	}

	public void setFloat(int name, float value){
		animator.SetFloat(name, value);
		Debug.Log("float " + name + " set to " + value+ " at " + Time.timeSinceLevelLoad);

	}

	public void setTrigger(int name){
		animator.SetTrigger(name);
	}

	//gets a random number between 0 and max-1
	public int getRandomInt(int max){
		return Random.Range(0,max);
	}

	public void resetBooleans(){
		Debug.Log("booleans reset"+ " at " + Time.timeSinceLevelLoad);
		animator.SetBool(hash.searchingBool,false);
		animator.SetBool(hash.wanderingBool,false);
		animator.SetBool(hash.shotBool,false);
		animator.SetBool(hash.attackingBool,false);
		animator.SetBool(hash.alertedBool,false);
		animator.SetBool(hash.deadBool,false);
		animator.SetBool(hash.changeBool,false);
		animator.SetBool(hash.wakeupBool,false);
		animator.SetBool(hash.chargeBool,false);

	}

}
