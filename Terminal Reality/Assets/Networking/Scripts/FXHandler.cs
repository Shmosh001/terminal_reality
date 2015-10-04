using UnityEngine;
using System.Collections;

public class FXHandler : MonoBehaviour {


    public GameObject pistol;
    public GameObject mg;
    public GameObject torch;


    void Start() {
        pistol = this.gameObject.GetComponent<playerDataScript>().pistolGameObject;
        mg = this.gameObject.GetComponent<playerDataScript>().machineGunGameObject;
        torch = this.gameObject.GetComponent<playerDataScript>().torch2GameObject;



    }


    [PunRPC]
    public void torchOn(int mode) {
        //off
        if (mode == 0) {
            if (this.gameObject.tag == Tags.PLAYER1) {
                GameObject obj = GameObject.FindGameObjectWithTag(Tags.PLAYER2);
                if (obj != null) {
                    obj.GetComponent<FXHandler>().torch.light.enabled = false;
                }
            }
            else if (this.gameObject.tag == Tags.PLAYER2) {
                GameObject obj = GameObject.FindGameObjectWithTag(Tags.PLAYER1);
                if (obj != null) {
                    obj.GetComponent<FXHandler>().torch.light.enabled = false;
                }
            }
        }
        //on
        else{
            if (this.gameObject.tag == Tags.PLAYER1) {
                GameObject obj = GameObject.FindGameObjectWithTag(Tags.PLAYER2);
                if (obj != null) {
                    obj.GetComponent<FXHandler>().torch.light.enabled = true;
                }
            }
            else if (this.gameObject.tag == Tags.PLAYER2) {
                GameObject obj = GameObject.FindGameObjectWithTag(Tags.PLAYER1);
                if (obj != null) {
                    obj.GetComponent<FXHandler>().torch.light.enabled = true;
                }
            }
        }
        
    }
   

    [PunRPC]
    public void pistolEquipped() {
        Debug.Log("pistol rpc called");
        //we need to set that the player has picked up the pistol
        //actually that he has it equipped
        if (this.gameObject.tag == Tags.PLAYER1) {
            GameObject obj = GameObject.FindGameObjectWithTag(Tags.PLAYER2);
            if (obj != null) {
                obj.GetComponent<FXHandler>().pistol.SetActive(true);
            }
        }
        else if (this.gameObject.tag == Tags.PLAYER2) {
            GameObject obj = GameObject.FindGameObjectWithTag(Tags.PLAYER1);
            if (obj != null) {
                obj.GetComponent<FXHandler>().pistol.SetActive(true);
            }
        }

        //pistol.SetActive(true);
    }

    [PunRPC]
    public void mgEquipped() {
        //we need to set that the player has picked up the pistol
        //actually that he has it equipped
        mg.SetActive(true);
    }


}
