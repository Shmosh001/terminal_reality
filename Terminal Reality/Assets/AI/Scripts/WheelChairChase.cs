using UnityEngine;
using System.Collections;

public class WheelChairChase : MonoBehaviour {


    private NavMeshAgent agent;
    private Animator main;
    private Animator fw;
    private Animator bw;
    private bool chasing;

    // Use this for initialization
    void Start () {
        agent = gameObject.GetComponent<NavMeshAgent>();
        main = gameObject.GetComponent<Animator>();
        fw = gameObject.GetComponentInChildren<Animator>();
        bw = gameObject.GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.F5)) {

            agent.SetDestination(GameObject.FindGameObjectWithTag(Tags.PLAYER1).transform.position);
            main.SetBool("Moving", true);
            fw.SetBool("Moving", true);
            bw.SetBool("Moving", true);
            chasing = true;

        }



        if (chasing) {
            agent.SetDestination(GameObject.FindGameObjectWithTag(Tags.PLAYER1).transform.position);
        }


        if (Input.GetKeyDown(KeyCode.F6)) {
            agent.Stop();
            chasing = false;
            main.SetBool("Moving", false);
            fw.SetBool("Moving", false);
            bw.SetBool("Moving", false);
        }
    }
}
