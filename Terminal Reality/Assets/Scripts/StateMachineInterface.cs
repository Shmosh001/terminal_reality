using UnityEngine;
using System.Collections;

public interface StateMachineInterface<T> {


	//eneters the specified state
	void enterState(T newState);

	//checks if a previous state exists
	bool hasPreviousState();

	//returns the previous state
	T getPreviousState();

	//returns the current state
	T getCurrentState();


	//checks if the current state is the state sent in
	bool checkCurrentState(T state);




	
}
