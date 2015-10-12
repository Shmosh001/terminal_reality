using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// class for the main zombie AI unit which contains its decision tree and all methods that delegates how it reacts in all situations
/// </summary>
public class BossZombieFSM : AIEntity<StateEnums.BossZombieStates> {


    //PUBLIC VARS

    //debug booleans
    public bool stateDebugStatements;
    public bool debugStatements;
    public bool debug;
    public bool animDebug;



    //offset used for raycasting for prey detection
    public float rayCastOffset = 1.5f;

    //floats values which are sued for counters and durations and general values
    public float  dyingD, chasingD = 1f, attackD = 2, soundD = 5;
    //sense values
    //speed values
    public float walkingSpeed, RunningSpeed, speed;
    //distance values
    public float runningDistance, attackingDistance = 1.5f;


    //PRIVATE VARS

    //the entities audio source
    public AudioSource deathSound, screamSound;

    //the box collider which is used to detect when a player is within a close range and is set as a target
    private BoxCollider boxCollider;

    //booleans to ascertain certain state specifics
    private bool trigger, puking, wandering, alerted, walking, running, soundAlert, sightAlert, soundTrigger, chasing, shot, deadBool;

    //counters
    private float eventChoiceC, checkPlayerC, wanderC, pukeC, alertedC, searchingC, dyingC, chasingC, shotC, attackC, soundC;

    //sensitivity of the AI unit
    private float viewingSens, listeningSens;

    //animation controller
    private ZombieAnimationController animatorCont;

    //the fsm which controls changing states and the state machine of the AI
    private StateMachineClass<StateEnums.BossZombieStates> fsm;

    //detection class which manages and handles detection events
    private PreyDetection detection;

    //the collider that checks for sound
    private SphereCollider soundCollider;




    //METHODS

    // Use this for initialization
    void Start() {

        //audioSource = gameObject.GetComponent<AudioSource>();
        detection = gameObject.GetComponent<PreyDetection>();
        fsm = new StateMachineClass<StateEnums.BossZombieStates>();
        fsm.enterState(StateEnums.BossZombieStates.Idle);


        animatorCont = gameObject.GetComponent<ZombieAnimationController>();
        soundCollider = gameObject.GetComponent<SphereCollider>();
        boxCollider = gameObject.GetComponent<BoxCollider>();

        //assign starting speed
        speed = walkingSpeed;

        //we need to choose a default position for the zombie to start on
        trigger = true;
    }




    /// <summary>
    /// Update is called once per frame, this checks which state we are currently in and acts accordingly
    /// </summary>
    void Update() {

        soundC += Time.deltaTime;

        if (!deadBool && soundC > soundD) {
            screamSound.Play();
            //sound.playBossScreamSound(transform.position);
            soundC = 0;
        }

        switch ((byte)fsm.getCurrentState()) {

            /***********Idle*******Idle*******Idle*******Idle*******Idle*******Idle*******Idle*/
            case (byte)StateEnums.BossZombieStates.Idle:

                if (stateDebugStatements)  Debug.Log("idle case: entering " + Time.timeSinceLevelLoad); 


                break;

            

            /***********Chasing*******Chasing*******Chasing*******Chasing*******Chasing*******Chasing*******Chasing*/
            case (byte)StateEnums.BossZombieStates.Chasing:
                if (stateDebugStatements)  Debug.Log("chasing case: entering " + Time.timeSinceLevelLoad); 
                //counter
                chasingC += Time.deltaTime;

                


                //we call the chasing method which handles the pursuit of the prey
                if (!chasing) {
                    chasePlayer();
                }
                //update the position of the target we are chasing
                if (chasing && chasingC > chasingD) {
                    soundD = 2;
                    if (stateDebugStatements)  Debug.Log("chasing case: if statement " + Time.timeSinceLevelLoad); 
                    chasePlayer();
                    //we store all information on the target we are chasing and send the unit to its updated position on the nav mesh
                    navAgent.SetDestination(target.transform.position);
                    detection.assignLastPosition(target.transform.position);

                    detection.assignTarget(target);
                    chasingC = 0;
                }
                break;

           
            /***********Attacking*******Attacking*******Attacking*******Attacking*******Attacking*******Attacking*******Attacking*/
            case (byte)StateEnums.BossZombieStates.Attacking:
                if (stateDebugStatements)  Debug.Log("attacking case: entering " + Time.timeSinceLevelLoad); 
                attackPlayer();
                break;

           
            /***********Dying*******Dying*******Dying*******Dying*******Dying*******Dying*******Dying*******Dying*******Dying*/
            case (byte)StateEnums.BossZombieStates.Dying:
                if (stateDebugStatements)  Debug.Log("dying case: entering " + Time.timeSinceLevelLoad); 
                killUnit();
                break;

            /***********Dead*******Dead*******Dead*******Dead*******Dead*******Dead*******Dead*******Dead*******Dead*/
            case (byte)StateEnums.BossZombieStates.Dead:
                if (stateDebugStatements)  Debug.Log("dead case: entering " + Time.timeSinceLevelLoad); 
                //we disable all non vital components
                animatorCont.turnOffRM();
                dead();
                break;
        }


    }





