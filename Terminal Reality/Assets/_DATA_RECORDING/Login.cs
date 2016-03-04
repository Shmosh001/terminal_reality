using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Login : MonoBehaviour {

	public playerDataScript playerData;

    public InputField inField;
    public Text infieldContent;
    public Text heading;

    public Button okBtn;

    public string userName;

    public string fileName;
    public string path;
    public string endPath = "_data_file.txt";


    private string finalPath;



    void Start() {
		playerData = this.GetComponent<playerDataScript>();	
		
        path = Application.dataPath + "/" + "_DATA_RECORDING/";
        okBtn.onClick.AddListener(() => { okClick(inField.text); });
    }


   

    
    public void okClick(string name) {
        if (!(name == "")) {
            Debug.Log("Creating instance for user: " + name);
            userName = name;
            createFile();
            Screen.showCursor = false;
            Application.LoadLevel("Final Game");

        }
        else {
            Debug.LogError("Enter valid username");
        }

    }


    public void createFile() {
        Debug.Log("Writing file to " + path + userName + endPath);

        string text = System.DateTime.Now.ToString() + "\r\nKills: " + playerData.kills;
        finalPath = path + userName + endPath;

        System.IO.File.WriteAllText(finalPath, text);
    }



    public string getFinalPath() {
        return finalPath;
    }

}
