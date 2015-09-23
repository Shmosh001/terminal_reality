using UnityEngine;
using System.Collections;

public class loadLevel2 : MonoBehaviour {

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == Tags.PLAYER1 || col.gameObject.tag == Tags.PLAYER2) 
		{
            col.gameObject.GetComponent<PhotonView>().RPC("transitionToNext", PhotonTargets.AllBufferedViaServer);
		}

	}


    [PunRPC]
    public void transitionToNext() {
        Application.LoadLevel("Credits");
    }

}
