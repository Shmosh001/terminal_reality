using UnityEngine;
using System.Collections;

public class toilets : MonoBehaviour {

	public playerDataScript playerData;
	public GameObject soundController;
	
	int enemyInRoom = 0;
	
	// Use this for initialization
	void Start () {
		playerData = GameObject.FindGameObjectWithTag(Tags.PLAYER1).GetComponent<playerDataScript>();
		soundController = GameObject.FindGameObjectWithTag(Tags.SOUNDCONTROLLER);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == Tags.PLAYER1)
		{
			playerData.inToilets = true;
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if (other.tag == Tags.PLAYER1)
		{
			playerData.inToilets = false;
		}
	}
	
	void OnTriggerStay(Collider other)
	{
		if (other.tag == Tags.PLAYER1)
		{
			playerData.inToilets = true;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		enemyInRoom = 0;
		
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY);
		
		foreach (GameObject e in enemies)
		{
			if (this.collider.bounds.Contains(e.transform.position))
			{
				enemyInRoom++;
			}
		}
		
		if (playerData.inToilets == true && enemyInRoom == 0)
		{
			soundController.GetComponent<soundControllerScript>().playSafeBackgroundSound();
		}
		else if (playerData.inToilets == true && enemyInRoom > 0)
		{
			soundController.GetComponent<soundControllerScript>().playTensionAudio();
		}
		
	}
}
