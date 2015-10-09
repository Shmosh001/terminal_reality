using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// class for the main zombie AI unit which contains its decision tree and all methods that delegates how it reacts in all situations
/// </summary>
public class WheelChairFSM : AIEntity<StateEnums.WheelZombieStates> {


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
    public float runningDistance, attackingDistance = 3.0f, lineOfSight = 30;


    private Vector3 shotPosition;

    //PRIVATE VARS



    //booleans to ascertain certain state specifics
    //private bool trigger, puking, wandering, alerted, walking, running, soundAlert, sightAlert, soundTrigger, chasing, shot, deadBool;
    private bool patroling, chasing;
    //counters
    //private float eventChoiceC, checkPlayerC, wanderC, pukeC, alertedC, searchingC, dyingC, chasingC, shotC, attackC;
    private float idleC, idleD = 2.0f, attackC, chaseSpeed = 6, patrolSpeed = 3;

	//animation controller
	private WheelchairAnimationController animatorCont;

    //the fsm which controls changing states and the state machine of the AI
	private StateMachineClass<StateEnums.WheelZombieStates> fsm;

    //detection class which manages and handles detection events
	private PreyDetection detection;
    private PatrolScript patrol;
	

    //photon view component for rpc calls
    private PhotonView pView;


    //METHODS

	// Use this for initialization
	void Start () {
        patrol = gameObject.GetComponent<PatrolScript>();
        pView = gameObject.GetComponent<PhotonView>();
        if (pView == null) {
            Debug.LogError("No photon view component found");
            return;
        }
		detection = gameObject.GetComponent<PreyDetection>();
		fsm = new StateMachineClass<StateEnums.WheelZombieStates>();
        fsm.enterState(StateEnums.WheelZombieStates.Idle);
        
        
        animatorCont = gameObject.GetComponent<WheelchairAnimationController>();


	}




    /// <summary>
    /// Update is called once per frame, this checks which state we are currently in and acts accordingly
    /// </summary>
    void Update () {


       
        switch ((byte)fsm.getCurrentState()) {

            /***********Idle*******Idle*******Idle*******Idle*******Idle*******Idle*******Idle*/
            case (byte)StateEnums.WheelZombieStates.Idle:

                if (stateDebugStatements)  Debug.Log("idle case: entering " + Time.timeSinceLevelLoad);


                idleC += Time.deltaTime;

                if (idleC > idleD) {
                    Debug.Log("Entered patrol mode from idle");
                    fsm.enterState(StateEnums.WheelZombieStates.Patrolling);
                    
                }


                checkForPlayer();
                break;

            
            /***********Chasing*******Chasing*******Chasing*******Chasing*******Chasing*******Chasing*******Chasing*/
            case (byte)StateEnums.WheelZombieStates.Chasing:
                if (stateDebugStatements)  Debug.Log("chasing case: entering " + Time.timeSinceLevelLoad);

                if (!chasing) {
                    if (PhotonNetwork.offlineMode) {
                        animatorCont.setTriggerWC(WheelchairHash.chasingTrigger, pView.viewID);
                    }
                    else {
                        pView.RPC("setTriggerWC", PhotonTargets.AllViaServer, WheelchairHash.chasingTrigger, pView.viewID);
                    }
                    chasing = true;
                    navAgent.speed = chaseSpeed;
                }


                chasePlayer();

                

                break;

           
            /***********Attacking*******Attacking*******Attacking*******Attacking*******Attacking*******Attacking*******Attacking*/
            case (byte)StateEnums.WheelZombieStates.Attacking:
                if (stateDebugStatements)  Debug.Log("attacking case: entering " + Time.timeSinceLevelLoad); 
                attackPlayer();
                break;



            /***********Patrolling*******Patrolling*******Patrolling*******Patrolling*******Patrolling*******Patrolling*******Patrolling*/
            case (byte)StateEnums.WheelZombieStates.Patrolling:
                if (stateDebugStatements)  Debug.Log("patroling case: entering " + Time.timeSinceLevelLoad);
                if (!patroling) {
                    patroling = true;
                    Debug.Log("started patroling");
                    startPatroling();
                    navAgent.speed = patrolSpeed;
                }
                float distance = Vector3.Distance(transform.position, patrol.getCurrentWayPoint());
                if (distance < 1) {
                    //we arrived at target
                    Debug.Log("arrived");
                    stopPatrolling();
                }
                

                //lastly we always check if we can see/hear the player
                checkForPlayer();
                break;

            
            /***********Dying*******Dying*******Dying*******Dying*******Dying*******Dying*******Dying*******Dying*******Dying*/
            case (byte)StateEnums.WheelZombieStates.Dying:
                if (stateDebugStatements)  Debug.Log("dying case: entering " + Time.timeSinceLevelLoad);
                navAgent.Stop();
                fsm.enterState(StateEnums.WheelZombieStates.Dead);
                break;

            /***********Dead*******Dead*******Dead*******Dead*******Dead*******Dead*******Dead*******Dead*******Dead*/
            case (byte)StateEnums.WheelZombieStates.Dead:
                if (stateDebugStatements)  Debug.Log("dead case: entering " + Time.timeSinceLevelLoad);
                //we disable all non vital components
                dead();
                
                break;
        }
        

	}



