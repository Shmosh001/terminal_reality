using UnityEngine;
using System.Collections;

public class CameraCycle : MonoBehaviour {
	

	//which camera we are currently on
	public int counter;
	//counter values
	//0 - orbit
	//1 - chase
	//2 - fps

	//the croisshair GUI Object, so we can disable it
	//all cameras
	public Camera [] cams;

	


	// Use this for initialization
	void Start () {
		//counter = 2;//start with the fps cam
		for (int i = 0; i < 2; i++){
			cams[i].enabled = false;
		}
	}


	void Update(){
		if (Input.GetKeyDown(KeyCode.C)){
			next();
		}
	}

	//selects the next camera and sets all nessesary values
	public void next(){
		counter++;
		//ensures no out of bounds
		if (counter > 2){
			counter = 0;
		}

		//disable all cameras
		for (int i = 0; i < 2; i++){
			cams[i].enabled = false;
		}


		//enable the right camera again
		cams[counter].enabled = true;
	}



}
