using UnityEngine;
using System.Collections;

public class MainMenuWander : MonoBehaviour {


    public Transform[] locations;
    private NavMeshAgent navAgent;
    private int index = 0;
    private int max;
    private Animator animator;
    


	// Use this for initialization
	void Start () {
        navAgent = this.gameObject.GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();
        max = locations.Length-1;
        nextLocation();
    }
	
	// Update is called once per frame
	void Update () {
	    if (checkArrival()) {
            navAgent.Stop();
          
            nextLocation();
        }
	}



    /// <summary>
    /// checks if we have arrived at a point
    /// </summary>
    /// <returns>
    /// true/false
    /// </returns>
    bool checkArrival() {
        //check if a path is already pending
        if (!navAgent.pathPending) {
            //check if we are close enough to the destination
            if (navAgent.remainingDistance <= navAgent.stoppingDistance) {
                //check if we have no path and that our velocity is 0
                if (!navAgent.hasPath || navAgent.velocity.sqrMagnitude == 0f) {
                    animator.SetBool("Walking", false);
                    return true;
                    
                }
            }
        }
        return false;
    }

    void nextLocation() {
        if (index > max) {
            index = 0;
        }

        navAgent.SetDestination(locations[index].position);
        index++;
        animator.SetBool("Walking", true);
    }

    
}
