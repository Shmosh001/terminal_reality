using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class torchScript : MonoBehaviour {

    //PUBLIC TORCH VARIABLES//
    public bool torchOn = false;
    public Light torch;
    public GameObject soundController;
    public Image torchHUD;
    public Sprite torchOnImage;
    public Sprite torchOffImage;
    public Slider torchSlider;


    private GameObject torchObj;
    private GameObject sliderObj;
    //PRIVATE TORCH VARIABLES//
    private float batteryLife;

    // Use this for initialization
    void Start() {

        batteryLife = 100.0f;
        soundController = GameObject.FindGameObjectWithTag(Tags.SOUNDCONTROLLER);


        torchObj = GameObject.FindGameObjectWithTag(Tags.FLASHLIGHTICON);
        if (torchObj != null) {
            torchHUD = torchObj.GetComponent<Image>() as Image;
        }

        sliderObj = GameObject.FindGameObjectWithTag(Tags.FLASHLIGHTSLIDER);
        if (sliderObj != null) {
            torchSlider = sliderObj.GetComponent<Slider>() as Slider;
        }




    }

    /*
    [PunRPC]
    void switchOnTorch() {
        soundController.GetComponent<soundControllerScript>().playTorchSound(transform.position); //play torch on/off sound
        torchOn = !torchOn;
    }*/



    void Update() {


       
        //check for missing game objects

        if (sliderObj == null) {
            sliderObj = GameObject.FindGameObjectWithTag(Tags.FLASHLIGHTSLIDER);
            if (sliderObj != null) {
                torchSlider = sliderObj.GetComponent<Slider>() as Slider;
            }
            else {
                return;
            }
        }


        if (torchObj == null) {
            torchObj = GameObject.FindGameObjectWithTag(Tags.FLASHLIGHTICON);
            if (torchObj != null) {
                torchHUD = torchObj.GetComponent<Image>() as Image;
            }
            else {
                return;
            }
        }

            

   
        




		//IF F IS PUSH TO TURN THE TORCH OFF OR ON//
		if (Input.GetKeyDown(KeyCode.F))
		{
			soundController.GetComponent<soundControllerScript>().playTorchSound(transform.position); //play torch on/off sound
			torchOn = !torchOn;
            updateTorchActivity();

        }

		//if the torch is on, reduce the battery life.
		if (torchOn && batteryLife >= 0.0f)
		{
			batteryLife -= 0.05f;
		}
		
		//if torch is off, charge the battery
		if (!torchOn && batteryLife <= 100.0f)
		{
			batteryLife += 0.1f;
		}
		
		//if the battery is dead (<0), then turn the torch off//
		if (batteryLife <= 0.0f)
		{
			torchOn = false;
            updateTorchActivity();
        }

		//at the end of each update cycle, check whether the torch needs to be turned on or off.
		
		updateTorchHUD();

	}



    void handleNetwork(int mode) {
        //PhotonView pView = transform.parent.gameObject.GetComponentInParent<PhotonView>();
        GameObject mainObj = this.gameObject.transform.parent.transform.parent.gameObject;

       
       
        if (mainObj.tag == Tags.PLAYER1) {
            GameObject player2 = GameObject.FindGameObjectWithTag(Tags.PLAYER2);
            if (player2 != null) {
                player2.GetComponent<PhotonView>().RPC("torchOn", PhotonTargets.OthersBuffered, mode);
            }
        }
        else if (mainObj.tag == Tags.PLAYER2) {
            GameObject player1 = GameObject.FindGameObjectWithTag(Tags.PLAYER1);
            if (player1 != null) {
                player1.GetComponent<PhotonView>().RPC("torchOn", PhotonTargets.OthersBuffered, mode );
            }
        }
        
        

        

    }


	//CHECK WHETHER THE TORCH IS ON OR OFF, 
	//AND THEN TURN THE LIGHT ON OR OFF.
	private void updateTorchActivity()
	{

        if (torchOn) {
            torch.enabled = true;
            torchHUD.sprite = torchOnImage;
            if (!PhotonNetwork.offlineMode) {
                handleNetwork(1);
            }
        }
        else {
            torch.enabled = false;
            torchHUD.sprite = torchOffImage;
            if (!PhotonNetwork.offlineMode) {
                handleNetwork(0);
            }
        }
	}

	//UPDATE THE TORCH DISPLAYED ON THE HUD//
	private void updateTorchHUD()
	{		
		torchSlider.value = batteryLife;
	}





}
