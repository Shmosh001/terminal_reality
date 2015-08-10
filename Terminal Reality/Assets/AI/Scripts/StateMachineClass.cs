using UnityEngine;
using System.Collections;


public class StateMachineClass <T> : MonoBehaviour{


	private T currentState;
	private T previousState;

	void Start(){
		currentState = default(T);
		previousState = default(T);
	}

	//eneters the specified state
	public void enterState(T newState){
		Debug.Log("entered state: " + newState.ToString());
		previousState = currentState;
		currentState = newState;

	}

	//reverts back to previous state
	public void enterPreviousState(){
		if (hasPreviousState()){
			enterState(previousState);
			Debug.Log("applied previous state: " + currentState.ToString());
		}
		
	}

	//checks if a previous state exists
	public bool hasPreviousState(){
		if (previousState != null){
			return true;
		}
		return false;
	}
	
	//returns the previous state
	public T getPreviousState(){
		return previousState;
	}
	
	//returns the current state
	public T getCurrentState(){
		return currentState;
	}
	

	//checks if the current state is the state sent in
	public bool checkCurrentState(T state){
		//if (currentState == state){
			//return true;
		//}
		return false;
	}






}
