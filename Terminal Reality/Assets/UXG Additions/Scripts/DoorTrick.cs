using UnityEngine;
using System.Collections;

public class DoorTrick : MonoBehaviour {



    public GameObject Door;
    private DoorScript ds;
    public GameObject eye;
    public playerDataScript pds;
    public torchScript ts;

	// Use this for initialization
	void Start () {
	    if (Door == null || eye == null) {
            Debug.LogError("not assigned");
        }
        ds = Door.GetComponent<DoorScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void eyeAppear() {
        eye.SetActive(true);
    }

    void OnTriggerEnter(Collider col) {
        if (col.tag == Tags.PLAYER1) {
            //Debug.Log("player enter");

            if (ds.open) {
                ds.interaction();
            }

            ds.openPeek();
            Invoke("eyeAppear",0.5f);
            pds.disabled = true;
            ts.hackTorch(0);
        }

    }
    void OnTriggerExit(Collider col) {
        if (col.tag == Tags.PLAYER1) {
            //Debug.Log("player exit");
            ds.closePeek();
            eye.SetActive(false);
            pds.disabled = false;
            ts.hackTorch(100);
        }
    }

}
