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

    public GameObject Player2;
    public GameObject Player1;


    //PRIVATE VARS

    //this needs to be set once the trigger is triggered(collider)
    private GameObject target;
    
    //field of view for seeing the player
	private float FOV = 110f;
	
    //nav agent
	private NavMeshAgent navAgent;

    //sphere collider
	private SphereCollider triggerCollider;




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
    /// assigns a target
    /// </summary>
    /// <param name="entity">
    /// new target
    /// </param>

    public GameObject assignTarget(string tag) {
        //Debug.Log(tag + "assigned as target");
        if (tag == Tags.PLAYER1) {
            target = Player1;
        }
        else if (tag == Tags.PLAYER2) {
            target = Player2;
        }
        return target;
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
        if (target == null) {
            Debug.LogWarning("target is null");
            return false;
        }

        if (target.GetComponent<playerDataScript>().canHear) {
            //Debug.LogWarning("player heard");
            lastSighting = target.transform.position;
            return true;
        }

        else {
            //Debug.LogWarning("player heard can hear false");
            return false;
        }
        
		//if we find that we do hear the player, we need to set the location for the search to work
		//still need to check if we heard the player
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

        if (target == null) {
            //Debug.Log("target is null");
            return false;
        }

        Vector3 direction = target.transform.position - transform.position;
        //Debug.Log(direction);
        //gets the angle between the player and the unit
        float angle = Vector3.Angle(direction, transform.forward);
        //Debug.Log(angle);
        //if the angle is smaller then we can see the target
        //now we need to check if anything is obstructing the view by raycasting
        if (angle < FOV / 2) {
            
            Debug.LogWarning("in view");
            RaycastHit hitObject;
            //Debug.Log(transform.position + transform.up);
            //Debug.Log(direction);
            if (Physics.Raycast(transform.position + transform.up, direction, out hitObject, distance)) {
                Debug.LogWarning(hitObject.collider.gameObject);
                if (hitObject.collider.gameObject == target) {
                    return true;
                }
                else if (hitObject.collider.gameObject.tag == Tags.PLAYER1 || hitObject.collider.gameObject.tag == Tags.PLAYER2) {
                    target = hitObject.collider.gameObject;
                    return true;
                }

            }
            
        }
        return false;

    }







}
