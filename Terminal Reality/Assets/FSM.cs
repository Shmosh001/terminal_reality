using UnityEngine;
using System.Collections;

public abstract class FSM : MonoBehaviour {

	public enum State{stationary,wandering,chase,search,attack,dead};
	public State currentState;
	public State prevState;


	//need to maybe make an abstract class for this and then add implementation;
	//void attack(){}=0;


	//need element here that links to the actions for each state

	abstract void enterStationary();

	abstract void enterWandering();

	public virtual abstract void enterChase();
	abstract void enterSearch();
	abstract void enterAttaack();
	abstract void enterDead();


	abstract void changeState(State newState);
	abstract void previousState();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		switch(currentState){
		case State.attack:
			break;
		case State.dead:
			break;
		case State.chase:
			break;
		case State.search:
			break;
		case State.stationary:
			break;
		case State.wandering:
			//need to execute the wandering code
		
			break;
		
		}
	}



















}
