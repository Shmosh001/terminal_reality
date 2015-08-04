using UnityEngine;
using System.Collections;

public class LevelCreator : MonoBehaviour {
	public GameObject wall;
	public GameObject halfWall;

	public GameObject[] oldWalls;
	public GameObject[] oldHalfWalls;
	// Use this for initialization
	void Start () 
	{
		//fills lists with gameObjects of with the specified tag
		oldWalls = GameObject.FindGameObjectsWithTag ("Old Wall");
		oldHalfWalls = GameObject.FindGameObjectsWithTag ("Old Half Wall");

		//go through each wall object and replace with new wall
		foreach (GameObject oldWall in oldWalls)
		{
			//positions dependant of rotation of wall
			if(oldWall.transform.rotation.y == 0)
			{
				GameObject tempWall = wall;
				tempWall.transform.rotation.y = 0;
				Instantiate(wall, oldWall.transform.position + Vector3(5.0f,2.5f,-0.5f), tempWall.transform.rotation);
			}

			if(oldWall.transform.rotation.y == 90)
			{
				Instantiate(wall, oldWall.transform.position + Vector3(-0.5f,2.5f,-5.0f), wall.transform.rotation);
			}
		}

		foreach (GameObject oldHalfWall in oldHalfWalls)
		{
			if(oldHalfWall.transform.rotation.y == 0)
			{
				GameObject tempHalfWall = wall;
				tempHalfWall.transform.rotation.y = 0;
				Instantiate(halfWall, oldHalfWall.transform.position + Vector3(2.5f,2.5f,-0.5f), tempHalfWall.transform.rotation);
			}
			
			if(oldHalfWall.transform.rotation.y == 90)
			{
				Instantiate(halfWall, oldHalfWall.transform.position + Vector3(-0.5f,2.5f,-2.5f), halfWall.transform.rotation);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
