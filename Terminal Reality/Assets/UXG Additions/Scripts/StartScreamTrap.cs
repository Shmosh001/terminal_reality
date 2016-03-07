using UnityEngine;
using System.Collections;

public class StartScreamTrap : MonoBehaviour {


    public OrbMovement scream;
    private bool called;

	// Use this for initialization
	void Start () {
        called = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}



    void OnTriggerEnter(Collider coll) {
      
        if (coll.tag == Tags.PLAYER1 && !called) {

            called = true;
            scream.specialStart();
        }
    }

}
