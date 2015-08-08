using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ZombieFSM : AIEntity<StateEnums.ZombieStates> {


	//particle effects

	public GameObject pukeEffect;

	public float rayCastOffset = 1.7f;
	//debug
	public bool debug;
	public bool animDebug;

	//booleans to accertain certain state specifics
	private bool puking, wandering, alerted, walking, running, soundAlert, sightAlert;
	//counters
	private float eventChoiceC, checkPlayerC, wanderC, pukeC, alertedC, searchingC, dyingC;
	//duration holders
	public float eventChoiceD, wanderD, checkPlayerD, pukeD, alertedD, searchingD, dyingD;
	//sense values
	public float viewingSenseNorm, viewingSensAlert, listeningSensNorm, listeningSensAlert;
	//speed values
	public float walkingSpeed, RunningSpeed;
	//distance values
	public float runningDistance, attackingDistance, losingDistance;
	//actual values
	private float viewingSens, listeningSens;

	public float speed;


	private HealthScript health;

	public GameObject target;

	public Text text;
	public Text text2;

	//animation
	private ZombieAnimationController animatorCont;


	private StateMachineClass<StateEnums.ZombieStates> fsm;

	// Use this for initialization
	void Start () {
		//fsm = gameObject.GetComponent<StateMachineClass<StateEnums.ZombieStates>>();
		fsm = new StateMachineClass<StateEnums.ZombieStates>();
		fsm.enterState(StateEnums.ZombieStates.Idle);
		animatorCont = gameObject.GetComponent<ZombieAnimationController>();
		lessenSenses();
		speed = walkingSpeed;

		//we need to choose a default position for the zombie to start on
		animatorCont.chooseStartingState();
		health = gameObject.GetComponent<HealthScript>();

		pukeD = 7.917f;

	}




	// Update is called once per frame
	void Update () {


		//update all counters

		if (health.health <= 0){
			fsm.enterState(StateEnums.ZombieStates.Dying);
		}


		if (animDebug){
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
			}
			
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
			
			if (Input.GetKeyDown(KeyCode.Alpha2)){
				//animatorCont.setInterger("DeathD", 0);
				//animatorCont.setBoolean("Dead", true);
				animatorCont.setBoolean("Charge", false);
				//animatorCont.setInterger("AttD", 0);
				animatorCont.setInteger("HitD", 0);
				animatorCont.setBoolean("Shot", true);
			}

		}

		//Debug.Log(gameObject.renderer.bounds.size.y);
		switch(fsm.getCurrentState()){

		case StateEnums.ZombieStates.Idle:

			if (animDebug){
				animatorCont.setStartState(0);
				

				//animatorCont.setInteger("IdleD", 3);
				//animatorCont.setBoolean("Charge",true);
				//animatorCont.setInteger("AttD",0);
				//fsm.enterState(StateEnums.ZombieStates.Puking);
				
				break;
			}




			//techincally do nothing
			eventChoiceC += Time.deltaTime;
			checkPlayerC += Time.deltaTime;
			//update counters
			//keep counting for random event
		/*	if (eventChoiceC > eventChoiceD){
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
			}*/

			//check if we can see player every 1s
			if (sightAlert && checkPlayerC > checkPlayerD){
				//need to still set this up
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
				Debug.Log("puke time is over");
				fsm.enterPreviousState();
				pukeC = 0;
				puking = false;
				pukeEffect.SetActive(false);
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


	bool checkRayCast(RaycastHit[] rayData){
		int i = 0;
		int size = rayData.Length;
		float lastDist = viewingSens;
		Transform closestObject = null;
		while(i < size){
				//we loop through and assign the closest object
			if (rayData[i].transform != this.transform && rayData[i].distance < lastDist){
				closestObject = rayData[i].transform;
				lastDist = rayData[i].distance;
			}
			i++;
			Debug.Log("checked object");
		}
		if (closestObject != null){

			Debug.Log(closestObject.ToString());
			Debug.Log(closestObject.parent.ToString());
			Debug.Log(closestObject.parent.tag.ToString());
			if (closestObject.parent.tag == "Player"){
				return true;
				
			}
		}


		return false;


	}


	//we check if the AI unit can see the player
	public void checkForPlayer(){
		//could maybe place view frustum and check if the player is detected?

		Vector3 enemyHead = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + rayCastOffset,gameObject.transform.position.z);
		transform.LookAt(player.transform.position);
		RaycastHit[] raycast = Physics.RaycastAll(enemyHead, transform.forward, viewingSens);
		Debug.DrawRay(enemyHead, transform.forward*viewingSens, Color.green);
		if (checkRayCast(raycast)){
			//if we find the player
			fsm.enterState(StateEnums.ZombieStates.Running);
			//activate the nav mesh agen here as well
			//maybe do this in running?
		}
		else{
			//do nothing
		}

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
			transform.LookAt(player.transform.position);
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
		Debug.Log("Enetered puke method");
		puking = true;
		pukeEffect.SetActive(true);
		/*Component[] comp = pukeEffect.GetComponentsInChildren<ParticleSystem>();
		for (int i = 0; i < 7;i++){

		}*/

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
	}

	void lessenSenses(){
		viewingSens = viewingSenseNorm;
		listeningSens = listeningSensNorm;
	}

	void OnTriggerEnter(Collider collider){
		if (collider.tag == "Player"){
			Debug.Log("registered collision with " + collider.gameObject.name);
			sightAlert = true;
			player = collider.gameObject;
		}

	}



}
