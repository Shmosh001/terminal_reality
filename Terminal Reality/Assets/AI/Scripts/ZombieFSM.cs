using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ZombieFSM : AIEntity<StateEnums.ZombieStates> {


	//particle effects

	public GameObject pukeEffect;

	public float rayCastOffset = 1.5f;
	//debug
	public bool debug;
	public bool animDebug;

	//booleans to accertain certain state specifics
	private bool puking, wandering, alerted, walking, running, soundAlert, sightAlert, soundTrigger;
	//counters
	private float eventChoiceC, checkPlayerC, wanderC, pukeC, alertedC, searchingC, dyingC;
	//duration holders
	public float eventChoiceD, wanderD, checkPlayerD, pukeD, alertedD, searchingD, dyingD;
	//sense values
	public float viewingSenseNorm, viewingSensAlert, listeningSensNorm = 8, listeningSensAlert = 12;
	//speed values
	public float walkingSpeed, RunningSpeed;
	//distance values
	public float runningDistance, attackingDistance, losingDistance;
	//actual values
	private float viewingSens, listeningSens;

	public float speed;



	/*private HealthScript health;*/

	public GameObject target;

	public Text text;
	public Text text2;

	//animation
	private ZombieAnimationController animatorCont;


	private Vector3 debugPos;
	private Vector3 debugPos1;

	private StateMachineClass<StateEnums.ZombieStates> fsm;
	private PreyDetection detection;
	
	private SphereCollider soundCollider;

	// Use this for initialization
	void Start () {
		//fsm = gameObject.GetComponent<StateMachineClass<StateEnums.ZombieStates>>();
		detection = gameObject.GetComponent<PreyDetection>();
		fsm = new StateMachineClass<StateEnums.ZombieStates>();
		fsm.enterState(StateEnums.ZombieStates.Idle);
		animatorCont = gameObject.GetComponent<ZombieAnimationController>();
		lessenSenses();
		speed = walkingSpeed;

		//we need to choose a default position for the zombie to start on
		animatorCont.chooseStartingState();
		/*health = gameObject.GetComponent<HealthScript>();*/

		pukeD = 7.917f;
		soundCollider  = gameObject.GetComponent<SphereCollider>();


	}




	// Update is called once per frame
	void Update () {

		//Debug.DrawRay(debugPos, debugPos1*viewingSens, Color.green);
		//update all counters

		if (health.health <= 0){
			fsm.enterState(StateEnums.ZombieStates.Dying);
		}


		/*if (animDebug){
			//fsm.enterState(StateEnums.ZombieStates.);
			if (Input.GetKeyDown(KeyCode.Space)){
				//animatorCont.setInterger("DeathD", 0);
				//animatorCont.setBoolean("Dead", true);
				animatorCont.setBoolean("Alerted", false);
				animatorCont.setBoolean("Charge",true);
				animatorCont.setInteger("AttD",0);

			}
			if (Input.GetKeyDown(KeyCode.Alpha1)){
				//animatorCont.setInterger("DeathD", 0);
				animatorCont.setBoolean("Alerted", false);
				animatorCont.setBoolean("Dead", true);
				animatorCont.setInteger("DeathD", 1);
				/*animatorCont.setBoolean("Charge", true);
			animatorCont.setInteger("AttD", 0);
			animatorCont.setBoolean("Alerted", false);*/
			/*}*/
			
			/*if (Input.GetKeyDown(KeyCode.Alpha2)){
			//animatorCont.setInterger("DeathD", 0);
			//animatorCont.setBoolean("Dead", true);
			animatorCont.setBoolean("Charge", false);
			//animatorCont.setInterger("AttD", 0);
			animatorCont.setBoolean("Attacking", true);
		}*/
			
			/*if (Input.GetKeyDown(KeyCode.Alpha3)){
			//animatorCont.setInterger("DeathD", 0);
			//animatorCont.setBoolean("Dead", true);
			animatorCont.setBoolean("Charge", false);
			//animatorCont.setInterger("AttD", 0);
			animatorCont.setBoolean("Searching", true);
		}*/
			
			/*if (Input.GetKeyDown(KeyCode.Alpha2)){
				//animatorCont.setInterger("DeathD", 0);
				//animatorCont.setBoolean("Dead", true);
				animatorCont.setBoolean("Charge", false);
				//animatorCont.setInterger("AttD", 0);
				animatorCont.setInteger("HitD", 0);
				animatorCont.setBoolean("Shot", true);
			}

		}*/

		//Debug.Log(gameObject.renderer.bounds.size.y);
		switch(fsm.getCurrentState()){

		case StateEnums.ZombieStates.Idle:

		/*	if (animDebug){
				animatorCont.setStartState(0);
				

				//animatorCont.setInteger("IdleD", 3);
				//animatorCont.setBoolean("Charge",true);
				//animatorCont.setInteger("AttD",0);
				//fsm.enterState(StateEnums.ZombieStates.Puking);
				
				break;
			}*/


			animatorCont.resetBooleans();

			//techincally do nothing
			eventChoiceC += Time.deltaTime;
			checkPlayerC += Time.deltaTime;
			//update counters
			//keep counting for random event
			if (eventChoiceC > eventChoiceD){
				bool result = animatorCont.setRandomBoolean("ChangeBool");
				if (result){
					int path  = animatorCont.setRandomInteger("IdleVarD", 4);

					switch(path){
					//agonizing
					case 0:
						//dont need to change state

						//need to play a sound that accuratelt represents this animation
						break;
					//scream
					case 1:
						//dont need to change state
						//need to play screaming sound
						break;
					//crying/puking
					case 2:
						fsm.enterState(StateEnums.ZombieStates.Puking);
						break;
					//wandering
					case 3:
						fsm.enterState(StateEnums.ZombieStates.Wandering);
						break;
					//default
					default:
						//set to screaming
						break;
					}


				}

				eventChoiceC = 0;
			}

			//check if we can see player every 1s
			if (soundTrigger && checkPlayerC > checkPlayerD){
				//need to still set this up
				checkForPlayer();
				checkPlayerC = 0;
			}

			//this handles event changes itself
			break;


		case StateEnums.ZombieStates.Alerted:
			animatorCont.resetBooleans();
			animatorCont.setBoolean("Alerted", true);
			alertedC += Time.deltaTime;

			checkForPlayer();

			if (alertedC > alertedD){
				animatorCont.setBoolean("Alerted", false);
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


			//we search for the player by moving to his last known position
			//once we are there, we stop searching

			if (detection.lastSighting == transform.position || !detection.hasLastPosition()){
				fsm.enterState(StateEnums.ZombieStates.Alerted);
			}
			else{
				searchForPlayer();

			}

			break;


		case StateEnums.ZombieStates.Attacking:
			attackPlayer(target);

			break;


		case StateEnums.ZombieStates.Puking:

			//updating counter
			pukeC += Time.deltaTime;

			if (!puking){
				puke();
			}
			//if timer is over the limit
			if (pukeC > pukeD){
				//Debug.Log("puke time is over");
				fsm.enterPreviousState();
				pukeC = 0;
				puking = false;
				pukeEffect.SetActive(false);
				animatorCont.resetBooleans();

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

			if (dyingC == 0){
				killUnit();
			}

			dyingC += Time.deltaTime;



			if (dyingC > dyingD){
				fsm.enterState(StateEnums.ZombieStates.Dead);
			}


			break;


		case StateEnums.ZombieStates.Dead:
			dead ();
			break;


		default:
			break;
		}
		//debugging
		if (debug){
			text.text = fsm.getCurrentState().ToString();
			//text2.text = getDistance(player.transform, transform).ToString();
		}
		/*else{
			text.text = "";
			text2.text = "";
		}*/
	}



	//*******************************************************************************************************************************


	
	//starts wandering in a random direction
	public void startWandering(){
		//set boolean 
		wandering = true;
		//we get a location to move to from the wandering script
		Transform dest = wanderScript.getClosestPoint(transform);
		navAgent.SetDestination(dest.position);
		navAgent.speed = walkingSpeed;
	}





	//we send the AI unit on a path to search for the player
	void searchForPlayer(){
		//we go to the players last known position
	
		navAgent.SetDestination(detection.lastSighting);
		animatorCont.setBoolean("Searching",true);
		

	
	}

	//attacks the player
	public void attackPlayer(GameObject target){
		//change animation
		//inflict damage on player
		HealthScript targetH = target.GetComponent<HealthScript>();
		if (targetH != null){
			targetH.takeDamage(damage);
		}
	}

	//chases the nearest player
	public void chasePlayer(){
		//check which entity is closer
		//based on distance

		float distance = 0.1f;//= checkDistance(Player, gameObject.transform);
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
		//set the puking particle effect
		//Debug.Log("Enetered puke method");
		puking = true;
		pukeEffect.SetActive(true);

	}





	//kills the unit and plays specific animation
	public void killUnit(){
		//play animation
	}

	//disables all parts to the unit to only leave dead body
	public void dead(){
		//remove unnessesary parts
	}


	void heightenSenses(){
		viewingSens = viewingSensAlert;
		listeningSens = listeningSensAlert;
		soundCollider.radius = listeningSens;
	}

	void lessenSenses(){
		viewingSens = viewingSenseNorm;
		listeningSens = listeningSensNorm;
		soundCollider.radius = listeningSens;
	}

	void OnTriggerEnter(Collider collider){
		if (collider.tag == "Player"){
			Debug.Log("registered collider entrance with " + collider.gameObject.name);
			detection.assignTarget(collider.gameObject);
			player = collider.gameObject;
			soundTrigger = true;
		}

	}
	void OnTriggerExit(Collider collider){
		if (collider.tag == "Player"){
			Debug.Log("registered collider exit with " + collider.gameObject.name);
			detection.assignTarget(null);
			player = null;
			soundTrigger = false;
		}
	}

	//we check if the AI unit can see the player
	void checkForPlayer(){
		//we check for sounds last as viewing is more important


		//we need to be in the radius of the sound collider in order to be seen. radius is much larger than the viewing distance
		if (soundTrigger && detection.targetInSight(viewingSens)){
			//we need to now change into the approriate state
			fsm.enterState(StateEnums.ZombieStates.Attacking);
		}

		if (soundTrigger){
			if (detection.targetHeard()){
				alertUnit();
			}
		}


	}

	//we alert this unit that a sound trigger has gone off
	void alertUnit(){
		//if this is the >second  sound alert
		if (soundAlert){
			//the position should have been set
			//then searching method should take care of moving the npc there
			fsm.enterState(StateEnums.ZombieStates.Searching);
			
		}
		else{
			soundAlert = true;
			//enhance viewing and listening 
			heightenSenses();
			
			fsm.enterState(StateEnums.ZombieStates.Alerted);
			
		}
	}


}
