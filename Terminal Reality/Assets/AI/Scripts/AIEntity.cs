using UnityEngine;
using System.Collections;

public class AIEntity <T>: MonoBehaviour{


	public int damage;
	public GameObject scripts;
	protected SoundManager sound;


	protected NavMeshAgent navAgent;
	protected HealthScript health;

	//list of targets that the AI element can attack
	public ArrayList targets;
	//ideally we should keep the players at 0 & 1
	protected WanderScript wanderScript;
	protected GameObject target;


	void Awake(){
		navAgent = gameObject.GetComponent<NavMeshAgent>();
		health = gameObject.GetComponent<HealthScript>();
		wanderScript = gameObject.GetComponent<WanderScript>();
		sound = scripts.GetComponent<SoundManager>();
		target = null;
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
	protected float getDistanceT(Transform entity1, Transform entity2){
		return Vector3.Distance(entity1.position,entity2.position);
	}

	protected float getDistanceP(Vector3 entity1, Vector3 entity2){
		return Vector3.Distance(entity1,entity2);
	}

	protected bool checkArrival(Vector3 pos1, Vector3 pos2){
		if (getDistanceP(pos1,pos2) < 1.5){
			return true;
		}
		return false;
	}


}
