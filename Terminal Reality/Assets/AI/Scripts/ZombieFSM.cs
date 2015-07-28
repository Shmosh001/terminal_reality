using UnityEngine;
using System.Collections;

public class ZombieFSM : MonoBehaviour {



	private StateMachineClass<StateEnums.ZombieStates> fsm = new StateMachineClass<StateEnums.ZombieStates>();
	private StateEnums states;

	// Use this for initialization
	void Start () {
		//fsm.enterState(states.);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
