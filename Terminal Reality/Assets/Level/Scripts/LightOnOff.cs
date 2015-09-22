using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class LightOnOff : MonoBehaviour {

    //bulbs 
    //public GameObject light1;
    //public GameObject light2;
    //public Material white;
    //public Material black;


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
