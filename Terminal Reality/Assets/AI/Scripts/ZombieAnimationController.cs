 using UnityEngine;
using System.Collections;

public class ZombieAnimationController : MonoBehaviour {

	private Animator animator;




	// Use this for initialization
	void Start () {
		animator = this.gameObject.GetComponent<Animator>();
		resetBooleans();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void chooseStartingState(){
		int choice = getRandomInt(4);
		//need to handle dead body spawn if we are in biting state
		setInteger("stateD",choice);
		Debug.Log("random starting state set to" + choice + " at " + Time.timeSinceLevelLoad);
	}

	public void setStartState(int value){
		setInteger("StateD",value);
		Debug.Log("starting state set to" + value+ " at " + Time.timeSinceLevelLoad);
	}


	public void setBoolean(string name, bool value){
		animator.SetBool(name, value);
		Debug.Log("boolean " + name + " set to " + value+ " at " + Time.timeSinceLevelLoad);
	}

	public bool setRandomBoolean(string name){
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
	
	public int setRandomInteger(string name, int max){
		int choice = getRandomInt(max);
		animator.SetInteger(name, choice);
		Debug.Log("random int " + name + " set to " + choice+ " at " + Time.timeSinceLevelLoad);
		return choice;
	}

	public void setInteger(string name, int value){
		animator.SetInteger(name, value);
		Debug.Log("int " + name + " set to " + value+ " at " + Time.timeSinceLevelLoad);
	}

	public void setFloat(string name, float value){
		animator.SetFloat(name, value);
		Debug.Log("float " + name + " set to " + value+ " at " + Time.timeSinceLevelLoad);

	}

	//gets a random number between 0 and max-1
	public int getRandomInt(int max){
		return Random.Range(0,max);
	}

	public void resetBooleans(){
		Debug.Log("booleans reset"+ " at " + Time.timeSinceLevelLoad);
		animator.SetBool("Searching",false);
		animator.SetBool("Wandering",false);
		animator.SetBool("Shot",false);
		animator.SetBool("Attacking",false);
		animator.SetBool("Alerted",false);
		animator.SetBool("Dead",false);
		animator.SetBool("ChangeBool",false);
		animator.SetBool("Wakeup",false);
		animator.SetBool("Charge",false);
		animator.SetBool("Chasing",false);
	}

}
