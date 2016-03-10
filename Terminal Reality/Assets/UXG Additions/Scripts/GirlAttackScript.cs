using UnityEngine;
using System.Collections;

public class GirlAttackScript : MonoBehaviour {

    public AudioSource scream;
    private GameObject player;
    private bool atack;

    void Update() {
        if (atack) {
            this.gameObject.GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
            this.gameObject.transform.LookAt(player.transform.position);

        }
    }
  
    public void attack(Collider col) {
        scream.Play();
        this.gameObject.GetComponent<Animator>().SetBool("Sprint",true);
        atack = true;
        this.gameObject.GetComponent<NavMeshAgent>().SetDestination(col.transform.position);
        player = col.gameObject;
    }


    void OnTriggerEnter(Collider col) {
        if (col.tag == Tags.PLAYER1) {
            this.gameObject.SetActive(false);
        }
    }

    /*
    public void turn() {
        this.gameObject.transform.LookAt(col.transform.position);
    }*/

    bool checkArrival(Vector3 pos1) {
        //Debug.Log(Vector3.Distance(pos1, this.transform.position));
        if (Vector3.Distance(pos1, this.transform.position) <= 1) {
            return true;
        }
        return false;
    }


    void OnCollisionEnter(Collision col) {
        Debug.Log("player hit");
        if (col.gameObject.tag == Tags.PLAYER1) {
            
        }
    }


}
