using UnityEngine;
using System.Collections;

public class Attackplayer : MonoBehaviour {


    public GameObject girl;


    public bool set;

	// Use this for initialization
	void Start () {
        set = false;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter(Collider col) {
        if (col.tag == Tags.PLAYER1 && !set) {
            set = true;
            girl.GetComponent<GirlAttackScript>().attack(col);

        }
    }

  

}
