using UnityEngine;
using System.Collections;

public class SceneChange : MonoBehaviour {


    public string nextLevel;
    public string previousLevel;


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
