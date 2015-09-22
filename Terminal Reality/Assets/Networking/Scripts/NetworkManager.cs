using UnityEngine;
using System;

/// <summary>
/// joins a single client into the server and spawns their player character as well as enabling all the local methods
/// </summary>
public class NetworkManager : MonoBehaviour {

    //PUBLIC VARS
    //array of enemies
    public GameObject[] parts;
    
    //boolean for offline mode
    public bool offlineMode;
    //the hud
    public GameObject P1HUD;
    public GameObject P2HUD;
    //temp spawning location
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject zombieSpawn;
    //main stand by camera 
    public GameObject mainCam;

    private bool connecting = false;

    /// <summary>
    /// initialization
    /// </summary>
    void Start () {

	}

    /// <summary>
    /// connects us to a network
    /// </summary>
    /// <param name="offline">
    /// true/false
    /// </param>
	void ConnectToNetwork(){
        Debug.Log("ConnectToNetwork");
        PhotonNetwork.ConnectUsingSettings("v1.0");
	}

    /// <summary>
    /// gui events and update
    /// </summary>
	void OnGUI(){
        //shows us the connection state top right corner
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

        if (!PhotonNetwork.connected && !connecting) {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Singleplayer Mode")) {
                Debug.Log("Singleplayer Mode");
                connecting = true;
                PhotonNetwork.offlineMode = true; //we use this for single player
                OnJoinedLobby();
            }
            if (GUILayout.Button("Multiplayer Mode")) {
                Debug.Log("Multiplayer Mode");
                connecting = true;
                ConnectToNetwork();
            }

            
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();

        }

	}

    /// <summary>
    /// when we join a lobby
    /// </summary>
	void OnJoinedLobby(){
		Debug.Log("OnJoinedLobby");

        RoomInfo[] roomstuff  = PhotonNetwork.GetRoomList();

        if (PhotonNetwork.isMasterClient) {
            PhotonNetwork.CreateRoom(null);
        }
        else {
            PhotonNetwork.JoinRandomRoom();
        }
        
        
        //PhotonNetwork.JoinRandomRoom();


    }

    /// <summary>
    /// if random joining fails
    /// create a room and join on that room
    /// </summary>
	void OnPhotonRandomJoinFailed(){
		Debug.Log("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom(null);
        
    }

    /// <summary>
    /// when we have joined a room
    /// we spawn a player as he has successfully joined the server
    /// </summary>
	void OnJoinedRoom(){
		Debug.Log("OnJoinedRoom");
		SpawnPlayer();
        //if (PhotonNetwork.isMasterClient) {
        //activateObjects();

       // }    

    }

    /// <summary>
    /// spawns a player into the game world using photons instantiate
    /// </summary>
    void SpawnPlayer(){
		Debug.Log("SpawnPlayer");
		mainCam.SetActive(false);
        //PhotonNetwork.Instantiate("Player", spawn.position, spawn.rotation, 0);//group id is for separating things
        Transform location;
        if (PhotonNetwork.isMasterClient) {
            location = spawn1.transform;
        }
        else {
            location = spawn2.transform;
        }

		//GameObject localPlayer = PhotonNetwork.Instantiate("First Person Controller", location.position, location.rotation, 0);//group id is for separating things
		GameObject localPlayer = PhotonNetwork.Instantiate("joseph", location.position, location.rotation, 0);
        if (PhotonNetwork.isMasterClient) {
            localPlayer.tag = Tags.PLAYER1;
        }
        else {
            localPlayer.tag = Tags.PLAYER2;
        }
        //we enable all parts here that have to do with each local player ie movement, and mouse scripts and main camera
        enableComponents(localPlayer);
        

    }

    /// <summary>
    /// enables all components which have sensitive information local to a player
    /// </summary>
    /// <param name="localPlayer">
    /// player
    /// </param>
    void enableComponents(GameObject localPlayer){
		Debug.Log("enableComponents");
		localPlayer.GetComponent<PlayerMovementScript>().enabled = true;//enable the movement script

        //rewrite
        GameObject mainCam = localPlayer.transform.FindChild("Main Camera").gameObject;
        mainCam.SetActive(true);//enable camera again


        //rewrite
        playerDataScript pData = localPlayer.GetComponent<playerDataScript>();
        pData.enabled = true;//enable the data script

        //assign pistol
        //pData.pistolGameObject = localPlayer.transform.FindChild("Pistol05").gameObject;
       // Transform obj = localPlayer.transform.FindChild("Pistol05");
        

        //assign machine gun 
       // pData.machineGunGameObject = localPlayer.transform.FindChild("UMP-45").gameObject;
        
        //check jtbs
        if (pData.pistolGameObject == null || pData.machineGunGameObject == null) {
            Debug.LogWarning("Gun objects have not been assigned successfully");
        }

        if (PhotonNetwork.isMasterClient) {
            P1HUD.SetActive(true);//enable the HUD
        }
        else {
            P2HUD.SetActive(true);//enable the HUD
        }

        //localPlayer.GetComponent<CapsuleCollider>().enabled = true;
        localPlayer.GetComponent<ShootingScript>().enabled = true;//enable the shooting script
		localPlayer.GetComponent<interactionScript>().enabled = true;//enable the interaction script	
		localPlayer.GetComponent<playerHealthScript>().enabled = true;//enable the health script
		localPlayer.GetComponent<weaponSwitchScript>().enabled = true;//enable the weapon script
		localPlayer.GetComponentInChildren<torchScript>().enabled = true;//enable the torch script
        
        
        //P1HUD.SetActive(true);

        //NOW NEED TO ASSIGN PARTS TO THE HUD ELEMENTS

    }


    void activateObjects() {
        for(int i  = 0; i < parts.Length; i++) {
            parts[i].SetActive(true);
        }
        //PhotonNetwork.InstantiateSceneObject("MALE_ZOMBIE", zombieSpawn.transform.position, zombieSpawn.transform.rotation, 0, null);
        
    }



}
