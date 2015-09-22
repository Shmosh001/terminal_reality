using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DoorScript : MonoBehaviour {

	private GameObject soundController;
	private Animator anim;
	public bool open = false;
	private Text pushE;

    private GameObject pushETextObj;

    // Use this for initialization
    void Start () {
		soundController = GameObject.FindGameObjectWithTag(Tags.SOUNDCONTROLLER);
		anim = this.GetComponent<Animator> ();

        pushETextObj = GameObject.FindGameObjectWithTag(Tags.PUSHEOPEN);
        if (pushETextObj != null) {
            pushE = pushETextObj.GetComponent<Text>();
        }
       
	}

    // Update is called once per frame
    void Update() {
        if (pushETextObj == null) {
            pushETextObj = GameObject.FindGameObjectWithTag(Tags.PUSHEOPEN);
            if (pushETextObj != null) {
                pushE = pushETextObj.GetComponent<Text>();
            }
        }
    }

    //WHEN THE PLAYER INTERACTS WITH THE DOOR//
    [PunRPC]
    public void interaction()
	{
		//IF THE DOOR IS OPEN//
		if (open)
		{
			//play sound of this component
			soundController.GetComponent<soundControllerScript> ().playDoorCreek (this.GetComponent<AudioSource>());
			anim.SetTrigger("Close");
			open = false;
		}
		//IF THE DOOR IS CLOSED//
		else
		{	
			//play sound of this component
			soundController.GetComponent<soundControllerScript> ().playDoorCreek (this.GetComponent<AudioSource>());
			anim.SetTrigger("Open");
			open = true;
		}
	}

	//WHEN SOMETHING ENTERS THE DOORS TRIGGER//
	void OnTriggerEnter (Collider other)
	{
		//IF A PLAYER ENTERS THE DOOR'S TRIGGER//
		if (other.tag == Tags.PLAYER1 || other.tag == Tags.PLAYER2)
		{
			if (!open) //Only show hint if the door is closed
			{
				pushE.enabled = true;
			}
		}

		//ENEMY OPEN DOOR WHEN THEY ENTER COLLIDER
		if ((other.tag == Tags.ENEMY || other.tag == Tags.BOSSENEMY) && other.GetType() == typeof(CapsuleCollider))
		{
			if (!open)
			{
				//play sound of this component
				soundController.GetComponent<soundControllerScript> ().playDoorCreek (this.GetComponent<AudioSource>());
				anim.SetTrigger("Open");
				open = true;
			}
		}
	}
	
	//WHEN SOMETHING LEAVES THE DOOR'S TRIGGER//
	void OnTriggerExit (Collider other)
	{
		//IF A PLAYER LEAVES THE DOOR'S TRIGGER//
		if (other.tag == Tags.PLAYER1 || other.tag == Tags.PLAYER2)
		{
			pushE.enabled = false;
		}

		//ENEMY CLOSE DOOR WHEN THEY EXIT THE COLLIDER
		if ((other.tag == Tags.ENEMY || other.tag == Tags.BOSSENEMY) && other.GetType() == typeof(CapsuleCollider))
		{
			if (open) //Only show hint if the door is closed
			{
				//play sound of this component
				soundController.GetComponent<soundControllerScript> ().playDoorCreek (this.GetComponent<AudioSource>());
				anim.SetTrigger("Close");
				open = false;
			}
		}
	}
}
