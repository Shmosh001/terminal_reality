using UnityEngine;
using System.Collections;

public class cameraScript : MonoBehaviour {

	private Transform player;
	private Transform playerSpine;
	private Vector3 relCameraPos;
	private float relCameraPosMag;
	private float relCameraPosMagup;
	private Vector3 newPos;

	// Use this for initialization
	void Awake () {
	
		player = gameObject.transform.parent;		
		
		relCameraPos = transform.position - player.position;
		relCameraPosMag = relCameraPos.magnitude + 120.0f;
		relCameraPosMagup = relCameraPos.magnitude + 40.0f;
	
	}
	
	
	public void playerDeadCam()
	{
		Vector3 standardPos = player.position + relCameraPos;
		Vector3 abovePos = player.position + Vector3.up * relCameraPosMagup + Vector3.back * relCameraPosMag;
		transform.position = Vector3.Lerp(transform.position, abovePos, 1.5f * Time.deltaTime);	
		
	}
	
	public void pickupCam()
	{
		transform.parent = gameObject.transform.parent.GetChild(1).GetChild (2).GetChild(0).GetChild(0).GetChild(1).GetChild (0);
	}
	public void resetCam()
	{
		transform.parent = player;
	}
	
}
