using UnityEngine;
using System.Collections;

public class FXHandler : MonoBehaviour {


    public GameObject pistol;
    public GameObject mg;
    public GameObject torch;
    private soundControllerScript soundController;
    private float timerD = 0.1f, timerC;
    private bool check;
    


    void Start() {
        pistol = this.gameObject.GetComponent<playerDataScript>().pistolGameObject;
        mg = this.gameObject.GetComponent<playerDataScript>().machineGunGameObject;
        torch = this.gameObject.GetComponent<playerDataScript>().torch2;
        soundController = GameObject.FindGameObjectWithTag(Tags.SOUNDCONTROLLER).GetComponent<soundControllerScript>();
    }



    [PunRPC]
    public void turnOnTorch2() {
        GameObject.FindGameObjectWithTag(Tags.P2TORCH).SetActive(true);
    }

    [PunRPC]
    public void turnOffTorch2() {
        GameObject.FindGameObjectWithTag(Tags.P2TORCH).SetActive(false);
    }

    [PunRPC]
    public void torchOn(int mode) {


        //off
        if (mode == 0) {

            GameObject.FindGameObjectWithTag(Tags.P2TORCH).GetComponent<Light>().enabled = false;

        }
        //on
        else{

            GameObject.FindGameObjectWithTag(Tags.P2TORCH).GetComponent<Light>().enabled = true;
        }
        
    }
   

    [PunRPC]
    public void pistolEquipped() {
        Debug.Log("pistol rpc called");
        //we need to set that the player has picked up the pistol
        //actually that he has it equipped
        /*if (this.gameObject.tag == Tags.PLAYER1) {
            GameObject obj = GameObject.FindGameObjectWithTag(Tags.PLAYER2);
            if (obj != null) {
                obj.GetComponent<FXHandler>().pistol.SetActive(true);
            }
        }
        else if (this.gameObject.tag == Tags.PLAYER2) {
            GameObject obj = GameObject.FindGameObjectWithTag(Tags.PLAYER1);
            if (obj != null) {
                
            }
        }*/
        gameObject.GetComponent<FXHandler>().pistol.SetActive(true);
        gameObject.GetComponent<playerDataScript>().pistolPickedUp = true;
        gameObject.GetComponent<playerDataScript>().pistolEquipped = true;
        //pistol.SetActive(true);
    }

    [PunRPC]
    public void mgEquipped() {
        //we need to set that the player has picked up the pistol
        //actually that he has it equipped
        mg.SetActive(true);
    }


    [PunRPC]
    public void gunShot() {
        Debug.LogWarning("gunshot rpc received " + gameObject.tag);
        playerDataScript pdata = gameObject.GetComponent<playerDataScript>();
        if (pdata.pistolEquipped) {
            Debug.LogWarning("gun equiped "  + gameObject.tag);
            pdata.pistolGameObject.GetComponent<weaponDataScript>().gunFlare(true);
            timerC = 0;
            check = true;
            soundController.playPistolShot(transform.position); //play sound of a pistol shot
        }
        else if (pdata.machineGunEquipped) {
            
        }
    }



    void Update() {
        timerC += Time.deltaTime;
        if (check && timerC > timerD ) {
            check = false;
            playerDataScript pdata = gameObject.GetComponent<playerDataScript>();
            pdata.pistolGameObject.GetComponent<weaponDataScript>().gunFlare(false);
        }
    }


}
