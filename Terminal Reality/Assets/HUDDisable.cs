using UnityEngine;
using System.Collections;

public class HUDDisable : MonoBehaviour {


	public GameObject hud;

	// Use this for initialization
	void Awake () {
		Instantiate (hud);
	}

}
