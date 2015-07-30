using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {

	public Vector3 speed = new Vector3(70,5,7);

	public Transform player;


	public bool debug;
	//public FSM.State state;
	private CharacterController controller;

	// Use this for initialization
	void Start () {
		//state = FSM.State.stationary;
		//controller.attachedRigidbody.
		controller = (CharacterController)this.gameObject.GetComponent<CharacterController>();
		//controller.Move(speed);
	}
	
	// Update is called once per frame
	void Update () {
		//we need to check the state of the enemy
		if (!debug){
			transform.LookAt(player.position);
			controller.Move(transform.forward * speed.x * Time.deltaTime); 


		}

	}
}
