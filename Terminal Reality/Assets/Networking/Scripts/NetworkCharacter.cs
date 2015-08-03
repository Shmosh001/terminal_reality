using UnityEngine;
using System.Collections;

public class NetworkCharacter : Photon.MonoBehaviour {


	Vector3 foreignPosition;
	Quaternion foreignRotation;


	public float smoothing = 0.1f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!photonView.isMine){
			transform.position = Vector3.Lerp(transform.position, foreignPosition, smoothing);
			transform.rotation = Quaternion.Lerp(transform.rotation, foreignRotation, smoothing);
		}
	}


	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		if (stream.isWriting){
			//need to send position, meaning our player
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
		}
		else{
			//receiving other players things
			foreignPosition = (Vector3)stream.ReceiveNext();
			foreignRotation = (Quaternion)stream.ReceiveNext();
		}
	} 

}
