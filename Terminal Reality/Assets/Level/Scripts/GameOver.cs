using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {


    public bool player1Dead, player2Dead;
    public bool player1done;



    void Update() {

        if (gameObject.GetComponent<GameManager>().singleplayer && player1Dead) {
			writeToFile(0);
            Application.LoadLevel(0);
        }

        if(gameObject.GetComponent<GameManager>().singleplayer && player1done) {
            writeToFile(1);
            Application.LoadLevel(0);
        }
        
        
    }

	public void writeToFile(int condition)
	{
		string path = PlayerPrefs.GetString("filePath");
		string text = PlayerPrefs.GetString("StartTime");
		int kills = PlayerPrefs.GetInt("kills");
		int shots = PlayerPrefs.GetInt("shots");
		int hits = PlayerPrefs.GetInt("hits");

		text = text + "\r\nKills: " + kills;
		text = text + "\r\nShots: " + shots;
		text = text + "\r\nHits: " + hits;

        if (player1Dead) {
            text = text + "\r\nPlayer died.";
        }
        else {
            //Wrtie to file whether the player died or made it to the end of the level.
            switch (condition) {
                case 0:
                    text = text + "\r\nPlayer died.";
                    break;
                case 1:
                    text = text + "\r\nPlayer finished the level.";
                    break;
                case 2:
                    text = text + "\r\nPlayer Exited.";
                    break;
            }
        }

       

		//Add the end time to the file.
		text = text + "\r\nEnd time: " + System.DateTime.Now.ToString();
        //Debug.Log(path);
		System.IO.File.WriteAllText(path, text);
	}

    
}
