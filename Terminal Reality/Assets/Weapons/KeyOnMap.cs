using UnityEngine;
using System.Collections;

public class KeyOnMap : MonoBehaviour {





    [PunRPC]
    public void destroyObject() {
        Destroy(gameObject);
    }
}
