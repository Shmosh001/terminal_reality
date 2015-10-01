using UnityEngine;
using System.Collections;

public class MMStarter : MonoBehaviour {


    public GameObject mmButtons;
    public GameObject startButton;



	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Return)) {
            startButton.SetActive(false);
            mmButtons.SetActive(true);
        }
	}
}
