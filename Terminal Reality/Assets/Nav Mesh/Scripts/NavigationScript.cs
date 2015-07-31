using UnityEngine;
using System.Collections;

public class NavigationScript : MonoBehaviour {


	public string pathName;
	public string fileName;




	// Use this for initialization
	void Start () {
		pathName = "Nav_Mesh/NAPS/Navigation Files";
		fileName = "testpath";
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void loadInPath(){
		Pathfinding.LoadNavigationData(pathName, fileName);
	}

}
