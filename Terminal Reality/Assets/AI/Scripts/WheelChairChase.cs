using UnityEngine;
using System.Collections;

public class WheelChairChase : MonoBehaviour {


    private NavMeshAgent agent;
    private Animator main;
    private Animator fw;
    private Animator bw;
    private Animator fw2;
    private Animator bw2;
    private bool chasing;

    // Use this for initialization
    void Start () {
        agent = gameObject.GetComponent<NavMeshAgent>();
        main = gameObject.GetComponent<Animator>();
        fw = gameObject.transform.GetChild(0).GetChild(2).GetComponentInChildren<Animator>();
        fw2 = gameObject.transform.GetChild(0).GetChild(3).GetComponentInChildren<Animator>();
        bw = gameObject.transform.GetChild(0).GetChild(4).GetComponentInChildren<Animator>();
        bw2 = gameObject.transform.GetChild(0).GetChild(5).GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.F5)) {

            agent.SetDestination(GameObject.FindGameObjectWithTag(Tags.PLAYER1).transform.position);
            main.SetBool("Moving", true);
            fw.SetBool("Moving", true);
            bw.SetBool("Moving", true);
            fw2.SetBool("Moving", true);
            bw2.SetBool("Moving", true);
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
            fw2.SetBool("Moving", false);
            bw2.SetBool("Moving", false);
        }
    }
}
