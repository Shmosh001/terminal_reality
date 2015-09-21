using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DoorScript : MonoBehaviour {

	private Animator anim;
	public bool open = false;
	private Text pushE;

    private GameObject pushETextObj;

    // Use this for initialization
    void Start () {
	
		anim = this.GetComponent<Animator> ();

        pushETextObj = GameObject.FindGameObjectWithTag("PushEOpen");
        if (pushETextObj != null) {
            pushE = pushETextObj.GetComponent<Text>();
        }
       
	}

    // Update is called once per frame
    void Update() {
        if (pushETextObj == null) {
            pushETextObj = GameObject.FindGameObjectWithTag("PushEOpen");
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
			anim.SetTrigger("Close");
			open = false;
		}
		//IF THE DOOR IS CLOSED//
		else
		{
			anim.SetTrigger("Open");
			open = true;
		}
	}

	//WHEN SOMETHING ENTERS THE DOORS TRIGGER//
	void OnTriggerEnter (Collider other)
	{
		//IF A PLAYER ENTERS THE DOOR'S TRIGGER//
		if (other.tag == "Player")
		{
			if (!open) //Only show hint if the door is closed
			{
				pushE.enabled = true;
			}
		}

		//ENEMY OPEN DOOR WHEN THEY ENTER COLLIDER
		if (other.tag == "Enemy" && other.GetType() == typeof(CapsuleCollider))
		{
			if (!open)
			{
				anim.SetTrigger("Open");
				open = true;
			}
		}
	}
	
	//WHEN SOMETHING LEAVES THE DOOR'S TRIGGER//
	void OnTriggerExit (Collider other)
	{
		//IF A PLAYER LEAVES THE DOOR'S TRIGGER//
		if (other.tag == "Player")
		{
			pushE.enabled = false;
		}

		//ENEMY CLOSE DOOR WHEN THEY EXIT THE COLLIDER
		if (other.tag == "Enemy" && other.GetType() == typeof(CapsuleCollider))
		{
			if (open) //Only show hint if the door is closed
			{
				anim.SetTrigger("Close");
				open = false;
			}
		}
	}
}
