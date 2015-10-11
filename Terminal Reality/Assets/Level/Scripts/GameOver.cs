using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {



    public GameObject scripts;
    public bool player1Dead, player2Dead;



    void Update() {

        if (scripts.GetComponent<GameManager>().singleplayer && player1Dead) {
            Application.LoadLevel("Thriller");
        }
        else if (!scripts.GetComponent<GameManager>().singleplayer && player1Dead && player2Dead) {
            Application.LoadLevel("Thriller");
        }

    }

    
}
