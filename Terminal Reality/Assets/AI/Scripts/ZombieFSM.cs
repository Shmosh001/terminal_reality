using UnityEngine;
using System.Collections;

public class ZombieFSM : MonoBehaviour {



	private StateMachineClass<StateEnums.ZombieStates> fsm;

	// Use this for initialization
	void Start () {
		//fsm = gameObject.GetComponent<StateMachineClass<StateEnums.ZombieStates>>();
		fsm = new StateMachineClass<StateEnums.ZombieStates>();
		fsm.enterState(StateEnums.ZombieStates.Idle);
	}
	
	// Update is called once per frame
	void Update () {
		switch(fsm.getCurrentState()){
		case StateEnums.ZombieStates.Idle:
			break;
		case StateEnums.ZombieStates.Alerted:
			break;
		case StateEnums.ZombieStates.Running:
			break;
		case StateEnums.ZombieStates.Searching:
			break;
		case StateEnums.ZombieStates.Attacking:
			break;
		case StateEnums.ZombieStates.Puking:
			break;
		case StateEnums.ZombieStates.Wandering:
			break;
		case StateEnums.ZombieStates.Dying:
			break;
		case StateEnums.ZombieStates.Dead:
			break;
		default:
			break;
		}
	}
	//moved into AI Entity
	/*//could have this in an AIEntity class ad base method
	public void activateEntity(){
		this.gameObject.SetActive(true);
	}

	//could also be in base class
	//choses between 2 states randomly
	public void chooseAction(StateEnums state1, StateEnums state2){

	}
	//general class
	public float checkDistance(Transform entity1, Transform entity2){
		return Vector3.Distance(entity1,entity2);
	}
	*/

	//we check if the AI unit can see the player
	public void checkForPlayer(){
		//could maybe place view frustum and check if the player is detected?
	}

	//we alert this unit that a sound trigger has gone off
	public void alertSound(){}

	//we send the AI unit on a path to search for the player
	public void searchForPlayer(){}

	//attacks the player
	public void attackPlayer(GameObject target){}

	//chases the nearest player
	public void chasePlayer(){
		//check which player is closer
		//if we are far away we walk
		//if we are closer we run

	}

	//check if we have lost the player
	public void lostPlayer(){
		//check if the player is still in sight
		//check how far away the player is
	}

	//instruct the AI unit that it has lost the player
	public void loosePlayer(){}

	//pukes at position
	public void puke(){}


	//starts wandering in a random direction
	public void startWandering(){
		//choose direction or path?
		//send AI unit off at slow pace
	}

	//alerts the unit
	public void alertUnit(){
		//set alerted bool = true
	}

	//kills the unit and plays specific animation
	public void killUnit(){}

	//disables all parts to the unit to only leave dead body
	public void dead(){}

}
