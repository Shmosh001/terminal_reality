using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {


    public Transform lead;
    public Transform lead2;
    public int offsetDistance;


	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(lead2.position.x-2, transform.position.y, lead2.position.z + offsetDistance);

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.LoadLevel("MainMenu");
        }

	}
}
