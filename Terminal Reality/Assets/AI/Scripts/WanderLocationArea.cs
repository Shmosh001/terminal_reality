using UnityEngine;
using System.Collections;

public class WanderLocationArea : MonoBehaviour {


    


    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == Tags.ENEMY && collider.GetType() == typeof(CapsuleCollider)) {
            ZombieFSM fsm = collider.gameObject.GetComponent<ZombieFSM>();
            if (fsm != null && fsm.wandering) {
                fsm.stopWandering();
            }
        }
           

    }





}
