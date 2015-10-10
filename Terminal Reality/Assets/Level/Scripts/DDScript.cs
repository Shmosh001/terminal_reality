using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DDScript : MonoBehaviour {

    private GameObject soundController;
    private Animator anim;
	private bool open;
	//private Text pushE;
	private Text needKey;
    //private GameObject pushEObj;
	private GameObject needKeyObj;

	// Use this for initialization
	void Start () 
	{
        soundController = GameObject.FindGameObjectWithTag(Tags.SOUNDCONTROLLER);
        anim = gameObject.GetComponent<Animator>();
		open = false;
		//pushEObj = GameObject.FindGameObjectWithTag(Tags.PUSHEOPEN);
		needKeyObj = GameObject.FindGameObjectWithTag(Tags.KEYTOOPEN);
		
	}


    //WHEN THE PLAYER INTERACTS WITH THE DOOR//
    [PunRPC]
    public void interaction()
	{
		//IF THE DOOR IS CLOSED//
		if(!open)
		{
            //play sound of this component
            //soundController.GetComponent<soundControllerScript>().playDoorCreek(transform.position);
            anim.SetTrigger("OpenFWD");
			open = true;


		}
	}

	//WHEN SOMETHING ENTERS THE DOORS TRIGGER//
	void OnTriggerEnter (Collider other)
	{
		//IF A PLAYER ENTERS THE DOOR'S TRIGGER//
		if (other.tag == Tags.PLAYER1)
		{
			if (!open) //Only show hint if the door is closed
			{
				//if the player has a key - show "Push E"
				if (other.GetComponentInParent<playerDataScript>().hasKey)
				{
					//pushE.enabled = true;
				}
				//if the player does not have a key - show "Need Key"
				else{
					needKey.enabled = true;
				}
			}
		}

        if (other.tag == Tags.PLAYER2) {
            if (!open) //Only show hint if the door is closed
            {
				//if the player has a key - show "Push E"
				if (other.GetComponentInParent<playerDataScript>().hasKey)
				{
					//pushE.enabled = true;
				}
				//if the player does not have a key - show "Need Key"
				else{
					needKey.enabled = true;
				}
            }
        }

    }
	
	//WHEN SOMETHING LEAVES THE DOOR'S TRIGGER//
	void OnTriggerExit (Collider other)
	{
		//IF A PLAYER LEAVES THE DOOR'S TRIGGER//
		if (other.tag == Tags.PLAYER1 || other.tag == Tags.PLAYER2)
		{
			//pushE.enabled = false;
			needKey.enabled = false;
		}

	}



    void Update() {
        /*if (pushEObj == null) {
            pushEObj = GameObject.FindGameObjectWithTag(Tags.PUSHEOPEN);
            if (pushEObj != null) {
                pushE = pushEObj.GetComponent<Text>();
            }
        }*/
		if (needKeyObj == null) {
			needKeyObj = GameObject.FindGameObjectWithTag(Tags.KEYTOOPEN);
			if (needKeyObj != null) {
				needKey = needKeyObj.GetComponent<Text>();
			}
		}
    }

}
