using UnityEngine;
using System.Collections;

public class destroyObjects : MonoBehaviour {

	[PunRPC]
	public void destroyObject() {
		Destroy(gameObject);
	}
}
