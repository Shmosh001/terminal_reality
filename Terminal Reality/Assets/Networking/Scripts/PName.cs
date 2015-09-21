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
        print("start:" + gameObject.tag);
        if (gameObject.tag != Tags.PLAYER1) {
            Debug.LogWarning("chnanging tag from " + gameObject.tag + " to " + Tags.PLAYER2);
            this.gameObject.tag = Tags.PLAYER2;
        }
       
        if (gameObject.tag != Tags.PLAYER2) {
            Debug.LogWarning("chnanging tag from " + gameObject.tag + " to " + Tags.PLAYER1);
            this.gameObject.tag = Tags.PLAYER1;
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
