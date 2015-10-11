using UnityEngine;
using System.Collections;

public class glassBreaking : MonoBehaviour {

	private AudioSource audioSource;

	// Use this for initialization
	void Start () 
	{
		
        audioSource = GetComponent<AudioSource>();
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == Tags.PLAYER1 || other.gameObject.tag == Tags.PLAYER2)
		{
            //play sound of this component
            audioSource.Play();
            //Destroy Component
            Destroy(this.gameObject, 3.0f);


		}
	}
}
