using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// class for the main zombie AI unit which contains its decision tree and all methods that delegates how it reacts in all situations
/// </summary>
public class ZombieFSM : AIEntity<StateEnums.ZombieStates> {


    //PUBLIC VARS

    //debug booleans
    public bool stateDebugStatements;
    public bool debugStatements;
    public bool debug;
    public bool animDebug;

    public GameObject ragdoll;

    //debug text for states
    public Text text;
    public Text text2;

    //puking particle effect
    public GameObject pukeEffect;

    //offset used for raycasting for prey detection
    public float rayCastOffset = 1.5f;

    //floats values which are sued for counters and durations and general values
    public float eventChoiceD = 10.0f, wanderD, checkPlayerD, pukeD = 7.917f, alertedD = 10.0f, searchingD, dyingD, chasingD = 1f, shotD, attackD = 2;
    //sense values
    public float viewingSenseNorm, viewingSensAlert, listeningSensNorm = 8, listeningSensAlert = 12;
    //speed values
    public float walkingSpeed, RunningSpeed, speed;
    //distance values
    public float runningDistance, attackingDistance = 1.5f, losingDistance = 20.0f;


    private Vector3 shotPosition;

    //PRIVATE VARS

    //the entities audio source
    private AudioSource audioSource;
    
    //the box collider which is used to detect when a player is within a close range and is set as a target
	private BoxCollider boxCollider;

	//booleans to ascertain certain state specifics
	private bool trigger, puking, wandering, alerted, walking, running, soundAlert, sightAlert, soundTrigger, chasing, shot, deadBool;
	
    //counters
	private float eventChoiceC, checkPlayerC, wanderC, pukeC, alertedC, searchingC, dyingC, chasingC, shotC, attackC;
	
	//sensitivity of the AI unit
	private float viewingSens, listeningSens;
    
    //the destination to wander to
	private Transform wanderDest;

	//animation controller
	private ZombieAnimationController animatorCont;

    //the fsm which controls changing states and the state machine of the AI
	private StateMachineClass<StateEnums.ZombieStates> fsm;

    //detection class which manages and handles detection events
	private PreyDetection detection;
	
    //the collider that checks for sound
	private SphereCollider soundCollider;

    //photon view component for rpc calls
    private PhotonView pView;


    //METHODS

	// Use this for initialization
	void Start () {
        pView = gameObject.GetComponent<PhotonView>();
        if (pView == null) {
            Debug.LogError("No photon view component found");
            return;
        }
        audioSource = gameObject.GetComponent<AudioSource>();
		detection = gameObject.GetComponent<PreyDetection>();
		fsm = new StateMachineClass<StateEnums.ZombieStates>();
        fsm.enterState(StateEnums.ZombieStates.Idle);
        /*if (PhotonNetwork.offlineMode) {
            enterState((byte)StateEnums.ZombieStates.Idle);
        }
        else {
            pView.RPC("enterState", PhotonTargets.AllViaServer, (byte)StateEnums.ZombieStates.Idle);
        }*/
        
        animatorCont = gameObject.GetComponent<ZombieAnimationController>();
		soundCollider  = gameObject.GetComponent<SphereCollider>();
		boxCollider = gameObject.GetComponent<BoxCollider>();
        
        
        //lessen senses
        lessenSenses();
        
        //assign starting speed
		speed = walkingSpeed;

		//we need to choose a default position for the zombie to start on
		animatorCont.chooseStartingState();
		trigger = true;
	}




