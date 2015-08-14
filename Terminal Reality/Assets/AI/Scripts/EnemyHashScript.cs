using UnityEngine;
using System.Collections;

public class EnemyHashScript : MonoBehaviour {

	/**
	 * paramaters
	 **/
	//booleans
	public static int attackingBool;
	public static  int searchingBool;
	public static int wanderingBool;
	//integers
	public static int attDInt;
	public static int hitDInt;
	public static int idleDInt;
	public static int stateDInt;
	public static int deathDsInt;
	public static int idleVarDInt;
	//floats
	public static int speedFloat;
	//triggers
	public static int wakeupTrigger;
	public static int chargeTrigger;
	public static int alertedTrigger;
	public static int deadTrigger;
	public static int shotTrigger;
	public static int alertToIdleTrigger;
	public static int changeTrigger;


	/**
	 * states
	 **/
	//base layer
	public static int startState;
	public static int alertedState;
	public static int searchingState;
	//StartState SSM
	public static int startDecisionState;
	public static int bitingState1;
	public static int bitingStandUp1;
	public static int standUpIdleState1;
	public static int standUpState1;
	public static int standUpIdleState2;
	public static int standUpState2;
	public static int standUpIdleState3;
	public static int standUpState3;
	//Idle SSM
	public static int idleChangeDecisionState;
	public static int idleDecisionState;
	public static int drunkWaitState;
	public static int idle1State;
	public static int idle2State;
	public static int idle3State;
	public static int idle4State;
	public static int idleScratchState;
	//IdleVar SSM
	public static int idleVarDecisionState;
	public static int wanderingState;
	public static int pukingState;
	public static int screamingState;
	public static int agonizingState;
	//Shot SSM
	public static int screamState;
	public static int shotDecisionState;
	public static int shotStumbleState;
	public static int shotStumble2State;
	public static int shotStumble3State;
	public static int shotGetUpState;
	//Attack SSM
	public static int attackDecisionState;
	public static int crawlState;
	public static int runningState;
	public static int punchingState;
	public static int attackState;
	//Dying SSM
	public static int dyingDecisionState;
	public static int death1State;
	public static int death2State;
	public static int death1IdleState;
	public static int death2IdleState;


	void Awake(){

		//params
		speedFloat = Animator.StringToHash("Speed");
		searchingBool = Animator.StringToHash("SearchingParam");
		wanderingBool = Animator.StringToHash("WanderingParam");
		shotTrigger = Animator.StringToHash("ShotParam");
		attackingBool = Animator.StringToHash("AttackingParam");
		alertedTrigger = Animator.StringToHash("AlertedParam");
		deadTrigger = Animator.StringToHash("DeadParam");
		deathDsInt = Animator.StringToHash("DeathD");
		idleVarDInt = Animator.StringToHash("IdleVarD");
		attDInt = Animator.StringToHash("AttD");
		hitDInt = Animator.StringToHash("HitD");
		idleDInt = Animator.StringToHash("IdleD");
		changeTrigger = Animator.StringToHash("ChangeTrigger");
		stateDInt = Animator.StringToHash("StateD");
		wakeupTrigger = Animator.StringToHash("WakeupParam");
		chargeTrigger = Animator.StringToHash("ChargeParam");
		alertToIdleTrigger = Animator.StringToHash("AlertToIdle");

		//states



		//base layer
		startState = Animator.StringToHash("Base Layer.Start");
		alertedState = Animator.StringToHash("Base Layer.Alerted");
		searchingState = Animator.StringToHash("Base Layer.Searching");
		//StartState SSM
		startDecisionState = Animator.StringToHash("Base Layer.StartState.Decision");
		bitingState1 = Animator.StringToHash("Base Layer.StartState.zomebie_biting_1");
		bitingStandUp1 = Animator.StringToHash("Base Layer.StartState.biting stand up");
		standUpIdleState1 = Animator.StringToHash("Base Layer.StartState.zombie_stand_up_base");
		standUpState1 = Animator.StringToHash("Base Layer.StartState.zombie_stand_up");
		standUpIdleState2 = Animator.StringToHash("Base Layer.StartState.zombie_stand_up_1_base");
		standUpState2 = Animator.StringToHash("Base Layer.StartState.zombie_stand_up_1");
		standUpIdleState3 = Animator.StringToHash("Base Layer.StartState.zombie_stand_up_2_base");
		standUpState3 = Animator.StringToHash("Base Layer.StartState.zombie_stand_up_2");
		//Idle SSM
		idleChangeDecisionState = Animator.StringToHash("Base Layer.Idle.ChangeDecision");
		idleDecisionState = Animator.StringToHash("Base Layer.Idle.IdleDecision");
		drunkWaitState = Animator.StringToHash("Base Layer.Idle.drunk wait");
		idle1State = Animator.StringToHash("Base Layer.Idle.zombie_idle");
		idle2State = Animator.StringToHash("Base Layer.Idle.zombie_idle3");
		idle3State = Animator.StringToHash("Base Layer.Idle.zombie_scratch_idle");
		idle4State = Animator.StringToHash("Base Layer.Idle.zombie_idle_1");
		idleScratchState = Animator.StringToHash("Base Layer.Idle.zombie_idle_3");
		//IdleVar SSM
		idleVarDecisionState = Animator.StringToHash("Base Layer.IdleVar.Decision");
		wanderingState = Animator.StringToHash("Base Layer.IdleVar.Wandering");
		pukingState = Animator.StringToHash("Base Layer.IdleVar.puking");
		screamingState = Animator.StringToHash("Base Layer.IdleVar.zombie_scream");
		agonizingState = Animator.StringToHash("Base Layer.IdleVar.zombie_agonizing");
		//Shot SSM
		screamState = Animator.StringToHash("Base Layer.Shot.zombie_scream");
		shotDecisionState = Animator.StringToHash("Base Layer.Shot.Decision");
		shotStumbleState = Animator.StringToHash("Base Layer.Shot.zombie_death");
		shotStumble2State = Animator.StringToHash("Base Layer.Shot.zombie_reaction_hit");
		shotStumble3State = Animator.StringToHash("Base Layer.Shot.zombie_reaction_hit_1");
		shotGetUpState = Animator.StringToHash("Base Layer.Shot.zombie_stand_up");
		//Attack SSM
		attackDecisionState = Animator.StringToHash("Base Layer.Attack.Decision");
		crawlState = Animator.StringToHash("Base Layer.Attack.Zombie_running_crawl");
		runningState = Animator.StringToHash("Base Layer.Attack.zombie_running");
		punchingState = Animator.StringToHash("Base Layer.Attack.zombie_punching");
		attackState = Animator.StringToHash("Base Layer.Attack.zombie_attack");
		//Dying SSM
		dyingDecisionState = Animator.StringToHash("Base Layer.Dying.Decision");
		death1State = Animator.StringToHash("Base Layer.Dying.zombie_death");
		death2State = Animator.StringToHash("Base Layer.Dying.zombie_dying");
		death1IdleState = Animator.StringToHash("Base Layer.Dying.death idle");
		death2IdleState = Animator.StringToHash("Base Layer.Dying.dying idle");
	}
}
