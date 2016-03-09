using UnityEngine;
using System.Collections;

public class GirlAttackScript : MonoBehaviour {

    

  

    void OnTriggerEnter(Collider col) {
        if (col.tag == Tags.PLAYER1) {

            this.gameObject.transform.LookAt(col.transform.position);
        }
    }
}
