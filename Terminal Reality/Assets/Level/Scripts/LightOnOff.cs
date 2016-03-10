using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class LightOnOff : MonoBehaviour {

    //bulbs 
    public GameObject light1;
    public GameObject light2;
    public Texture black;
    public Texture white;


    public bool special;
    //public Material white;
    //public Material black;

	// Use this for initialization
	void Start () 
	{
        if (special) {
            light1.renderer.materials[0].SetTexture("_MainTex", black);
            light1.renderer.materials[1].SetTexture("_MainTex", black);
            light2.renderer.materials[0].SetTexture("_MainTex", black);
            light2.renderer.materials[1].SetTexture("_MainTex", black);
        }
        else {
            InvokeRepeating("Flicker", 0.01f, 1.5f);
        }
	}

	void Flicker()
	{
		if (light.enabled == true)
        {
			light.enabled = false;
            //set the texture of the light bulbs to black when light is off
            light1.renderer.materials[0].SetTexture("_MainTex", black);
            light1.renderer.materials[1].SetTexture("_MainTex", black);
            light2.renderer.materials[0].SetTexture("_MainTex", black);
            light2.renderer.materials[1].SetTexture("_MainTex", black);
            
        } 
		else 
		{
			light.enabled = true;
            //set the texture of the light bulbs to white when light is on
            light1.renderer.materials[0].SetTexture("_MainTex", white);
            light1.renderer.materials[1].SetTexture("_MainTex", white);
            light2.renderer.materials[0].SetTexture("_MainTex", white);
            light2.renderer.materials[1].SetTexture("_MainTex", white);
        }

	}
}
