using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {


    public bool player1Dead, player2Dead;



    void Update() {

        if (gameObject.GetComponent<GameManager>().singleplayer && player1Dead) {

			writeToFile();

            Application.LoadLevel("Thriller");
        }
        else if (!gameObject.GetComponent<GameManager>().singleplayer && player1Dead && player2Dead) {
            Application.LoadLevel("Thriller");
        }

    }

	public void writeToFile()
	{
		string path = PlayerPrefs.GetString("filePath");
		string text = PlayerPrefs.GetString("StartTime");
		string how = PlayerPrefs.GetString("how");
		int kills = PlayerPrefs.GetInt("kills");
		int shots = PlayerPrefs.GetInt("shots");
		int hits = PlayerPrefs.GetInt("hits");

		text = text + "\r\n" + how;
		text = text + "\r\nKills: " + kills;
		text = text + "\r\nShots: " + shots;
		text = text + "\r\nHits: " + hits;

		

		//Add the end time to the file.
		text = text + "\r\nEnd time: " + System.DateTime.Now.ToString();
		
		System.IO.File.WriteAllText(path, text);
	}

    
}