    /// <summary>
    /// Update is called once per frame, this checks which state we are currently in and acts accordingly
    /// </summary>
    void Update () {


       
        switch ((byte)fsm.getCurrentState()) {

            /***********Idle*******Idle*******Idle*******Idle*******Idle*******Idle*******Idle*/
            case (byte)StateEnums.ZombieStates.Idle:

                if (stateDebugStatements) { Debug.Log("idle case: entering " + Time.timeSinceLevelLoad); }

                //increment counters with delta time
                eventChoiceC += Time.deltaTime;
                checkPlayerC += Time.deltaTime;
                //keep counting for random event trigger
                //based on this trigger we have a selection of random paths that are used
                //to traverse the animation controller to give a random flow of animations
                if (eventChoiceC > eventChoiceD) {
                    //we get a random value
                    bool result = animatorCont.setRandomTrigger(EnemyHashScript.changeTrigger);

                    //if we got a positive result
                    if (result) {
                        //we determine what path we take
                        int path = animatorCont.setRandomInteger(EnemyHashScript.idleVarDInt, 4);

                        switch (path) {
                            //agonizing animation
                            case 0:
                                //play the sound and assign the animator controller parameter 
                                sound.playFemaleScream(transform.position);
                                animatorCont.setRandomInteger(EnemyHashScript.idleDInt, 6);
                                break;
                            //scream animation
                            case 1:
                                //dont need to change state
                                //play screaming sound and assign the animator parameter
                                sound.playMaleScream(transform.position);
                                animatorCont.setRandomInteger(EnemyHashScript.idleDInt, 6);
                                break;
                            //crying/puking animation
                            case 2:
                                //enter the new state 
                                //set the animator  parameter
                                animatorCont.setRandomInteger(EnemyHashScript.idleDInt, 6);
                                

                                fsm.enterState(StateEnums.ZombieStates.Puking);
                                
                                /*if (PhotonNetwork.offlineMode) {
                                    enterState((byte)StateEnums.ZombieStates.Puking);
                                }
                                else {
                                    pView.RPC("enterState", PhotonTargets.AllViaServer, (byte)StateEnums.ZombieStates.Puking);
                                }*/
                                break;
                            //wandering animation
                            case 3:
                                animatorCont.setRandomInteger(EnemyHashScript.idleDInt, 6);
                                fsm.enterState(StateEnums.ZombieStates.Wandering);
                                
                                /*if (PhotonNetwork.offlineMode) {
                                    enterState((byte)StateEnums.ZombieStates.Wandering);
                                }
                                else {
                                    pView.RPC("enterState", PhotonTargets.AllViaServer, (byte)StateEnums.ZombieStates.Wandering);
                                }*/
                                break;
                        }
                    }
                    //reset
                    eventChoiceC = 0;
                }
                //check if we can see player every 1s
                if (soundTrigger && checkPlayerC > checkPlayerD) {
                    //check for the player and reset the counter
                    checkForPlayer();
                    checkPlayerC = 0;
                }
                break;

            /***********Alerted*******Alerted*******Alerted*******Alerted*******Alerted*******Alerted*******Alerted*/
            case (byte)StateEnums.ZombieStates.Alerted:
                //debug
                if (stateDebugStatements) Debug.Log("alerted case: entering " + Time.timeSinceLevelLoad);



                if (PhotonNetwork.offlineMode) {
                    animatorCont.resetBooleans(pView.viewID);
                }
                else {
                    pView.RPC("resetBooleans", PhotonTargets.AllViaServer, pView.viewID);
                }

                

                //increment counter
                alertedC += Time.deltaTime;

                //check for the player
                checkForPlayer();

                //if the unit has been alerted for a certain amount if time we set it back to its old state
                //is supposed to seem like the unit loses interest because nothing else triggered it
                if (alertedC > alertedD) {
                    if (stateDebugStatements) { Debug.Log("alerted case: timer ran over " + Time.timeSinceLevelLoad); }
                    //we got back to idle
                    //animatorCont.setTrigger(EnemyHashScript.alertToIdleTrigger);
                    //rpc conversion
                    
                    if (PhotonNetwork.offlineMode) {
                        animatorCont.setTrigger(EnemyHashScript.alertToIdleTrigger, pView.viewID);
                    }
                    else {
                        pView.RPC("setTrigger", PhotonTargets.AllViaServer, EnemyHashScript.alertToIdleTrigger, pView.viewID);
                    }
                    animatorCont.setRandomInteger(EnemyHashScript.idleDInt, 6);
                    //we change states
                    //
                    fsm.enterState(StateEnums.ZombieStates.Idle);
                    
                    /*if (PhotonNetwork.offlineMode) {
                        enterState((byte)StateEnums.ZombieStates.Idle);
                    }
                    else {
                        pView.RPC("enterState", PhotonTargets.AllViaServer, (byte)StateEnums.ZombieStates.Idle);
                    }*/
                    alertedC = 0;
                    alerted = false;
                    //we revert the heightened senses again
                    lessenSenses();
                }
                break;

            /***********Chasing*******Chasing*******Chasing*******Chasing*******Chasing*******Chasing*******Chasing*/
            case (byte)StateEnums.ZombieStates.Chasing:
                if (stateDebugStatements) { Debug.Log("chasing case: entering " + Time.timeSinceLevelLoad); }
                //counter
                chasingC += Time.deltaTime;
                //we call the chasing method which handles the pursuit of the prey
                if (!chasing) {
                    chasePlayer();
                }
                //update the position of the target we are chasing
                if (chasing && chasingC > chasingD) {
                    if (stateDebugStatements) { Debug.Log("chasing case: if statement " + Time.timeSinceLevelLoad); }
                    chasePlayer();
                    //we store all information on the target we are chasing and send the unit to its updated position on the nav mesh
                    
                    if (target == null) {
                        handleNoTarget();
                        break;
                    }
                    detection.assignTarget(target);
                    chasingC = 0;
                }
                navAgent.SetDestination(target.transform.position);
                detection.assignLastPosition(target.transform.position);
                break;

            /***********Searching*******Searching*******Searching*******Searching*******Searching*******Searching*******Searching*/
            case (byte)StateEnums.ZombieStates.Searching:

                if (stateDebugStatements) { Debug.Log("searching case: entering " + Time.timeSinceLevelLoad); }
                //we search for the player by moving to his last known position
                //once we are there, we stop searching

                //check for arrival at the position or if we don't have a last known position
                if (!detection.hasLastPosition() || checkArrival(transform.position, navAgent.destination)) {
                    //stop the nav agent from moving and revert to the alerted state
                    navAgent.Stop();
                    //rpc conversion
                    
                   if (PhotonNetwork.offlineMode) {
                        animatorCont.setTrigger(EnemyHashScript.alertedTrigger, pView.viewID);
                    }
                    else {
                        pView.RPC("setTrigger", PhotonTargets.AllViaServer, EnemyHashScript.alertedTrigger, pView.viewID);
                    }
                    //animatorCont.setTrigger(EnemyHashScript.alertedTrigger);
                    fsm.enterState(StateEnums.ZombieStates.Alerted);
                    //rpc conversion
                    
                    /*if (PhotonNetwork.offlineMode) {
                        enterState((byte)StateEnums.ZombieStates.Alerted);
                    }
                    else {
                        pView.RPC("enterState", PhotonTargets.AllViaServer, (byte)StateEnums.ZombieStates.Alerted);
                    }*/
                }
                else {
                    //keep searching
                    searchForPlayer();
                }
                break;

            /***********Attacking*******Attacking*******Attacking*******Attacking*******Attacking*******Attacking*******Attacking*/
            case (byte)StateEnums.ZombieStates.Attacking:
                if (stateDebugStatements) { Debug.Log("attacking case: entering " + Time.timeSinceLevelLoad); }
                attackPlayer();
                break;

            /***********Puking*******Puking*******Puking*******Puking*******Puking*******Puking*******Puking*******Puking*/
            case (byte)StateEnums.ZombieStates.Puking:
               // Debug.Log("puking case: entering " + Time.timeSinceLevelLoad);
                //updating counter

                //Debug.Log(pukeC);
                pukeC += Time.deltaTime;

                if (!puking) {
                    Debug.Log("puke started");
                    puke();
                }

               // Debug.Log(pukeC);
                //if timer is over the limit
                if (pukeC > pukeD) {
                    //Debug.Log("puking case: puke time is over " + Time.timeSinceLevelLoad); 
                    fsm.enterPreviousState();
                    
                   /* if (PhotonNetwork.offlineMode) {
                        fsm.enterPreviousState();
                    }
                    else {
                        pView.RPC("enterPrevState", PhotonTargets.AllViaServer);
                    }*/
                    pukeC = 0;
                    puking = false;
                    pukeEffect.SetActive(false);
                }

                break;

            /***********Wandering*******Wandering*******Wandering*******Wandering*******Wandering*******Wandering*******Wandering*/
            case (byte)StateEnums.ZombieStates.Wandering:
                if (stateDebugStatements) { Debug.Log("wandering case: entering " + Time.timeSinceLevelLoad); }

                //if we have not started wandering yet
                if (!wandering) {
                    if (debugStatements) { Debug.Log("wandering case: !wandering = true " + Time.timeSinceLevelLoad); }
                    startWandering();
                    //animatorCont.setBoolean(EnemyHashScript.wanderingBool, true);
                    //rpc conversion
                    
                    if (PhotonNetwork.offlineMode) {
                        animatorCont.setBoolean(EnemyHashScript.wanderingBool, true, pView.viewID);
                    }
                    else {
                        pView.RPC("setBoolean", PhotonTargets.AllViaServer, EnemyHashScript.wanderingBool, true, pView.viewID);
                    }

                }

                //check if we have arrived at the destination
                //this is now done by the actaul wander point that has its own colldier whcih stops the ai unit fromreaching the cerntre but stops a them a bit more randomly

                //lastly we always check if we can see/hear the player
                checkForPlayer();
                break;

            /***********Shot*******Shot*******Shot*******Shot*******Shot*******Shot*******Shot*******Shot*******Shot*/
            case (byte)StateEnums.ZombieStates.Shot:
                if (stateDebugStatements) { Debug.Log("shot case: entering " + Time.timeSinceLevelLoad); }

                //if entity has been shot
                if (!shot) {

                    if (stateDebugStatements) { Debug.Log("shot case: setting up animation " + Time.timeSinceLevelLoad); }

                    //we set the random int value for the decision
                    animatorCont.setRandomInteger(EnemyHashScript.hitDInt, 3);
                    //we activate the shot trigger 
                    //animatorCont.setTrigger(EnemyHashScript.shotTrigger);
                    //rpc conversion
                    
                    if (PhotonNetwork.offlineMode) {
                        animatorCont.setTrigger(EnemyHashScript.shotTrigger, pView.viewID);
                    }
                    else {
                        pView.RPC("setTrigger", PhotonTargets.All, EnemyHashScript.shotTrigger, pView.viewID);
                    }
                    shot = true;
                    //stop nav agent movement
                    navAgent.Stop();
                }

                //play screaming sound
                if (animatorCont.checkAnimationState(EnemyHashScript.shotScreamState) && trigger) {
                    sound.playMaleScream(transform.position);
                    trigger = false;
                }

                //check to see if we are in the attack state
                if (animatorCont.checkAnimationState(EnemyHashScript.attackDecisionState)) {
                    if (stateDebugStatements) { Debug.Log("shot case: animation has stopped " + Time.timeSinceLevelLoad); }
                    fsm.enterState(StateEnums.ZombieStates.Chasing);
                    //rpc conversion
                    

                    //resume movement
                    navAgent.Resume();
                    shot = false;
                }
                break;

            /***********Dying*******Dying*******Dying*******Dying*******Dying*******Dying*******Dying*******Dying*******Dying*/
            case (byte)StateEnums.ZombieStates.Dying:
                if (stateDebugStatements) { Debug.Log("dying case: entering " + Time.timeSinceLevelLoad); }
                killUnit();
                break;

            /***********Dead*******Dead*******Dead*******Dead*******Dead*******Dead*******Dead*******Dead*******Dead*/
            case (byte)StateEnums.ZombieStates.Dead:
                if (stateDebugStatements) { Debug.Log("dead case: entering " + Time.timeSinceLevelLoad); }
                //we disable all non vital components
                if (!deadBool) {
                    animatorCont.turnOffRM();
                    dead();
                    deadBool = true;
                }
                
                break;
        }
        
        //debugging
		/*if (debug){
			text.text = fsm.getCurrentState().ToString();
			text2.text = getDistance(player.transform, transform).ToString();
		}
		else{
			text.text = "";
			text2.text = "";
		}*/
	}




