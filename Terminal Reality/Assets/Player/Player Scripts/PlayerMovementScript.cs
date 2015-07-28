using UnityEngine;
using System.Collections;

public class PlayerMovementScript : MonoBehaviour {

	private CharacterController characterController;

	//PUBLIC MOVEMENT VARIABLES//
	public float movementSpeed = 6.5f;
	public float mouseSpeed = 3.0f;
	public float jumpSpeed = 5.5f;
	public float pushingPower = 2.5f;

	//PRIVATE MOVEMENT WARIABLES//
	private float rotUD = 0;
	private float verticalVelocity = -1;


	// Use this for initialization
	void Start () 
	{
		Screen.lockCursor = true; //take the mouse off the screen.

		characterController = this.GetComponent<CharacterController>();
	
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
		verticalVelocity += -9.8f * Time.deltaTime; //increase falling velocity as you are falling OR decrease velocity as you're going up.

		Vector3 speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed); //set speed vector in which player is moving in xyz		
		speed = transform.rotation * speed;		
		characterController.Move(speed * Time.deltaTime);
	}

	void OnControllerColliderHit(ControllerColliderHit hit) 
	{
			Rigidbody body = hit.collider.attachedRigidbody;
			if (body == null || body.isKinematic)
				return;
			
			if (hit.moveDirection.y < -0.3F)
				return;
			
			Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
			body.velocity = pushDir * pushingPower;
	} 

}
