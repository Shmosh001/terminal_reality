using UnityEngine;
using System.Collections;

public class torchScript : MonoBehaviour {

	//PUBLIC TORCH VARIABLES//
	public bool torchOn = false;
	public Light torch;
	public GameObject soundController;
	
	//PRIVATE TORCH VARIABLES//
	private float batteryLife;

	// Use this for initialization
	void Start () {
	
		batteryLife = 100.0f;
	
	}
	
	void Update()
	{
		//IF F IS PUSH TO TURN THE TORCH OFF OR ON//
		if (Input.GetKeyDown(KeyCode.F))
		{
			soundController.GetComponent<soundControllerScript>().playTorchSound(this.GetComponent<AudioSource>()); //play torch on/off sound
			torchOn = !torchOn;
		}

		//if the torch is on, reduce the battery life.
		if (torchOn && batteryLife >= 0.0f)
		{
			batteryLife -= 0.05f;
		}
		
		//if torch is off, charge the battery
		if (!torchOn && batteryLife <= 100.0f)
		{
			batteryLife += 0.1f;
		}
		
		//if the battery is dead (<0), then turn the torch off//
		if (batteryLife <= 0.0f)
		{
			torchOn = false;
		}

		print ("Battery life: " + batteryLife);

		//at the end of each update cycle, check whether the torch needs to be turned on or off.
		updateTorchActivity();

	}

	//CHECK WHETHER THE TORCH IS ON OR OFF, 
	//AND THEN TURN THE LIGHT ON OR OFF.
	private void updateTorchActivity()
	{
		if (torchOn)
		{
			torch.enabled = true;
		}
		else
		{
			torch.enabled = false;
		}
	}
}
