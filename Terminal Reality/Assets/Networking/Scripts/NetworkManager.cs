using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {


	public Camera mainCam;
	public Transform spawn;

	// Use this for initialization
	void Start () {
		Connect();
	}

	void Connect(){
		PhotonNetwork.ConnectUsingSettings("v1.0");
		//PhotonNetwork.offlineMode = true;  we use this for single player
	}

	void OnGUI(){
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}

	void OnJoinedLobby(){
		Debug.Log("OnJoinedLobby");
		PhotonNetwork.JoinRandomRoom();
	}

	void OnPhotonRandomJoinFailed(){
		Debug.Log("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom(null);
	}

	void OnJoinedRoom(){
		Debug.Log("OnJoinedRoom");
		SpawnPlayer();
	}

	void SpawnPlayer(){
		Debug.Log("SpawnPlayer");
		mainCam.enabled = false;
		PhotonNetwork.Instantiate("Player", spawn.position, spawn.rotation, 0);//group id is for seperating things
	}


}
