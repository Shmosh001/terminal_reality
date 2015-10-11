using UnityEngine;
using System.Collections;

public class EscKeyListener : Photon.MonoBehaviour {

    public Light directionalLight;
    

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.L))
        {
            if(directionalLight.enabled == true)
            {
                directionalLight.enabled = false;
            }

            else if (directionalLight.enabled == false)
            {
                directionalLight.enabled = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {

            //Application.LoadLevel("MainMenu");
            Application.Quit();
        }

        



    }
}
