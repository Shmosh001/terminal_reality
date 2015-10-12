using UnityEngine;
using System.Collections;

public class SceneChange : MonoBehaviour {


    public string nextLevel;
    public string previousLevel;


    public GameObject exitGO;
    public GameObject creditsGO;
    public GameObject mmGO;
    public GameObject controlsGO;
    public GameObject xbox;
    public GameObject keyboard;


    public GameObject current;

    void Start() {
        exitGO.SetActive(false);
        creditsGO.SetActive(false);
        controlsGO.SetActive(false);
        xbox.SetActive(false);
        keyboard.SetActive(false);
    }

    public void keyboardScene() {
        keyboard.SetActive(true);
        current.SetActive(false);
        current = keyboard;
    }


    public void xboxScene() {
        xbox.SetActive(true);
        current.SetActive(false);
        current = xbox;
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
        Application.LoadLevel("Final Game");
    }

    public void multiPlayer() {
        Screen.showCursor = false;
        Application.LoadLevel("Final Game MP");
    }





    public void creditScene() {
        Application.LoadLevel("Thriller");
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
