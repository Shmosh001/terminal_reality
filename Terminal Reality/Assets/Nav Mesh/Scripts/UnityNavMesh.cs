using UnityEngine;
using System.Collections;

public class UnityNavMesh : MonoBehaviour {


	public Transform destination;

	// Use this for initialization
	void Start () {
		NavMeshAgent agent = GetComponent<NavMeshAgent>();
		agent.destination = destination.position; 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
