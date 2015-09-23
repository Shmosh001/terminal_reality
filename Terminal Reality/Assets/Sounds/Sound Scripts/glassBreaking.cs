using UnityEngine;
using System.Collections;

public class glassBreaking : MonoBehaviour {

	private GameObject soundController;

	// Use this for initialization
	void Start () 
	{
		soundController = GameObject.FindGameObjectWithTag(Tags.SOUNDCONTROLLER);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == Tags.PLAYER1 || other.gameObject.tag == Tags.PLAYER2)
		{
			//play sound of this component
			soundController.GetComponent<soundControllerScript> ().playGlassBreaking (transform.position);
			//Destroy(this.gameObject, 3.0f)
		}
	}
}
