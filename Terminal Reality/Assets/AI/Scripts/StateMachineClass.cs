using UnityEngine;
using System.Collections;


public class StateMachineClass <T>{


	private T currenState;
	private T previousState;

	void Start(){
<<<<<<< HEAD
		currenState = default(T);
		previousState = default(T);
=======
		/*currenState = null;
		previousState = null;*/
>>>>>>> cd8b7bf3b38de4a314836c2037c5365f2bfc0a56
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
<<<<<<< HEAD
		//if (currenState == state){
			//return true;
		//}
=======
		/*if (currenState == state){
			return true;
		}*/
>>>>>>> cd8b7bf3b38de4a314836c2037c5365f2bfc0a56
		return false;
	}






}
