using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour {


	private Animator animator;
	private bool open;

	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponent<Animator>();
		open = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha3)){
			openDoor ();
		}
		if (Input.GetKeyDown(KeyCode.Alpha4)){
			closeDoor ();
		}
	}

	public void openDoor(){
		if (!open){
			animator.SetTrigger("Open");
			open = true;
		}
	}


	public void closeDoor(){
		if (open){
			animator.SetTrigger("Close");
			open = false;
		}
	}
}
