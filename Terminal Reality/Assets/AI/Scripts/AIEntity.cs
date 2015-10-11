using UnityEngine;
using System.Collections;

/// <summary>
/// This script acts as a base class to Enemy AI units having some basic methods and variables that they will share
/// </summary>
/// <typeparam name="T">
/// this is the type of ai this entity is 
/// </typeparam>
public class AIEntity <T>: Photon.MonoBehaviour{

	//PUBLIC VARS

	//list of targets that the AI element can attack
	public ArrayList targets;
	//damage of the unit
	public int damage;
    //arrival distance (default = 1.5)
    public float arrivalDistance = 1.5f;

	
	//PROTECTED VARS - TO BE INHERITED

	//script that is used to play sound
	public soundControllerScript sound;
	//nav mesh agent component that is attached
	protected NavMeshAgent navAgent;
	//health script attached
	protected EnemyHealthScript health;
	//the script we use to wander around	
	protected WanderScript wanderScript;
	//the current target if any
	protected GameObject target;

    /// <summary>
    /// called when the script starts and initializes and quired most variables
    /// </summary>
    void Awake(){
		navAgent = gameObject.GetComponent<NavMeshAgent>();
		health = gameObject.GetComponent<EnemyHealthScript>();
		wanderScript = gameObject.GetComponent<WanderScript>();
        //temp
        target = null;
	}

    /// <summary>
    /// this method assigns the target variable to a new target
    /// </summary>
    /// <param name="entity">
    /// the new target
    /// </param>
	public void addTarget(GameObject entity){
		targets.Add(entity);
	}

    /// <summary>
    /// this method removes a specific target from the list of targets
    /// </summary>
    /// <param name="entity">
    /// the new target
    /// </param>
	public void removeTarget(GameObject entity){
		targets.Remove(entity);
	}


    /// <summary>
    /// gets the distance between 2 entities
    /// </summary>
    /// <param name="entity1">
    /// first entity
    /// </param>
    /// <param name="entity2">
    /// second entity
    /// </param>
    /// <returns>
    /// the distance between the 2 entities
    /// </returns>
    protected float getDistanceT(Transform entity1, Transform entity2){
		return Vector3.Distance(entity1.position,entity2.position);
	}

    /// <summary>
    /// gets the distance between 2 vector3's
    /// </summary>
    /// <param name="entity1">
    /// first entity
    /// </param>
    /// <param name="entity2">
    /// second entity
    /// </param>
    /// <returns>
    /// the distance between the 2 entities
    /// </returns>
	protected float getDistanceP(Vector3 entity1, Vector3 entity2){
		return Vector3.Distance(entity1,entity2);
	}

    /// <summary>
    /// checks the arrival of an entity at a position based on a assigned threshold
    /// </summary>
    /// <param name="pos1">
    /// current position
    /// </param>
    /// <param name="pos2">
    /// destination position
    /// </param>
    /// <returns>
    /// true/false -> if we are at the position or not
    /// </returns>
	protected bool checkArrival(Vector3 pos1, Vector3 pos2){
		if (getDistanceP(pos1,pos2) <= arrivalDistance){
			return true;
		}
		return false;
	}


    

}
