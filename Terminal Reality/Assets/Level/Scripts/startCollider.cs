using UnityEngine;
using System.Collections;

public class startCollider : MonoBehaviour {

	public GameObject soundController;

	// Use this for initialization
	void Start () {
		soundController = GameObject.FindGameObjectWithTag(Tags.SOUNDCONTROLLER);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == Tags.PLAYER1)
		{
			soundController.GetComponent<soundControllerScript>().playTensionAudio();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
