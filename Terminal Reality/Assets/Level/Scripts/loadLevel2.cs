﻿using UnityEngine;
using System.Collections;

public class loadLevel2 : MonoBehaviour {

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == Tags.PLAYER1 || col.gameObject.tag == Tags.PLAYER2) 
		{
            PhotonNetwork.Destroy(col.gameObject);
            Application.LoadLevel("LevelTwo");
            
            
        }

	}


    [PunRPC]
    public void transitionToNext() {
        
    }

}
