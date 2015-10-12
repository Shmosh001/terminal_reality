using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreditRoll : MonoBehaviour {



    public float speed = 20;


	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.Translate(Vector3.up * Time.deltaTime * speed);
	}
}
