using UnityEngine;
using System.Collections;

public class PlayerMovementScript : MonoBehaviour {
	
	private CharacterController characterController;
	
	//the animator
	//private Animator animator;

    private playerAnimatorSync animSync;

	
	//PRIVATE MOVEMENT WARIABLES//
	private float rotUD = 0;
	private float verticalVelocity = -1;
	private float movementMultiplier = 1;
	private float forwardSpeed = 0;
	private float sideSpeed = 0;
	private playerDataScript playerData;
	private float sprintEnergy = 15.0f;
		
	// Use this for initialization
	void Start () 
	{
		Screen.lockCursor = true; //take the mouse off the screen.
		playerData = this.GetComponent<playerDataScript>();
		characterController = this.GetComponent<CharacterController>();
		//animator = this.gameObject.GetComponent<Animator>();
        animSync = this.gameObject.GetComponent<playerAnimatorSync>();
    }
	
	// Update is called once per frame
	void Update () 
	{
		//if player not moving...
		if (forwardSpeed == 0 && sideSpeed == 0)
		{
			playerData.canHear = false;
		}
		
		//////////////////////////////////////////////////////////
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
		//-----------PLAYER ONE----------PLAYER ONE------------//
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
		/////////////////////////////////////////////////////////
		if (gameObject.tag == "Player1")
		{
			/***********
			//ROTATION//
			***********/			
			if (playerData.playerAlive) // can only use the mouse if the player is alive.
			{
				//left-right//
				float rotLR = Input.GetAxis("Mouse X") * playerData.mouseSpeed;
				transform.Rotate(0, rotLR, 0);
				
				//up-down//
				rotUD -= Input.GetAxis ("Mouse Y") * playerData.mouseSpeed;
				rotUD = Mathf.Clamp (rotUD, -60.0f, 45.0f);
				if (Camera.main != null){
					Camera.main.transform.localRotation = Quaternion.Euler (rotUD, 0, 0);
				}
				
			}
			/***********
			//MOVEMENT//
			**********/	
			
			//SPRINTING//
			if (Input.GetKeyDown(KeyCode.LeftShift)) //If sprint button is being held down//
			{
				if (sprintEnergy > 0.0f) // if the player has sprint energy --> can sprint
				{
					movementMultiplier = 1.5f;
					
					//update movement state
					playerData.sprinting = true;
					playerData.sneaking = false;
					playerData.walking = false;
					playerData.canHear = true;
				}
			}
			if (Input.GetKeyUp(KeyCode.LeftShift)) //Release sprint button//
			{
				movementMultiplier = 1.0f;
				
				//update movement state
				playerData.sprinting = false;
				playerData.sneaking = false;
				playerData.walking = true;
			}
			
			// if the player is sprinting --> decrease sprint energry 
			if (playerData.sprinting)
			{
				if (sprintEnergy > -1.0f) {sprintEnergy -= Time.deltaTime;} // decrease sprint energy only if sprint energy is greater than 0
				if (sprintEnergy <= 0)
				{
					movementMultiplier = 1.0f;
					
					//update movement state
					playerData.sprinting = false;
					playerData.sneaking = false;
					playerData.walking = true;
				}
			}
			else //if the player is NOT sprinting --> increase sprint energy
			{
				if (sprintEnergy < 15.0f) {sprintEnergy += Time.deltaTime;} //increase sprint energy until 30.0f
			}
			
			//SNEAKING//
			if (Input.GetKeyDown(KeyCode.LeftControl)) //If sneak button is being held down//
			{
				movementMultiplier = 0.70f;
				
				//update movement state
				playerData.sprinting = false;
				playerData.sneaking = true;
				playerData.walking = false;
				playerData.canHear = false;
			}
			if (Input.GetKeyUp(KeyCode.LeftControl)) //Release sneak button//
			{
				movementMultiplier = 1.0f;
				
				//update movement state
				playerData.sprinting = false;
				playerData.sneaking = false;
				playerData.walking = true;
				playerData.canHear = true;
			}
			
			if (characterController.isGrounded && playerData.playerAlive) //can only adjust directional speed when player is grounded//
			{
				forwardSpeed = Input.GetAxis("Vertical") * playerData.movementSpeed * movementMultiplier;
				sideSpeed = Input.GetAxis ("Horizontal") * playerData.movementSpeed * movementMultiplier;
				
			}
			else //if player is in the air, slow down speed//
			{
				if (forwardSpeed > 0)
				{
					forwardSpeed -= 0.025f;
				}
				else if (forwardSpeed < 0)
				{
					forwardSpeed += 0.025f;
				}
				
				if (sideSpeed > 0)
				{
					sideSpeed -= 0.025f;
				}
				else if (sideSpeed < 0)
				{
					sideSpeed += 0.025f;
				}
			}
			//JUMPING//
			if (characterController.isGrounded && Input.GetButtonDown("Jump") && playerData.playerAlive)
			{
				
				animSync.setTriggerP(playerAnimationHash.jumpTrigger);
				verticalVelocity = playerData.jumpSpeed;
				
			}
			
			verticalVelocity += -10.0f * Time.deltaTime; //increase falling velocity as you are falling OR decrease velocity as you're going up.
			
			Vector3 speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed); //set speed vector in which player is moving in xyz		
			speed = transform.rotation * speed;		
			characterController.Move(speed * Time.deltaTime);
			
			//Set speed in animator controller
			//animator.SetFloat(playerAnimationHash.forwardSpeedFloat, forwardSpeed);
			
			animSync.setFloatP(playerAnimationHash.forwardSpeedFloat, forwardSpeed);
		}
		
		
		
