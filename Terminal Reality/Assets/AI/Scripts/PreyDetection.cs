using UnityEngine;
using System.Collections;

/// <summary>
/// this class handles everything to do with prey detection and when prey is detected and assigning targets
/// </summary>
public class PreyDetection : MonoBehaviour {


    //PUBLIC VARS

    //targets last sighting
    public Vector3 lastSighting;
    public Transform rayCastPosition;


    //PRIVATE VARS

    //this needs to be set once the trigger is triggered(collider)
    private GameObject target;
    
    //field of view for seeing the player
	private float FOV = 110f;
	
    //nav agent
	private NavMeshAgent navAgent;

    //sphere collider
	private SphereCollider triggerCollider;

    //animator of the target
	private Animator targetAnimator;


    /// <summary>
    /// initialization of variables
    /// </summary>
    void Start () {
		lastSighting = Vector3.zero;
		navAgent = gameObject.GetComponent<NavMeshAgent>();
		triggerCollider = gameObject.GetComponent<SphereCollider>();
	}
	
    /// <summary>
    /// assigns a target
    /// </summary>
    /// <param name="entity">
    /// new target
    /// </param>
	public void assignTarget(GameObject entity){
		target = entity;
        //TODO get animator
		//targetAnimator = target.GetComponent<Animator>();
	}

    /// <summary>
    /// checks to see if a last known position is available
    /// </summary>
    /// <returns>
    /// true/false based on outcome
    /// </returns>
	public bool hasLastPosition(){
		if (lastSighting == Vector3.zero){
			return false;
		}
		else{
			return true;
		}
	}

    /// <summary>
    /// stores a last position
    /// </summary>
    /// <param name="pos">
    /// position
    /// </param>
	public void assignLastPosition(Vector3 pos){
		lastSighting = pos;
	}

    /// <summary>
    /// determines if we can hear the player based on his animation state
    /// </summary>
    /// <returns>
    /// outcome of the above
    /// </returns>
	public bool targetHeard(){
		//we use the players animator controller to decide this
		//need to set up hashes for this? not actually


		//if we find that we do hear the player, we need to set the location for the search to work
		//still need to check if we heard the player
		lastSighting = target.transform.position;
		return false;
	}

    /// <summary>
    /// checks to see if the player is in sight and not constructed by anything
    /// </summary>
    /// <param name="distance">
    /// distance unit can see
    /// </param>
    /// <returns>
    /// true/false based on above
    /// </returns>
    public bool targetInSight(float distance){
        //Debug.Log("targetInSight");
        //Debug.Log(distance);
        //Debug.Log(target);
        //calculates if we can see the target based on the fov and the distance we can see
        //Vector3 direction = target.transform.position - transform.position;
        Vector3 direction = target.transform.position - transform.position;
        //Debug.Log(direction);
        //gets the angle between the player and the unit
        float angle = Vector3.Angle(direction, transform.forward);
        //Debug.Log(angle);
        //if the angle is smaller then we can see the target
        //now we need to check if anything is obstructing the view by raycasting
        if (angle < FOV/2){
            Debug.LogWarning("in view");
			RaycastHit hitObject;
			if (Physics.Raycast(transform.position+transform.up, target.transform.position + target.transform.up, out hitObject, distance)){
                Debug.LogWarning(hitObject.collider.gameObject);
                if (hitObject.collider.gameObject == target){
					return true;
				}
			}
		}
		return false;
	}


    void Update() {
        if (target != null)Debug.DrawLine(transform.position + transform.up, target.transform.position + target.transform.up);
    }
    
}
