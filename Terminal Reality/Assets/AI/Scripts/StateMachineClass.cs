using UnityEngine;
using System.Collections;

/// <summary>
/// a base class which has methods to switch between states and revert to previous states
/// </summary>
/// <typeparam name="T">
/// which type of ai states we are dealing with
/// </typeparam>
public class StateMachineClass <T> {

    //vars
	private T currentState;
	private T previousState;

    /// <summary>
    /// initialisation
    /// </summary>
	void Start(){
		currentState = default(T);
		previousState = default(T);
	}

    /// <summary>
    /// enters the specified state
    /// </summary>
    /// <param name="newState">
    /// new state
    /// </param>
    public void enterState(T newState){
		Debug.Log("entered state: " + newState.ToString()+ " at " + Time.timeSinceLevelLoad);
		previousState = currentState;
		currentState = newState;
	}

    /// <summary>
    /// reverts back to previous state
    /// </summary>
    public void enterPreviousState(){
		if (hasPreviousState()){
			enterState(previousState);
			Debug.Log("applied previous state: " + currentState.ToString()+ " at " + Time.timeSinceLevelLoad);
		}
		
	}

    /// <summary>
    /// checks if a previous state exists
    /// </summary>
    /// <returns>
    /// true/false based on above
    /// </returns>
    public bool hasPreviousState(){
		if (previousState != null){
			return true;
		}
		return false;
	}

    /// <summary>
    /// returns the previous state
    /// </summary>
    /// <returns>
    /// previous state
    /// </returns>
    public T getPreviousState(){
		return previousState;
	}

    /// <summary>
    /// returns the current state
    /// </summary>
    /// <returns>
    /// current state
    /// </returns>
    public T getCurrentState(){
		return currentState;
	}







}
