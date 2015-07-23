using UnityEngine;
using System.Collections;

public abstract class FSM : MonoBehaviour {

	public enum State{stationary,wandering,chase,search,attack,dead};
	public State currentState;
	public State prevState;


	/*abstract public void enterStationary();
	abstract public void enterWandering();
	abstract public void enterChase();
	abstract public void enterSearch();
	abstract public void enterAttaack();
	abstract public void enterDead();

	abstract public void changeState(State newState);
	abstract public void previousState();*/
	
}
