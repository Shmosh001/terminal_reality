using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AmmoBoxScript : MonoBehaviour {

	
	//PUBLIC AMMO BOX VARIABLES//
	public int pistolAmmo;
	public int machineGunAmmo;
	
	
	private bool playerInRange;
    
	//WHEN SOMETHING ENTERS THE DOORS TRIGGER//
	void OnTriggerEnter (Collider other)
	{
        //IF A PLAYER ENTERS THE DOOR'S TRIGGER//
        if (other.tag == Tags.PLAYER1) {
			playerInRange = true;
			//pushE.enabled = true;
			
		}
        if (other.tag == Tags.PLAYER2) {
            playerInRange = true;

        }
    }
	
	//WHEN SOMETHING LEAVES THE DORR'S TRIGGER//
	void OnTriggerExit (Collider other)
	{
        //IF A PLAYER LEAVES THE DOOR'S TRIGGER//
        if (other.tag == Tags.PLAYER1) {
			playerInRange = false;
		}

        if (other.tag == Tags.PLAYER2) {
            playerInRange = false;
        }
    }


	
}
