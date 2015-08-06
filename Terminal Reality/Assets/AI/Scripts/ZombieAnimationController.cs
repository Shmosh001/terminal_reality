 using UnityEngine;
using System.Collections;

public class ZombieAnimationController : MonoBehaviour {

	private Animator animator;




	// Use this for initialization
	void Start () {
		animator = this.gameObject.GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void chooseStartingState(){
		int choice = getRandomInt(4);
		//need to handle dead body spawn if we are in biting state
		setInteger("stateD",choice);
	}

	public void setStartState(int value){
		setInteger("StateD",value);
	}


	public void setBoolean(string name, bool value){
		animator.SetBool(name, value);
	}
	
	public void setInteger(string name, int value){
		animator.SetInteger(name, value);
	}

	public void setFloat(string name, float value){
		animator.SetFloat(name, value);
	}

	//gets a random number between 0 and max-1
	public int getRandomInt(int max){
		return Random.Range(0,max);
	}
}
