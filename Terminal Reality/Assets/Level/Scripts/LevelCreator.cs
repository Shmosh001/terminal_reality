using UnityEngine;
using System.Collections;

public class LevelCreator : MonoBehaviour {

	public GameObject parent;

	public GameObject wall;
	public GameObject halfWall;
	public bool deleteOld;
	public bool showOld;
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


			//Debug.LogError(oldWall.transform.rotation.y);

			//positions dependant of rotation of wall
			if(oldWall.transform.rotation.y == 0)
			//if(oldWall.transform.rotation.y < 1 && oldWall.transform.rotation.y > -1)
			{

				GameObject tempWall = wall;
				tempWall.transform.eulerAngles = new Vector3(tempWall.transform.eulerAngles.x, 0, tempWall.transform.eulerAngles.z);
				tempWall.transform.rotation = new Quaternion(tempWall.transform.rotation.x,0,tempWall.transform.rotation.z,tempWall.transform.rotation.w);
				Vector3 pos = new Vector3(oldWall.transform.position.x + 5.0f, oldWall.transform.position.y + 2.5f , oldWall.transform.position.z - 0.5f);

				GameObject obj = (GameObject)Instantiate(wall, pos, tempWall.transform.rotation);
				//GameObject obj = (GameObject)Instantiate(wall, pos,  new Quaternion (Quaternion.identity.x, 90, Quaternion.identity.z, Quaternion.identity.w));
				obj.transform.parent = parent.transform;
				if (!showOld){
					oldWall.SetActive(false);
				}
				if (deleteOld){
					Destroy(oldWall);
				}

			}

			else if(oldWall.transform.eulerAngles.y > 90.0f )
			{
				GameObject tempWall = wall;
				tempWall.transform.eulerAngles = new Vector3(tempWall.transform.eulerAngles.x, 90.0f, tempWall.transform.eulerAngles.z);
				Vector3 pos = new Vector3(oldWall.transform.position.x - 0.5f, oldWall.transform.position.y + 2.5f , oldWall.transform.position.z - 5.0f);
				GameObject obj = (GameObject)Instantiate(wall, pos, tempWall.transform.rotation);
				//GameObject obj = (GameObject)Instantiate(wall, pos, new Quaternion (Quaternion.identity.x, 90, Quaternion.identity.z, Quaternion.identity.w));
				obj.transform.parent = parent.transform;
				if (!showOld){
					oldWall.SetActive(false);
				}
				if (deleteOld){
					Destroy(oldWall);
				}
			}
		}

		foreach (GameObject oldHalfWall in oldHalfWalls)
		{
			if(oldHalfWall.transform.rotation.y == 0)
			{
				GameObject tempHalfWall = oldHalfWall;
				tempHalfWall.transform.eulerAngles = new Vector3(90.0f, 0.0f, tempHalfWall.transform.eulerAngles.z);
				tempHalfWall.transform.rotation = new Quaternion(tempHalfWall.transform.rotation.x,0,tempHalfWall.transform.rotation.z,tempHalfWall.transform.rotation.w);
				Vector3 pos = new Vector3(oldHalfWall.transform.position.x + 2.5f, oldHalfWall.transform.position.y + 2.5f , oldHalfWall.transform.position.z - 0.5f);
				GameObject obj = (GameObject)Instantiate(halfWall, pos, tempHalfWall.transform.rotation);
				//GameObject obj = (GameObject)Instantiate(halfWall, pos,  new Quaternion (Quaternion.identity.x, 90, Quaternion.identity.z, Quaternion.identity.w));
				obj.transform.parent = parent.transform;
				if (!showOld){
					oldHalfWall.SetActive(false);
				}
				if (deleteOld){
					Destroy(oldHalfWall);
				}
			}
			
			if(oldHalfWall.transform.eulerAngles.y > 90.0f )
			{
				Vector3 pos = new Vector3(oldHalfWall.transform.position.x - 0.5f, oldHalfWall.transform.position.y + 2.5f , oldHalfWall.transform.position.z - 2.5f);
				GameObject obj = (GameObject)Instantiate(halfWall, pos, halfWall.transform.rotation);
				//GameObject obj = (GameObject)Instantiate(halfWall, pos,  new Quaternion (Quaternion.identity.x, 90, Quaternion.identity.z, Quaternion.identity.w));
				obj.transform.parent = parent.transform;
				if (!showOld){
					oldHalfWall.SetActive(false);
				}
				if (deleteOld){
					Destroy(oldHalfWall);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
