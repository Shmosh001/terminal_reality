﻿using UnityEngine;
using System.Collections;

public class NavigationScript : MonoBehaviour {
	//debug
	public bool debug;
	public GameObject pointDebug;
	public GameObject pointDebug2;

	public string pathName;
	public string fileName;
	public GameObject target;
	private CharacterController cc;
	public float speed = 4;

	private Vector3 oldPosition;
	private float positionCheckC;
	public float positionCheckD = 1.5f;

	string[] paths;
	Pathfinding.RouteData data = new Pathfinding.RouteData();

	bool chasePlayer;

	// Use this for initialization
	void Start () {
		pathName = "Nav Mesh/Navigation Files";
		fileName = "testpath";
		cc = gameObject.GetComponent<CharacterController>();
		loadInPath();
		paths = new string[1];
		paths[0] = Pathfinding.Paths[0].type;

		getPathToTarget();
	}
	
	// Update is called once per frame
	void Update () {

		//can create ramble at any point if we just choose random point in the field? does not make sense though
		//maybe we can get random point close to the node we are moving to?

		positionCheckC += Time.deltaTime;

		if (positionCheckC > positionCheckD){
			if (oldPosition != target.transform.position){
				getPathToTarget();
			}
			positionCheckC = 0;
		}


		if (chasePlayer){
			transform.LookAt(target.transform.position);
			cc.Move(transform.forward * speed * Time.deltaTime);
		}
		else if (Vector3.Distance(Pathfinding.GetWaypointInSpace(0.5f,data.nextNode), transform.position) < 1){
			//Debug.Log("point reached");
			data = Pathfinding.GetNextNode(data);
			//Debug.Log(data.nextNode);
			//Debug.Log(data.nextNode);
			//need to check visibility here before we movev to the player
			float nodeDistance = Vector3.Distance(Pathfinding.GetWaypointInSpace(0.5f,data.nextNode), transform.position);
			float playerDistance = Vector3.Distance(target.transform.position,gameObject.transform.position );
			/*Pathfinding.ObjectIsVisible(target.transform.position,transform.position,0.7f)*/ 
			if ( nodeDistance > playerDistance || Pathfinding.NodesEqual(data.curNode,data.nextNode)){
				chasePlayer = true;
			}
		}
		else{


			//print(pointDebug.transform.position.x + " " + pointDebug.transform.position.y + " " + pointDebug.transform.position.z );
			transform.LookAt(Pathfinding.GetWaypointInSpace(0.5f,data.nextNode));
			cc.Move(transform.forward * speed * Time.deltaTime);

		}

		if (debug){
			pointDebug.transform.position = Pathfinding.GetWaypointInSpace(0.5f,data.nextNode);
			pointDebug2.transform.position = Pathfinding.GetWaypointInSpace(0.5f,data.curNode);
		}
		//print("test");
		//Debug.Log("test");
	}

	//loads in default path
	public void loadInPath(){
		if (!Pathfinding.DataLoaded()){
			Pathfinding.LoadNavigationData(Application.dataPath + "/" + pathName, fileName);
		}

	}

	//loads in a specified file
	public void loadInPath(string newPathName, string newFileName){

		Pathfinding.LoadNavigationData(Application.dataPath + "/" + newPathName, newFileName);
		
	}





	public void getPathToTarget(){
		data = Pathfinding.GetRouteForPoint(data, paths, target.transform.position, transform.position, 0.4f, 0);
		oldPosition = target.transform.position;
	}



	/***
	 * NOTES
	 * 
	 * if player moves away while we are following him:
	 * 
	 * have his last position as a new destination when he goes out of sight?
	 * /



}