    /// <summary>
    /// stops patrolling
    /// </summary>
    public void stopPatrolling() {
        //if we have arrived we stop nav agent movement and assign the previous state
        navAgent.Stop();
        fsm.enterState(StateEnums.WheelZombieStates.Idle);
        idleC = 0;

        patroling = false;

        if (PhotonNetwork.offlineMode) {
            animatorCont.setTriggerWC(WheelchairHash.idleTrigger, pView.viewID);
        }
        else {
            pView.RPC("setTriggerWC", PhotonTargets.AllViaServer, WheelchairHash.idleTrigger, pView.viewID);
        }

    }




    /// <summary>
    /// starts wandering in a random direction
    /// </summary>
    void startPatroling() {
        if (debugStatements)  Debug.Log("startWandering method at" + Time.timeSinceLevelLoad); 
        
        //we get a location to move to from the patrol script
        
        

        //we check if the path settings works
        if (navAgent.SetDestination(patrol.getNextWayPoint())) {
            if (debugStatements)  Debug.Log("startWandering method: if path was set succesfully at" + Time.timeSinceLevelLoad);

            if (PhotonNetwork.offlineMode) {
                animatorCont.setTriggerWC(WheelchairHash.patrolingTrigger, pView.viewID);
            }
            else {
                pView.RPC("setTriggerWC", PhotonTargets.AllViaServer, WheelchairHash.patrolingTrigger, pView.viewID);
            }
        }
        //if for some reason the path setting fails
        else {
            if (debugStatements)  Debug.Log("startWandering method: if path set failed at" + Time.timeSinceLevelLoad); 
            //stop any path navigation
            navAgent.Stop();
            fsm.enterPreviousState();

            
        }

    }


    /// <summary>
    /// chases the nearest player and checks weither or not to attack him or if the
    /// unit has lost him based on the distance
    /// </summary>
    void chasePlayer(){
    	if (debugStatements)Debug.Log("chasePlayer method at" + Time.timeSinceLevelLoad);


        navAgent.SetDestination(target.transform.position);
        detection.assignLastPosition(target.transform.position);




        float distance = getDistanceT(this.gameObject.transform, target.transform);


        //Debug.LogWarning(distance);
        //Debug.LogWarning("*"+attackingDistance);

        //we check if we are close enough to attack the target
        //or if we are far away enough to have lost the target
        //if (navAgent.remainingDistance < navAgent.stoppingDistance)
        if (distance <= attackingDistance) {
            if (debugStatements)  Debug.Log("chasePlayer method: ready to attack at" + Time.timeSinceLevelLoad);
            //we make appropriate changes in the fsm, the navigation mesh traversal and the animations

            //Debug.LogWarning("ATTACKING");
            fsm.enterState(StateEnums.WheelZombieStates.Attacking);
            
            navAgent.Stop();

			
            if (PhotonNetwork.offlineMode) {
                animatorCont.setTriggerWC(WheelchairHash.attackingTrigger, pView.viewID);
            }
            else {
                pView.RPC("setTriggerWC", PhotonTargets.AllViaServer, WheelchairHash.attackingTrigger, pView.viewID);
            }
        }
        
	}

    




    /// <summary>
    /// disables all parts to the unit to only leave dead body
    /// </summary>
    void dead(){
		if (debugStatements)Debug.Log("dead method at" + Time.timeSinceLevelLoad);
        transform.parent.gameObject.GetComponent<RagdollFollow>().enableRagdoll(shotPosition);

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
            targetH.reducePlayerHealth(damage);
            attackC = 0;
		}

            //we check the distance to make sure we are still close enough to attack
            //if we are too far away we start chasing the player again
		float distance = getDistanceT(transform, target.transform);
		//add a small offset
		if (distance > attackingDistance+3){
			fsm.enterState(StateEnums.WheelZombieStates.Chasing);
			
            
            
            if (PhotonNetwork.offlineMode) {
                animatorCont.setTriggerWC(WheelchairHash.chasingTrigger, pView.viewID);
            }
            else {
                pView.RPC("setTriggerWC", PhotonTargets.AllViaServer, WheelchairHash.chasingTrigger, pView.viewID);
            }
        }
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
			if (debugStatements)Debug.Log("collider entrance with " + collider.gameObject.name + " at " + Time.timeSinceLevelLoad);
            //assign the target
 
            detection.assignTarget(collider.gameObject);
            target = collider.gameObject;
		}
        

	}



    /// <summary>
    /// we check if the AI unit can see the player
    /// </summary>
    void checkForPlayer(){
		
		if (debugStatements)Debug.Log("checkForPlayer method at" + Time.timeSinceLevelLoad);
            //we check for sounds last as viewing is more important
        //we need to be in the radius of the sound collider in order to be seen. radius is much larger than the viewing distance
        if (detection.targetInSight(lineOfSight)){
			if (debugStatements)Debug.Log("checkForPlayer method: player spotted " + Time.timeSinceLevelLoad);
			//we need to now change into the appropriate state
			fsm.enterState(StateEnums.WheelZombieStates.Chasing);
			
           
        }
        
	}

    

    /// <summary>
    /// alerts the unit that is has died, basically just a state change
    /// </summary>
	public void alertDead(Vector3 position){

        shotPosition = position;
        enterState((byte)StateEnums.WheelZombieStates.Dying);
    }


    




    public void enterState(byte state) {
        //we only switch states if we are in a different state and fsm is not null
        if (fsm != null && (byte)fsm.getCurrentState() != state) {

            fsm.enterState((StateEnums.WheelZombieStates)state);
        }
        


    }

    public void enterState(StateEnums.WheelZombieStates state) {
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
            if (fsm != null) {
                fsm.enterState((StateEnums.WheelZombieStates)stream.ReceiveNext());
            }
            
        }
        
    }


}

