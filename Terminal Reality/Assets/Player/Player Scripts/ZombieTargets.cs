using UnityEngine;
using System.Collections;

public class ZombieTargets : MonoBehaviour {



    private GameObject[] inRange;
    private GameObject[] outOfRange;
    private GameObject player1;
    private GameObject player2;


    public GameObject zombies;
    public int amount;


    private ArrayList indivZombies;


	// Use this for initialization
	void Start () {
	    for (int i = 0; i < amount; i++) {
            indivZombies.Add(zombies.transform.GetChild(i).gameObject);
        }

        player1 = GameObject.FindGameObjectWithTag(Tags.PLAYER1);
        if (!PhotonNetwork.offlineMode) {
            player2 = GameObject.FindGameObjectWithTag(Tags.PLAYER2);
        }

	}
	
    // Update is called once per frame
	void Update () {


        if (player1 == null) {
            player1 = GameObject.FindGameObjectWithTag(Tags.PLAYER1);
        }
        
        if (player2 == null && !PhotonNetwork.offlineMode) {
            player2 = GameObject.FindGameObjectWithTag(Tags.PLAYER2);
        }
    }




}
