using UnityEngine;
using System.Collections;

public class PreyDetection : MonoBehaviour {

	//this needs to be set once the trigger is triggerred(collider)
	private GameObject target;

	private float FOV = 110f;
	public Vector3 lastSighting;

	private NavMeshAgent navAgent;
	private SphereCollider triggerCollider;
	private Animator targetAnimator;


	// Use this for initialization
	void Start () {
		lastSighting = Vector3.zero;
		navAgent = gameObject.GetComponent<NavMeshAgent>();
		triggerCollider = gameObject.GetComponent<SphereCollider>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void assignTarget(GameObject entity){
		target = entity;
		targetAnimator = target.GetComponent<Animator>();
	}

	void OnTriggerStay(Collider entity){

	}

	public bool hasLastPosition(){
		if (lastSighting == Vector3.zero){
			return false;
		}
		else{
			return true;
		}
	}

	public void assignLastPosition(Vector3 pos){
		lastSighting = pos;
	}

	public bool targetHeard(){
		//we use the players animator controller to decide this
		//need to set up hashes for this? not actually


		//if we find that we do hear the player, we need to set the location for the search to work
		//still need to check if we heard the player
		lastSighting = target.transform.position;
		return false;
	}

	//checks to see if the player is in sight and not onstructed by anything
	public bool targetInSight(float distance){
		Vector3 direction = target.transform.position - transform.position;
		float angle = Vector3.Angle(direction, transform.forward);
		if (angle < FOV/2){
			RaycastHit hitObject;
			if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hitObject, distance)){
				if (hitObject.collider.gameObject == target){
					return true;
				}
			}
		}
		return false;
	}



	/*bool checkRayCast(RaycastHit[] rayData){
		int i = 0;
		int size = rayData.Length;
		float lastDist = viewingSens;
		Transform closestObject = null;
		while(i < size){
			//we loop through and assign the closest object
			if (rayData[i].transform != this.transform && rayData[i].distance < lastDist){
				closestObject = rayData[i].transform;
				lastDist = rayData[i].distance;
			}
			i++;
			//Debug.Log("checked object");
		}
		if (closestObject != null){
			
			
			if (closestObject.parent.tag == "Player"){
				Debug.Log(closestObject.ToString());
				Debug.Log(closestObject.parent.ToString());
				Debug.LogError(closestObject.parent.tag.ToString());
				return true;
				
			}
		}
		
		
		return false;
		
		
	}
*/

}
