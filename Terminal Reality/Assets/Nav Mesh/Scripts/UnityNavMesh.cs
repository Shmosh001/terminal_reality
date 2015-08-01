using UnityEngine;
using System.Collections;

public class UnityNavMesh : MonoBehaviour {


	private float counter1,time;

	public Transform destination;
	private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		agent.destination = destination.position; 
		time = 1.5f;
	}
	
	// Update is called once per frame
	void Update () {
		counter1 += Time.deltaTime;
		if (counter1 > time){
			agent.destination = destination.position; 
			counter1 = 0;
		}
	}
}
