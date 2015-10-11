using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;




    


    //boolean for offline mode
    public bool offlineMode;

    //temp spawning location
    public GameObject spawn1;
    public GameObject spawn2;

    public GameObject P1HUD;
    public GameObject P2HUD;

    //main stand by camera 
    public GameObject mainCam;

    private bool connecting = false;
    public bool level1Loaded = false;
    public bool level2Loaded = false;


    public AsyncOperation level2Load;


    void Start() {
        DontDestroyOnLoad(gameObject);
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

    }

    /// <summary>
    /// when we join a lobby
    /// </summary>
    void OnJoinedLobby() {
        Debug.Log("OnJoinedLobby");

        RoomInfo[] roomstuff = PhotonNetwork.GetRoomList();

        PhotonNetwork.JoinRandomRoom();


    }

    /// <summary>
    /// if random joining fails
    /// create a room and join on that room
    /// </summary>
    void OnPhotonRandomJoinFailed() {
        Debug.Log("OnPhotonRandomJoinFailed");
        PhotonNetwork.CreateRoom(null);

    }

    /// <summary>
    /// when we have joined a room
    /// we spawn a player as he has successfully joined the server
    /// </summary>
    void OnJoinedRoom() {
        Debug.Log("OnJoinedRoom");
        //SpawnPlayer();
        if (!level1Loaded) {
            SpawnPlayer();
            //loadLevel2();
        }
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
        if (level2Load != null && level2Load.isDone) {
            Debug.Log("level 2 is ready");
            startLevel2();
        }
        if (Application.loadedLevelName == "LevelTwo for andi") {
            spawnOnLevel2();
        }


        if (player1 == null) {
            player1 = GameObject.FindGameObjectWithTag(Tags.PLAYER1);

        }
        if (player2 == null) {
            player2 = GameObject.FindGameObjectWithTag(Tags.PLAYER2);

        }


        if (Input.GetKeyDown(KeyCode.P)) {
            loadLevel2();
        }


    }


    void spawnOnLevel2() {

        spawn1 = GameObject.FindGameObjectWithTag(Tags.SPAWN1);
        spawn2 = GameObject.FindGameObjectWithTag(Tags.SPAWN2);

        player1.transform.position = spawn1.transform.position;
        player1.transform.rotation = spawn1.transform.rotation;
        player2.transform.position = spawn2.transform.position;
        player2.transform.rotation = spawn2.transform.rotation;
    }


    IEnumerator startLevel2() {
        if (level2Loaded) {
            yield return level2Load;
        }
    }


    //loads level 2 in the background
    public void loadLevel2() {
        //level2Load = Application.LoadLevelAsync("LevelTwo for andi");
        Application.LoadLevel("LevelTwo for andi");
        //yield return level2Load;

        Debug.Log("level 2 set for loading");


    }


}
