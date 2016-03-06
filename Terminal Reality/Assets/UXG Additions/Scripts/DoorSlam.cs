using UnityEngine;
using System.Collections;

public class DoorSlam : MonoBehaviour {



    private DoorScript ds;

    private float timer, time = 1.0f;
    private bool called;

	// Use this for initialization
	void Start () {
        ds = this.gameObject.GetComponent<DoorScript>();
        
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
	    if (!called && timer > time) {
            ds.specialOpen();
            called = true;
        }
	}


    public void SlamDoor() {
        ds.specialClose();
    }


    
}
