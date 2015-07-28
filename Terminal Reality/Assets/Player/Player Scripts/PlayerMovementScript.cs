using UnityEngine;
using System.Collections;

public class PlayerMovementScript : MonoBehaviour {

	private CharacterController characterController;

	//PUBLIC MOVEMENT VARIABLES//
	public float movementSpeed = 6.5f;
	public float mouseSpeed = 3.0f;
	public float jumpSpeed = 5.5f;

	//PRIVATE MOVEMENT WARIABLES//
	private float rotUD = 0;
	private float verticalVelocity = 0;


	// Use this for initialization
	void Start () 
	{
		Screen.lockCursor = true; //take the mouse off the screen.

		characterController = GetComponent<CharacterController>();
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
		/***********
		//ROTATION//
		***********/

		//left-right//
		float rotLR = Input.GetAxis("Mouse X") * mouseSpeed;
		transform.Rotate(0, rotLR, 0);

		//up-down//
		rotUD -= Input.GetAxis ("Mouse Y") * mouseSpeed;
		rotUD = Mathf.Clamp (rotUD, -45.0f, 45.0f);
		Camera.main.transform.localRotation = Quaternion.Euler (rotUD, 0, 0);


		/***********
		//MOVEMENT//
		**********/		
		
		float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
		float sideSpeed = Input.GetAxis ("Horizontal") * movementSpeed;

		//JUMPING//
		if (characterController.isGrounded && Input.GetButtonDown ("Jump"))
		{
			verticalVelocity = jumpSpeed;
		}
		verticalVelocity += Physics.gravity.y * Time.deltaTime; //increase falling velocity as you are falling OR decrease velocity as you're going up.

		Vector3 speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed); //set speed vector in which player is moving in xyz		
		speed = transform.rotation * speed;		
		characterController.Move(speed * Time.deltaTime);
	}
}
