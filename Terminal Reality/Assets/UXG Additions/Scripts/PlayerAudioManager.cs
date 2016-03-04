using UnityEngine;
using System.Collections;

public class PlayerAudioManager : MonoBehaviour {



    public AudioSource normalBG;
    public AudioSource keyRoomBG;


	// Use this for initialization
	void Start () {
	    if (normalBG == null || keyRoomBG == null) {
            Debug.LogError("Not assigned");
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}



    public void keyRoomSwitch() {
        //switch on
        keyRoomBG.Play();
        normalBG.Stop();

    }

    public void normalSwitch() {
        normalBG.Play();
        keyRoomBG.Stop();
    }
}
