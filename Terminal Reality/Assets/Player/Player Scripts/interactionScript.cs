using UnityEngine;
using System.Collections;

public class interactionScript : MonoBehaviour {

	//PRIVATE VARIABLES INTERACTION//
	private Ray ray;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//WHEN THE PLAYER PUSHES E TO INTERACT WITH THE ENVIRONMENT//
		if (Input.GetKeyDown(KeyCode.E))
		{
			Debug.Log("Attempting interaction...");
			ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

			RaycastHit hitInfo; //to store what the ray hit

			//IF RAY HITS AN OBJECT IN RANGE//
			if (Physics.Raycast(ray, out hitInfo, 2.5f))
			{
				Vector3 hitPoint = hitInfo.point; //point where the collision happened
				GameObject hitObject = hitInfo.collider.gameObject; //get the game object which the ray hits

				//IF THE RAY HIT A DOOR//
				if (hitObject.CompareTag("Door"))
				{
					Debug.Log("Open Door!");
				}
			}
		}
	
	}
}
