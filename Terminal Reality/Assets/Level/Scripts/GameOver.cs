using UnityEngine;
using System.Collections;

public class GameOver : Photon.MonoBehaviour {




    public bool loadReady, player1Dead, player2Dead;
    private float loadTimerD = 1, loadTimerC;


    void Update() {




        if (player1Dead && player2Dead && !loadReady) {
            loadReady = true;
            loadTimerC = 0;
            gameObject.GetComponent<PhotonView>().RPC("transition", PhotonTargets.Others);
        }

        loadTimerC += Time.deltaTime;
        if (loadReady && loadTimerC > loadTimerD) {
            Application.LoadLevel("Thriller");
            PhotonNetwork.Disconnect();
        }
    }





    void OnGUI() {

        if (loadReady) {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Game Over...");
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }

    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        
        if (stream.isWriting) {
            stream.SendNext(player1Dead);
            stream.SendNext(player2Dead);
        }
        //receiving other players things
        else {
            player1Dead = (bool)stream.ReceiveNext();
            player2Dead = (bool)stream.ReceiveNext();
        }

    }

    [PunRPC]
    public void transition() {
        player1Dead = true;
        player2Dead = true;
    }


}
