using UnityEngine;
using System.Collections;

public class NetworkCharacter : Photon.MonoBehaviour {


	Vector3 foreignPosition;
	Quaternion foreignRotation;
	Animator animator;

	public float smoothing = 0.1f;


	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!photonView.isMine){
			transform.position = Vector3.Lerp(transform.position, foreignPosition, smoothing);
			transform.rotation = Quaternion.Lerp(transform.rotation, foreignRotation, smoothing);
			//Mathf.Lerp(animator.GetFloat("Speed"), realspeed, 0.1f);//aniamtion lerping
		}
	}


	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		if (stream.isWriting){
			//need to send position, meaning our player
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
			//aniamtion sync
			stream.SendNext(animator.GetFloat("Speed"));//provided we have speed
			//sends the speed so that the aniamotr gets updated with this value
		}
		else{
			//receiving other players things
			foreignPosition = (Vector3)stream.ReceiveNext();
			foreignRotation = (Quaternion)stream.ReceiveNext();
			//receives speed vra for animation
			animator.SetFloat("Speed",(float)stream.ReceiveNext());
			//might need to add Mathf.lerp to lerp this
		}
	} 

}
