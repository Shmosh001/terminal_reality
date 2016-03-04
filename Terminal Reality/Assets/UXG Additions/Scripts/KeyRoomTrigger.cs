using UnityEngine;
using System.Collections;

public class KeyRoomTrigger : MonoBehaviour {



    public PlayerAudioManager audio;

	// Use this for initialization
	void Start () {
	    if (audio == null) {
            Debug.LogError("not assigned");
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter(Collider other) {
        if (other.tag == Tags.PLAYER1) {
            audio.keyRoomSwitch();
        }
    }
    void OnTriggerExit(Collider other) {
        if (other.tag == Tags.PLAYER1) {
            audio.normalSwitch();
        }
    }

}
