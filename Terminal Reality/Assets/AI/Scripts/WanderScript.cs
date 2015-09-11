using UnityEngine;
using System.Collections;

/// <summary>
/// script to determine where the AI entity should wander to
/// based on a collection of pre defined spots 
/// </summary>
public class WanderScript : MonoBehaviour {

    //valid locations
	private Transform[] locations;

    /// <summary>
    /// initialization
    /// </summary>
    void Start () {
		GameObject[] locs = GameObject.FindGameObjectsWithTag(Tags.WANDERLOC);
		locations = new Transform[locs.Length];
		assignPositions(locs);
	}
	
    /// <summary>
    /// writes all the transforms from the locations into the array
    /// </summary>
    /// <param name="pos"></param>
	void assignPositions(GameObject[] pos){
		for (int i = 0; i < pos.Length; i++){
			locations[i] = pos[i].transform;
		}
	}

    /// <summary>
    /// gets the closest location
    /// </summary>
    /// <param name="entity">
    /// entity that is requesting a position
    /// </param>
    /// <returns>
    /// transform of that location
    /// </returns>
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
