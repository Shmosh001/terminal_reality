using UnityEngine;
using System.Collections;

/// <summary>
/// script to determine where the AI entity should wander to
/// based on a collection of pre defined spots 
/// </summary>
public class WanderScript : MonoBehaviour {


    


    //valid locations
	private ArrayList unvisitedLocations;

    private ArrayList visitedLocations;

    private int initialSize;
    private int visitedCount;
    private int unvisitedCount;

    /// <summary>
    /// initialization
    /// </summary>
    void Start () {
		GameObject[] locs = GameObject.FindGameObjectsWithTag(Tags.WANDERLOC);
        initialSize = locs.Length;
        unvisitedCount = initialSize;
        unvisitedLocations = new ArrayList(initialSize);
        visitedLocations = new ArrayList(initialSize);
        visitedCount = 0;
        assignPositions(locs);
	}
	
    /// <summary>
    /// writes all the transforms from the locations into the array
    /// </summary>
    /// <param name="pos"></param>
	void assignPositions(GameObject[] objects){

        for (int i = 0; i < initialSize; i++){
            unvisitedLocations.Add(objects[i].transform);
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
        //get first element
        Debug.Log(unvisitedLocations.Count);
        Debug.Log(unvisitedCount);
        //Debug.Log(unvisitedLocations.Capacity);
       

        //get random location


        Transform closest = (Transform)unvisitedLocations[0];
        //get basic distance

		float lastDist = Vector3.Distance(closest.position, entity.position);

        
        for (int i = 1; i < unvisitedCount; i++){
            //get distance
            Transform location = (Transform)unvisitedLocations[i];
            float newDist =  Vector3.Distance(location.position, entity.position);
            //check if it is shorter than current shortest distance
            if (newDist < lastDist){
				closest = location;
                lastDist = newDist;
			}
		}
        unvisitedLocations.Remove((object)closest);
        visitedLocations.Add(closest);
        visitedCount++;
        unvisitedCount--;       
        if (unvisitedCount == 0) {
            unvisitedLocations = visitedLocations;
            visitedLocations.Clear();
            unvisitedCount = visitedCount;
            visitedCount = 0;
        }
		return closest;
	}

}