    /// <summary>
    /// chases the nearest player and checks weither or not to attack him or if the
    /// unit has lost him based on the distance
    /// </summary>
    void chasePlayer() {
        if (debugStatements) { Debug.Log("chasePlayer method at" + Time.timeSinceLevelLoad); }

        //if we are not yest chasing the target (first response actions)
        if (!chasing) {
            if (debugStatements) { Debug.Log("chasing case:: forcing chase animation " + Time.timeSinceLevelLoad); }
            //we set the animation and relevant parameters

            //rpc conversion


                animatorCont.setTrigger(BossHashScript.chargeTrigger);
            
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

        if (distance < attackingDistance) {
            if (debugStatements) { Debug.Log("chasePlayer method: ready to attack at" + Time.timeSinceLevelLoad); }
            //we make appropriate changes in the fsm, the navigation mesh traversal and the animations
            //fsm.enterState(StateEnums.ZombieStates.Attacking);
            //rpc conversion
            enterState((byte)StateEnums.BossZombieStates.Attacking);
            
            navAgent.Stop();
            //animatorCont.setBoolean(EnemyHashScript.attackingBool, true);
            //rpc conversion

            
                animatorCont.setBoolean(BossHashScript.attackingBool, true);
            
            chasing = false;
        }
        
    }




    /// <summary>
    /// this kills the unit and plays specific animation
    /// </summary>
    void killUnit() {
        if (debugStatements) { Debug.Log("killUnit method at" + Time.timeSinceLevelLoad); }
        //stop moving on the nav mesh
        navAgent.Stop();
        deadBool = true;
        deathSound.Play();



   
        enterState((byte)StateEnums.BossZombieStates.Dead);


    }

    /// <summary>
    /// disables all parts to the unit to only leave dead body
    /// </summary>
    void dead() {
        if (debugStatements) Debug.Log("dead method at" + Time.timeSinceLevelLoad);
        transform.parent.gameObject.GetComponent<RagdollFollow>().enableRagdoll(Vector3.forward);
    }




   



    /// <summary>
    /// entity pukes at its positon
    /// </summary>
    void attackPlayer() {
        if (debugStatements) Debug.Log("attackPlayer method at" + Time.timeSinceLevelLoad);
        //inflict damage on player
        transform.LookAt(target.transform, Vector3.up);


        playerHealthScript targetH = target.GetComponent<playerHealthScript>();

        //increment counter
        attackC += Time.deltaTime;

        //if we are able to attack
        if (targetH != null && attackC > attackD) {
            //reduce health
            targetH.reducePlayerHealth(damage);
            attackC = 0;
        }

        //we check the distance to make sure we are still close enough to attack
        //if we are too far away we start chasing the player again
        float distance = getDistanceT(transform, target.transform);
        //add a small offset
        if (distance > attackingDistance ) {

            enterState((byte)StateEnums.BossZombieStates.Chasing);


            
                animatorCont.setBoolean(BossHashScript.attackingBool, false);
            
        }
    }

    

    /// <summary>
    /// is entered when something enters a collider attached to the AI unit
    /// </summary>
    /// <param name="collider">
    /// the entity that is colliding with the collider
    /// </param>
	void OnTriggerEnter(Collider collider) {
        //if we player entered
        if (collider.tag == Tags.PLAYER1 || collider.tag == Tags.PLAYER2) {
            if (debugStatements) Debug.Log("collider entrance with " + collider.gameObject.name + " at " + Time.timeSinceLevelLoad); 
            //assign the target
            detection.assignTarget(collider.gameObject);
           
            target = collider.gameObject;

            enterState((byte)StateEnums.BossZombieStates.Chasing);
           
        }

    }





    /// <summary>
    /// alerts the unit that it has been shot
    /// changes state 
    /// </summary>
    /// <param name="entity">
    /// which entity shot it
    /// </param>
    public void alertShot(GameObject entity) {        
        target = entity.gameObject;
        // we only want to transition into the state if are not currently in the state
        


            enterState((byte)StateEnums.BossZombieStates.Chasing);


        
    }

    /// <summary>
    /// alerts the unit that is has died, basically just a state change
    /// </summary>
	public void alertDead() {
        //fsm.enterState(StateEnums.ZombieStates.Dying);


            enterState((byte)StateEnums.BossZombieStates.Dying);

    }

    




    //for networking purposes

    public void enterState(byte state) {
        //we only switch states if we are in a different state and fsm is not null
        if (fsm != null && (byte)fsm.getCurrentState() != state) {

            fsm.enterState((StateEnums.BossZombieStates)state);
        }

    }


    public void enterPrevState() {
        if (fsm != null) {
            fsm.enterPreviousState();
        }

    }

    




}

