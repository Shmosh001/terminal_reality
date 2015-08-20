using UnityEngine;
using System.Collections;

public class DDScript : MonoBehaviour {


	private bool open;
	private Animator aniamtor;

	// Use this for initialization
	void Start () {
		aniamtor = gameObject.GetComponent<Animator>();
		open = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha6)){
			openDoorForwards();
		}
		if (Input.GetKeyDown(KeyCode.Alpha7)){
			closeDoorForwards();
		}
		if (Input.GetKeyDown(KeyCode.Alpha8)){
			openDoorBackwards();
		}
		if (Input.GetKeyDown(KeyCode.Alpha9)){
			closeDoorBackwards();
		}

	}

	public void openDoorForwards(){
		if (!open){
			aniamtor.SetTrigger("OpenFWD");
			open = true;
		}
	}


	public void closeDoorForwards(){
		if (open){
			aniamtor.SetTrigger("CloseFWD");
			open = false;
		}
	}


	public void openDoorBackwards(){
		if (!open){
			aniamtor.SetTrigger("OpenBCK");
			open = true;
		}
	}
	
	
	public void closeDoorBackwards(){
		if (open){
			aniamtor.SetTrigger("CloseBCK");
			open = false;
		}
	}



}
