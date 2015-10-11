using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;
    public GameObject scripts;


    public GameObject level1;
    public GameObject level2;
    


    //boolean for offline mode
    public bool offlineMode;

    //temp spawning location
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;
    public GameObject spawn4;



    public bool loading = false;




   
    /// <summary>
    /// gui events and update
    /// </summary>
    void OnGUI() {
        
        if (loading) {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Loading...");
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }

    }

    

    public void loadLevel2() {
        loading = true;
        spawnIntermediate();
        spawnOnLevel2();
        loading = false;
    }


    public void spawnIntermediate() {

        player1.transform.position = spawn1.transform.position;
        player1.transform.rotation = spawn1.transform.rotation;

        if (!scripts.GetComponent<GameManager>().singleplayer) {
            player2.transform.position = spawn2.transform.position;
            player2.transform.rotation = spawn2.transform.rotation;
        }
    }



    public void spawnOnLevel2() {

        level1.SetActive(false);
        level2.SetActive(true);
        player1.transform.position = spawn3.transform.position;
        player1.transform.rotation = spawn3.transform.rotation;
        
        if (!scripts.GetComponent<GameManager>().singleplayer) {
            player2.transform.position = spawn4.transform.position;
            player2.transform.rotation = spawn4.transform.rotation;
        }

    }





}