    public void stopWandering() {
        //if we have arrived we stop nav agent movement and assign the previous state
        navAgent.Stop();
        fsm.enterPreviousState();

        /*if (PhotonNetwork.offlineMode) {
            fsm.enterPreviousState();
        }
        else {
            pView.RPC("enterPrevState", PhotonTargets.AllViaServer);
        }*/
        wandering = false;
        //animatorCont.setBoolean(EnemyHashScript.wanderingBool, false);
        //rpc conversion

        if (PhotonNetwork.offlineMode) {
            animatorCont.setBoolean(EnemyHashScript.wanderingBool, false, pView.viewID);
        }
        else {
            pView.RPC("setBoolean", PhotonTargets.AllViaServer, EnemyHashScript.wanderingBool, false, pView.viewID);
        }

    }


    /// <summary>
    /// chases the nearest player and checks weither or not to attack him or if the
    /// unit has lost him based on the distance
    /// </summary>
    void chasePlayer(){
    	if (debugStatements){Debug.Log("chasePlayer method at" + Time.timeSinceLevelLoad);}

        //if we are not yest chasing the target (first response actions)
		if (!chasing){
			if (debugStatements)Debug.Log("chasing case:: forcing chase animation " + Time.timeSinceLevelLoad);
			    //we set the animation and relevant parameters
			animatorCont.resetBooleans(pView.viewID);
			animatorCont.setRandomInteger(EnemyHashScript.attDInt,2);
            animatorCont.forceAnimation(EnemyHashScript.attackDecisionState);
            //rpc conversion

           /* if (PhotonNetwork.offlineMode) {

                animatorCont.resetBooleans(pView.viewID);
            }
            else {
                pView.RPC("resetBooleans", PhotonTargets.AllViaServer, pView.viewID);
            }*/




            if (PhotonNetwork.offlineMode) {
                
                animatorCont.forceAnimation(EnemyHashScript.attackDecisionState, pView.viewID);
            }
            else {
                pView.RPC("forceAnimation", PhotonTargets.AllViaServer, EnemyHashScript.attackDecisionState, pView.viewID);
            }
            //we set a new nav mesh destination
            if (target == null) {
                return;
            }
            navAgent.SetDestination(target.transform.position);
			chasing = true;
		}

		float distance = getDistanceT(target.transform, gameObject.transform);

            //we check if we are close enough to attack the target
            //or if we are far away enough to have lost the target
        //if (navAgent.remainingDistance < navAgent.stoppingDistance)
        if (distance < attackingDistance) {
            if (debugStatements) { Debug.Log("chasePlayer method: ready to attack at" + Time.timeSinceLevelLoad); }
            //we make appropriate changes in the fsm, the navigation mesh traversal and the animations
            fsm.enterState(StateEnums.ZombieStates.Attacking);
            //rpc conversion
			
           /*if (PhotonNetwork.offlineMode) {
                enterState((byte)StateEnums.ZombieStates.Attacking);
            }
            else {
                pView.RPC("enterState", PhotonTargets.AllViaServer, (byte)StateEnums.ZombieStates.Attacking);
            }*/
            navAgent.Stop();
            //animatorCont.setBoolean(EnemyHashScript.attackingBool, true);
            //rpc conversion
			
            if (PhotonNetwork.offlineMode) {
                animatorCont.setBoolean(EnemyHashScript.attackingBool, true, pView.viewID);
            }
            else {
                pView.RPC("setBoolean", PhotonTargets.AllViaServer, EnemyHashScript.attackingBool, true, pView.viewID);
            }
            chasing = false;
        }
        //lost the target
        else if (distance > losingDistance) {
            if (debugStatements) Debug.Log("chasePlayer method: too far away to attack at" + Time.timeSinceLevelLoad); 
                //we make appropriate changes in the fsm and the animations
            fsm.enterState(StateEnums.ZombieStates.Searching);
            //rpc conversion
			
            /*if (PhotonNetwork.offlineMode) {
                enterState((byte)StateEnums.ZombieStates.Searching);
            }
            else {
                pView.RPC("enterState", PhotonTargets.AllViaServer, (byte)StateEnums.ZombieStates.Searching);
            }*/
            animatorCont.resetBooleans(pView.viewID);
            chasing = false;
        }
	}




