using UnityEngine;
using System.Collections;

public class GirlSound : MonoBehaviour {

    public soundControllerScript sc;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter(Collider other) { 
        //when something enters the girls collider

        if (other.tag == Tags.PLAYER1) {
            //player entereds
            
        }
    }
}
