using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public bool offlineMode;
	public GameObject mainCam;
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
		mainCam.SetActive(false);
		//PhotonNetwork.Instantiate("Player", spawn.position, spawn.rotation, 0);//group id is for seperating things


		Transform location = spawn.getSpawnLocation(ObjectSpawner.SpawnTypes.player);

		//GameObject localPlayer = PhotonNetwork.Instantiate("First Person Controller", location.position, location.rotation, 0);//group id is for seperating things
		GameObject localPlayer = PhotonNetwork.Instantiate("Beta", location.position, location.rotation, 0);
		//we enable all parts here that have to do with each local player ie movement, and mouse scripts and main camera

		/*localPlayer.GetComponent<MouseLook>().enabled = true;//remove mosue movement
		((MonoBehaviour)localPlayer.GetComponent("FPSInputController")).enabled = true;//remove movement
		((MonoBehaviour)localPlayer.GetComponent("CharacterMotor")).enabled = true;//remove jitter while jumping
		localPlayer.transform.FindChild("Main Camera").gameObject.SetActive(true);//remove camera*/

		localPlayer.GetComponent<MouseLook>().enabled = true;//remove mosue movement
		localPlayer.GetComponent<PlayerScript>().enabled = true;//remove mosue movement
		localPlayer.GetComponent<Animator>().enabled = true;//remove mosue movement
		localPlayer.transform.FindChild("Main Camera").gameObject.SetActive(true);//remove camera
	}


}
