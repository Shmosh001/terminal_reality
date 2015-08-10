using UnityEngine;
using System.Collections;

public class WanderScript : MonoBehaviour {


	private Transform[] locations;

	// Use this for initialization
	void Start () {

		GameObject[] locs = GameObject.FindGameObjectsWithTag(Tags.WANDERLOC);
		locations = new Transform[locs.Length];
		assignPositions(locs);
	}
	

	void assignPositions(GameObject[] pos){
		for (int i = 0; i < pos.Length; i++){
			locations[i] = pos[i].transform;
			//Debug.Log("added in location");
		}
	}


	public Transform getClosestPoint(Transform entity){
		Transform closest = locations[0];
		float lastDist = Vector3.Distance(locations[0].position, entity.position);
		for (int i = 1; i < locations.Length; i++){
			float newDist =  Vector3.Distance(locations[i].position, entity.position);
			if (newDist < lastDist){
				closest = locations[i];
				lastDist = newDist;
			}
		}
		return closest;
	}

}
