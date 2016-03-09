using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Login : MonoBehaviour {


    public InputField inField;
    public Text infieldContent;
    public Text heading;

    public Button okBtn;

    public string userName;

    public string fileName;
    public string path;
    public string endPath = "_data_file.txt";


    private string finalPath;


	void Awake()
	{
		DontDestroyOnLoad(this);
	}

    void Start() {	
		
        path = Application.dataPath + "/" + "_DATA_RECORDING/";
        System.IO.Directory.CreateDirectory(path);
        okBtn.onClick.AddListener(() => { okClick(inField.text); });
    }


   

    
    public void okClick(string name) {
        if (!(name == "")) {
            Debug.Log("Creating instance for user: " + name);
            userName = name;
            createFile();
            Screen.showCursor = false;
            Application.LoadLevel(2);

        }
        else {
            Debug.LogError("Enter valid username");
        }

    }


    public void createFile() {
        //Debug.Log("Writing file to " + path + userName + endPath);

        string text = "Start Time: " + System.DateTime.Now.ToString();
        finalPath = path + userName + endPath;
		PlayerPrefs.SetString("filePath", finalPath);
		PlayerPrefs.SetString ("StartTime", text);
		PlayerPrefs.SetInt("kills", 0);
		PlayerPrefs.SetInt("shots", 0);
		PlayerPrefs.SetInt("hits", 0);
        //System.IO.File.WriteAllText(finalPath, text);
    }



    public string getFinalPath() {
        return finalPath;
    }

}
