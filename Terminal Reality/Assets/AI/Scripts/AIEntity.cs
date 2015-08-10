using UnityEngine;
using System.Collections;

public class AIEntity <T>: MonoBehaviour{


	public int damage;

	public GameObject player;

	protected NavMeshAgent navAgent;
	protected HealthScript health;

	//list of targets that the AI element can attack
	public ArrayList targets;
	//ideally we should keep the players at 0 & 1
	protected WanderScript wanderScript;


	void Start(){
		navAgent = gameObject.GetComponent<NavMeshAgent>();
		health = gameObject.GetComponent<HealthScript>();
		wanderScript = gameObject.GetComponent<WanderScript>();
	}

	public void addTarget(GameObject entity){
		targets.Add(entity);
	}

	public void removeTarget(GameObject entity){
		targets.Remove(entity);
	}


	//could have this in an AIEntity class ad base method
	public void activateEntity(){
		this.gameObject.SetActive(true);
	}

	//choses weither or not to change states  to a new event
	public bool eventChoice(){
		int choice = Random.Range(0,1);
		if (choice == 1){
			return true;
		}
		else{
			return false;
		}
	}

	//could also be in base class
	//choses between 2 states randomly
	public T chooseAction(T state1, T state2){
		int choice = Random.Range(0,1);
		if (choice == 1){
			return state1;
		}
		else{
			return state2;
		}
	}
	//general class
	public float getDistance(Transform entity1, Transform entity2){
		return Vector3.Distance(entity1.position,entity2.position);
	}



}
