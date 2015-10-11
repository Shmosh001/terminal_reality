using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {


    public bool player1Dead, player2Dead;



    void Update() {

        if (gameObject.GetComponent<GameManager>().singleplayer && player1Dead) {
            Application.LoadLevel("Thriller");
        }
        else if (!gameObject.GetComponent<GameManager>().singleplayer && player1Dead && player2Dead) {
            Application.LoadLevel("Thriller");
        }

    }

    
}
