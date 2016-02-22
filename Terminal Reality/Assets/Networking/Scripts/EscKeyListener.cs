using UnityEngine;
using System.Collections;

public class EscKeyListener : Photon.MonoBehaviour {

    public Light directionalLight;
    public GameObject exitConf;

    // Update is called once per frame
    void Update() {
        /*if(Input.GetKeyDown(KeyCode.L))
        {
            if(directionalLight.enabled == true)
            {
                directionalLight.enabled = false;
            }

            else if (directionalLight.enabled == false)
            {
                directionalLight.enabled = true;
            }
        }*/

		if (gameObject.tag == Tags.PLAYER1)
		{
	        if (Input.GetKeyDown(KeyCode.Escape)) {

                //Application.LoadLevel("MainMenu");
                exitConf.SetActive(true);
                //Application.Quit();
            }
	  	}
		if (gameObject.tag == Tags.PLAYER2)
		{
			if (Input.GetButtonDown("ESC")) {
                exitConf.SetActive(true);
                //Application.LoadLevel("MainMenu");
                //Application.Quit();
            }
		}
}
}