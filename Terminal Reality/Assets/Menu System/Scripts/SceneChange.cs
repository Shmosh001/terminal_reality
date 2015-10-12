using UnityEngine;
using System.Collections;

public class SceneChange : MonoBehaviour {


    public string nextLevel;
    public string previousLevel;


    public GameObject exitGO;
    public GameObject settingsGO;

    public GameObject creditsGO;
    public GameObject mmGO;
    public GameObject controlsGO;

    public GameObject current;

    void Start() {
        exitGO.SetActive(false);
        creditsGO.SetActive(false);
        
        controlsGO.SetActive(false);


    }


    public void controlsScene() {
        controlsGO.SetActive(true);
        current.SetActive(false);
        current = controlsGO;
    }



    public void exitScene() {
        exitGO.SetActive(true);
        current.SetActive(false);
        current = exitGO;
    }

    public void mainMenu() {
        mmGO.SetActive(true);
        current.SetActive(false);
        current = mmGO;
    }

    public void singlePlayer() {
        Application.LoadLevel("Final Level SP");
    }

    public void multiPlayer() {
        Screen.showCursor = false;
        Application.LoadLevel("Final Level MP");
    }



    public void settingsScene() {
        settingsGO.SetActive(true);
        current.SetActive(false);
        current = settingsGO;
    }

    public void creditScene() {
        creditsGO.SetActive(true);
        current.SetActive(false);
        current = creditsGO;
    }




    void Update() {
        Screen.showCursor = true;
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }














    public void nextScene() {
        Application.LoadLevel(nextLevel);
    }

    public void previousScene() {
        Application.LoadLevel(previousLevel);
    }

    public void setPrevious(string name) {
        previousLevel = name;
    }

    public void setNext(string name) {
        nextLevel = name;
    }

    public void exit() {
        Application.Quit();
    }
}
