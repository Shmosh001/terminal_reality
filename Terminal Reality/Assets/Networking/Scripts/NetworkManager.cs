using UnityEngine;
using System.Collections;


/// <summary>
/// joins a single client into the server and spawns their player character as well as enabling all the local methods
/// </summary>
public class NetworkManager : MonoBehaviour {


    //PUBLIC VARS
    public bool offlineMode;
    //the hud
    public GameObject hud;
    //temp spawning location
    public GameObject temp;
    public GameObject zombieSpawn;
    //main stand by camera 
    public GameObject mainCam;


    /// <summary>
    /// initialization
    /// </summary>
    void Start () {
		ConnectToNetwork();
	}

    /// <summary>
    /// connects us to a network
    /// </summary>
    /// <param name="offline">
    /// true/false
    /// </param>
	void ConnectToNetwork(){
		PhotonNetwork.ConnectUsingSettings("v1.0");
		if (offlineMode) {
            PhotonNetwork.offlineMode = true; //we use this for single player
        }
		Debug.Log("ConnectToNetwork");
	}

    /// <summary>
    /// gui events and update
    /// </summary>
	void OnGUI(){
        //shows us the connection state top right corner
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}

    /// <summary>
    /// when we join a lobby
    /// </summary>
	void OnJoinedLobby(){
		Debug.Log("OnJoinedLobby");
		PhotonNetwork.JoinRandomRoom();
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
        if (PhotonNetwork.isMasterClient) {
            //PhotonNetwork.InstantiateSceneObject()
            //PhotonNetwork.InstantiateSceneObject("MALE_ZOMBIE", zombieSpawn.transform.position, zombieSpawn.transform.rotation, 0, new object[0]);

        }
        
	}

    /// <summary>
    /// spawns a player into the game world using photons instantiate
    /// </summary>
    void SpawnPlayer(){
		Debug.Log("SpawnPlayer");
		mainCam.SetActive(false);
		//PhotonNetwork.Instantiate("Player", spawn.position, spawn.rotation, 0);//group id is for separating things

		Transform location = temp.transform;
		//GameObject localPlayer = PhotonNetwork.Instantiate("First Person Controller", location.position, location.rotation, 0);//group id is for separating things
		GameObject localPlayer = PhotonNetwork.Instantiate("NEWPLAYER", location.position, location.rotation, 0);
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
        pData.pistolGameObject = mainCam.transform.FindChild("Pistol05").gameObject;

        //assign machine gun 
        pData.machineGunGameObject = mainCam.transform.FindChild("UMP-45").gameObject;
        
        //check jtbs
        if (pData.pistolGameObject == null || pData.machineGunGameObject == null) {
            Debug.LogWarning("Gun objects have not been assigned successfully");
        }




        localPlayer.GetComponent<ShootingScript>().enabled = true;//enable the shooting script
		localPlayer.GetComponent<interactionScript>().enabled = true;//enable the interaction script	
		localPlayer.GetComponent<playerHealthScript>().enabled = true;//enable the health script
		localPlayer.GetComponent<weaponSwitchScript>().enabled = true;//enable the weapon script
		localPlayer.GetComponentInChildren<torchScript>().enabled = true;//enable the torch script
		localPlayer.GetComponent<NetworkCharacter>().enabled = true;
		hud.SetActive(true);//enable the HUD


	}


}
