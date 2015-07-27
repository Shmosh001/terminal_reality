using UnityEngine;
using System.Collections;


public class StateMachineClass<T> : MonoBehaviour,  StateMachineInterface<T> {


	public T currenState;
	public T previousState;






	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	//eneters the specified state
	void enterState(T newState){

	}
	
	//checks if a previous state exists
	bool hasPreviousState(){
		return true;
	}
	
	//returns the previous state
	T getPreviousState(){
		return previousState;
	}
	
	//returns the current state
	T getCurrentState(){
		return currenState;
	}
	
	
	//checks if the current state is the state sent in
	bool checkCurrentState(T state){
		return true;
	}






}
