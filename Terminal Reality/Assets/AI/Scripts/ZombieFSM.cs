using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ZombieFSM : AIEntity<StateEnums.ZombieStates> {


	//particle effects
	public bool debugStatements;
	public GameObject pukeEffect;

	public float rayCastOffset = 1.5f;
	//debug
	public bool debug;
	public bool animDebug;

	//booleans to accertain certain state specifics
	private bool puking, wandering, alerted, walking, running, soundAlert, sightAlert, soundTrigger, chasing;
	//counters
	private float eventChoiceC, checkPlayerC, wanderC, pukeC, alertedC, searchingC, dyingC, chasingC;
	//duration holders
	public float eventChoiceD, wanderD, checkPlayerD, pukeD, alertedD, searchingD, dyingD, chasingD = 1.5f;
	//sense values
	public float viewingSenseNorm, viewingSensAlert, listeningSensNorm = 8, listeningSensAlert = 12;
	//speed values
	public float walkingSpeed, RunningSpeed;
	//distance values
	public float runningDistance, attackingDistance = 1.5f, losingDistance = 20.0f;
	//actual values
	private float viewingSens, listeningSens;

	public float speed;



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
		soundCollider  = gameObject.GetComponent<SphereCollider>();
		lessenSenses();
		speed = walkingSpeed;

		//we need to choose a default position for the zombie to start on
		animatorCont.chooseStartingState();
		/*health = gameObject.GetComponent<HealthScript>();*/

		pukeD = 7.917f;


	}




	// Update is called once per frame
	void Update () {



		if (health.health <= 0){
			fsm.enterState(StateEnums.ZombieStates.Dying);
		}



		switch(fsm.getCurrentState()){

		case StateEnums.ZombieStates.Idle:

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


		case StateEnums.ZombieStates.Chasing:
			//maybe add in timer so we dont update path every frame?

			chasingC += Time.deltaTime;



			chasePlayer();

			if (chasing && chasingC > chasingD){
				navAgent.SetDestination(target.transform.position);
				detection.assignLastPosition(target.transform.position);
				detection.assignTarget(target);
				chasingC = 0;
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
			attackPlayer();

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


			if (!wandering){
				startWandering();
				animatorCont.resetBooleans();
				animatorCont.setBoolean("Wandering", true);
			}

			if (navAgent.destination == transform.position){
				navAgent.Stop();
				fsm.enterPreviousState();
				wandering = false;
			}


			break;


		case StateEnums.ZombieStates.Dying:
			killUnit();
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
	//working zone


	
	//chases the nearest player
	void chasePlayer(){


		if (debugStatements){Debug.Log("chasePlayer method");}

		if (!chasing){
			if (debugStatements){Debug.Log("chasePlayer method: chasing false");}
			animatorCont.resetBooleans();
			//we set the animation
			animatorCont.setBoolean("Charge",true);
			animatorCont.setRandomInteger("AttD",2);
			navAgent.SetDestination(target.transform.position);
			chasing = true;
		}




		float distance = getDistance(target.transform, gameObject.transform);


		if (distance < attackingDistance){
			if (debugStatements){Debug.Log("chasePlayer method: ready to attack");}
			fsm.enterState(StateEnums.ZombieStates.Attacking);
			navAgent.Stop();
			animatorCont.setBoolean("Attacking",true);
			animatorCont.setBoolean("Charge",false);
		}
		else if (distance > losingDistance){
			if (debugStatements){Debug.Log("chasePlayer method: too far away to attack");}
			fsm.enterState(StateEnums.ZombieStates.Searching);
			animatorCont.resetBooleans();
		}
		
	}




	//kills the unit and plays specific animation
	void killUnit(){
		if (debugStatements){Debug.Log("killUnit method");}
		//play animation
		animatorCont.resetBooleans();
		animatorCont.setBoolean("Dead", true);
		animatorCont.setRandomInteger("DeathD",2);
		fsm.enterState(StateEnums.ZombieStates.Dead);
	}
	
	//disables all parts to the unit to only leave dead body
	void dead(){
		if (debugStatements){Debug.Log("dead method");}
		gameObject.GetComponent<CharacterController>().enabled = false;
		gameObject.GetComponent<SphereCollider>().enabled = false;
		//remove unnessesary parts
	}




	//we send the AI unit on a path to search for the player
	void searchForPlayer(){
		//we go to the players last known position
		if (debugStatements){Debug.Log("searchForPlayer method");}
		if (navAgent.SetDestination(detection.lastSighting)){
			if (debugStatements){Debug.Log("searchForPlayer method: dest set true");}
			navAgent.speed = walkingSpeed;
			animatorCont.setBoolean("Searching",true);
			heightenSenses();
		}
		//if the paths 
		else{
			if (debugStatements){Debug.Log("searchForPlayer method: dest set false");}
			//stop any path navigation
			lessenSenses();
			navAgent.Stop();
			fsm.enterState(StateEnums.ZombieStates.Wandering);
		}

	}


	//starts wandering in a random direction
	void startWandering(){
		if (debugStatements){Debug.Log("startWandering method");}
		//set boolean 
		lessenSenses();
		wandering = true;
		//we get a location to move to from the wandering script
		Transform dest = wanderScript.getClosestPoint(transform);
		if (navAgent.SetDestination(dest.position)){
			if (debugStatements){Debug.Log("startWandering method: if path was set succesfully");}
			navAgent.speed = walkingSpeed;
		}
		//if for some reason the path setting fails
		else{
			if (debugStatements){Debug.Log("startWandering method: if path set failed");}
			//stop any path navigation
			navAgent.Stop();
			fsm.enterPreviousState();
		}
		
	}

	//attacks the player
	void attackPlayer(){
		if (debugStatements){Debug.Log("attackPlayer method");}
		//change animation
		//inflict damage on player
		HealthScript targetH = target.GetComponent<HealthScript>();
		if (targetH != null){
			targetH.takeDamage(damage);
		}
		float distance = getDistance(transform, target.transform);
		//add a small offset
		if (distance > attackingDistance+5){
			fsm.enterState(StateEnums.ZombieStates.Chasing);
		}
	}



	//pukes at position
	void puke(){
		if (debugStatements){Debug.Log("puke method");}
		//after exit time of the animation we revert back to idle
		//set the puking particle effect
		//Debug.Log("Enetered puke method");
		puking = true;
		pukeEffect.SetActive(true);

	}








	void heightenSenses(){
		if (debugStatements){Debug.Log("heightenSenses");}
		viewingSens = viewingSensAlert;
		listeningSens = listeningSensAlert;
		soundCollider.radius = listeningSens;
	}

	void lessenSenses(){
		if (debugStatements){Debug.Log("lessenSenses");}
		viewingSens = viewingSenseNorm;
		listeningSens = listeningSensNorm;
		soundCollider.radius = listeningSens;
	}

	void OnTriggerEnter(Collider collider){
		if (collider.tag == Tags.PLAYER){
			if (debugStatements){Debug.Log("collider entrance with " + collider.gameObject.name);}
			detection.assignTarget(collider.gameObject);
			target = collider.gameObject;
			soundTrigger = true;
		}

	}
	void OnTriggerExit(Collider collider){
		if (collider.tag == Tags.PLAYER){
			if (debugStatements){Debug.Log("collider exit with " + collider.gameObject.name);}
			detection.assignTarget(null);
			target = null;
			soundTrigger = false;
		}
	}

	//we check if the AI unit can see the player
	void checkForPlayer(){
		//we check for sounds last as viewing is more important
		if (debugStatements){Debug.Log("checkForPlayer method");}

		//we need to be in the radius of the sound collider in order to be seen. radius is much larger than the viewing distance
		if (soundTrigger && detection.targetInSight(viewingSens)){
			//we need to now change into the approriate state
			fsm.enterState(StateEnums.ZombieStates.Chasing);
		}

		if (soundTrigger){
			if (debugStatements){Debug.Log("checkForPlayer method: soundTrigger = true");}
			if (detection.targetHeard()){
				if (debugStatements){Debug.Log("checkForPlayer method: target heard");}
				alertUnit();
			}
		}


	}

	//we alert this unit that a sound trigger has gone off
	void alertUnit(){
		if (debugStatements){Debug.Log("alertUnit method");}
		//if this is the >second  sound alert
		if (soundAlert){
			if (debugStatements){Debug.Log("alertUnit method: sound alert = true");}
			//the position should have been set
			//then searching method should take care of moving the npc there
			fsm.enterState(StateEnums.ZombieStates.Searching);
			
		}
		else{
			if (debugStatements){Debug.Log("alertUnit method else branch");}
			soundAlert = true;
			//enhance viewing and listening 
			heightenSenses();
			
			fsm.enterState(StateEnums.ZombieStates.Alerted);
			
		}
	}


}
