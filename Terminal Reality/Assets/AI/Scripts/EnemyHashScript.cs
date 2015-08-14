using UnityEngine;
using System.Collections;

public class EnemyHashScript : MonoBehaviour {

	/**
	 * paramaters
	 **/
	//booleans
	public static int attackingBool = Animator.StringToHash("AttackingParam");
	public int searchingBool;
	public int wanderingBool;
	//integers
	public int attDInt;
	public int hitDInt;
	public int idleDInt;
	public int stateDInt;
	public int deathDsInt;
	public int idleVarDInt;
	//floats
	public int speedFloat;
	//triggers
	public int wakeupTrigger;
	public int chargeTrigger;
	public int alertedTrigger;
	public int deadTrigger;
	public int shotTrigger;
	public int alertToIdleTrigger;
	public int changeTrigger;


	/**
	 * states
	 **/
	//base layer
	public int startState;
	public int alertedState;
	public int searchingState;
	//StartState SSM
	public int startDecisionState;
	public int bitingState1;
	public int bitingStandUp1;
	public int standUpIdleState1;
	public int standUpState1;
	public int standUpIdleState2;
	public int standUpState2;
	public int standUpIdleState3;
	public int standUpState3;
	//Idle SSM
	public int idleChangeDecisionState;
	public int idleDecisionState;
	public int drunkWaitState;
	public int idle1State;
	public int idle2State;
	public int idle3State;
	public int idle4State;
	public int idleScratchState;
	//IdleVar SSM
	public int idleVarDecisionState;
	public int wanderingState;
	public int pukingState;
	public int screamingState;
	public int agonizingState;
	//Shot SSM
	public int screamState;
	public int shotDecisionState;
	public int shotStumbleState;
	public int shotStumble2State;
	public int shotStumble3State;
	public int shotGetUpState;
	//Attack SSM
	public int attackDecisionState;
	public int crawlState;
	public int runningState;
	public int punchingState;
	public int attackState;
	//Dying SSM
	public int dyingDecisionState;
	public int death1State;
	public int death2State;
	public int death1IdleState;
	public int death2IdleState;


	void Awake(){

		//params
		speedFloat = Animator.StringToHash("Speed");
		searchingBool = Animator.StringToHash("SearchingParam");
		wanderingBool = Animator.StringToHash("WanderingParam");
		shotTrigger = Animator.StringToHash("ShotParam");
		//attackingBool = Animator.StringToHash("AttackingParam");
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
