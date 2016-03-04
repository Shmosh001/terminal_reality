using UnityEngine;
using System.Collections;

public class OrbMovement : MonoBehaviour {



    public NavMeshAgent agent;
    private PatrolScript ps;

    // Use this for initialization
    void Start () {
	    if (agent == null) {
            Debug.LogError("Agent not assigned");
        }
        ps = this.gameObject.GetComponent<PatrolScript>();
        agent.SetDestination(ps.getNextWayPoint());
    }
	
	// Update is called once per frame
	void Update () {
	    if (checkArrival(ps.getCurrentWayPoint())){
            Debug.Log("at point");
            agent.Stop();
            agent.SetDestination(ps.getNextWayPoint());
        }
	}

    bool checkArrival(Vector3 pos1) {
        if (Vector3.Distance(pos1, this.transform.position) <= 1) {
            return true;
        }
        return false;
    }



}