		//////////////////////////////////////////////////////////
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
		//-----------PLAYER TWO----------PLAYER TWO------------//
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
		/////////////////////////////////////////////////////////
		if (gameObject.tag == "Player2")
		{
			/***********
			//ROTATION//
			***********/			
			if (playerData.playerAlive) // can only use the mouse if the player is alive.
			{
				//left-right//
				//float rotLR = Input.GetAxis("contX") * playerData.mouseSpeed;				
				transform.Rotate(0, Input.GetAxis("contX")*3, 0);				
				
				//up-down//
				rotUD -= Input.GetAxis ("contY") * playerData.mouseSpeed;
				rotUD = Mathf.Clamp (rotUD, -60.0f, 45.0f);
				transform.GetChild(2).localRotation = Quaternion.Euler (rotUD, 0, 0);
				
				
			}
			
			/***********
			//MOVEMENT//
			**********/	
			//SPRINTING//
			if (Input.GetAxis("SprintC") > 0) //If sprint button is being held down//
			{
				if (sprintEnergy > 0.0f) // if the player has sprint energy --> can sprint
				{
					movementMultiplier = Input.GetAxis("SprintC") * 1.8f;
					
					//update movement state
					playerData.sprinting = true;
					playerData.sneaking = false;
					playerData.walking = false;
					playerData.canHear = true;
				}
			}
			if (Input.GetAxis("SprintC") <= 0) //Release sprint button//
			{
				movementMultiplier = 1.0f;
				
				//update movement state
				playerData.sprinting = false;
				playerData.sneaking = false;
				playerData.walking = true;
			}
			
			// if the player is sprinting --> decrease sprint energry 
			if (playerData.sprinting)
			{
				if (sprintEnergy > -1.0f) {sprintEnergy -= Time.deltaTime;} // decrease sprint energy only if sprint energy is greater than 0
				if (sprintEnergy <= 0)
				{
					movementMultiplier = 1.0f;
					
					//update movement state
					playerData.sprinting = false;
					playerData.sneaking = false;
					playerData.walking = true;
				}
			}
			else //if the player is NOT sprinting --> increase sprint energy
			{
				if (sprintEnergy < 15.0f) {sprintEnergy += Time.deltaTime;} //increase sprint energy until 30.0f
			}
			
			//SNEAKING//
			if (Input.GetButtonDown("SneakC")) //If sneak button is being held down//
			{
				movementMultiplier = 0.70f;
				
				//update movement state
				playerData.sprinting = false;
				playerData.sneaking = true;
				playerData.walking = false;
				playerData.canHear = false;
			}
			if (Input.GetButtonUp("SneakC")) //Release sneak button//
			{
				movementMultiplier = 1.0f;
				
				//update movement state
				playerData.sprinting = false;
				playerData.sneaking = false;
				playerData.walking = true;
				playerData.canHear = true;
			}
			
			if (characterController.isGrounded && Input.GetButtonDown("JumpC") && playerData.playerAlive)
			{
				
				animSync.setTriggerP(playerAnimationHash.jumpTrigger);
				verticalVelocity = playerData.jumpSpeed;
				
			}
			
			if (characterController.isGrounded && playerData.playerAlive) //can only adjust directional speed when player is grounded//
			{
				forwardSpeed = Input.GetAxis("VerticalC") * playerData.movementSpeed * movementMultiplier;
				sideSpeed = Input.GetAxis ("HorizontalC") * playerData.movementSpeed * movementMultiplier;
				
			}
			else //if player is in the air, slow down speed//
			{
				if (forwardSpeed > 0)
				{
					forwardSpeed -= 0.025f;
				}
				else if (forwardSpeed < 0)
				{
					forwardSpeed += 0.025f;
				}
				
				if (sideSpeed > 0)
				{
					sideSpeed -= 0.025f;
				}
				else if (sideSpeed < 0)
				{
					sideSpeed += 0.025f;
				}
			}
			
			verticalVelocity += -10.0f * Time.deltaTime; //increase falling velocity as you are falling OR decrease velocity as you're going up.
			
			Vector3 speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed); //set speed vector in which player is moving in xyz		
			speed = transform.rotation * speed;		
			characterController.Move(speed * Time.deltaTime);
			
			//Set speed in animator controller
			//animator.SetFloat(playerAnimationHash.forwardSpeedFloat, forwardSpeed);
			
			animSync.setFloatP(playerAnimationHash.forwardSpeedFloat, forwardSpeed);
		}		
        

    }
	
		
	void OnControllerColliderHit(ControllerColliderHit hit) 
	{
		Rigidbody body = hit.collider.attachedRigidbody;
		if (body == null || body.isKinematic)
			return;
		
		if (hit.moveDirection.y < -0.3F)
			return;
		
		Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
		body.velocity = pushDir * playerData.pushingPower;
	} 
	
}