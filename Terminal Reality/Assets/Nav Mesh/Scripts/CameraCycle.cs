using UnityEngine;
using System.Collections;

public class CameraCycle : MonoBehaviour {
	
	public GameObject player;

	//which camera we are currently on
	public int counter;
	private int amount;
	//counter values
	//0 - orbit
	//1 - chase
	//2 - fps

	//the croisshair GUI Object, so we can disable it
	//all cameras
	public Camera [] cams;

	


	// Use this for initialization
	void Start () {
		amount = cams.Length;
		//counter = 2;//start with the fps cam
		for (int i = 0; i < amount; i++){
			cams[i].enabled = false;
		}
		cams[counter].enabled = true;
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
		if (counter >= amount){
			counter = 0;
		}

		//disable all cameras
		for (int i = 0; i < amount; i++){
			cams[i].enabled = false;
		}
		if(counter != 0){
			player.SetActive(false);
		}else{
			player.SetActive(true);
		}

		//enable the right camera again
		cams[counter].enabled = true;
	}



}
