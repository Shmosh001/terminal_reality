using UnityEngine;
using System.Collections;

public class torchScript : MonoBehaviour {

	//PUBLIC TORCH VARIABLES//
	public bool torchOn = false;
	
	//PRIVATE TORCH VARIABLES//
	private float batteryLife;

	// Use this for initialization
	void Start () {
	
		batteryLife = 100.0f;
	
	}
	
	void Update()
	{
		//if the torch is on, reduce the battery life.
		if (torchOn && batteryLife >= 0.0f)
		{
			batteryLife -= 0.5f;
		}
		
		//if torch is off, charge the battery
		if (!torchOn && batteryLife <= 100.0f)
		{
			batteryLife += 1.0f;
		}
		
		//if the battery is dead (<0), then turn the torch off//
		if (batteryLife <= 0.0f)
		{
			torchOn = false;
		}
	}
}
