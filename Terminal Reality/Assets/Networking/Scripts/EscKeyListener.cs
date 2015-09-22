using UnityEngine;
using System.Collections;

public class EscKeyListener : MonoBehaviour {

    public GameObject light;


	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}

        if (Input.GetKeyDown(KeyCode.L)) {
            if (light.active) {
                light.SetActive(false);
            }
            else {
                light.SetActive(true);
            }

        }
	}
}
