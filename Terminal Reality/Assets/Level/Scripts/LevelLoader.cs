using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;



    public GameObject level1;
    public GameObject level2;
    


    //boolean for offline mode
    public bool offlineMode;

    //temp spawning location
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;
    public GameObject spawn4;

    public GameObject P1HUD;
    public GameObject P2HUD;

    //main stand by camera 
    public GameObject mainCam;

    private bool connecting = false;
    public bool level1Loaded = true;
    public bool level2Loaded = false;
    public bool loading = false;




    void Start() {
        DontDestroyOnLoad(gameObject);
        level1Loaded = true;
    }



    /// <summary>
    /// connects us to a network
    /// </summary>
    /// <param name="offline">
    /// true/false
    /// </param>
    void ConnectToNetwork() {
        Debug.Log("ConnectToNetwork");
        PhotonNetwork.ConnectUsingSettings("v1.0");
    }

    /// <summary>
    /// gui events and update
    /// </summary>
    void OnGUI() {
        //shows us the connection state top right corner
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

        if (!PhotonNetwork.connected && !connecting) {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            /*if (GUILayout.Button("Singleplayer Mode")) {
                Debug.Log("Singleplayer Mode");
                connecting = true;
                PhotonNetwork.offlineMode = true; //we use this for single player
                OnJoinedLobby();
            }*/
            if (GUILayout.Button("Start Game")) {
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
        if (loading) {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Loading...");
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
    void OnJoinedLobby() {
        Debug.Log("OnJoinedLobby");

        RoomInfo[] roomstuff = PhotonNetwork.GetRoomList();

        //PhotonNetwork.JoinRandomRoom();
        PhotonNetwork.JoinRoom("Andi");

    }


    void OnPhotonJoinRoomFailed() {
        PhotonNetwork.CreateRoom("Andi");
    }


    /// <summary>
    /// if random joining fails
    /// create a room and join on that room
    /// </summary>
    void OnPhotonRandomJoinFailed() {
        Debug.Log("OnPhotonRandomJoinFailed");
        

    }

    /// <summary>
    /// when we have joined a room
    /// we spawn a player as he has successfully joined the server
    /// </summary>
    void OnJoinedRoom() {
        Debug.Log("OnJoinedRoom");
        //SpawnPlayer();
        
        SpawnPlayer();
        
        
    }

    /// <summary>
    /// spawns a player into the game world using photons instantiate
    /// </summary>
    void SpawnPlayer() {
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
        GameObject localPlayer = PhotonNetwork.Instantiate("joseph 1", location.position, location.rotation, 0);
        if (PhotonNetwork.isMasterClient) {
            this.player1 = localPlayer;
            localPlayer.tag = Tags.PLAYER1;
        }
        else {
            this.player2 = localPlayer;
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
    void enableComponents(GameObject localPlayer) {
        Debug.Log("enableComponents");
        localPlayer.GetComponent<PlayerMovementScript>().enabled = true;//enable the movement script

        //rewrite
        GameObject mainCam = localPlayer.transform.FindChild("Main Camera").gameObject;
        mainCam.SetActive(true);//enable camera again


        //rewrite
        playerDataScript pData = localPlayer.GetComponent<playerDataScript>();
        pData.enabled = true;//enable the data script

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
        localPlayer.GetComponent<CharacterController>().enabled = true;
        localPlayer.GetComponent<ShootingScript>().enabled = true;//enable the shooting script
        localPlayer.GetComponent<interactionScript>().enabled = true;//enable the interaction script	
        localPlayer.GetComponent<playerHealthScript>().enabled = true;//enable the health script
        localPlayer.GetComponent<weaponSwitchScript>().enabled = true;//enable the weapon script
        localPlayer.GetComponent<EscKeyListener>().enabled = true;//enable the weapon script
        localPlayer.GetComponentInChildren<torchScript>().enabled = true;//enable the torch script
        pData.torch2.SetActive(false);


    }


    /* void activateObjects() {
        for (int i = 0; i < parts.Length; i++) {
            parts[i].SetActive(true);
        }
        //PhotonNetwork.InstantiateSceneObject("MALE_ZOMBIE", zombieSpawn.transform.position, zombieSpawn.transform.rotation, 0, null);

    }
    */

    void Update() {
        


        if (player1 == null) {
            player1 = GameObject.FindGameObjectWithTag(Tags.PLAYER1);

        }
        if (player2 == null) {
            player2 = GameObject.FindGameObjectWithTag(Tags.PLAYER2);

        }


        if (Input.GetKeyDown(KeyCode.P)) {
            spawnOnLevel2();
        }


    }


    public void spawnOnLevel2() {

        
        level1.SetActive(false);
        level2.SetActive(true);
        level1Loaded = false;
        
        player1.transform.position = spawn3.transform.position;
        player1.transform.rotation = spawn3.transform.rotation;
        if (player2 == null) {
            return;
        }
        player2.transform.position = spawn4.transform.position;
        player2.transform.rotation = spawn4.transform.rotation;
        loading = false;
    }





}
