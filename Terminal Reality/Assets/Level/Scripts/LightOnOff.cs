using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class LightOnOff : MonoBehaviour {


	// Use this for initialization
	void Start () 
	{
		InvokeRepeating("Flicker",0.01f,1.5f);

	}

	void Flicker()
	{
		if (light.enabled == true) {
			light.enabled = false;
		} 
		else 
		{
			light.enabled = true;
		}

	}
}
