using UnityEngine;
using System.Collections;

public class loadCredits : MonoBehaviour
{
    public GameObject script;
    	
	// Update is called once per frame
	void OnTriggerEnter(Collider other)
    {
	    if(other.tag == Tags.PLAYER1)
        {
            script.GetComponent<GameOver>().player1Done = true;
        }

	}
}
