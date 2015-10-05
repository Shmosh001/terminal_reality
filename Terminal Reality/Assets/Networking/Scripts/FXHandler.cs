using UnityEngine;
using System.Collections;

public class FXHandler : MonoBehaviour {


    public GameObject pistol;
    public GameObject mg;
    public GameObject torch;


    void Start() {
        pistol = this.gameObject.GetComponent<playerDataScript>().pistolGameObject;
        mg = this.gameObject.GetComponent<playerDataScript>().machineGunGameObject;
        torch = this.gameObject.GetComponent<playerDataScript>().torch2;



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


        Debug.LogWarning("Torch rpc called on " + gameObject.tag + " with mode " + mode);
        //off
        if (mode == 0) {
            Debug.LogWarning("mode 0 start: " + torch.light.enabled);
            /*if (torch == null) {
                Debug.LogWarning("torch obj is null");
            }*/
            GameObject.FindGameObjectWithTag(Tags.P2TORCH).GetComponent<Light>().enabled = false;
            //Debug.LogWarning("mode 0 done: " + torch.light.enabled);
            /*if (this.gameObject.tag == Tags.PLAYER1) {
                GameObject obj = GameObject.FindGameObjectWithTag(Tags.PLAYER2);
                if (obj != null) {
                    obj.GetComponent<FXHandler>().torch.SetActive(false);
                }
            }
            else if (this.gameObject.tag == Tags.PLAYER2) {
                GameObject obj = GameObject.FindGameObjectWithTag(Tags.PLAYER1);
                if (obj != null) {
                    obj.GetComponent<FXHandler>().torch.SetActive(false);
                }
            }*/
        }
        //on
        else{
            Debug.LogWarning("mode 1 start: " + torch.light.enabled);
            /*if (torch == null) {
                Debug.LogWarning("torch obj is null");
            }*/
            GameObject.FindGameObjectWithTag(Tags.P2TORCH).GetComponent<Light>().enabled = true;
            //Debug.LogWarning("mode 1 done: " + torch.light.enabled);
            /*if (this.gameObject.tag == Tags.PLAYER1) {
                GameObject obj = GameObject.FindGameObjectWithTag(Tags.PLAYER2);
                if (obj != null) {
                    obj.GetComponent<FXHandler>().torch.SetActive(true);
                }
            }
            else if (this.gameObject.tag == Tags.PLAYER2) {
                GameObject obj = GameObject.FindGameObjectWithTag(Tags.PLAYER1);
                if (obj != null) {
                    obj.GetComponent<FXHandler>().torch.SetActive(true);
                }
            }*/
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
