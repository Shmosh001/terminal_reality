using UnityEngine;
using System.Collections;

/// <summary>
/// hash script which contains all the hashes for the enemy animator controller parameters and states
/// </summary>
public class BossHashScript : MonoBehaviour {

	//PARAMETERS

	//booleans
	public static int attackingBool;
	//triggers
    public static int chargeTrigger;
	public static int deadTrigger;

	//STATES

	//Idle SSM
	public static int idleState;

	
	//Attack SSM
	public static int attackDecisionState;
	public static int runningState;
	public static int punchingState;
	public static int attackState;
	//Dying SSM
	public static int deathState;
	public static int deathIdleState;


    /// <summary>
    /// assigns all the hashes
    /// </summary>
	void Awake(){

		//PARAMS
		attackingBool = Animator.StringToHash("AttackingParam");
		deadTrigger = Animator.StringToHash("DeadParam");
		chargeTrigger = Animator.StringToHash("ChargeParam");


        //STATES

        idleState = Animator.StringToHash("Base Layer.zombie_idle4");
		
		//Attack SSM
		attackDecisionState = Animator.StringToHash("Attack.Decision");
		runningState = Animator.StringToHash("Attack.zombie_running");
		punchingState = Animator.StringToHash("Attack.zombie_punching");
		attackState = Animator.StringToHash("Attack.zombie_attack");
		//Dying SSM
		deathState = Animator.StringToHash("Dying.zombie_dying");
        deathIdleState = Animator.StringToHash("Dying.dying idle");
	}
}
