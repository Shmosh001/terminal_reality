using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBoxScript : MonoBehaviour {

	private Text pushE;
    private GameObject pushEObj;

	// Use this for initialization
	void Start () {

        pushEObj = GameObject.FindGameObjectWithTag(Tags.PUSHE);
	
	}
	


    void Update() {
        if (pushEObj == null) {
            pushEObj = GameObject.FindGameObjectWithTag(Tags.PUSHE);
            if (pushEObj != null) {
                pushE = pushEObj.GetComponent<Text>();
            }
        }
    }

    //WHEN SOMETHING ENTERS THE DOORS TRIGGER//
    void OnTriggerEnter (Collider other)
	{
        //IF A PLAYER ENTERS THE DOOR'S TRIGGER//
        if (other.tag == Tags.PLAYER1) {
            //pushE.enabled = true;
        }

        if (other.tag == Tags.PLAYER2) {
            //pushE.enabled = true;
        }
    }
	
	//WHEN SOMETHING LEAVES THE DORR'S TRIGGER//
	void OnTriggerExit (Collider other)
	{
        //IF A PLAYER LEAVES THE DOOR'S TRIGGER//
        if (other.tag == Tags.PLAYER1) { 
			//pushE.enabled = false;
		}

        if (other.tag == Tags.PLAYER2) {
            //pushE.enabled = false;
        }
    }

	//METHOD TO TURN OFF TEXT JUST BEFORE OBJECT IS DESTROYED
	public void turnOffText()
	{
        if (pushE != null) {
            pushE.enabled = false;
        }
	}

    [PunRPC]
    public void destroyObject() {
        turnOffText();
        Destroy(gameObject);
    }

}
