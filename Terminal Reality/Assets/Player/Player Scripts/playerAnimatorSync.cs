using UnityEngine;
using System.Collections;

public class playerAnimatorSync : MonoBehaviour {

	
    void Start() {
        PhotonAnimatorView anim = this.gameObject.GetComponent<PhotonAnimatorView>();
        anim.SetLayerSynchronized(0, PhotonAnimatorView.SynchronizeType.Continuous);
        anim.SetLayerSynchronized(1, PhotonAnimatorView.SynchronizeType.Continuous);
    }


}
