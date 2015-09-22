using UnityEngine;
using System.Collections;

public class LightSwitch : MonoBehaviour {

    public Light directionalLight;
	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
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
	}
}
