using UnityEngine;
using System.Collections;

public class EscKeyListener : Photon.MonoBehaviour {

    public Light directionalLight;
    public GameObject exitConf;
    private bool displayed;
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
            if (!displayed) {
                exitConf.SetActive(true);
                displayed = true;
            }
            else {
                exitConf.SetActive(false);
                displayed = false;
            }

            
        }
        
        if (Input.GetKeyDown(KeyCode.Y) && displayed) {
            Application.LoadLevel("MainMenu");
        }

        if (Input.GetKeyDown(KeyCode.N) && displayed) {
            exitConf.SetActive(false);
            displayed = false;
        }
        

    }
}