using UnityEngine;
using System.Collections;

public class WanderLocationArea : MonoBehaviour {


    


    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == Tags.ENEMY) {
            ZombieFSM fsm = collider.gameObject.GetComponent<ZombieFSM>();
            if (fsm != null) {
                fsm.stopWandering();
            }
        }
           

    }





}
