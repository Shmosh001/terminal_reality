using UnityEngine;
using System.Collections;

public class loadLevel2 : MonoBehaviour {



    public GameObject scripts;
    public GameManager manager;
    public bool player1Arrived, player2Arrived, loadReady;
    private float loadTimerD = 1, loadTimerC;


    void Start()
    {
        manager = scripts.GetComponent<GameManager>();
    }

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
        if (manager.singleplayer && player1Arrived ) {
            scripts.GetComponent<LevelLoader>().loadLevel2();
        }

        
        if (!manager.singleplayer && player1Arrived && player2Arrived) {
            scripts.GetComponent<LevelLoader>().spawnOnLevel2();
        }
    }

    

}
