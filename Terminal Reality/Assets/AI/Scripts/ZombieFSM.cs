using UnityEngine;
using System.Collections;

public class ZombieFSM : AIEntity<StateEnums.ZombieStates> {

	//booleans to accertain certain state specifics
	private bool puking, wandering, alerted, walking, running;
	//counters
	private float eventChoiceC, checkPlayerC, wanderC, pukeC, alertedC, searchingC;
	//duration holders
	public float eventChoiceD, wanderD, checkPlayerD, pukeD, alertedD, searchingD;
	//sense values
	public float viewingSenseNorm, viewingSensAlert, listeningSensNorm, listeningSensAlert;
	//speed values
	public float walkingSpeed, RunningSpeed;
	//distance values
	public float runningDistance, attackingDistance, losingDistance;
	//actual values
	private float viewingSens, listeningSens, speed;


	private StateMachineClass<StateEnums.ZombieStates> fsm;

	// Use this for initialization
	void Start () {
<<<<<<< HEAD
		//fsm = gameObject.GetComponent<StateMachineClass<StateEnums.ZombieStates>>();
		fsm = new StateMachineClass<StateEnums.ZombieStates>();
		fsm.enterState(StateEnums.ZombieStates.Idle);
		lessenSenses();
		speed = walkingSpeed;
=======
>>>>>>> cd8b7bf3b38de4a314836c2037c5365f2bfc0a56
	}




	// Update is called once per frame
	void Update () {


		//update all counters





		switch(fsm.getCurrentState()){

		case StateEnums.ZombieStates.Idle:
			//techincally do nothing
			eventChoiceC += Time.deltaTime;
			checkPlayerC += Time.deltaTime;
			//update counters
			//keep counting for random event
			if (eventChoiceC > eventChoiceD){
				if (eventChoice()){
					StateEnums.ZombieStates eventChange = chooseAction(StateEnums.ZombieStates.Puking, StateEnums.ZombieStates.Wandering);
					fsm.enterState(eventChange);
				}
				eventChoiceC = 0;
			}

			//check if we can see player every 1s
			if (checkPlayerC > checkPlayerD){
				checkForPlayer();
				checkPlayerC = 0;
			}

			//this handles event changes itself
			break;


		case StateEnums.ZombieStates.Alerted:
			alertedC += Time.deltaTime;

			checkForPlayer();

			if (alertedC > alertedD){
				fsm.enterState(StateEnums.ZombieStates.Idle);
				alertedC = 0;
				alerted = false;
				lessenSenses();

			}
			break;


		case StateEnums.ZombieStates.Running:
			//maybe add in timer so we dont update path every frame?
			if (lostPlayer()){
				loosePlayer();
			}
			else{
				chasePlayer();
			}
			break;


		case StateEnums.ZombieStates.Searching:
			searchingC += Time.deltaTime;

			if (searchingC > searchingD){
				fsm.enterState(StateEnums.ZombieStates.Alerted);
			}
			else{
				searchForPlayer();
			}


			break;


		case StateEnums.ZombieStates.Attacking:

			break;


		case StateEnums.ZombieStates.Puking:

			//updating counter
			pukeC += Time.deltaTime;

			if (!puking){
				puke();
			}
			//if timer is over the limit
			if (pukeC > pukeD){
				fsm.enterPreviousState();
				pukeC = 0;
				puking = false;
			}

			break;
		case StateEnums.ZombieStates.Wandering:
			//updating counter
			wanderC += Time.deltaTime;

			if (!wandering){
				startWandering();
			}

			if (wanderC > wanderD){
				fsm.enterPreviousState();
				wanderC = 0;
				wandering = false;
			}


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
		//if we find the player
		fsm.enterState(StateEnums.ZombieStates.Running);
	}

	//we alert this unit that a sound trigger has gone off
	public void alertUnit(){
		if (alerted){
			fsm.enterState(StateEnums.ZombieStates.Searching);
		}
		else{
			alerted = true;
			fsm.enterState(StateEnums.ZombieStates.Alerted);
			//set rotation = players position to make viewing easier
			//enhance viewing and listening 
			heightenSenses();
		}
	}

	//we send the AI unit on a path to search for the player
	public void searchForPlayer(){
		//based on players position lay a path that leads to a point close to the player or in his general direction
		//needs to be reassesed often
		//maybe have a counter of how often the path gets recalculated
	}

	//attacks the player
	public void attackPlayer(GameObject target){}

	//chases the nearest player
	public void chasePlayer(){
		//check which entity is closer
		//based on distance

		float distance ;//= checkDistance(Player, gameObject.transform);
		if (distance > runningDistance){
			//if we are far away we walk if not already set
			
			if (!walking){
				//set speed
				speed = walkingSpeed;
				//set animation
			}
			//reasess direction & path

		}
		else if (distance > attackingDistance){
			
			//if we are closer we run
			if (!running){
				//set speed
				speed = RunningSpeed;
				//set animation
			}
			//reasess direction & path
		}
		else if (distance < attackingDistance){
			fsm.enterState(StateEnums.ZombieStates.Attacking);
		}
		else if (distance > losingDistance){
			loosePlayer();
		}

	}

	//check if we have lost the player
	public bool lostPlayer(){
		//check if the player is still in sight
		//check how far away the player is
		return true;
	}

	//instruct the AI unit that it has lost the player
	public void loosePlayer(){
		fsm.enterState(StateEnums.ZombieStates.Searching);
		walking = false;
		running = false;
	}

	//pukes at position
	public void puke(){
		//after exit time of the animation we revert back to idle
		//set animation and boolean
		puking = true;
	}


	//starts wandering in a random direction
	public void startWandering(){
		//choose direction or path?
		//send AI unit off at slow pace
		speed = walkingSpeed;
		//set boolean 
		wandering = true;
	}



	//kills the unit and plays specific animation
	public void killUnit(){}

	//disables all parts to the unit to only leave dead body
	public void dead(){}


	void heightenSenses(){
		viewingSens = viewingSensAlert;
		listeningSens = listeningSensAlert;
	}

	void lessenSenses(){
		viewingSens = viewingSenseNorm;
		listeningSens = listeningSensNorm;
	}

}
