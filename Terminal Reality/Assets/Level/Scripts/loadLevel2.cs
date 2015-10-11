﻿using UnityEngine;
using System.Collections;

public class loadLevel2 : MonoBehaviour {



    public GameObject scripts;
    public bool player1Arrived, player2Arrived, loadReady;
    private float loadTimerD = 1, loadTimerC;


    void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == Tags.PLAYER1) {
            player1Arrived = true;           
            
        }

        if (col.gameObject.tag == Tags.PLAYER2) {
            player2Arrived = true;

        }



    }


    void Update() {
        if (player1Arrived && player2Arrived && !loadReady) {
            loadReady = true;
            loadTimerC = 0;
            scripts.GetComponent<LevelLoader>().loading = true;
        }

        loadTimerC += Time.deltaTime;
        if (loadReady && loadTimerC > loadTimerD) {
            scripts.GetComponent<LevelLoader>().spawnOnLevel2();
        }
    }

    

}
