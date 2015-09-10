using UnityEngine;
using System.Collections;

public class NetworkCharacter : Photon.MonoBehaviour {


	Vector3 foreignPosition;
	Quaternion foreignRotation;
	Animator animator;

	public float smoothing = 5.0f;


	// Use this for initialization
	void Start () {
		//animator = gameObject.GetComponent<Animator>();
		//TODO should be fine
		//PhotonNetwork.sendRate = 20;
		//PhotonNetwork.sendRateOnSerialize = 10;
	}
	
	// Update is called once per frame
	void Update () {
		if (!photonView.isMine){
			transform.position = Vector3.Lerp(transform.position, foreignPosition, smoothing *  Time.deltaTime);
			transform.rotation = Quaternion.Lerp(transform.rotation, foreignRotation, smoothing * Time.deltaTime);
			//Mathf.Lerp(animator.GetFloat("Speed"), realspeed, 0.1f);//aniamtion lerping
		}
	}


	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		if (stream.isWriting){
			//need to send position, meaning our player
			//Debug.Log("own pos: " + transform.position + " at " + PhotonNetwork.time);
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
			//aniamtion sync
			//stream.SendNext(animator.GetFloat("Speed"));//provided we have speed
			//sends the speed so that the aniamotr gets updated with this value
		}
		else{
			//receiving other players things

			foreignPosition = (Vector3)stream.ReceiveNext();
			//Debug.Log("received pos: " + foreignPosition + " at " + PhotonNetwork.time);

			foreignRotation = (Quaternion)stream.ReceiveNext();
			//receives speed vra for animation
			//animator.SetFloat("Speed",(float)stream.ReceiveNext());
			//might need to add Mathf.lerp to lerp this
		}
	} 

}
