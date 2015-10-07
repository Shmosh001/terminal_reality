using UnityEngine;
using System.Collections;

public class RagdollFollow : MonoBehaviour {



    private GameObject zombie;
    private GameObject ragdoll;

	// Use this for initialization
	void Start () {
        zombie = transform.GetChild(0).gameObject;
        ragdoll = transform.GetChild(1).gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        ragdoll.transform.position = zombie.transform.position;
        ragdoll.transform.rotation = zombie.transform.rotation;
    }


    public void enableRagdoll() {
        zombie.SetActive(false);
        ragdoll.SetActive(true);
    }

    public void enableZombie() {

        ragdoll.SetActive(false);
        zombie.SetActive(true);
        
    }


}