    /// <summary>
    /// this kills the unit and plays specific animation
    /// </summary>
    void killUnit(){
		if (debugStatements){Debug.Log("killUnit method at" + Time.timeSinceLevelLoad);}
		    //stop moving on the nav mesh
		navAgent.Stop();
		    //play animation
		animatorCont.resetBooleans(pView.viewID);
		//animatorCont.setTrigger(EnemyHashScript.deadTrigger);
        //rpc conversion
		
        if (PhotonNetwork.offlineMode) {
            animatorCont.setTrigger(EnemyHashScript.deadTrigger, pView.viewID);
        }
        else {
            pView.RPC("setTrigger", PhotonTargets.AllBufferedViaServer, EnemyHashScript.deadTrigger, pView.viewID);
        }
        animatorCont.setRandomInteger(EnemyHashScript.deathDsInt,2);
		fsm.enterState(StateEnums.ZombieStates.Dead);
        
        /*if (PhotonNetwork.offlineMode) {
            enterState((byte)StateEnums.ZombieStates.Dead);
        }
        else {
            pView.RPC("enterState", PhotonTargets.AllBufferedViaServer, (byte)StateEnums.ZombieStates.Dead);
        }*/

    }

    /// <summary>
    /// disables all parts to the unit to only leave dead body
    /// </summary>
    void dead(){
		if (debugStatements)Debug.Log("dead method at" + Time.timeSinceLevelLoad);
        transform.parent.gameObject.GetComponent<RagdollFollow>().enableRagdoll(shotPosition);
        


       

    }




