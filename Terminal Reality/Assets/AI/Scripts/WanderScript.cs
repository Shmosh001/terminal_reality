using UnityEngine;
using System.Collections;

public class WanderScript : MonoBehaviour {


	private Transform[] locations;

	// Use this for initialization
	void Awake () {

		GameObject[] locs = GameObject.FindGameObjectsWithTag(Tags.WANDERLOC);
		locations = new Transform[locs.Length];
	}
	

	void assignPositions(GameObject[] pos){
		for (int i = 0; i < pos.Length; i++){
			locations[i] = pos[i].transform;
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
