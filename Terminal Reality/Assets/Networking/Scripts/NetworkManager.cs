using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public bool offlineMode;
	public GameObject mainCam;
	private ObjectSpawner spawn;
	public GameObject hud;
	public GameObject temp;

	// Use this for initialization
	void Start () {
		spawn = gameObject.GetComponent<ObjectSpawner>();
		ConnectToNetwork(offlineMode);
	}

	void ConnectToNetwork(bool offline){
		PhotonNetwork.ConnectUsingSettings("v1.0");
		//PhotonNetwork.offlineMode = true;  we use this for single player
		Debug.Log("ConnectToNetwork");
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


//		Transform location = spawn.getSpawnLocation(ObjectSpawner.SpawnTypes.player);
		Transform location = temp.transform;
		//GameObject localPlayer = PhotonNetwork.Instantiate("First Person Controller", location.position, location.rotation, 0);//group id is for seperating things
		GameObject localPlayer = PhotonNetwork.Instantiate("NEWPLAYER", location.position, location.rotation, 0);
		//we enable all parts here that have to do with each local player ie movement, and mouse scripts and main camera
		enableComponents(localPlayer);



	}

	void enableComponents(GameObject localPlayer){
		Debug.Log("enableComponents");
		localPlayer.GetComponent<PlayerMovementScript>().enabled = true;//enable the movement script
		localPlayer.transform.FindChild("Main Camera").gameObject.SetActive(true);//enable camera again



		//localPlayer.GetComponent<ShootingScript>().enabled = true;//enable the shooting script
		//localPlayer.GetComponent<interactionScript>().enabled = true;//enable the interaction script
		//localPlayer.GetComponent<playerDataScript>().enabled = true;//enable the data script
		//localPlayer.GetComponent<playerHealthScript>().enabled = true;//enable the health script
		//localPlayer.GetComponent<weaponSwitchScript>().enabled = true;//enable the weapon script
		//hud.SetActive(true);


	}


}
