using UnityEngine;
using System.Collections;

public class SparkSound : MonoBehaviour {


	private GameObject soundController;
    private AudioSource audioSource;

	// Use this for initialization
	void Start ()
	{
		soundController = GameObject.FindGameObjectWithTag(Tags.SOUNDCONTROLLER);
        audioSource = GetComponent<AudioSource>();
		InvokeRepeating("Spark",0.01f,4.0f);
	}
	
	//called every 4 seconds
	void Spark ()
	{
		//play sound of this component
		soundController.GetComponent<soundControllerScript> ().playSparkSound (transform.position);
	}
}
