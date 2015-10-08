using UnityEngine;
using System.Collections;

/// <summary>
/// script to determine where the AI entity should wander to
/// based on a collection of pre defined spots 
/// </summary>
public class WanderScript : MonoBehaviour {




    private Transform[] locations;
    private int noOfLocations;

    /// <summary>
    /// initialization
    /// </summary>
    void Start () {
		GameObject[] locs = GameObject.FindGameObjectsWithTag(Tags.WANDERLOC);
        noOfLocations = locs.Length;
        //Debug.LogWarning(noOfLocations);
        locations = new Transform[noOfLocations];
        assignPositions(locs);
        
	}
	
    /// <summary>
    /// writes all the transforms from the locations into the array
    /// </summary>
    /// <param name="pos"></param>
	void assignPositions(GameObject[] objects){

        for (int i = 0; i < noOfLocations; i++){
            //Debug.Log(objects[i]);
           // Debug.Log(objects[i].transform);
            //Debug.Log(locations[i]);
            locations[i] = objects[i].transform;
		}
	}

    /// <summary>
    /// gets the random location
    /// </summary>
    /// <param name="entity">
    /// entity that is requesting a position
    /// </param>
    /// <returns>
    /// transform of that location
    /// </returns>
	public Transform getClosestPoint(Transform entity){



        //get random location

        int index = Random.Range(0, noOfLocations-1);


       
		return locations[index];
	}

}
