using UnityEngine;
using System.Collections;

public class SparkSound : MonoBehaviour {


	private GameObject soundController;

	// Use this for initialization
	void Start ()
	{
		soundController = GameObject.FindGameObjectWithTag("Sound Controller");
		InvokeRepeating("Spark",0.01f,3.9f);
	}
	
	//called every 4 seconds
	void Spark ()
	{
		//play sound of this component
		soundController.GetComponent<soundControllerScript> ().playSparkSound (this.GetComponent<AudioSource>());
	}
}
