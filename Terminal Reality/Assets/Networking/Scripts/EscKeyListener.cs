using UnityEngine;
using System.Collections;

public class EscKeyListener : Photon.MonoBehaviour {

    private Light directionalLight;
    private GameObject light_;

    // Use this for initialization
    void Start() {
        light_ = GameObject.FindGameObjectWithTag(Tags.DEBUGLIGHT);
        if (light_ != null) {
            directionalLight = light_.light;
        }
    }

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
            //PhotonNetwork.Destroy(gameObject);
        }




    }
}
