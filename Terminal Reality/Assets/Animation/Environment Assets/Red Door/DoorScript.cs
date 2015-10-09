using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DoorScript : MonoBehaviour {

	private AudioSource audioSource;
	private Animator anim;
    private bool playerFWD = false; 
    public bool open = false;
    public bool openFWD = false;
    public bool openBCK = false;
    private Text pushE;

    //field of view for detecting zombie
    private float FOV = 160f;

    private GameObject pushETextObj;

    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
		anim = this.GetComponent<Animator> ();

        pushETextObj = GameObject.FindGameObjectWithTag(Tags.PUSHEOPEN);
        if (pushETextObj != null)
        {
            pushE = pushETextObj.GetComponent<Text>();
        }
       
	}

    // Update is called once per frame
    void Update()
    {
        if (pushETextObj == null)
        {
            pushETextObj = GameObject.FindGameObjectWithTag(Tags.PUSHEOPEN);
            if (pushETextObj != null)
            {
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
            open = false;
            //IF THE DOOR IS OPEN forwards//
            if (openFWD)
            {
                //play sound of this component
                audioSource.Play();
                anim.SetTrigger("CloseFWD");
                openFWD = false;
            }

            //IF THE DOOR IS OPEN Backwards//
            else if (openBCK)
            {
                //play sound of this component
                audioSource.Play();
                anim.SetTrigger("CloseBCK");
                openBCK = false;
            }
        }

        //IF THE DOOR IS CLOSED//
        else
        {
            open = true;

            //IF PLAYER INSIDE ROOM//
            if (playerFWD) 
            {
                
                //play sound of this component
                audioSource.Play();
                anim.SetTrigger("OpenFWD");
                openFWD = true;
            }


            //IF PLAYER OUTSIDE ROOM//
            else
            {
                //play sound of this component
                audioSource.Play();
                anim.SetTrigger("OpenBCK");
                openBCK = true;
            }
            
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
                //this checks which side of the door the player is. If the player is in a room or not
                Vector3 direction = other.transform.position - transform.position;
                //gets the angle between the player and the unit
                float angle = Vector3.Angle(direction, transform.up);
                //Debug.Log(angle);
                //if the angle is smaller then we can see the target
                //now we need to check if anything is obstructing the view by raycasting
                if (angle < FOV / 2)
                {
                    playerFWD = true;
                }

                //player entering room
                else
                {
                    playerFWD = false;
                }
            }
		}

		//ENEMY OPEN DOOR WHEN THEY ENTER COLLIDER
		if ((other.tag == Tags.ENEMY || other.tag == Tags.BOSSENEMY) && other.GetType() == typeof(CapsuleCollider))
		{
			if (!openFWD)
			{

                Vector3 direction = other.gameObject.transform.position - transform.position;
                //gets the angle between the player and the unit
                float angle = Vector3.Angle(direction, transform.up);
                //Debug.Log(angle);
                //if the angle is smaller then we can see the target
                //now we need to check if anything is obstructing the view by raycasting
                if (angle < FOV / 2)
                {
                    //play sound of this component
                    audioSource.Play();
                    anim.SetTrigger("OpenFWD");
                    openFWD = true;
                }

                //zombie entering room
                else
                {
                    //play sound of this component
                    audioSource.Play();
                    anim.SetTrigger("OpenBCK");
                    openBCK = true;
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
			if (openFWD) //Only show hint if the door is closed
			{
				//play sound of this component
                audioSource.Play();
				anim.SetTrigger("CloseFWD");
                openFWD = false;
			}

            else if (openBCK)
            {
                //play sound of this component
                audioSource.Play();
                anim.SetTrigger("CloseBCK");
                openBCK = false;
            }
		}
	}
}