    /// <summary>
    /// we send the AI unit on a path to search for the player
    /// </summary>
    void searchForPlayer(){
		//we go to the players last known position
		if (debugStatements){Debug.Log("searchForPlayer method at" + Time.timeSinceLevelLoad);}

        //we check if the setting a destination succeeds (should always succeed)
        if (navAgent.SetDestination(detection.lastSighting)){
			if (debugStatements){Debug.Log("searchForPlayer method: dest set true at" + Time.timeSinceLevelLoad);}
                //set the entity up to go search 
            navAgent.speed = walkingSpeed;
			//animatorCont.setTrigger(EnemyHashScript.searchingTrigger);
            //rpc conversion
			
            if (PhotonNetwork.offlineMode) {
                animatorCont.setTrigger(EnemyHashScript.searchingTrigger, pView.viewID);
            }
            else {
                pView.RPC("setTrigger", PhotonTargets.AllViaServer, EnemyHashScript.searchingTrigger, pView.viewID);
            }
            heightenSenses();
		}
		//if the paths 
		else{
			if (debugStatements){Debug.Log("searchForPlayer method: dest set false at" + Time.timeSinceLevelLoad);}
			//stop any path navigation
			lessenSenses();
			navAgent.Stop();
			fsm.enterState(StateEnums.ZombieStates.Wandering);
            //rpc conversion
			
            /*if (PhotonNetwork.offlineMode) {
                enterState((byte)StateEnums.ZombieStates.Wandering, pView.viewID);
            }
            else {
                pView.RPC("enterState", PhotonTargets.AllViaServer, (byte)StateEnums.ZombieStates.Wandering, pView.viewID);
            }*/
        }

	}


