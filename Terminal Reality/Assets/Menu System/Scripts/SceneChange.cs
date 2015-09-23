using UnityEngine;
using System.Collections;

public class SceneChange : MonoBehaviour {


    public string nextLevel;
    public string previousLevel;


    public GameObject exitGO;
    public GameObject settingsGO;
    public GameObject instrGO;
    public GameObject creditsGO;
    public GameObject mmGO;
    public GameObject controlsGO;
    public GameObject htpGO;

    public GameObject current;

    void Start() {
        exitGO.SetActive(false);
        creditsGO.SetActive(false);
        instrGO.SetActive(false);
        settingsGO.SetActive(false);
        controlsGO.SetActive(false);
        htpGO.SetActive(false);


    }


    public void controlsScene() {
        controlsGO.SetActive(true);
        current.SetActive(false);
        current = controlsGO;
    }

    public void htpScene() {
        htpGO.SetActive(true);
        current.SetActive(false);
        current = htpGO;
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
        Application.LoadLevel("LevelOne with AI");
    }

    public void multiPlayer() {
        Application.LoadLevel("LevelOne with AI");
    }

    public void instructionsScene() {
        instrGO.SetActive(true);
        current.SetActive(false);
        current = instrGO;
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
