using UnityEngine;
using System.Collections;

public class PName : MonoBehaviour {

    private GameObject player1;
    private GameObject player2;
    private GameObject characterObject;
    private float windowWidth = 60;
    private float windowHeight = 40;
    private float offsetX = 20;
    private float offsetY = 20;


   

    void Start() {
        characterObject = this.gameObject;
       // Debug.LogWarning("start:" + gameObject.tag);
        assignPlayerTags();
        player1 = GameObject.FindGameObjectWithTag(Tags.PLAYER1);
        player2 = GameObject.FindGameObjectWithTag(Tags.PLAYER2);

    }


 

    /// <summary>
    /// we set the tags to be properly set on each local machine so that the AI can properly identify between players
    /// </summary>
    void assignPlayerTags() {
        if (!PhotonNetwork.offlineMode) {
            player1 = GameObject.FindGameObjectWithTag(Tags.PLAYER1);
            player2 = GameObject.FindGameObjectWithTag(Tags.PLAYER2);


            if (player1 == null) {
                GameObject player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
                player.tag = Tags.PLAYER1;

            }
            if (player2 == null) {
                GameObject player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
                if (player != null) {
                    player.tag = Tags.PLAYER2;
                }
            }
        }
        
    }



    /// <summary>
    /// this method draws the characters tag above his head
    /// </summary>
    void OnGUI() {
        if (characterObject != null) {



            if (PhotonNetwork.isMasterClient && this.gameObject.tag == Tags.PLAYER1) {
                //do nothing
            }
            else if (!PhotonNetwork.isMasterClient && this.gameObject.tag == Tags.PLAYER2) {
                //do nothing
            }
            else{
                    Vector3 characterPos = Camera.main.WorldToScreenPoint(characterObject.transform.position + new Vector3(0, 1.5f, 0));
                    
                    characterPos = new Vector3(Mathf.Clamp(characterPos.x, 0 + (windowWidth / 2), Screen.width - (windowWidth / 2)),
                                                    Mathf.Clamp(characterPos.y, 50, Screen.height),
                                                    characterPos.z);

                    GUILayout.BeginArea(new Rect((characterPos.x + offsetX) - (windowWidth / 2), (Screen.height - characterPos.y) + offsetY, windowWidth, windowHeight));

                    GUILayout.Label(gameObject.tag);

                    GUILayout.EndArea();
 
            }

            
        }
        
    }
}
