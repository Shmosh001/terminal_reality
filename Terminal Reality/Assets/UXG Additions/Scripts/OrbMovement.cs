using UnityEngine;
using System.Collections;

public class OrbMovement : MonoBehaviour {



    public NavMeshAgent agent;
    private PatrolScript ps;
    public bool stopAtEnd;
    public int endCount;
    private int count;
    public DoorScript ds;
    private bool start;
    public AudioSource scream;

    // Use this for initialization
    void Start () {
        start = false;
	    if (agent == null) {
            Debug.LogError("Agent not assigned");
        }
        ps = this.gameObject.GetComponent<PatrolScript>();
        //agent.SetDestination(ps.getNextWayPoint());
    }
	
	// Update is called once per frame
	void Update () {

        if (start && count >= endCount) {
            Debug.Log("reached end");
            agent.Stop();
            ds.specialClose();
            Destroy(this);
        }

	    if (start && checkArrival(ps.getCurrentWayPoint())){
            count++;
            Debug.Log("at point");
            agent.Stop();
            agent.SetDestination(ps.getNextWayPoint());
        }
        else {
            Debug.Log("cant reach point");
        }
       
	}

    bool checkArrival(Vector3 pos1) {
        if (Vector3.Distance(pos1, this.transform.position) <= 2) {
            return true;
        }
        return false;
    }


    public void specialStart() {
        start = true;
        scream.Play();
        Debug.Log("started");

    }


}
