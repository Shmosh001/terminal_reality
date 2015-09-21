using UnityEngine;
using System.Collections;

public class PName : MonoBehaviour {


    private GameObject characterObject;
    private float windowWidth = 60;
    private float windowHeight = 40;
    private float offsetX = 20;
    private float offsetY = 20;


    void Start() {
        characterObject = this.gameObject;
        Debug.LogWarning("start:" + gameObject.tag);
        assignPlayerTags();
        


        /*if (player1 == null ) {
            Debug.LogWarning("chnanging tag from " + gameObject.tag + " to " + Tags.PLAYER2);
            gameObject.tag = Tags.PLAYER2;
        }
        else if (player2 == null) {
            gameObject.tag = Tags.PLAYER1;
            Debug.LogWarning("chnanging tag from " + gameObject.tag + " to " + Tags.PLAYER1);
        }*/



       /* if (gameObject.tag != Tags.PLAYER1) {
            Debug.LogWarning("chnanging tag from " + gameObject.tag + " to " + Tags.PLAYER2);
            this.gameObject.tag = Tags.PLAYER2;
        }
       
        if (gameObject.tag != Tags.PLAYER2) {
            Debug.LogWarning("chnanging tag from " + gameObject.tag + " to " + Tags.PLAYER1);
            this.gameObject.tag = Tags.PLAYER1;
        }
        */
        
    }


    void assignPlayerTags() {
        GameObject player1 = GameObject.FindGameObjectWithTag(Tags.PLAYER1);
        GameObject player2 = GameObject.FindGameObjectWithTag(Tags.PLAYER2);


        if (player1 == null) {
            GameObject player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
            player.tag = Tags.PLAYER1;

        }
        if (player2 == null) {
            GameObject player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
            player.tag = Tags.PLAYER2;
        }
    }


    void Update() {
        if (!PhotonNetwork.offlineMode) {

        }
    }


    void OnGUI() {
        if (characterObject != null) {

            Vector3 characterPos = Camera.main.WorldToScreenPoint(characterObject.transform.position + new Vector3(0,1.5f,0));
            characterPos = new Vector3(Mathf.Clamp(characterPos.x, 0 + (windowWidth / 2), Screen.width - (windowWidth / 2)),
                                               Mathf.Clamp(characterPos.y, 50, Screen.height),
                                               characterPos.z);
            GUILayout.BeginArea(new Rect((characterPos.x + offsetX) - (windowWidth / 2), (Screen.height - characterPos.y) + offsetY, windowWidth, windowHeight));
            // GUI CODE GOES HERE
            GUILayout.Label(gameObject.tag);

            GUILayout.EndArea();
        }
        
    }
}
