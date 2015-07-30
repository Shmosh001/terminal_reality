using UnityEngine;
using System.Collections;

public class KeyListener : MonoBehaviour {


	Animator animator;

	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {



		if (Input.GetKeyDown(KeyCode.Alpha1)){
			animator.SetBool("Alerted",true);
			animator.SetBool("Arm Stretch",false);
			animator.SetBool("Dead",false);
			animator.SetBool("Searching",false);
			animator.SetBool("Attacking",false);
			animator.SetBool("Puking",false);
			animator.SetBool("Wandering",false);
			animator.SetBool("Searching",false);
		}

		if (Input.GetKeyDown(KeyCode.Alpha2)){
			animator.SetBool("Puking",true);
			animator.SetBool("Arm Stretch",false);
			animator.SetBool("Dead",false);
			animator.SetBool("Searching",false);
			animator.SetBool("Alerted",false);
			animator.SetBool("Attacking",false);
			animator.SetBool("Wandering",false);
			animator.SetBool("Searching",false);
		}

		if (Input.GetKeyDown(KeyCode.Alpha3)){
			animator.SetBool("Wandering",true);
			animator.SetBool("Arm Stretch",false);
			animator.SetBool("Dead",false);
			animator.SetBool("Searching",false);
			animator.SetBool("Alerted",false);
			animator.SetBool("Puking",false);
			animator.SetBool("Attacking",false);
			animator.SetBool("Searching",false);
		}

		if (Input.GetKeyDown(KeyCode.Alpha4)){
			animator.SetBool("Searching",true);
			animator.SetBool("Arm Stretch",false);
			animator.SetBool("Dead",false);
			animator.SetBool("Searching",false);
			animator.SetBool("Alerted",false);
			animator.SetBool("Puking",false);
			animator.SetBool("Wandering",false);
			animator.SetBool("Attacking",false);
		}

		if (Input.GetKeyDown(KeyCode.Alpha5)){
			animator.SetBool("Dead",true);
			animator.SetBool("Arm Stretch",false);
			animator.SetBool("Attacking",false);
			animator.SetBool("Searching",false);
			animator.SetBool("Alerted",false);
			animator.SetBool("Puking",false);
			animator.SetBool("Wandering",false);
			animator.SetBool("Searching",false);
		}

		if (Input.GetKeyDown(KeyCode.Alpha6)){
			animator.SetBool("Attacking",true);
			animator.SetBool("Arm Stretch",false);
			animator.SetBool("Dead",false);
			animator.SetBool("Searching",false);
			animator.SetBool("Alerted",false);
			animator.SetBool("Puking",false);
			animator.SetBool("Wandering",false);
			animator.SetBool("Searching",false);

		}

		if (Input.GetKeyDown(KeyCode.Alpha7)){
			animator.SetBool("Arm Stretch",true);
			animator.SetBool("Arm Stretch",false);
			animator.SetBool("Dead",false);
			animator.SetBool("Searching",false);
			animator.SetBool("Alerted",false);
			animator.SetBool("Puking",false);
			animator.SetBool("Wandering",false);
			animator.SetBool("Searching",false);
			animator.SetBool("Attacking",false);
		}

		if (Input.GetKeyDown(KeyCode.UpArrow)){
			animator.SetFloat("Speed", 3);
		}

		if (Input.GetKeyDown(KeyCode.DownArrow)){
			animator.SetFloat("Speed", 6);
		}



	}
}
