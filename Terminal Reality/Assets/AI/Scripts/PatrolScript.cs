using UnityEngine;
using System.Collections;

public class PatrolScript : MonoBehaviour {


    public Transform[] route;
    private int index;


	// Use this for initialization
	void Start () {
        index = 0;
	}
	

    public Vector3 getNextWayPoint() {
        index++;
        if (index >= route.Length) {
            index = 0;
        }
        return route[index].position;
    }

}
