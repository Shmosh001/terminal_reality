using UnityEngine;
using System.Collections;

public class NavigationScript : MonoBehaviour {

	public GameObject pointDebug;
	public string pathName;
	public string fileName;
	public GameObject target;
	private CharacterController cc;
	float speed = 4;

	string[] paths;
	Pathfinding.RouteData data = new Pathfinding.RouteData();

	public bool debug;

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
		if (Pathfinding.NodesEqual(data.curNode, data.nextNode)){
			Debug.Log("point reached");
			getPathToTarget();
		}
		else{
			if (debug){
				pointDebug.transform.position = Pathfinding.GetWaypointInSpace(0.5f,data.nextNode);
			}

			print(pointDebug.transform.position.x + " " + pointDebug.transform.position.y + " " + pointDebug.transform.position.z );
			transform.LookAt(Pathfinding.GetWaypointInSpace(0.5f,data.nextNode));
			cc.Move(transform.forward * speed * Time.deltaTime);

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

	}

	public void moveToTarget(){

	}



}
