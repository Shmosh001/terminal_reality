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
				tempWall.transform.rotation = new Quaternion(tempWall.transform.rotation.x,0,tempWall.transform.rotation.z,tempWall.transform.rotation.w);
				Vector3 pos = new Vector3(oldWall.transform.position.x + 5.0f, oldWall.transform.position.y + 2.5f , oldWall.transform.position.z - 0.5f);
				Instantiate(wall, pos, tempWall.transform.rotation);
			}

			if(oldWall.transform.rotation.y == 90)
			{
				Vector3 pos = new Vector3(oldWall.transform.position.x - 0.5f, oldWall.transform.position.y + 2.5f , oldWall.transform.position.z - 5.0f);
				Instantiate(wall, pos, wall.transform.rotation);
			}
		}

		foreach (GameObject oldHalfWall in oldHalfWalls)
		{
			if(oldHalfWall.transform.rotation.y == 0)
			{
				GameObject tempHalfWall = oldHalfWall;
				tempHalfWall.transform.rotation = new Quaternion(tempHalfWall.transform.rotation.x,0,tempHalfWall.transform.rotation.z,tempHalfWall.transform.rotation.w);
				Vector3 pos = new Vector3(oldHalfWall.transform.position.x + 2.5f, oldHalfWall.transform.position.y + 2.5f , oldHalfWall.transform.position.z - 0.5f);
				Instantiate(halfWall, pos, tempHalfWall.transform.rotation);
			}
			
			if(oldHalfWall.transform.rotation.y == 90)
			{
				Vector3 pos = new Vector3(oldHalfWall.transform.position.x - 0.5f, oldHalfWall.transform.position.y + 2.5f , oldHalfWall.transform.position.z - 2.5f);
				Instantiate(halfWall, pos, halfWall.transform.rotation);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
