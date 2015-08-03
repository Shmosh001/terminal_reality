using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public bool offlineMode;
	public Camera mainCam;
	private ObjectSpawner spawn;

	// Use this for initialization
	void Start () {
		spawn = gameObject.GetComponent<ObjectSpawner>();
		ConnectToNetwork(offlineMode);
	}

	void ConnectToNetwork(bool offline){
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
		//PhotonNetwork.Instantiate("Player", spawn.position, spawn.rotation, 0);//group id is for seperating things


		Transform location = spawn.getSpawnLocation(ObjectSpawner.SpawnTypes.player);

		PhotonNetwork.Instantiate("First Person Controller", location.position, location.rotation, 0);//group id is for seperating things
	}


}
