using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class weaponOnMapScript : MonoBehaviour {

	private Text pushE;

    private GameObject pushETextObj;

    // Use this for initialization
    void Start() {
        pushETextObj = GameObject.FindGameObjectWithTag("PushE");
        if (pushETextObj != null) {
            pushE = pushETextObj.GetComponent<Text>();
        }


    }

    // Update is called once per frame
    void Update() {
        if (pushETextObj == null) {
            pushETextObj = GameObject.FindGameObjectWithTag("PushE");
            if (pushETextObj != null) {
                pushE = pushETextObj.GetComponent<Text>();
            }
        }
    }

    //WALKING INTO THE TRIGGER SURROUNDING WEAPON//
    void OnTriggerEnter(Collider other)
	{
		//If player walks into trigger...
		if (other.tag == "Player")
		{
			pushE.enabled = true;

		}
	}

	//WALKING OUT OF THE TRIGGER SURROUNDING WEAPON//
	void OnTriggerExit(Collider other)
	{
		//If player walks into trigger...
		if (other.tag == "Player")
		{
			pushE.enabled = false;
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
