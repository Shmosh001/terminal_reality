using UnityEngine;
using System.Collections;

/// <summary>
/// this class is assigned to a player and managed all the data that needs to be
/// synced across the network
/// it also receives all the data being synced
/// </summary>
public class NetworkEnemy : Photon.MonoBehaviour {

    //PUBLIC VARS
    public int sendRate = 20;
    public int sendRateOnSerialize = 10;
    public float lerpSmoothing = 5.0f;

    //PRIVATE VARS

    private Vector3 foreignPosition;
	private Quaternion foreignRotation;
	private Animator animator;

    //other scripts that have data that needs to be synced
    private ZombieFSM script;

    //used to first set the script vars before syncing
	private bool init = false;

	


    /// <summary>
    /// initialization
    /// </summary>
    void Start () {

		//animator = gameObject.GetComponent<Animator>();
		//PhotonNetwork.sendRate = 20;
		//PhotonNetwork.sendRateOnSerialize = 10;
		init = true;
	}
	
	/// <summary>
    /// updates position if this photon view is ours (local player)
    /// </summary>
	void Update () {
        //we lerp towards the received position to not teleport there immediately
        if (!photonView.isMine && init){
			transform.position = Vector3.Lerp(transform.position, foreignPosition, lerpSmoothing *  Time.deltaTime);
			transform.rotation = Quaternion.Lerp(transform.rotation, foreignRotation, lerpSmoothing * Time.deltaTime);
			//Mathf.Lerp(animator.GetFloat("Speed"), realspeed, 0.1f);//animation lerping
		}
	}

    /// <summary>
    /// gets called by photon and synces data
    /// </summary>
    /// <param name="stream">
    /// stream of data
    /// </param>
    /// <param name="info">
    /// info
    /// </param>
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		Debug.Log("OnPhotonSerializeView");
		//means we are sending local data
		if (init){

			if (stream.isWriting){
				sendData(stream,info);
				//need to send position, meaning our player
				//Debug.Log("own pos: " + transform.position + " at " + PhotonNetwork.time)
				//aniamtion sync
				//stream.SendNext(animator.GetFloat("Speed"));//provided we have speed
				//sends the speed so that the aniamotr gets updated with this value
			}
			//receiving other players things
			else{
				receiveData(stream,info);


				//receives speed vra for animation
				//animator.SetFloat("Speed",(float)stream.ReceiveNext());
				//might need to add Mathf.lerp to lerp this
			}
		}
	}

    /// <summary>
    /// Handles all the method calls to different scripts to send their data over the network
    /// NOTE: the order is important: needs to be the same as below in the receiving method
    /// </summary>
    /// <param name="stream">
    /// stream of data
    /// </param>
    /// <param name="info">
    /// info
    /// </param>
    void sendData(PhotonStream stream, PhotonMessageInfo info){
		Debug.Log("sendData");
		//POSITION AND ROTATION
		stream.SendNext(transform.position);
		stream.SendNext(transform.rotation);


	}

    /// <summary>
    /// handles all the received data that has been send over the network by calling the appropriate methods in other scripts
    /// NOTE: the order is important: needs to be the same as above in the sending method
    /// </summary>
    /// <param name="stream">
    /// stream of data
    /// </param>
    /// <param name="info">
    /// info
    /// </param>
    void receiveData(PhotonStream stream, PhotonMessageInfo info){
		Debug.Log("receiveData");
		//POSITION AND ROTATION
		foreignPosition = (Vector3)stream.ReceiveNext();
		foreignRotation = (Quaternion)stream.ReceiveNext();



	}



}
