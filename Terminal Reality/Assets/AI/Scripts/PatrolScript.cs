using UnityEngine;
using System.Collections;

public class PatrolScript : MonoBehaviour {


    public Transform[] route;
    private int index;


	// Use this for initialization
	void Start () {
        index = 0;
	}
	

    public Vector3 getCurrentWayPoint() {
        Debug.Log("current wp pos " + route[index].position.x + " " + route[index].position.y + " " + route[index].position.z);
        return route[index].position;
    }

    public Vector3 getNextWayPoint() {
        index++;
        if (index >= route.Length) {
            index = 0;
        }
        return route[index].position;
    }

}