    /// <summary>
    /// starts wandering in a random direction
    /// </summary>
    void startWandering(){
		if (debugStatements){Debug.Log("startWandering method at" + Time.timeSinceLevelLoad);}
		lessenSenses();
		wandering = true;
		//we get a location to move to from the wandering script
		wanderDest = wanderScript.getPoint();

        //we check if the path settings works
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
			
            /*if (PhotonNetwork.offlineMode) {
                fsm.enterPreviousState();
            }
            else {
                pView.RPC("enterPrevState", PhotonTargets.AllViaServer);
            }*/
        }
		
	}

    /// <summary>
    /// entity pukes at its positon
    /// </summary>
    void attackPlayer(){
		if (debugStatements)Debug.Log("attackPlayer method at" + Time.timeSinceLevelLoad);
        //inflict damage on player

        if (target == null) {
            handleNoTarget();
            return;
        }

        playerHealthScript targetH = target.GetComponent<playerHealthScript>();

            //increment counter
        attackC += Time.deltaTime;

            //if we are able to attack
        if (targetH != null && attackC > attackD){
            //reduce health

            if (targetH.playerData == null) {
                return;
            }
            if (targetH.playerData.playerAlive) {
                targetH.reducePlayerHealth(damage);
            }
            else {
                fsm.enterState(StateEnums.ZombieStates.Alerted);
            }
            attackC = 0;
		}

            //we check the distance to make sure we are still close enough to attack
            //if we are too far away we start chasing the player again
		float distance = getDistanceT(transform, target.transform);
		//add a small offset
		if (distance > attackingDistance){
			fsm.enterState(StateEnums.ZombieStates.Chasing);
			
            /*if (PhotonNetwork.offlineMode) {
                enterState((byte)StateEnums.ZombieStates.Chasing);
            }
            else {
                pView.RPC("enterState", PhotonTargets.AllViaServer, (byte)StateEnums.ZombieStates.Chasing);
            }*/
            //animatorCont.setBoolean(EnemyHashScript.attackingBool,false);
            //rpc conversion
            
            if (PhotonNetwork.offlineMode) {
                animatorCont.setBoolean(EnemyHashScript.attackingBool,false, pView.viewID);
            }
            else {
                pView.RPC("setBoolean", PhotonTargets.AllViaServer, EnemyHashScript.attackingBool, false, pView.viewID);
            }
        }
	}



    /// <summary>
    /// entity pukes at its position
    /// </summary>
    void puke(){
		Debug.Log("puke method at" + Time.timeSinceLevelLoad);
		//after exit time of the animation we revert back to idle
		//set the puking particle effect
		puking = true;
		pukeEffect.SetActive(true);
	}

    /// <summary>
    /// heightens the senses of the ai unit to make it seem smarter
    /// </summary>
	void heightenSenses(){
		if (debugStatements){Debug.Log("heightenSenses at" + Time.timeSinceLevelLoad);}
		viewingSens = viewingSensAlert;
		listeningSens = listeningSensAlert;
		soundCollider.radius = listeningSens;
	}

    /// <summary>
    /// lessens the senses again to make it seem dumber
    /// </summary>
	void lessenSenses(){
		if (debugStatements){Debug.Log("lessenSenses at" + Time.timeSinceLevelLoad);}
		viewingSens = viewingSenseNorm;
		listeningSens = listeningSensNorm;
		soundCollider.radius = listeningSens;
	}

    /// <summary>
    /// is entered when something enters a collider attached to the AI unit
    /// </summary>
    /// <param name="collider">
    /// the entity that is colliding with the collider
    /// </param>
	void OnTriggerEnter(Collider collider){
        //if we player entered
        if (collider.tag == Tags.PLAYER1 || collider.tag == Tags.PLAYER2) {
			if (debugStatements){Debug.Log("collider entrance with " + collider.gameObject.name + " at " + Time.timeSinceLevelLoad);}
            //assign the target
            detection.assignTarget(collider.gameObject);
            /*if (PhotonNetwork.offlineMode) {
                if (collider == null) {
                    return;
                }
                detection.assignTarget(collider.gameObject);
            }
            else {
                pView.RPC("assignTarget", PhotonTargets.AllViaServer, collider.gameObject.tag);
            }*/
            target = collider.gameObject;
			soundTrigger = true;
		}
        else if(collider is BoxCollider){
            //we set the awake boolean
            //animatorCont.setTrigger(EnemyHashScript.wakeupTrigger, pView.viewID);
            //rpc conversion
            if (pView != null) {
                
                if (PhotonNetwork.offlineMode) {
                    animatorCont.setTrigger(EnemyHashScript.wakeupTrigger, pView.viewID);
                }
                else {
                    pView.RPC("setTrigger", PhotonTargets.AllViaServer, EnemyHashScript.wakeupTrigger, pView.viewID);
                }
            }
            //disable collider
            if (boxCollider != null) {
                boxCollider.enabled = false;
            }
		}

	}

    /// <summary>
    /// if something leaves a collider attached to the AI unit
    /// </summary>
    /// <param name="collider">
    /// unit colliding
    /// </param>
	void OnTriggerExit(Collider collider){
		if (collider.tag == Tags.PLAYER1 || collider.tag == Tags.PLAYER2){
			if (debugStatements){Debug.Log("collider exit with " + collider.gameObject.name + " at " + Time.timeSinceLevelLoad);}
			soundTrigger = false;
		}
	}

    /// <summary>
    /// we check if the AI unit can see the player
    /// </summary>
    void checkForPlayer(){
		
		if (debugStatements){Debug.Log("checkForPlayer method at" + Time.timeSinceLevelLoad);}
            //we check for sounds last as viewing is more important
        //we need to be in the radius of the sound collider in order to be seen. radius is much larger than the viewing distance
        if (soundTrigger && detection.targetInSight(viewingSens)){
			if (debugStatements){Debug.Log("checkForPlayer method: player spotted " + Time.timeSinceLevelLoad);}
			    //we need to now change into the appropriate state
			fsm.enterState(StateEnums.ZombieStates.Chasing);

        }
        //if we hear the player based on his animation state
		else if (soundTrigger){
            //TODO set up
			if (debugStatements)Debug.Log("checkForPlayer method: soundTrigger = true at" + Time.timeSinceLevelLoad);
			if (detection.targetHeard()){
				if (debugStatements)Debug.Log("checkForPlayer method: target heard at" + Time.timeSinceLevelLoad);
				alertUnit();
			}
		}
	}

    /// <summary>
    /// we alert this unit that a sound trigger has gone off
    /// </summary>
    void alertUnit(){
		if (debugStatements)Debug.Log("alertUnit method at" + Time.timeSinceLevelLoad);
		//if this is the >second  sound alert
		if (soundAlert){
			if (debugStatements)Debug.Log("alertUnit method: sound alert = true at" + Time.timeSinceLevelLoad);
			//the position should have been set
			//then searching method should take care of moving the npc there
			fsm.enterState(StateEnums.ZombieStates.Searching);
			
            /*if (PhotonNetwork.offlineMode) {
                enterState((byte)StateEnums.ZombieStates.Searching);
            }
            else {
                pView.RPC("enterState", PhotonTargets.AllViaServer, (byte)StateEnums.ZombieStates.Searching);
            }*/
        }
		else{
			if (debugStatements){Debug.Log("alertUnit method else branch at" + Time.timeSinceLevelLoad);}
			soundAlert = true;
			//enhance viewing and listening 
			heightenSenses();
			fsm.enterState(StateEnums.ZombieStates.Alerted);
			
            /*if (PhotonNetwork.offlineMode) {
                enterState((byte)StateEnums.ZombieStates.Alerted);
            }
            else {
                //pView.RPC("enterState", /*PhotonTargets.AllViaServerphotonView.GetInstanceID(), (byte)StateEnums.ZombieStates.Alerted);
                //TODO
            }*/
        }
	}

    /// <summary>
    /// alerts the unit that is has died, basically just a state change
    /// </summary>
	public void alertDead(Vector3 position){
        //fsm.enterState(StateEnums.ZombieStates.Dying);

        /*if (PhotonNetwork.offlineMode) {
            enterState((byte)StateEnums.ZombieStates.Dying);
        }
        else {
            pView.RPC("enterState", PhotonTargets.AllBufferedViaServer, (byte)StateEnums.ZombieStates.Dying);
        }*/
        shotPosition = position;
        enterState((byte)StateEnums.ZombieStates.Dying);
    }

    /// <summary>
    /// alerts the unit that it has been shot
    /// changes state 
    /// </summary>
    /// <param name="entity">
    /// which entity shot it
    /// </param>
	public void alertShot(GameObject entity){
		    //we want to assign the new target either way
		detection.assignTarget(entity.gameObject);
		target = entity.gameObject;
		// we only want to transition into the state if are not currently in the state
		if (!shot){

            enterState((byte)StateEnums.ZombieStates.Shot);
        }
	}




    //for networking purposes

    public void enterState(byte state) {
        //we only switch states if we are in a different state and fsm is not null
        if (fsm != null && (byte)fsm.getCurrentState() != state) {

            fsm.enterState((StateEnums.ZombieStates)state);
        }
        


    }

    public void enterState(StateEnums.ZombieStates state) {
        //we only switch states if we are in a different state and fsm is not null
        if (fsm != null && fsm.getCurrentState() != state) {

            fsm.enterState(state);
        }

    }

    [PunRPC]
    public void enterPrevState() {
        if (fsm != null) {
            fsm.enterPreviousState();
        }
        
    }



    [PunRPC]
    void requestCurrentTarget() {
        Debug.LogWarning("requestCurrentTarget called for " + PhotonNetwork.isMasterClient);
        if (target != null) {
            pView.RPC("receiveCurrentTarget", PhotonTargets.Others, target.tag);
        }
        else {
            Debug.LogError("Also null by me :(");
        }
        
    }


    [PunRPC]
    void receiveCurrentTarget(string tagName) {
        Debug.LogWarning("receiveCurrentTarget called for " + PhotonNetwork.isMasterClient);
        target = detection.assignTarget(tagName);
    }


    void handleNoTarget() {
        Debug.LogWarning("handleNoTarget requested");
        pView.RPC("requestCurrentTarget", PhotonTargets.Others);
    }




    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
       

        
        if (fsm == null) {
            return;
        }
        if (stream.isWriting) {
            stream.SendNext((byte)fsm.getCurrentState());
        }
        //receiving other players things
        else {
            fsm.enterState((StateEnums.ZombieStates)stream.ReceiveNext());
        }
        
    }


}

