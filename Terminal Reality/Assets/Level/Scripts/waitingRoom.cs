using UnityEngine;
using System.Collections;

public class waitingRoom : MonoBehaviour {

	public playerDataScript playerData;

	int enemyInRoom = 0;

	public GameObject soundController;

	// Use this for initialization
	void Start () {
		playerData = GameObject.FindGameObjectWithTag(Tags.PLAYER1).GetComponent<playerDataScript>();
		soundController = GameObject.FindGameObjectWithTag(Tags.SOUNDCONTROLLER);


	}

	void OnTriggerEnter(Collider other)
	{
		playerData.inWaitingRoom = true;

	}

	void OnTriggerExit(Collider other)
	{
		playerData.inWaitingRoom = false;
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

		if (playerData.inWaitingRoom == true && enemyInRoom == 0)
		{
			soundController.GetComponent<soundControllerScript>().playSafeBackgroundSound();
		}
		else if (playerData.inWaitingRoom == true && enemyInRoom > 0)
		{
			soundController.GetComponent<soundControllerScript>().playTensionAudio();
		}


	}
}
