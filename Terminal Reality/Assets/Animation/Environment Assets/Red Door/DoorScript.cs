using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DoorScript : MonoBehaviour {

	private GameObject soundController;
    private AudioSource audioSource;
	private Animator anim;
	public bool open = false;
    public bool openFWD = false;
    private Text pushE;
    //field of view for detecting zombie
    private float FOV = 160f;

    private GameObject pushETextObj;

    // Use this for initialization
    void Start ()
    {
		soundController = GameObject.FindGameObjectWithTag(Tags.SOUNDCONTROLLER);
        audioSource = GetComponent<AudioSource>();
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
			//soundController.GetComponent<soundControllerScript> ().playDoorCreek (transform.position);
            audioSource.Play();
            anim.SetTrigger("Close");
			open = false;
		}
		//IF THE DOOR IS CLOSED//
		else
		{	
			//play sound of this component
			//soundController.GetComponent<soundControllerScript> ().playDoorCreek (transform.position);
            audioSource.Play();
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

                Vector3 direction = other.gameObject.transform.position - transform.position;
                //gets the angle between the player and the unit
                float angle = Vector3.Angle(direction, transform.forward);
                //Debug.Log(angle);
                //if the angle is smaller then we can see the target
                //now we need to check if anything is obstructing the view by raycasting
                if (angle < FOV / 2)
                {
                    Debug.LogWarning("inview");

                }

                //zombie leaving room
                else
                {
                    Debug.LogWarning("not inview");
                    //play sound of this component
                    //soundController.GetComponent<soundControllerScript>().playDoorCreek(transform.position);
                    audioSource.Play();
                    anim.SetTrigger("Open");
                    open = true;
                }
                
			}
		}
	}
	
	//WHEN SOMETHING LEAVES THE DOOR'S TRIGGER//
	void OnTriggerExit (Collider other)
	{
		//IF A PLAYER LEAVES THE DOOR'S TRIGGER//
		if (other.tag == Tags.PLAYER1)
		{
			pushE.enabled = false;
		}
        if (other.tag == Tags.PLAYER2) {
            pushE.enabled = false;
        }


        //ENEMY CLOSE DOOR WHEN THEY EXIT THE COLLIDER
        if ((other.tag == Tags.ENEMY || other.tag == Tags.BOSSENEMY) && other.GetType() == typeof(CapsuleCollider))
		{
			if (open) //Only show hint if the door is closed
			{
				//play sound of this component
				//soundController.GetComponent<soundControllerScript> ().playDoorCreek (transform.position);
                audioSource.Play();
				anim.SetTrigger("Close");
				open = false;
			}

            else if (openFWD)
            {
                //play sound of this component
                //soundController.GetComponent<soundControllerScript>().playDoorCreek(transform.position);
                audioSource.Play();
                anim.SetTrigger("Close");
                openFWD = false;
            }
		}
	}
}
