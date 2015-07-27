using UnityEngine;
using System.Collections;


public class StateMachineClass<T> : MonoBehaviour{


	private T currenState;
	private T previousState;

	void Start(){
		currenState = null;
		previousState = null;
	}

	//eneters the specified state
	public void enterState(T newState){
		previousState = currenState;
		currenState = newState;

	}

	//reverts back to previous state
	public void enterPreviousState(){
		T temp = currenState;
		currenState = previousState;
		previousState = temp;
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
		return currenState;
	}
	
	
	//checks if the current state is the state sent in
	public bool checkCurrentState(T state){
		if (currenState == state){
			return true;
		}
		return false;
	}






}
