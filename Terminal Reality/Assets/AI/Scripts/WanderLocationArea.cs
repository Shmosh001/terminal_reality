using UnityEngine;
using System.Collections;

public class WanderLocationArea : MonoBehaviour {


    


    void OnTriggerEnter(Collider collider) {
//        Debug.Log(collider.gameObject.name + " entered");

        if (collider.gameObject.tag == Tags.ENEMY) {
            ZombieFSM fsm = collider.gameObject.GetComponent<ZombieFSM>();
            if (fsm != null) {
                fsm.stopWandering();
            }
        }
           

    }





}
