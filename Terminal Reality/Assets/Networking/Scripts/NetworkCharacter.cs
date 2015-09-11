using UnityEngine;
using System.Collections;

public class NetworkCharacter : Photon.MonoBehaviour {


	private Vector3 foreignPosition;
	private Quaternion foreignRotation;
	private Animator animator;

	private playerDataScript playerData;
	private torchScript torch;

	private bool init = false;

	//lerping smoothing factor
	public float smoothing = 5.0f;


	// Use this for initialization
	void Start () {
		playerData = gameObject.GetComponent<playerDataScript>();
		Debug.LogWarning(playerData);
		torch = gameObject.GetComponentInChildren<torchScript>();
		Debug.LogWarning(torch);
		//animator = gameObject.GetComponent<Animator>();
		//TODO should be fine
		//PhotonNetwork.sendRate = 20;
		//PhotonNetwork.sendRateOnSerialize = 10;
		init = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (!photonView.isMine && init){
			transform.position = Vector3.Lerp(transform.position, foreignPosition, smoothing *  Time.deltaTime);
			transform.rotation = Quaternion.Lerp(transform.rotation, foreignRotation, smoothing * Time.deltaTime);
			//Mathf.Lerp(animator.GetFloat("Speed"), realspeed, 0.1f);//aniamtion lerping
		}
	}


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

	//Handles all the method calls to different scripts to send their data over the network
	//NOTE: the order is important: needs to be the same as below in the receiving method
	void sendData(PhotonStream stream, PhotonMessageInfo info){
		Debug.Log("sendData");
		//POSITION AND ROTATION
		stream.SendNext(transform.position);
		stream.SendNext(transform.rotation);

		//PLAYER DATA
		playerData.sendNetworkData(stream,info);

		//FLASHLIGHT
		torch.sendNetworkData(stream,info);
	}

	//handles all the received data that has been send over the network by calling the approriate methods in other scripts
	//NOTE: the order is important: needs to be the same as above in the sending method
	void receiveData(PhotonStream stream, PhotonMessageInfo info){
		Debug.Log("receiveData");
		//POSITION AND ROTATION
		foreignPosition = (Vector3)stream.ReceiveNext();
		foreignRotation = (Quaternion)stream.ReceiveNext();


		//PLAYER DATA
		playerData.receiveNetworkData(stream,info);

		//FLASHLIGHT
		torch.receiveNetworkData(stream,info);
	}



}
