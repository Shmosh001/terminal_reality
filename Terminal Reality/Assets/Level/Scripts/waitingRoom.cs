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


	}

	void OnTriggerExit(Collider other)
	{
		/*if (other.tag == Tags.PLAYER1)
		{
			playerData.inWaitingRoom = false;
			soundController.GetComponent<soundControllerScript>().playSafeBackgroundSound();
		}

		if (other.tag == Tags.ENEMY)
		{
			enemyInRoom--;
		}*/
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
		Debug.Log("Enemies in the waiting room: " + enemyInRoom);

	}
}
