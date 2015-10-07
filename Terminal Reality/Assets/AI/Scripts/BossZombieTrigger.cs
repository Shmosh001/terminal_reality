using UnityEngine;
using System.Collections;

public class BossZombieTrigger : MonoBehaviour {



    public GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER1);
	}
	
	// Update is called once per frame
	void Update () {
	    if (player == null) {
            player = GameObject.FindGameObjectWithTag(Tags.PLAYER1);
        }


        if (Input.GetKeyDown(KeyCode.Alpha9)) {
            gameObject.GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
            gameObject.GetComponent<Animator>().SetTrigger(BossHashScript.chargeTrigger);
        }
	}
}
