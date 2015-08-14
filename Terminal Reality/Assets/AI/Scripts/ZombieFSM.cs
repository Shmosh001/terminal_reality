using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ZombieFSM : AIEntity<StateEnums.ZombieStates> {


	//particle effects
	public bool stateDebugStatements;
	public bool debugStatements;
	public GameObject pukeEffect;
	private AudioSource audioSource;
	private BoxCollider boxCollider;

	public float rayCastOffset = 1.5f;
	//debug
	public bool debug;
	public bool animDebug;
	private float time;

	//booleans to accertain certain state specifics
	private bool puking, wandering, alerted, walking, running, soundAlert, sightAlert, soundTrigger, chasing, shot;
	//counters
	private float eventChoiceC, checkPlayerC, wanderC, pukeC, alertedC, searchingC, dyingC, chasingC, shotC;
	//duration holders
	public float eventChoiceD = 10.0f, wanderD, checkPlayerD, pukeD, alertedD, searchingD, dyingD, chasingD = 1.5f, shotD;
	//sense values
	public float viewingSenseNorm, viewingSensAlert, listeningSensNorm = 8, listeningSensAlert = 12;
	//speed values
	public float walkingSpeed, RunningSpeed;
	//distance values
	public float runningDistance, attackingDistance = 1.5f, losingDistance = 20.0f;
	//actual values
	private float viewingSens, listeningSens;

	public float speed;

	private Transform wanderDest;

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
		audioSource = gameObject.GetComponent<AudioSource>();
		detection = gameObject.GetComponent<PreyDetection>();
		fsm = new StateMachineClass<StateEnums.ZombieStates>();
		fsm.enterState(StateEnums.ZombieStates.Idle);
		animatorCont = gameObject.GetComponent<ZombieAnimationController>();
		soundCollider  = gameObject.GetComponent<SphereCollider>();
		boxCollider = gameObject.GetComponent<BoxCollider>();
		lessenSenses();
		speed = walkingSpeed;

		//we need to choose a default position for the zombie to start on
		animatorCont.chooseStartingState();
		/*health = gameObject.GetComponent<HealthScript>();*/

		pukeD = 7.917f;


	}




	// Update is called once per frame
	void Update () {


		switch(fsm.getCurrentState()){
			/***********Idle*******Idle*******Idle*******Idle*******Idle*******Idle*******Idle*/
		case StateEnums.ZombieStates.Idle:


			if (stateDebugStatements){Debug.Log("idle case: entering " + Time.timeSinceLevelLoad);}
			//techincally do nothing
			eventChoiceC += Time.deltaTime;
			checkPlayerC += Time.deltaTime;
			//update counters
			//keep counting for random event
			if (eventChoiceC > eventChoiceD){
				bool result = animatorCont.setRandomTrigger(EnemyHashScript.changeTrigger);
				if (result){
					int path  = animatorCont.setRandomInteger(EnemyHashScript.idleVarDInt, 4);

					switch(path){
					//agonizing
					case 0:
						//dont need to change state

						//need to play a sound that accuratelt represents this animation
						sound.playFemaleScream(audioSource);
						break;
					//scream
					case 1:
						//dont need to change state
						//need to play screaming sound
						sound.playMaleScream(audioSource);
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

			/***********Alerted*******Alerted*******Alerted*******Alerted*******Alerted*******Alerted*******Alerted*/
		case StateEnums.ZombieStates.Alerted:
			if (stateDebugStatements){Debug.Log("alerted case: entering " + Time.timeSinceLevelLoad);}
			animatorCont.resetBooleans();
			animatorCont.setTrigger(EnemyHashScript.alertedTrigger);
			alertedC += Time.deltaTime;

			checkForPlayer();

			if (alertedC > alertedD){
				animatorCont.setTrigger(EnemyHashScript.alertedTrigger);
				fsm.enterState(StateEnums.ZombieStates.Idle);
				alertedC = 0;
				alerted = false;
				lessenSenses();

			}
			break;

			/***********Chasing*******Chasing*******Chasing*******Chasing*******Chasing*******Chasing*******Chasing*/
		case StateEnums.ZombieStates.Chasing:
			//maybe add in timer so we dont update path every frame?
			if (stateDebugStatements){Debug.Log("chasing case: entering " + Time.timeSinceLevelLoad);}
			chasingC += Time.deltaTime;



			chasePlayer();
			//update the position of the target we are chasing
			if (chasing && chasingC > chasingD){
				if (stateDebugStatements){Debug.LogError("chasing case: if statement " + Time.timeSinceLevelLoad);}
				navAgent.SetDestination(target.transform.position);
				detection.assignLastPosition(target.transform.position);
				detection.assignTarget(target);
				chasingC = 0;
			}


			break;

			/***********Searching*******Searching*******Searching*******Searching*******Searching*******Searching*******Searching*/
		case StateEnums.ZombieStates.Searching:

			if (stateDebugStatements){Debug.Log("searching case: entering " + Time.timeSinceLevelLoad);}
			//we search for the player by moving to his last known position
			//once we are there, we stop searching

			if (checkArrival(transform.position, navAgent.destination) || !detection.hasLastPosition()){
				fsm.enterState(StateEnums.ZombieStates.Alerted);

			}
			else{
				searchForPlayer();

			}

			break;

			/***********Attacking*******Attacking*******Attacking*******Attacking*******Attacking*******Attacking*******Attacking*/
		case StateEnums.ZombieStates.Attacking:
			if (stateDebugStatements){Debug.Log("attacking case: entering " + Time.timeSinceLevelLoad);}
			attackPlayer();

			break;

			/***********Puking*******Puking*******Puking*******Puking*******Puking*******Puking*******Puking*******Puking*/
		case StateEnums.ZombieStates.Puking:
			if (stateDebugStatements){Debug.Log("puking case: entering " + Time.timeSinceLevelLoad);}
			//updating counter
			pukeC += Time.deltaTime;

			if (!puking){
				puke();
			}
			//if timer is over the limit
			if (pukeC > pukeD){
				if (debugStatements){Debug.Log("puking case: puke time is over " + Time.timeSinceLevelLoad);}
				fsm.enterPreviousState();
				pukeC = 0;
				puking = false;
				pukeEffect.SetActive(false);
				//animatorCont.resetBooleans();

			}

			break;
			/***********Wandering*******Wandering*******Wandering*******Wandering*******Wandering*******Wandering*******Wandering*/
		case StateEnums.ZombieStates.Wandering:
			if (stateDebugStatements){Debug.Log("wandering case: entering " + Time.timeSinceLevelLoad);}

			if (!wandering){
				if (debugStatements){Debug.Log("wandering case: !wandering = true " + Time.timeSinceLevelLoad);}
				startWandering();
				//animatorCont.resetBooleans();
				animatorCont.setBoolean(EnemyHashScript.wanderingBool, true);
			}


			if (checkArrival(transform.position, wanderDest.position)){
				if (debugStatements){Debug.Log("wandering case: at position " + Time.timeSinceLevelLoad);}
				navAgent.Stop();
				fsm.enterPreviousState();
				wandering = false;
				animatorCont.setBoolean(EnemyHashScript.wanderingBool, false);
			}
			checkForPlayer();

			break;
			/***********Shot*******Shot*******Shot*******Shot*******Shot*******Shot*******Shot*******Shot*******Shot*/
		case StateEnums.ZombieStates.Shot:
			if (stateDebugStatements){Debug.Log("shot case: entering " + Time.timeSinceLevelLoad);}
			if (!shot){
				if (stateDebugStatements){Debug.Log("shot case: setting up animation " + Time.timeSinceLevelLoad);}
				//we set the random int value for the decision
				animatorCont.setRandomInteger(EnemyHashScript.hitDInt,3);
				//we activate the shot trigger 
				animatorCont.setTrigger(EnemyHashScript.shotTrigger);
				shot = true;
				navAgent.Stop();
			}

			if (animatorCont.checkAnimationState(EnemyHashScript.attackDecisionState)){
				if (stateDebugStatements){Debug.Log("shot case: animation has stopped " + Time.timeSinceLevelLoad);}
				fsm.enterState(StateEnums.ZombieStates.Chasing);
				navAgent.Resume();
				shot = false;
			}
			else{
				//Debug.Log("some other state");
			}
			break;
			/***********Dying*******Dying*******Dying*******Dying*******Dying*******Dying*******Dying*******Dying*******Dying*/
		case StateEnums.ZombieStates.Dying:
			if (stateDebugStatements){Debug.Log("dying case: entering " + Time.timeSinceLevelLoad);}
			killUnit();
			break;
			/***********Dead*******Dead*******Dead*******Dead*******Dead*******Dead*******Dead*******Dead*******Dead*/
		case StateEnums.ZombieStates.Dead:
			if (stateDebugStatements){Debug.Log("dead case: entering " + Time.timeSinceLevelLoad);}
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
	//working zone





	//chases the nearest player
	void chasePlayer(){


		if (debugStatements){Debug.Log("chasePlayer method at" + Time.timeSinceLevelLoad);}

		if (!chasing){
			if (debugStatements){Debug.Log("chasePlayer method: chasing false at" + Time.timeSinceLevelLoad);}
			animatorCont.resetBooleans();
			//we set the animation
			//animatorCont.setBoolean("Charge",true);
			//animatorCont.setTrigger("TriggerTest");
			animatorCont.setRandomInteger(EnemyHashScript.attDInt,2);
			navAgent.SetDestination(target.transform.position);

			chasing = true;
		}




		float distance = getDistanceT(target.transform, gameObject.transform);


		if (distance < attackingDistance){
			if (debugStatements){Debug.Log("chasePlayer method: ready to attack at" + Time.timeSinceLevelLoad);}
			fsm.enterState(StateEnums.ZombieStates.Attacking);
			navAgent.Stop();
			animatorCont.setBoolean(EnemyHashScript.attackingBool,true);
			animatorCont.setTrigger(EnemyHashScript.chargeTrigger);
		}
		else if (distance > losingDistance){
			if (debugStatements){Debug.Log("chasePlayer method: too far away to attack at" + Time.timeSinceLevelLoad);}
			fsm.enterState(StateEnums.ZombieStates.Searching);
			animatorCont.resetBooleans();
		}
		
	}




	//kills the unit and plays specific animation
	void killUnit(){
		if (debugStatements){Debug.Log("killUnit method at" + Time.timeSinceLevelLoad);}
		//play animation
		animatorCont.resetBooleans();
		animatorCont.setTrigger(EnemyHashScript.deadTrigger);
		animatorCont.setRandomInteger(EnemyHashScript.deathDsInt,2);
		fsm.enterState(StateEnums.ZombieStates.Dead);
	}
	
	//disables all parts to the unit to only leave dead body
	void dead(){
		if (debugStatements){Debug.Log("dead method at" + Time.timeSinceLevelLoad);}
		gameObject.GetComponent<CharacterController>().enabled = false;
		gameObject.GetComponent<SphereCollider>().enabled = false;
		//remove unnessesary parts
	}




	//we send the AI unit on a path to search for the player
	void searchForPlayer(){
		//we go to the players last known position
		if (debugStatements){Debug.Log("searchForPlayer method at" + Time.timeSinceLevelLoad);}
		if (navAgent.SetDestination(detection.lastSighting)){
			if (debugStatements){Debug.Log("searchForPlayer method: dest set true at" + Time.timeSinceLevelLoad);}
			navAgent.speed = walkingSpeed;
			animatorCont.setBoolean(EnemyHashScript.searchingBool,true);
			heightenSenses();
		}
		//if the paths 
		else{
			if (debugStatements){Debug.Log("searchForPlayer method: dest set false at" + Time.timeSinceLevelLoad);}
			//stop any path navigation
			lessenSenses();
			navAgent.Stop();
			fsm.enterState(StateEnums.ZombieStates.Wandering);
		}

	}


	//starts wandering in a random direction
	void startWandering(){
		if (debugStatements){Debug.Log("startWandering method at" + Time.timeSinceLevelLoad);}
		//set boolean 
		lessenSenses();
		wandering = true;
		//we get a location to move to from the wandering script
		wanderDest = wanderScript.getClosestPoint(transform);
		if (navAgent.SetDestination(wanderDest.position)){
			if (debugStatements){Debug.Log("startWandering method: if path was set succesfully at" + Time.timeSinceLevelLoad);}
			navAgent.speed = walkingSpeed;
		}
		//if for some reason the path setting fails
		else{
			if (debugStatements){Debug.Log("startWandering method: if path set failed at" + Time.timeSinceLevelLoad);}
			//stop any path navigation
			navAgent.Stop();
			fsm.enterPreviousState();
		}
		
	}

	//attacks the player
	void attackPlayer(){
		if (debugStatements){Debug.Log("attackPlayer method at" + Time.timeSinceLevelLoad);}
		//change animation
		//inflict damage on player
		playerHealthScript targetH = target.GetComponent<playerHealthScript>();
		if (targetH != null){
			targetH.reducePlayerHealth(damage);
			//targetH.takeDamage(damage);
		}
		float distance = getDistanceT(transform, target.transform);
		//add a small offset
		if (distance > attackingDistance+5){
			fsm.enterState(StateEnums.ZombieStates.Chasing);
		}
	}



	//pukes at position
	void puke(){
		if (debugStatements){Debug.Log("puke method at" + Time.timeSinceLevelLoad);}
		//after exit time of the animation we revert back to idle
		//set the puking particle effect
		//Debug.Log("Enetered puke method");
		puking = true;
		pukeEffect.SetActive(true);

	}








	void heightenSenses(){
		if (debugStatements){Debug.Log("heightenSenses at" + Time.timeSinceLevelLoad);}
		viewingSens = viewingSensAlert;
		listeningSens = listeningSensAlert;
		soundCollider.radius = listeningSens;
	}

	void lessenSenses(){
		if (debugStatements){Debug.Log("lessenSenses at" + Time.timeSinceLevelLoad);}
		viewingSens = viewingSenseNorm;
		listeningSens = listeningSensNorm;
		soundCollider.radius = listeningSens;
	}

	void OnTriggerEnter(Collider collider){
		if (collider.tag == Tags.PLAYER){
			if (debugStatements){Debug.Log("collider entrance with " + collider.gameObject.name + " at " + Time.timeSinceLevelLoad);}
			detection.assignTarget(collider.gameObject);
			target = collider.gameObject;
			soundTrigger = true;
		}
		else if(collider is BoxCollider){
			//we set the awake boolean
			animatorCont.setTrigger(EnemyHashScript.wakeupTrigger);
			boxCollider.enabled = false;
		}

	}
	void OnTriggerExit(Collider collider){
		if (collider.tag == Tags.PLAYER){
			if (debugStatements){Debug.Log("collider exit with " + collider.gameObject.name + " at " + Time.timeSinceLevelLoad);}
			//detection.assignTarget(null);
			//target = null;
			soundTrigger = false;
		}
	}

	//we check if the AI unit can see the player
	void checkForPlayer(){
		//we check for sounds last as viewing is more important
		if (debugStatements){Debug.Log("checkForPlayer method at" + Time.timeSinceLevelLoad);}

		//we need to be in the radius of the sound collider in order to be seen. radius is much larger than the viewing distance
		if (soundTrigger && detection.targetInSight(viewingSens)){
			if (debugStatements){Debug.LogError("checkForPlayer method: player spotted " + Time.timeSinceLevelLoad);}
			//we need to now change into the approriate state
			fsm.enterState(StateEnums.ZombieStates.Chasing);
		}

		else if (soundTrigger){
			if (debugStatements){Debug.Log("checkForPlayer method: soundTrigger = true at" + Time.timeSinceLevelLoad);}
			if (detection.targetHeard()){
				if (debugStatements){Debug.Log("checkForPlayer method: target heard at" + Time.timeSinceLevelLoad);}
				alertUnit();
			}
		}


	}

	//we alert this unit that a sound trigger has gone off
	void alertUnit(){
		if (debugStatements){Debug.Log("alertUnit method at" + Time.timeSinceLevelLoad);}
		//if this is the >second  sound alert
		if (soundAlert){
			if (debugStatements){Debug.Log("alertUnit method: sound alert = true at" + Time.timeSinceLevelLoad);}
			//the position should have been set
			//then searching method should take care of moving the npc there
			fsm.enterState(StateEnums.ZombieStates.Searching);
			
		}
		else{
			if (debugStatements){Debug.Log("alertUnit method else branch at" + Time.timeSinceLevelLoad);}
			soundAlert = true;
			//enhance viewing and listening 
			heightenSenses();
			
			fsm.enterState(StateEnums.ZombieStates.Alerted);
			
		}
	}

	public void alertDead(){
		fsm.enterState(StateEnums.ZombieStates.Dying);
	}

	public void alertShot(GameObject entity){
		//we want to assign the new target either way
		detection.assignTarget(entity.gameObject);
		target = entity.gameObject;
		// we only want to transition into the state if are not currently in the state
		if (!shot){
			fsm.enterState(StateEnums.ZombieStates.Shot);
		}
	}
}

